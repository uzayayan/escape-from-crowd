using UnityEngine;

public class FinishView : BaseView
{
    #region Serializable Fields
    
    [Header("Transforms")] 
    [SerializeField] private Transform m_centerPoint;
    [SerializeField] private Transform m_cameraPoint;

    
    [Header("Particles")] 
    [SerializeField] private ParticleSystem m_confettiParticle;

    #endregion

    /// <summary>
    /// This function called when related controller initialized.
    /// </summary>
    protected override void OnControllerInitialized()
    {
        m_confettiParticle.Play();
        
        SoundManager.Instance.Play(SoundType.Finish);
        
        base.OnControllerInitialized();
    }

    /// <summary>
    /// This function return center position of this component.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCenterPosition()
    {
        return m_centerPoint.position;
    }
    
    /// <summary>
    /// This function return camera transform.
    /// </summary>
    /// <returns></returns>
    public Transform GetCameraTransform()
    {
        return m_cameraPoint;
    }
}
