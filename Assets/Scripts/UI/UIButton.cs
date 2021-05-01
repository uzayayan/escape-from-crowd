using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButton : Button
{
    /// <summary>
    /// This function called when player click the component.
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.Play(SoundType.Click);
        
        base.OnPointerClick(eventData);
    }
}