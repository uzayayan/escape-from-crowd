using UnityEngine;
using UnityEngine.UI;

public class UISoundToggle : MonoBehaviour
{
    #region Serializable Fields

    [SerializeField] private Image m_image;
    [SerializeField] private Sprite m_onIcon;
    [SerializeField] private Sprite m_offIcon;

    #endregion
    #region Private Fields

    private bool state;

    #endregion

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        UpdateContent();
    }

    /// <summary>
    /// This function helper for update fields on this component.
    /// </summary>
    private void UpdateContent()
    {
        state = PlayerPrefs.GetInt(CommonTypes.SOUND_STATE_KEY) == 0;
        m_image.sprite = state ? m_onIcon : m_offIcon;
    }

    /// <summary>
    /// This function helper for change sound state.
    /// </summary>
    public void ToggleSound()
    {
        SoundManager.Instance.SetMuteState(!state);
        UpdateContent();
    }
}
