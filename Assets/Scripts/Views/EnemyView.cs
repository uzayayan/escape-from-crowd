using UnityEngine;
using UnityEngine.AI;

public class EnemyView : BaseView
{
    #region Serializable Fields

    [SerializeField] private Animator m_animator;
    [SerializeField] private Animation m_bloodAnimation;
    [SerializeField] private NavMeshAgent m_agent;
    [SerializeField] private ParticleSystem m_bloodParticle;

    #endregion
    #region Protected Fields

    protected EnemyController enemyController => (EnemyController) m_baseController;

    #endregion

    /// <summary>
    /// This function called when related 'Controller' initialized.
    /// </summary>
    protected override void OnControllerInitialized()
    {
        enemyController.Killed += OnKilled;
        enemyController.Stopped += OnStopped;

        m_agent.speed = GameManager.Instance.GetGameSettings().EnemySpeed * CommonTypes.ENEMY_SPEED_MULTIPLIER;
        m_agent.enabled = true;
        
        base.OnControllerInitialized();
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if(enemyController.IsStopped())
            return;
        
        if(enemyController.IsKilled())
            return;
        
        m_agent.SetDestination(GameManager.Instance.GetPlayerController().transform.position);
    }
    
    /// <summary>
    /// This function called when related Controller stopped.
    /// </summary>
    private void OnStopped()
    {
        m_animator.SetTrigger(CommonTypes.ANIMATOR_DANCE);
    }

    /// <summary>
    /// This function called when the enemy killed.
    /// </summary>
    protected virtual void OnKilled()
    {
        m_agent.enabled = false;
        m_bloodParticle.Play();
        m_bloodAnimation.Play();
        m_animator.SetTrigger(CommonTypes.ANIMATOR_DEATH);
        
        SoundManager.Instance.Play(SoundType.Death);
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected override void OnDestroy()
    {
        enemyController.Killed -= OnKilled;
        enemyController.Stopped -= OnStopped;

        base.OnDestroy();
    }
}
