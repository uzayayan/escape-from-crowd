using UnityEngine;
using UnityEngine.UI;

public class BossView : EnemyView
{
    #region Serializable Fields

    [SerializeField] private Slider m_healthSlider;

    #endregion

    /// <summary>
    /// This function called when related 'Controller' initialized.
    /// </summary>
    protected override void OnControllerInitialized()
    {
        enemyController.HealthDecreased += OnHealthDecreased;
        base.OnControllerInitialized();
    }

    /// <summary>
    /// This function called when enemy health decreased.
    /// </summary>
    private void OnHealthDecreased()
    {
        m_healthSlider.value = (float) enemyController.GetHealth() / enemyController.GetEnemy().Health;
    }

    /// <summary>
    /// This function called when the enemy killed.
    /// </summary>
    protected override void OnKilled()
    {
        m_healthSlider.gameObject.SetActive(false);
        
        base.OnKilled();
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected override void OnDestroy()
    {
        enemyController.HealthDecreased -= OnHealthDecreased;

        base.OnDestroy();
    }
}
