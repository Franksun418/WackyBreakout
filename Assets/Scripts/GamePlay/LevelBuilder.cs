using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelBuilder : MonoBehaviour,IEventInvoker {
    [SerializeField]
    GameObject paddle;

    [SerializeField]
    GameObject block;

    [SerializeField]
    int rowNumber;

    [SerializeField]
    GameObject bonusBlock;

    [SerializeField]
    GameObject pickUpBlock;

    Dictionary<EventName, UnityEvent> unityEvents = new Dictionary<EventName, UnityEvent>();

    // Use this for initialization
    void Start () {
        Instantiate(paddle);


        float blockWidth = block.GetComponent<BoxCollider2D>().size.x * block.transform.localScale.x;
        float blockHeight = block.GetComponent<BoxCollider2D>().size.y * block.transform.localScale.y;

        float screenWidth = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        float blocksPerRow = screenWidth / blockWidth;
        float xOffset = ScreenUtils.ScreenLeft + ((screenWidth - (blocksPerRow * blockWidth)) / 2) + blockWidth / 2;
        float yOffset = ScreenUtils.ScreenTop - (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) / 3.5f - blockHeight / 2;

        Vector2 currentPosition = new Vector2(xOffset, yOffset);
        //Instantiate rows
        for (int i = 0; i < rowNumber; i++)
        {
            for (int j = 0; j < blocksPerRow; j++)
            {
                PlaceBlock(currentPosition);
                currentPosition.x += blockWidth;
            }
            currentPosition.x = xOffset;
            currentPosition.y += blockHeight;
            
        }

        EventManager.AddListener(EventName.BlockDestroyedEvent, HandleBlockDestoryedEvent);

        unityEvents.Add(EventName.AllBlockDestroyedEvent, new AllBlockDestroyedEvent());
        EventManager.AddInvoker(EventName.AllBlockDestroyedEvent, this);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale==1)
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }


    void PlaceBlock(Vector2 position) {
        float magicNum = Random.value;
       // float magicNum = 0.95f;
        
        if (magicNum < ConfigurationUtils.StandardBlockProbability)
        {
            Instantiate(block, position, Quaternion.identity);
        }
        else if (magicNum < ConfigurationUtils.StandardBlockProbability + ConfigurationUtils.BonusBlockProbability)
        {
            Instantiate(bonusBlock, position, Quaternion.identity);
        }
        else {
            if (magicNum < ConfigurationUtils.StandardBlockProbability + ConfigurationUtils.BonusBlockProbability + ConfigurationUtils.FreezerBlockProbability)
            {
                Instantiate(pickUpBlock, position, Quaternion.identity);
                pickUpBlock.GetComponent<PickUpBlock>().Effect = PickUpEffect.Freezer;
            }
            else
            {
                Instantiate(pickUpBlock, position, Quaternion.identity);
                pickUpBlock.GetComponent<PickUpBlock>().Effect = PickUpEffect.SpeedUp;
            }
        }
    }

    void HandleBlockDestoryedEvent()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length == 1)
        {
            unityEvents[EventName.AllBlockDestroyedEvent].Invoke();
            AudioManager.Play(AudioClipName.GameWon);
        }
    }

    public void AddListener(EventName eventName, UnityAction unityAction)
    {
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(unityAction);
        }
    }
}
