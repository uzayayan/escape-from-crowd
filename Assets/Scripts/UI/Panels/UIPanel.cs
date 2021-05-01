using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class UIPanel : MonoBehaviour
{
    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    /// <returns></returns>
    public abstract UniTask Initialize();

    /// <summary>
    /// This function helper for update fields on this component.
    /// </summary>
    protected virtual async UniTask UpdateContent() { }
    
    /// <summary>
    /// This function helper for reset fields on this component.
    /// </summary>
    public virtual void ResetContent() { }
    
    /// <summary>
    /// This function helper for close this component.
    /// </summary>
    public virtual void Close()
    {
        ResetContent();
    }
}
