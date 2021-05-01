using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class UIEndGamePanel : UIPanel
{
    #region Serializable Fields

    [Header("Generic")]
    [SerializeField] private UIElement m_element;
    
    [Header("Images")]
    [SerializeField] private Image m_missionComplete;
    [SerializeField] private Image m_missionFailed;
    
    [Header("Buttons")]
    [SerializeField] private UIButton m_restartButton;
    [SerializeField] private UIButton m_nextButton;
    
    #endregion
    #region Private Fields

    private bool isInitialized;

    #endregion
    
    /// <summary>
    /// Initialize
    /// </summary>
    /// <returns></returns>
    public override async UniTask Initialize()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        
        if(isInitialized)
            return;
        
        await UpdateContent();
        m_element.Open();
    }
    
    /// <summary>
    /// This function helper for update content of this component.
    /// </summary>
    /// <returns></returns>
    protected override async UniTask UpdateContent()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.IsGameCompleted())
        {
            m_missionComplete.gameObject.SetActive(true);
            m_restartButton.gameObject.SetActive(true);
            m_nextButton.gameObject.SetActive(true);
        }
        else
        {
            m_missionFailed.gameObject.SetActive(true);
            m_restartButton.gameObject.SetActive(true);
            m_nextButton.gameObject.SetActive(false);
        }

        isInitialized = true;
    }

    /// <summary>
    /// This function helper for load next level.
    /// </summary>
    public void NextLevel()
    {
        MainManager.Instance.NextLevel();
    }
    
    /// <summary>
    /// This function helper for fast re-play.
    /// </summary>
    public void PlayAgain()
    {
        MainManager.Instance.RetryLevel();
    }
    
    /// <summary>
    /// This function helper for close related 'UIElement' component.
    /// </summary>
    public override void Close()
    {
        m_element.Close();
        
        base.Close();
    }
}
