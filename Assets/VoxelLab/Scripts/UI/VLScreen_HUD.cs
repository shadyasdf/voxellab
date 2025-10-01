using UIKit;
using UnityEngine;

[UIKScreen(name = VLUI.screenName_hud, layer = VLUI.screenLayer_hud)]
public class VLScreen_HUD : VLScreen
{
    public void OnClicked_Tool1()
    {
        Debug.Log("OnClicked Tool 1");
    }
    
    public void OnClicked_Tool2()
    {
        Debug.Log("OnClicked Tool 2");
    }
    
    
    public override void HandleBackAction()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_pause);
    }
}
