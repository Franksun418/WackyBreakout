using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData_Frank.csv";

    // configuration data
    static float paddleMoveUnitsPerSecond = 15f;
    static float ballImpulseForce = 200f;
    static float ballLifeTime = 20.0f;
    static float minSpawnTime = 5.0f;
    static float maxSpawnTime = 10.0f;
    static int standardBlockPoints = 1;
    static int bonusBlockPoints = 3;
    static int pickUpBlockPoints = 2;
    static float standardBlockProbability = 0.6f;
    static float bonusBlcokProbability=0.2f;
    static float freezerBlockProbability=0.1f;
    static float speedUpBlockProbability=0.1f;
    static int ballsPerGame = 5;
    static float freezerEffectDuration = 1.5f;
    static float speedUpEffectDuration = 4.0f;
    static float speedUpFactor = 2.0f;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    public float BallLifeTime
    {

        get { return ballLifeTime; }
    }

    public float MinSpawnTime {
        get { return minSpawnTime; }
    }

    public float MaxSpawnTime
    {
        get { return maxSpawnTime; }
    }

    public int StandardBlockPoints {
        get { return standardBlockPoints; }
    }

    public int BonusBlockPoints {
        get { return bonusBlockPoints; }
    }

    public int PickUpBlockPoints {
        get { return pickUpBlockPoints; }
    }

    public float StandardBlockProbability
    {
        get { return standardBlockProbability; }
    }

    public float BonusBlockProbability {
        get { return bonusBlcokProbability; }
    }

    public float FreezerBlockProbability {
        get { return freezerBlockProbability; }
    }

    public float SpeedUpBlockProbability {
        get { return speedUpBlockProbability; }
    }

    public int BallsPerGame
    {
        get { return ballsPerGame; }
    }

    public float FreezerEffectDuration {
        get { return freezerEffectDuration; }
    }

    public float SpeepUpEffectDuration {
        get { return speedUpEffectDuration; }  
    }

    public float SpeedUpFactor {
        get { return speedUpFactor; }
    }
    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader input = null;
        try
        {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
            string names = input.ReadLine();
            string values = input.ReadLine();
            SetConfigurationData(values);
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
        finally
        {
            if (input != null)
                input.Close();
        }
    }
    void SetConfigurationData(string csvValues)
    {
        string[] values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse( values[0]);
        ballImpulseForce = float.Parse (values[1]);
        ballLifeTime = float.Parse(values[2]);
        minSpawnTime = float.Parse(values[3]);
        maxSpawnTime = float.Parse(values[4]);
        standardBlockPoints = int.Parse(values[5]);
        bonusBlockPoints = int.Parse(values[6]);
        pickUpBlockPoints = int.Parse(values[7]);
        standardBlockProbability = float.Parse(values[8]);
        bonusBlcokProbability = float.Parse(values[9]);
        freezerBlockProbability = float.Parse(values[10]);
        speedUpBlockProbability = float.Parse(values[11]);
        ballsPerGame = int.Parse(values[12]);
        freezerEffectDuration = float.Parse(values[13]);
        speedUpEffectDuration = float.Parse(values[14]);
        speedUpFactor = float.Parse(values[15]);
    }
    #endregion
}
