using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level", menuName = "SPACE/Level", order = 2)]
public class Level : ScriptableObject
{
    public int Id;
    public LevelContainer PlayArea;
    public LevelContainer EnemyArea;
    public Material Skybox;
    
    /// <summary>
    /// This function return Play Area length.
    /// </summary>
    /// <returns></returns>
    public float GetPlayAreaLength()
    {
        return PlayArea.Middle.Count * CommonTypes.AREA_SIZE_MULTIPLIER;
    }

    /// <summary>
    /// This function return Enemy Area length.
    /// </summary>
    /// <returns></returns>
    public float GetEnemyAreaLength()
    {
        return EnemyArea.Middle.Count * CommonTypes.AREA_SIZE_MULTIPLIER;
    }
}

[Serializable]
public class LevelContainer
{
    public List<EDataType> Left = new List<EDataType>();
    public List<EDataType> Middle = new List<EDataType>();
    public List<EDataType> Right = new List<EDataType>();
}
