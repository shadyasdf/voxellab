using System.Linq;
using UIKit;
using UnityEngine;

[UIKActionObject(actionText = "Close")]
public class VLActionObject_CloseActiveScreen : VLActionObject
{
    protected VLScreen screen;


    public override void OnReceivedContext(params object[] _args)
    {
        base.OnReceivedContext(_args);

        if (_args.FirstOrDefault(a => a is VLScreen) is VLScreen screenArg)
        {
            screen = screenArg;
        }
    }

    public override bool CanExecute()
    {
        if (base.CanExecute())
        {
            return screen != null;
        }

        return false;
    }

    public override void Execute()
    {
        screen?.Close();
    }
}
