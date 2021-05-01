using UnityEngine;

public static class DataService
{
    /// <summary>
    /// This function returns object class by type from prefs.
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T LoadObjectWithKey<T>(string key) where T : new()
    {
        T cachedClass = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));

        if (cachedClass == null)
        {
            cachedClass = new T();
            Debug.Log($"Data Not Found. Type : {typeof(T)}");
        }
        else
        {
            Debug.Log($"Data Found. Type : {typeof(T)} JSON Data : {PlayerPrefs.GetString(key)}");
        }
        
        return cachedClass;
    }

    /// <summary>
    /// This function helper for save target data to prefs.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    public static void SaveDataAsJson(string key, object data)
    {
        PlayerPrefs.SetString(key, ObjectToJson(data));
    }

    /// <summary>
    /// This function returns target object as json.
    /// </summary>
    /// <param name="targetObject"></param>
    /// <returns></returns>
    public static string ObjectToJson(object targetObject)
    {
        return JsonUtility.ToJson(targetObject);
    }
}