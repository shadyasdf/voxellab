using UIKit;
using UnityEngine;

[UIKScreen(name = VLUI.screenName_hud, layer = VLUI.screenLayer_hud)]
public class VLScreen_HUD : VLScreen
{
    public void OnSelected_Tool1(UIKEventData _eventData)
    {
        UIKActionObjectReflector.ReceiveContext<VLActionObject_SelectTool>(1);
    }
    
    public void OnSelected_Tool2(UIKEventData _eventData)
    {
        UIKActionObjectReflector.ReceiveContext<VLActionObject_SelectTool>(2);
    }
    
    
    public override void HandleBackAction()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_pause);
    }
}
