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
            text.SetText(buttonText);
        }
    }


    public void TestClick()
    {
        Debug.Log("TestClick");
    }
}
