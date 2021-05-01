using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponItem : MonoBehaviour
{
    #region Serializable Fields

    [Header("Images")]
    [SerializeField] private Image m_icon;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text m_damageText;
    [SerializeField] private TMP_Text m_capacityText;
    [SerializeField] private TMP_Text m_reloadTimeText;
    [SerializeField] private TMP_Text m_triggerDelayext;
    [SerializeField] private TMP_Text m_footerText;

    #endregion
    #region Private Fields

    private Weapon weapon;
    private UIWeaponPanel weaponPanel;

    #endregion

    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    /// <param name="weapon"></param>
    public void Initialize(Weapon weapon, UIWeaponPanel weaponPanel)
    {
        this.weapon = weapon;
        this.weaponPanel = weaponPanel;
        
        UpdateContent();
    }

    /// <summary>
    /// This function helper for update fields on this component.
    /// </summary>
    public void UpdateContent()
    {
        Player player = MainManager.Instance.GetPlayer();
        
        m_icon.sprite = weapon.Icon;
        
        m_damageText.text = $"DAMAGE : {weapon.Damage}";
        m_capacityText.text = $"CAPACITY : {weapon.Capacity}";
        m_reloadTimeText.text = $"RELOAD TIME : {weapon.ReloadTime}";
        m_triggerDelayext.text = $"TRIGGER DELAY : {weapon.TriggerDelay}";

        m_footerText.text = player.Weapon == weapon.Id ? "Selected" : "Select";
    }

    /// <summary>
    /// This function helper for select this weapon.
    /// </summary>
    public void Select()
    {
        Player player = MainManager.Instance.GetPlayer();
        player.Weapon = weapon.Id;
        
        weaponPanel.UpdateAllItems();
    }
}
