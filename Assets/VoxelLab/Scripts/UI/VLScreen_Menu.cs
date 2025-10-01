using UIKit;
using UnityEngine;

[UIKScreen(name = VLUI.screenName_menu, layer = VLUI.screenLayer_menu)]
public class VLScreen_Menu : VLScreen
{
    public void OnClicked_New()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_hud);
        CloseScreen();
    }


    public override void HandleBackAction()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_pause);
    }
}
