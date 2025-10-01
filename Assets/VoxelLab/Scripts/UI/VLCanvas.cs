using UIKit;
using UnityEngine;

public class VLCanvas : UIKCanvas
{
    public void TryQuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else // UNITY_EDITOR
        Application.Quit();
#endif // UNITY_EDITOR
    }
}
