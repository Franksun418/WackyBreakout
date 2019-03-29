using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : IntEventInvoker {
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

    [SerializeField]
    int blockNumber;

    // Use this for initialization
    void Start () {
        Instantiate(paddle);
        GameObject tempBlock = Instantiate(block);
        float blockWidth = tempBlock.GetComponent<BoxCollider2D>().size.x * tempBlock.transform.localScale.x;
        float blockHeight = tempBlock.GetComponent<BoxCollider2D>().size.y * tempBlock.transform.localScale.y;
        Destroy(tempBlock);

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
                blockNumber += 1;
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
        if (blockNumber==1)
        {
            unityEvents[EventName.AllBlockDestroyedEvent].Invoke();
            AudioManager.Play(AudioClipName.GameWon);
        }
        else
        blockNumber -= 1;
    }
}
