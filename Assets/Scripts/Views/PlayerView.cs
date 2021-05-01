using TMPro;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerView : BaseView
{
    #region Serializable Fields

    [Header("Transforms")]
    [SerializeField] private Transform m_spineBone;
    [SerializeField] private Transform m_weaponSlot;
    [SerializeField] private Transform m_weaponPanel;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text m_gemText;
    [SerializeField] private TMP_Text m_ammoText;

    [Header("Images")]
    [SerializeField] private Image m_shadow;
    [SerializeField] private Image m_reloadProgress;
    
    [Header("Animations")]
    [SerializeField] private Animator m_animator;
    [SerializeField] private Animation m_bloodAnimation;

    [Header("Misc")]
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private CapsuleCollider m_capsuleCollider;
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private CanvasGroup m_weaponPanelCanvasGroup;

    #endregion
    #region Private Fields

    private PlayerController playerController => (PlayerController) m_baseController;

    #endregion

    /// <summary>
    /// This function called when related 'Controller' initialized.
    /// </summary>
    protected override void OnControllerInitialized()
    {
        TouchManager.Instance.HorizontalAxisChanged += OnHorizontalAxisChanged;
        
        playerController.Killed += OnKilled;
        playerController.Completed += OnCompleted;
        playerController.GemCollected += OnGemCollected;
        playerController.GetWeaponController().Fired += OnFired;
        playerController.GetWeaponController().ReloadStarted += OnReloadStarted;
        playerController.GetWeaponController().ReloadCompleted += OnReloadCompleted;
        
        AdjustWeapon();
        m_animator.SetTrigger(CommonTypes.ANIMATOR_RUN);
        
        base.OnControllerInitialized();
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if(playerController.IsFinished())
            return;
        
        if(playerController.IsKilled())
            return;
        
        if(GameManager.Instance.IsGameOver())
            return;
        
        if(!GameManager.Instance.IsGameStarted())
            return;

        AutoAim();
        Movement();
    }
    
    /// <summary>
    /// This function called when player collect gem.
    /// </summary>
    private void OnGemCollected(int totalGem)
    {
        m_gemText.text = totalGem.ToString();
    }
    
    /// <summary>
    /// This function called when related 'Weapon Controller' reload completed.
    /// </summary>
    private void OnReloadCompleted()
    {
        m_ammoText.text = playerController.GetWeaponController().GetCurrentAmmo().ToString();
    }

    /// <summary>
    /// This function called when related 'Weapon Controller' reload started.
    /// </summary>
    private void OnReloadStarted()
    {
        m_reloadProgress.DOFillAmount(1, playerController.GetWeaponController().GetWeapon().ReloadTime).SetEase(Ease.Linear);
    }

    /// <summary>
    /// This function called when related 'Weapon Controller' fired.
    /// </summary>
    private void OnFired()
    {
        WeaponController weaponController = playerController.GetWeaponController();
        
        m_ammoText.text = weaponController.GetCurrentAmmo().ToString();
        m_reloadProgress.fillAmount -= 1F / weaponController.GetWeapon().Capacity;
    }

    /// <summary>
    /// This function called when the player finished the level.
    /// </summary>
    private void OnCompleted()
    {
        FinishView finishView = (FinishView) GameManager.Instance.GetFinishController().GetView();

        Sequence sequence = DOTween.Sequence();
        
        sequence.Join(transform.DOMove(finishView.GetCenterPosition(), 0.5F).SetEase(Ease.Linear));

        sequence.OnComplete(() =>
        {
            m_animator.SetTrigger(CommonTypes.ANIMATOR_DANCE);
        });
        
        sequence.Play();
        
        ChangeWeaponPanelState(false);
    }
    
    /// <summary>
    /// This function called when the player killed.
    /// </summary>
    private void OnKilled()
    {
        ChangeRagdollState(true);
        m_shadow.gameObject.SetActive(false);
        m_weaponPanel.gameObject.SetActive(false);
        m_lineRenderer.gameObject.SetActive(false);

        m_bloodAnimation.Play();
        
        SoundManager.Instance.Play(SoundType.Death);
        SoundManager.Instance.Play(SoundType.Failed);
    }
    
    /// <summary>
    /// This function called when 'Horizontal Input' changed.
    /// </summary>
    /// <param name="value"></param>
    private void OnHorizontalAxisChanged(float value)
    {
        if(playerController.IsFinished())
            return;
        
        if(playerController.IsKilled())
            return;
        
        if((transform.position.x < -CommonTypes.PLAYER_CLAMP_X_AXIS && value < 0) || (transform.position.x > CommonTypes.PLAYER_CLAMP_X_AXIS && value > 0))
            return;
        
        transform.Translate(value * 5 * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// This function helper for adjust player weapon.
    /// </summary>
    public void AdjustWeapon()
    {
        playerController.GetWeaponController().transform.SetParent(m_weaponSlot, false);
        m_ammoText.text = playerController.GetWeaponController().GetCurrentAmmo().ToString();
        
        ChangeWeaponPanelState(true);
    }

    /// <summary>
    /// This function helper for auto aim to nearest enemy.
    /// </summary>
    private void AutoAim()
    {
        EnemyController targetEnemyController = playerController.GetNearestEnemyController();
        
        if(targetEnemyController == null)
            return;
        
        Vector3 aimDirection = transform.forward;
        Vector3 targetDirection = targetEnemyController.transform.position - transform.position;
        targetDirection.y = 0;
        
        Quaternion targetRotation = Quaternion.FromToRotation(aimDirection, targetDirection);
        
        m_spineBone.localRotation = targetRotation;
    }   

    /// <summary>
    /// This function helper for move to this component.
    /// </summary>
    private void Movement()
    {
        transform.Translate(0, 0, GameManager.Instance.GetGameSettings().PlayerSpeed * -CommonTypes.PLAYER_SPEED_MULTIPLIER * Time.deltaTime);
    }

    /// <summary>
    /// This function helper for change ragdoll state.
    /// </summary>
    /// <param name="state"></param>
    private void ChangeRagdollState(bool state)
    {
        List<Collider> colliders = GetComponentsInChildren<Collider>().ToList();
        List<Rigidbody> rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();

        colliders.Remove(m_capsuleCollider);
        rigidbodies.Remove(m_rigidbody);

        foreach (Collider targetCollider in colliders)
        {
            targetCollider.enabled = state;
        }

        foreach (Rigidbody targetRigidbody in rigidbodies)
        {
            targetRigidbody.isKinematic = !state;
        }

        m_animator.enabled = !state;
        m_capsuleCollider.enabled = !state;
    }

    /// <summary>
    /// This function helper for change weapon panel state.
    /// </summary>
    /// <param name="state"></param>
    private void ChangeWeaponPanelState(bool state)
    {
        if (state)
        {
            m_weaponPanelCanvasGroup.DOFade(1, 0.5F);
            m_lineRenderer.transform.DOScaleX(1, 0.5F);
        }
        else
        {
            m_weaponPanelCanvasGroup.DOFade(0, 0.5F);
            m_lineRenderer.transform.DOScaleX(0, 0.5F);
        }
        
        m_weaponSlot.gameObject.SetActive(state);
    }
    
    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected override void OnDestroy()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.HorizontalAxisChanged -= OnHorizontalAxisChanged;
        }
        
        playerController.Killed -= OnKilled;
        playerController.Completed -= OnCompleted;
        playerController.GemCollected -= OnGemCollected;

        if (playerController.GetWeaponController() != null)
        {
            playerController.GetWeaponController().Fired -= OnFired;
            playerController.GetWeaponController().ReloadStarted -= OnReloadStarted;
            playerController.GetWeaponController().ReloadCompleted -= OnReloadCompleted;
        }

        base.OnDestroy();
    }
}
