using System;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    #region Public Fields

    public Action Initialized;

    #endregion
    #region Protected Fields

    [SerializeField] protected BaseView m_baseView;

    #endregion

    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    /// <param name="parameters"></param>
    public virtual void Initialize(params object[] parameters)
    {
        Initialized?.Invoke();
    }
    
    /// <summary>
    /// This function return related 'Base View' component.
    /// </summary>
    /// <returns></returns>
    public virtual BaseView GetView()
    {
        return m_baseView;
    }
    
    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected virtual void OnDestroy() { }
}
