using MyBox;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIElement : MonoBehaviour
{
    #region Public Fields
    public Action<bool> OnStateChangeStarted;
    public Action<bool> OnStateChangeFinished;
    #endregion
    #region Serializable Fields
    [Separator("General Properties", true)]
    #region General Fields
    [SerializeField] private ElementType m_type = ElementType.Generic;
    [SerializeField] private Image m_background;
    [SerializeField] private RectTransform m_container;
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private Color m_backgroundColor;
    #endregion
    [Separator("Animation Properties", true)]
    #region Animation Fields
    [SerializeField] private float m_inTime = 0.25F;
    [SerializeField] private float m_outTime = 0.25F;
    [SerializeField] private Ease m_inCurve = Ease.OutSine;
    [SerializeField] private Ease m_outCurve = Ease.InSine;
    #endregion
    [Separator("Element Properties", true)]
    #region Generic Fields
    [ConditionalField("m_type", false, ElementType.Generic)]
    [SerializeField] private ElementDirection m_elementDirection;
    #endregion
    #region Scale Fields
    [ConditionalField("m_type", false, ElementType.Scale)]
    [SerializeField] private Vector3 m_scalePower;
    #endregion
    #endregion
    #region Private Fields
    private Vector2 initialPosition;
    private bool isInteractable = true;
    private bool state = false;
    #endregion
    
    #region Events

    private void Reset()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Container")
            {
                child.gameObject.TryGetComponent(out m_container);
            }
            
            if (child.gameObject.name == "Background")
            {
                child.gameObject.TryGetComponent(out m_background);
            }
        }
    }
    
    private void Awake()
    {
        initialPosition = m_container.anchoredPosition;
        
        if (m_type != ElementType.Scale)
        {
            m_container.anchoredPosition = GetScreenOffPosition();
        }
    }

    public void Open()
    {
        if(state || !isInteractable || DOTween.IsTweening(GetInstanceID()))
            DOTween.Kill(GetInstanceID(), true);

        //fire event
        OnStateChangeStarted?.Invoke(true);

        isInteractable = false;
        m_container.gameObject.SetActive(true);

        switch (m_type)
        {
            case ElementType.Generic:
                GenericSequenceHelper(RevealState.Open);
                break;
            case ElementType.Scale:
                ScaleSequenceHelper(RevealState.Open);
                break;
        }

        if (m_background != null)
        {
            m_background.raycastTarget = true;
            m_background.DOColor(m_backgroundColor, m_inTime).OnPlay(()=> m_background.gameObject.SetActive(true));
        }
        
        SoundManager.Instance.Play(SoundType.Click);
        state = true;
    }

    public void Close()
    {
        if(!state || !isInteractable || DOTween.IsTweening(GetInstanceID()))
            DOTween.Kill(GetInstanceID(), true);
        
        //fire event
        OnStateChangeStarted?.Invoke(false);

        isInteractable = false;

        switch (m_type)
        {
            case ElementType.Generic:
                GenericSequenceHelper(RevealState.Close);
                break;
            case ElementType.Scale:
                ScaleSequenceHelper(RevealState.Close);
                break;
        }

        if (m_background != null)
        {
            m_background.raycastTarget = false;
            m_background.DOColor(Color.clear, m_inTime).OnComplete(()=> m_background.gameObject.SetActive(false));
        }
        
        SoundManager.Instance.Play(SoundType.Click);
        state = false;
    }
    #endregion
    #region Helpers
    private void GenericSequenceHelper(RevealState revealState)
    {
        if (revealState == RevealState.Open)
        {
            Sequence genericSequence = DOTween.Sequence();

            genericSequence.Append(DOTween.To(() => m_container.anchoredPosition, x => m_container.anchoredPosition = x, initialPosition, m_inTime).SetEase(m_inCurve));
            genericSequence.SetId(GetInstanceID());

            genericSequence.OnComplete(() =>
            {
                isInteractable = true;
                OnStateChangeFinished?.Invoke(true);
            });
            
            genericSequence.Play();

            return;
        }
        
        if (revealState == RevealState.Close)
        {
            Sequence genericSequence = DOTween.Sequence();

            genericSequence.Append(DOTween.To(() => m_container.anchoredPosition, x => m_container.anchoredPosition = x, GetScreenOffPosition(), m_outTime).SetEase(m_outCurve));
            genericSequence.SetId(GetInstanceID());
            
            genericSequence.OnComplete(() =>
            {
                isInteractable = true;
                OnStateChangeFinished?.Invoke(false);
                
                m_container.gameObject.SetActive(false);
            });

            genericSequence.Play();
        }
    }

    private void ScaleSequenceHelper(RevealState revealState)
    {
        if (revealState == RevealState.Open)
        {
            m_canvasGroup.alpha = 0;
            m_container.localScale = m_scalePower;
            
            Sequence scaleSequence = DOTween.Sequence();

            scaleSequence.Join(m_container.DOScale(Vector3.one, m_inTime).SetEase(m_inCurve));
            scaleSequence.Join(m_canvasGroup.DOFade(1, m_inTime).SetEase(m_inCurve));
            scaleSequence.SetId(GetInstanceID());

            scaleSequence.OnComplete(() =>
            {
                isInteractable = true;
                OnStateChangeFinished?.Invoke(true);
            });

            scaleSequence.Play();

            return;
        }
        
        if (revealState == RevealState.Close)
        {
            Sequence scaleSequence = DOTween.Sequence();

            scaleSequence.Join(m_container.DOScale(m_scalePower, m_outTime).SetEase(m_outCurve));
            scaleSequence.Join(m_canvasGroup.DOFade(0, m_outTime).SetEase(m_outCurve));
            scaleSequence.SetId(GetInstanceID());
            
            scaleSequence.OnComplete(() =>
            {
                isInteractable = true;
                OnStateChangeFinished?.Invoke(false);
                
                m_container.gameObject.SetActive(false);
            });

            scaleSequence.Play();
            
            return;
        }
    }

    public bool GetState()
    {
        return state;
    }
    
    public Vector2 GetScreenOffPosition()
    {
        switch (m_type)
        {
            case ElementType.Generic:
                switch (m_elementDirection)
                {
                    case ElementDirection.Left:
                        return new  Vector2(-(m_container.pivot.x == 0.5F ? ((m_container.sizeDelta.x / 2) + (1440 / 2)) : (m_container.sizeDelta.x)), 0);
                    case ElementDirection.Right:
                        return new  Vector2((m_container.pivot.x == 0.5F ? ((m_container.sizeDelta.x / 2) + (1440 / 2)) : (m_container.sizeDelta.x)), 0);
                    case ElementDirection.Top:
                        return new Vector2(0,(m_container.pivot.y == 0.5F ? ((m_container.sizeDelta.y / 2) + (2960 / 2)) : (m_container.sizeDelta.y)));
                    case ElementDirection.Down:
                        return new Vector2(0,-(m_container.pivot.y == 0.5F ? ((m_container.sizeDelta.y / 2) + (2960 / 2)) : (m_container.sizeDelta.y)));
                    default:
                        return Vector2.zero;
                }
        }
        
        return Vector2.zero;
    }
    #endregion
}
#region Enums
public enum ElementType
{
    Generic,
    Scale,
}

public enum ElementDirection
{
    Left,
    Right,
    Top,
    Down
}

public enum RevealState
{
    Open,
    Close
}
#endregion

