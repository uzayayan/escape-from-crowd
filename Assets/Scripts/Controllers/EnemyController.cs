using System;
using UnityEngine;

public class EnemyController : BaseController
{
    #region Public Fields

    public Action Killed;
    public Action HealthDecreased;
    public Action Stopped;

    #endregion
    #region Serializable Fields

    [SerializeField] private Enemy m_enemy;

    #endregion
    #region Private Fields

    private int health;
    private bool isStopped;
    
    #endregion

    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Initialize(params object[] parameters)
    {
        health = m_enemy.Health;
        
        base.Initialize(parameters);
    }

    /// <summary>
    /// This function return distance between character.
    /// </summary>
    /// <returns></returns>
    public float GetDistanceFromPlayer()
    {
        return Mathf.Abs(Vector3.Distance(transform.position, GameManager.Instance.GetPlayerController().transform.position));
    }

    /// <summary>
    /// This function helper for stop this component.
    /// </summary>
    public void Stop()
    {
        isStopped = true;
        Stopped?.Invoke();
        Debug.Log($"Enemy Stopped. Name : {name}");
    }
    
    /// <summary>
    /// This function helper for kill this component.
    /// </summary>
    public void Damage(int damageAmount = 1)
    {
        health -= damageAmount;
        HealthDecreased?.Invoke();
        Debug.Log($"Enemy Health Decreased. Name : {name}, Health : {health}");

        if (IsKilled())
        {
            Killed?.Invoke();
            Debug.Log($"Enemy Killed. Name : {name}");
        }
    }

    /// <summary>
    /// This function return true if this component stopped.
    /// </summary>
    /// <returns></returns>
    public bool IsStopped()
    {
        return isStopped;
    }
    
    /// <summary>
    /// This function return true if this component killed.
    /// </summary>
    /// <returns></returns>
    public bool IsKilled()
    {
        return health <= 0;
    }

    /// <summary>
    /// This function return related 'Enemy' component.
    /// </summary>
    /// <returns></returns>
    public Enemy GetEnemy()
    {
        return m_enemy;
    }
    
    /// <summary>
    /// This function return health of enemy.
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return health;
    }
}
