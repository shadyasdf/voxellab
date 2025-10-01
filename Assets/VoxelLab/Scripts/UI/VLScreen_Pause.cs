using UIKit;
using UnityEngine;

[UIKScreen(name = VLUI.screenName_pause, layer = VLUI.screenLayer_menu)]
public class VLScreen_Pause : VLScreen
{
    public void OnClicked_Menu()
    {
        VLCanvas.instance.PopScreen(VLUI.screenName_menu);
        VLCanvas.instance.PushScreen(VLUI.screenName_menu);
        VLCanvas.instance.PopScreen(VLUI.screenName_hud);
        CloseScreen();
    }
    
    public void OnClicked_Quit()
    {
        ((VLCanvas)VLCanvas.instance).TryQuitApplication();
    }
}
