using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : BaseController
{
    #region Public Fields

    public Action Killed;
    public Action Completed;
    public Action<int> GemCollected;

    #endregion
    #region Private Fields
    
    private Player player;
    private WeaponController weaponController;

    private bool isKilled;
    private bool isFinished;
    private int collectedGem;

    #endregion

    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Initialize(params object[] parameters)
    {
        player = MainManager.Instance.GetPlayer();
        weaponController = Instantiate(MainManager.Instance.GetAllWeapons()[player.Weapon].Prefab);
        weaponController.Initialize();
        
        base.Initialize(parameters);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if(IsKilled())
            return;
        
        CheckEnemys();
    }

    /// <summary>
    /// This function helper for check around enemies.
    /// </summary>
    private void CheckEnemys()
    {
        List<EnemyController> enemyControllers = GameManager.Instance.GetAllEnemyControllers();

        EnemyController targetEnemyController = enemyControllers.Where(x=> x != null).OrderBy(x=> x.GetDistanceFromPlayer()).FirstOrDefault();
        
        if(targetEnemyController == null)
            return;
        
        if(targetEnemyController.GetDistanceFromPlayer() > GameManager.Instance.GetGameSettings().DistanceThreshold)
            return;

        if (weaponController.Fire())
        {
            targetEnemyController.Damage(weaponController.GetWeapon().Damage);
            
            if(targetEnemyController.IsKilled())
                InterfaceManager.Instance.PrintFloatingText("+1");
            
            Debug.Log($"Enemy Found. Name : {targetEnemyController.name}");
        }
    }

    /// <summary>
    /// This function helper for increase collected gem.
    /// </summary>
    private void IncreaseCollectedGem()
    {
        collectedGem++;
        player.Gem++;
        
        GemCollected?.Invoke(collectedGem);
        
        Debug.Log($"Gem Collected. Total Amount :{collectedGem}");
    }

    /// <summary>
    /// This function helper for kill this component.
    /// </summary>
    public void Kill()
    {
        isKilled = true;
        Killed?.Invoke();
        
        GameManager.Instance.OverGame();
    }

    /// <summary>
    /// This function called when this component trigger any object.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Kill();
        }
        
        if (other.gameObject.CompareTag("Gem"))
        {
            InterfaceManager.Instance.FlyGemToSlot(other.transform.position);
            Destroy(other.gameObject);
            
            IncreaseCollectedGem();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            FinishController finishController = other.gameObject.GetComponent<FinishController>();

            finishController.Initialize();

            isFinished = true;
            Completed?.Invoke();
            
            GameManager.Instance.CompleteLevel();
        }
    }

    /// <summary>
    /// This function return true if this component killed.
    /// </summary>
    /// <returns></returns>
    public bool IsKilled()
    {
        return isKilled;
    }
    
    /// <summary>
    /// This function return true if player finished the level.
    /// </summary>
    /// <returns></returns>
    public bool IsFinished()
    {
        return isFinished;
    }
    
    /// <summary>
    /// This function return nearest 'Enemy Controller'. 
    /// </summary>
    /// <returns></returns>
    public EnemyController GetNearestEnemyController()
    {
        List<EnemyController> enemyControllers = GameManager.Instance.GetAllEnemyControllers();
        return enemyControllers.OrderBy(x=> x.GetDistanceFromPlayer()).FirstOrDefault();
    }

    /// <summary>
    /// This function return related 'Weapon Controller'.
    /// </summary>
    /// <returns></returns>
    public WeaponController GetWeaponController()
    {
        return weaponController;
    }
}
