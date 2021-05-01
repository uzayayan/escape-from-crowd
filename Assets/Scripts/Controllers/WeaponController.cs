using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class WeaponController : BaseController
{
    #region Public Fields

    public Action Fired;
    public Action ReloadStarted;
    public Action ReloadCompleted;

    #endregion
    #region Serializable Fields

    [SerializeField] private Weapon m_weapon;

    #endregion
    #region Private Fields

    private bool isReady = true;
    private bool isReloading;
    private int currentAmmo;

    #endregion

    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Initialize(params object[] parameters)
    {
        currentAmmo = m_weapon.Capacity;
        
        base.Initialize(parameters);
    }

    /// <summary>
    /// This function helper for shoot.
    /// </summary>
    public bool Fire()
    {
        if(!isReady)
            return false;

        if(isReloading)
            return false;

        currentAmmo--;
        Fired?.Invoke();

        _= CheckAmmo();
        _= ResetReadyState();

        return true;
    }

    /// <summary>
    /// This function helper for check current ammo.
    /// </summary>
    public async UniTask CheckAmmo()
    {
        if(currentAmmo > 0)
            return;
        
        isReloading = true;
        ReloadStarted?.Invoke();
        Debug.Log("Weapon Reload Started.");

        await UniTask.Delay(TimeSpan.FromSeconds(m_weapon.ReloadTime));
        
        currentAmmo = m_weapon.Capacity;
        isReloading = false;
        ReloadCompleted?.Invoke();
        Debug.Log("Weapon Reload Completed.");
    }
    
    /// <summary>
    /// This function helper for reset state of this weapon in delay.
    /// </summary>
    public async UniTask ResetReadyState()
    {
        isReady = false;
        Debug.Log("Weapon Delay Started.");

        await UniTask.Delay(TimeSpan.FromSeconds(m_weapon.TriggerDelay));
        
        isReady = true;
        Debug.Log("Weapon Delay Completed.");
    }

    /// <summary>
    /// This function return Current Ammo.
    /// </summary>
    /// <returns></returns>
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    /// <summary>
    /// This function return related 'Weapon' component.
    /// </summary>
    /// <returns></returns>
    public Weapon GetWeapon()
    {
        return m_weapon;
    }
}
