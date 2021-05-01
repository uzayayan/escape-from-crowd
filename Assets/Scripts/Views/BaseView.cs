using UnityEngine;

public class BaseView : MonoBehaviour
{
    #region Protected Fields

    [SerializeField] protected BaseController m_baseController;

    #endregion

    /// <summary>
    /// Awake
    /// </summary>
    protected virtual void Awake()
    {
        m_baseController.Initialized += OnControllerInitialized;
    }

    /// <summary>
    /// This function called when related controller initialized.
    /// </summary>
    protected virtual void OnControllerInitialized() { }

    /// <summary>
    /// This function return related 'Base Controller' component.
    /// </summary>
    /// <returns></returns>
    public virtual BaseController GetController()
    {
        return m_baseController;
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected virtual void OnDestroy()
    {
        m_baseController.Initialized -= OnControllerInitialized;
    }
}
