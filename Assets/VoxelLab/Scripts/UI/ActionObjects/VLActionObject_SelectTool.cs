using System.Linq;
using UIKit;
using UnityEngine;

[UIKActionObject(actionText = "Select")]
public class VLActionObject_SelectTool : VLActionObject
{
    protected int toolIndex = 0; // 0 = invalid
    
    
    public override void OnReceivedContext(params object[] _args)
    {
        if (_args.FirstOrDefault(a => a is int) is int toolIndexArg)
        {
            toolIndex = toolIndexArg;
        }
    }

    public override bool CanExecute()
    {
        if (base.CanExecute())
        {
            return toolIndex > 0;
        }

        return false;
    }

    public override void Execute()
    {
        Debug.Log($"Selected Tool {toolIndex}");
    }
}
