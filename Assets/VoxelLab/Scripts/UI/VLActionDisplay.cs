using TMPro;
using UIKit;
using UnityEngine;

public class VLActionDisplay : UIKActionDisplay
{
    [SerializeField] protected VL2DButton buttonToUseInputActionFrom;

    [SerializeField] protected CanvasGroup actionTextCanvasGroup;
    [SerializeField] protected TMP_Text actionTextText;


    protected override void OnPreConstruct(bool _isOnValidate)
    {
        base.OnPreConstruct(_isOnValidate);

        if (buttonToUseInputActionFrom != null
            && buttonToUseInputActionFrom.GetClickAction() is UIKInputAction inputAction
            && inputAction.IsValid())
        {
            SetInputAction(inputAction);
        }
    }


    protected override void UpdateDisplayWithInvalid()
    {
        if (!actionTextCanvasGroup)
        {
            return;
        }

        actionTextCanvasGroup.alpha = 0;
        actionTextCanvasGroup.interactable = false;
        actionTextCanvasGroup.blocksRaycasts = false;
    }

    protected override void UpdateDisplayWithText(string _text)
    {
        if (!actionTextCanvasGroup
            || !actionTextText)
        {
            return;
        }
        
        actionTextCanvasGroup.alpha = 1;
        actionTextCanvasGroup.interactable = true;
        actionTextCanvasGroup.blocksRaycasts = true;
        
        actionTextText.SetText(_text);
    }
}
