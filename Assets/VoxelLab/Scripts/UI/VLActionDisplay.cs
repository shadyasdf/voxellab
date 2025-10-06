using TMPro;
using UIKit;
using UnityEngine;

public class VLActionDisplay : UIKActionDisplay
{
    [SerializeField] protected GameObject actionTextGO;
    [SerializeField] protected TMP_Text actionTextText;
    
    
    protected override void UpdateDisplayWithInvalid()
    {
        if (!actionTextGO)
        {
            return;
        }
        
        actionTextGO.SetActive(false);
    }

    protected override void UpdateDisplayWithText(string _text)
    {
        if (!actionTextGO
            || !actionTextText)
        {
            return;
        }
        
        actionTextGO.SetActive(true);
        actionTextText.SetText(_text);
    }
}
