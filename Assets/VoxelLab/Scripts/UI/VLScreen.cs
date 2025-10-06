using UIKit;
using UnityEngine;

public abstract class VLScreen : UIKScreen
{
    protected override void OnActiveChanged()
    {
        base.OnActiveChanged();

        if (active)
        {
            UIKActionObjectReflector.ReceiveContext<VLActionObject_CloseActiveScreen>(this);
        }
    }
    
    public override void HandleBackAction()
    {
        UIKActionObjectReflector.TryExecute<VLActionObject_CloseActiveScreen>();
    }
}
