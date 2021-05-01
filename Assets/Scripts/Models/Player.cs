using System;

[Serializable]
public class Player : BaseModel
{
    public int Gem;
    public int Level;
    public int Weapon;
    
    /// <summary>
    /// This function return related data key.
    /// </summary>
    protected override string GetDataKey => CommonTypes.PLAYER_DATA_KEY;
}
