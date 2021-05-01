using DG.Tweening;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    #region Serializable Fields

    [Header("Generic")]
    [SerializeField] private Camera m_camera;
    
    [Header("Transforms")]
    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_startCamera;
    [SerializeField] private Transform m_gameCamera;
    [SerializeField] private Transform m_deathCamera;
    [SerializeField] private Transform m_finishCamera;

    #endregion
    #region Private Fields

    private Vector3 offset;

    #endregion

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        GameManager.Instance.GameStarted += OnGameStarted;
        GameManager.Instance.GameCompleted += OnGameCompleted;
        GameManager.Instance.GameOver += OnGameOver;
        
        offset = transform.position - m_target.transform.position;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if(DOTween.IsTweening(CommonTypes.CAMERA_TWEEN_KEY))
            return;
        
        if(GameManager.Instance.IsGameCompleted())
            return;
        
        if(GameManager.Instance.IsGameOver())
            return;
        
        Movement();
    }
    
    /// <summary>
    /// This function called when game started.
    /// </summary>
    private void OnGameStarted()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Join(transform.DOMove(m_gameCamera.position, CommonTypes.CAMERA_LERP_TIME));
        sequence.Join(transform.DORotate(m_gameCamera.eulerAngles, CommonTypes.CAMERA_LERP_TIME));
        sequence.SetId(CommonTypes.CAMERA_TWEEN_KEY);

        sequence.OnComplete(() =>
        {
            offset = transform.position - m_target.transform.position;
        });
        
        sequence.Play();
    }

    /// <summary>
    /// This function called when game completed.
    /// </summary>
    private void OnGameCompleted()
    {
        Transform finishCameraTransform = ((FinishView) GameManager.Instance.GetFinishController().GetView()).GetCameraTransform();
        
        transform.DOMove(finishCameraTransform.position, CommonTypes.CAMERA_LERP_TIME).SetId(CommonTypes.CAMERA_TWEEN_KEY);
        transform.DORotate(finishCameraTransform.eulerAngles, CommonTypes.CAMERA_LERP_TIME).SetId(CommonTypes.CAMERA_TWEEN_KEY);
    }
    
    /// <summary>
    /// This function called when game over.
    /// </summary>
    private void OnGameOver()
    {
        transform.DOMove(m_deathCamera.position, CommonTypes.CAMERA_LERP_TIME).SetId(CommonTypes.CAMERA_TWEEN_KEY);
    }
    
    /// <summary>
    /// This function helper for move to this component.
    /// </summary>
    private void Movement()
    {
        transform.position = Vector3.Lerp(transform.position, m_target.position + offset, CommonTypes.CAMERA_SPEED * Time.deltaTime);
    }

    /// <summary>
    /// This function return related 'Camera' component.
    /// </summary>
    /// <returns></returns>
    public Camera GetCamera()
    {
        return m_camera;
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected override void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameStarted -= OnGameStarted;
            GameManager.Instance.GameCompleted -= OnGameCompleted;
            GameManager.Instance.GameOver -= OnGameOver;
        }

        base.OnDestroy();
    }
}
