using System.Reflection;
using TMPro;
using UIKit;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class VL2DButton : UIK2DButton
{
    [SerializeField] protected TMP_Text text;
    [SerializeField] protected string buttonText = "Button";
    
    protected Button button;
    protected Image image;
    

    protected override void OnPreConstruct(bool _isOnValidate)
    {
        base.OnPreConstruct(_isOnValidate);
        
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        
        if (text != null)
        {
            if (clickActionObject != null
                && clickActionObject.GetActionObject() is UIKActionObject actionObject
                && actionObject.GetType().GetCustomAttribute<UIKActionObjectAttribute>() is UIKActionObjectAttribute attribute
                && !string.IsNullOrEmpty(attribute.actionText))
            {
                text.SetText(attribute.actionText);
            }
            else
            {
                text.SetText(buttonText);
            }
        }
    }
}
