using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class UIWeaponPanel : UIPanel
{
    #region Serialzaible Fields

    [Header("Generic")]
    [SerializeField] private UIElement m_element;

    [Header("Transforms")]
    [SerializeField] private Transform m_container;

    [Header("Prefabs")]
    [SerializeField] private UIWeaponItem m_weaponItemPrefab;
    
    #endregion
    #region Private Fields

    private bool isInitialized;
    private List<UIWeaponItem> weaponItems = new List<UIWeaponItem>();

    #endregion
    
    /// <summary>
    /// This function helper for initialize this component.
    /// </summary>
    public override async UniTask Initialize()
    {
        m_element.Open();
        
        if(isInitialized)
            return;
        
        await UpdateContent();

        isInitialized = true;
    }

    /// <summary>
    /// This function helper for update fields on this component.
    /// </summary>
    protected override UniTask UpdateContent()
    {
        Weapon[] weapons = MainManager.Instance.GetAllWeapons();

        foreach (Weapon weapon in weapons)
        {
            UIWeaponItem weaponItem = Instantiate(m_weaponItemPrefab, m_container);
            
            weaponItem.Initialize(weapon, this);
            weaponItems.Add(weaponItem);
        }
        
        return base.UpdateContent();
    }

    /// <summary>
    /// This function helper for update all related items.
    /// </summary>
    public void UpdateAllItems()
    {
        weaponItems.ForEach(x=> x.UpdateContent());
    }

    /// <summary>
    /// This function helper for close this component.
    /// </summary>
    public override void Close()
    {
        m_element.Close();
        
        base.Close();
    }
}
