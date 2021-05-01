public class BaseModel
{
    /// <summary>
    /// This function helper for save data to prefs.
    /// </summary>
    public void SaveData()
    {
        DataService.SaveDataAsJson(GetDataKey, this);
    }
    
    /// <summary>
    /// This function return related data key.
    /// </summary>
    protected virtual string GetDataKey => "";
}
