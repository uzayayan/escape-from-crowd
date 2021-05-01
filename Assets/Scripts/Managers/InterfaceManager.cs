using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InterfaceManager : Singleton<InterfaceManager>
{
    #region Serializable Fields

    [Header("Panels")]
    [SerializeField] private UIEndGamePanel m_endGamePanel;
    [SerializeField] private UIWeaponPanel m_weaponPanel;
    
    [Header("Images")]
    [SerializeField] private Image m_gemIcon;
    
    [Header("Prefabs")]
    [SerializeField] private RectTransform m_gemPrefab;
    [SerializeField] private TMP_Text m_floatingTextPrefab;
    
    [Header("Transforms")]
    [SerializeField] private Transform m_floatingTextSlot;
    
    [Header("Misc")]
    [SerializeField] private Canvas m_canvas;
    [SerializeField] private CanvasGroup m_gameCanvasGroup;
    [SerializeField] private CanvasGroup m_menuCanvasGroup;

    #endregion

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        GameManager.Instance.GameStarted += OnGameStarted;
    }

    /// <summary>
    /// This function called when game started.
    /// </summary>
    private void OnGameStarted()
    {
        ChangeCanvasState(true);
    }

    /// <summary>
    /// This function helper for print Floating Text to screen.
    /// </summary>
    public void PrintFloatingText(string text)
    {
        TMP_Text floatingText = Instantiate(m_floatingTextPrefab, m_floatingTextSlot);
        floatingText.transform.localPosition = Vector3.zero;

        floatingText.text = text;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(floatingText.DOFade(1, CommonTypes.FLOATING_TEXT_TIME / 2));
        sequence.Join(floatingText.transform.DOLocalMoveY(1, CommonTypes.FLOATING_TEXT_TIME));
        sequence.Join(floatingText.DOFade(0, CommonTypes.FLOATING_TEXT_TIME / 2).SetDelay(CommonTypes.FLOATING_TEXT_TIME / 2));

        sequence.OnComplete(() =>
        {
            Destroy(floatingText.gameObject);
        });

        sequence.Play();
    }
    
    /// <summary>
    /// This function helper for fly gem animation to target icon.
    /// </summary>
    /// <param name="worldPosition"></param>
    public void FlyGemToSlot(Vector3 worldPosition)
    {
        Vector3 screenPosition = GameUtils.WorldToCanvasPosition(m_canvas.GetComponent<RectTransform>(), CameraManager.Instance.GetCamera(), worldPosition);

        RectTransform createdGem = Instantiate(m_gemPrefab, m_canvas.transform);
        createdGem.anchoredPosition = screenPosition;

        Sequence sequence = DOTween.Sequence();
        
        sequence.Join(createdGem.transform.DOScale(Vector3.one, CommonTypes.FLY_GEM_TIME));
        sequence.Join(createdGem.transform.DOLocalMove(m_canvas.transform.InverseTransformPoint(m_gemIcon.transform.position), CommonTypes.FLY_GEM_TIME));

        sequence.OnComplete(() =>
        {
            Destroy(createdGem.gameObject);
        });
        
        sequence.Play();
        
        SoundManager.Instance.Play(SoundType.FlyGem);
    }

    /// <summary>
    /// This function helper for change state of canvas groups.
    /// </summary>
    /// <param name="isGameState"></param>
    private void ChangeCanvasState(bool isGameState)
    {
        if (isGameState)
        {
            m_gameCanvasGroup.DOFade(1, 0.5F);
            m_menuCanvasGroup.DOFade(0, 0.5F);
        }
        else
        {
            m_gameCanvasGroup.DOFade(0, 0.5F);
            m_menuCanvasGroup.DOFade(1, 0.5F);
        }
    }

    /// <summary>
    /// This function helper for open end game panel.
    /// </summary>
    public void OpenEndGamePanel()
    {
        _ = m_endGamePanel.Initialize();
    }
    
    /// <summary>
    /// This function helper for open weapon panel.
    /// </summary>
    public void OpenWeaponPanel()
    {
        _ = m_weaponPanel.Initialize();
    }
    
    /// <summary>
    /// This function helper for open weapon panel.
    /// </summary>
    public void CloseWeaponPanel()
    {
        m_weaponPanel.Close();
    }

    /// <summary>
    /// This function called when this component destroyed.
    /// </summary>
    protected override void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameStarted -= OnGameStarted;
        }
        
        base.OnDestroy();
    }
}
