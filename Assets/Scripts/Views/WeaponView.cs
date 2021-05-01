using UnityEngine;

public class WeaponView : BaseView
{
    #region Serializable Fields
    
    [Header("Particles")]
    [SerializeField] private ParticleSystem m_muzzleParticle;
    [SerializeField] private ParticleSystem m_bulletParticle;

    #endregion
    #region Private Fields

    private WeaponController weaponController => (WeaponController) m_baseController;

    #endregion

    /// <summary>
    /// This function called when related controller initialized.
    /// </summary>
    protected override void OnControllerInitialized()
    {
        weaponController.Fired += OnFired;
        weaponController.ReloadStarted += OnReloadStarted;
        
        base.OnControllerInitialized();
    }

    /// <summary>
    /// This function called when shoot.
    /// </summary>
    public void OnFired()
    {
        m_bulletParticle.Play();
        m_muzzleParticle.Play();
        
        SoundManager.Instance.Play(weaponController.GetWeapon().FireClip);
    }
    
    /// <summary>
    /// This function called when related 'Weapon Controller' reload started.
    /// </summary>
    private void OnReloadStarted()
    {
        SoundManager.Instance.Play(weaponController.GetWeapon().ReloadClip);
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected override void OnDestroy()
    {
        weaponController.Fired -= OnFired;
        weaponController.ReloadStarted -= OnReloadStarted;

        base.OnDestroy();
    }
}
