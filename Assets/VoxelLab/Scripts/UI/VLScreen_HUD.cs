using SFB;
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
        StandaloneFileBrowser.OpenFilePanelAsync($"Load {VLProject.projectFriendlyName}",
            VLProject.defaultProjectDirectory,
            VLProject.fileExtensionFilters,
            false, 
            (string[] paths) =>
            {
                if (paths.Length > 0
                    && !string.IsNullOrEmpty(paths[0])
                    && paths[0] is string path
                    && VLProject.instance
                    && VLProject.instance.TryLoadProjectFromFile(path))
                {
                    Debug.Log("Load complete");
                    
                    return;
                }
                
                // Error
                Debug.Log("Error loading");
            });
    }
    
    public void OnClicked_Save()
    {
        StandaloneFileBrowser.SaveFilePanelAsync($"Save {VLProject.projectFriendlyName}",
            VLProject.defaultProjectDirectory,
            VLProject.instance.projectName,
            VLProject.fileExtensionFilters,
            (string path) =>
            {
                if (!string.IsNullOrEmpty(path)
                    && VLProject.instance
                    && VLProject.instance.TrySaveProjectToFile(path))
                {
                    Debug.Log("Save complete");
                    
                    return;
                }
                
                // Error
                Debug.Log("Error saving");
            });
    }
    
    
    public override void HandleBackAction()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_pause);
    }
}
