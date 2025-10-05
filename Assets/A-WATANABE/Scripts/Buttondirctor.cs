using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverDetector : MonoBehaviour, IPointerEnterHandler
{
    public int buttonIndex;
    public ButtonChanger buttonChanger;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonChanger.SetSelectedIndex(buttonIndex);
    }
}
