using System.Reflection;
using UIKit;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class VLScreen : UIKScreen
{
    [SerializeField] protected bool handlesBackAction = true;
    
    
    protected override bool ShouldHandleInputActionAsBackAction(InputAction.CallbackContext _context)
    {
        if (handlesBackAction
            && active
            && _context.action.name == VLInput.actionUICancel
            && _context.action.WasPressedThisFrame()
            && _context.action.triggered)
        {
            return true;
        }

        return base.ShouldHandleInputActionAsBackAction(_context);
    }
}
