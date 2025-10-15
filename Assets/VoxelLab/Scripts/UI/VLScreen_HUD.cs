using UIKit;
using UnityEngine;

[UIKScreen(name = VLUI.screenName_hud, layer = VLUI.screenLayer_hud)]
public class VLScreen_HUD : VLScreen
{
    public void OnTargeted_Tool1(UIKPlayer _player)
    {
        UIKActionObjectReflector.ReceiveContext<VLActionObject_SelectTool>(1);
    }
    
    public void OnTargeted_Tool2(UIKPlayer _player)
    {
        UIKActionObjectReflector.ReceiveContext<VLActionObject_SelectTool>(2);
    }

    public void OnClicked_Load()
    {
        Debug.Log("Load");
    }
    
    public void OnClicked_Save()
    {
        Debug.Log("Save");
    }
    
    
    public override void HandleBackAction()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_pause);
    }
}
