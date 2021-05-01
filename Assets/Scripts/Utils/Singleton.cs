using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    #region Fields

    public bool IsPersistant;
    
    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    private static bool isShoutDown;

    #endregion
    #region Properties

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (isShoutDown)
                return null;
            
            if ( instance == null )
            {
                instance = FindObjectOfType<T> ();
                if ( instance == null )
                {
                    GameObject obj = new GameObject ();
                    obj.name = typeof ( T ).Name;
                    instance = obj.AddComponent<T> ();
                }
            }
            return instance;
        }
    }

    #endregion
    #region Methods

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    protected virtual void Awake ()
    {
        if ( instance == null )
        {
            instance = this as T;
            
            if(IsPersistant)
                DontDestroyOnLoad ( gameObject );
            
            isShoutDown = false;
        }
        else
        {
            Destroy ( gameObject );
        }
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected virtual void OnDestroy()
    {
        isShoutDown = true;
    }

    #endregion
}