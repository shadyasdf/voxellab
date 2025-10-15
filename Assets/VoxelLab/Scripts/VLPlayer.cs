using System;
using System.Collections.Generic;
using UIKit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VLPlayer : MonoBehaviour, UIKPlayer
{
    public UnityEvent<InputAction> OnInputActionTriggered { get; set; } = new();

    [SerializeField] protected UIKInputAction leftClickInputAction;
    [SerializeField] protected UIKInputAction rightClickInputAction;
    [SerializeField] protected UIKInputAction submitInputAction;
    [SerializeField] protected UIKInputAction lookInputAction;
    [SerializeField] protected UIKInputAction moveInputAction;
    
    public PlayerInput playerInput { get; set; }
    public UIKTarget targetUI { get; set; }
    public UIKInputDevice inputDeviceType { get; set; }


    protected virtual void Start()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_menu);
    }


    public bool GetIsLocal()
    {
        return true; // We know there is only one local player
    }

    public bool OnPreInputActionTriggered(InputAction.CallbackContext _context)
    {
        // Consume UI inputs before broadcasting them
        if (_context.action == leftClickInputAction
            || _context.action == submitInputAction)
        {
            if (_context.action.WasPressedThisFrame()
                && _context.action.triggered)
            {
                if (TrySubmitUI(GetTargetUI()))
                {
                    return false;
                }
            }
        }
        else if (_context.action == moveInputAction)
        {
            if (_context.action.WasPerformedThisFrame())
            {
                if (TryNavigateUIByDirection(_context.ReadValue<Vector2>()))
                {
                    return false;
                }
            }
        }
        
        // Handle cursor locking
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            // If we're using a mouse device, and we're right-clicking, attempt to lock the cursor to the game
            if (_context.GetInputDeviceType().UsesCursor())
            {
                if (_context.action == rightClickInputAction
                    && _context.action.WasPressedThisFrame()
                    && UIKEventSystem.instance != null)
                {
                    // Make sure we're not hovering any UI
                    RaycastResult lastRaycastResult = UIKEventSystem.instance.GetUIInputModule().GetLastRaycastResult(Mouse.current.deviceId);
                    if (lastRaycastResult.gameObject == null
                        || lastRaycastResult.gameObject.layer != VLLayer.layerIdUI)
                    {
                        // Lock the cursor
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;

                        return false;
                    }
                }
            }
        }
        
        // Handle cursor unlocking
        if (Cursor.lockState != CursorLockMode.None)
        {
            if (_context.GetInputDeviceType().UsesCursor())
            {
                if (_context.action == rightClickInputAction
                    &&  _context.action.WasReleasedThisFrame()
                    && UIKEventSystem.instance != null)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // Send an empty input action for look so anything listening to it, if they are caching incoming values, will cache 0,0
                    OnInputActionTriggered.Invoke(new InputAction());

                    return false;
                }
            }
        }

        return true;
    }

    InputDevice[] UIKPlayer.GetInputDevices()
    {
        return UIKPlayer.GetInputDevices(this);
    }

    void UIKPlayer.OnTargetUIChanged(UIKTarget _oldTarget, UIKTarget _newTarget)
    {
    }

    public UIKTarget GetTargetUI()
    {
        return targetUI;
    }

    public bool SelectUI(UIKTarget _target)
    {
        return UIKPlayer.SelectUI(this, _target);
    }

    public bool DeselectUI()
    {
        return UIKPlayer.DeselectUI(this);
    }

    public bool TryNavigateUIByDirection(Vector2 _direction)
    {
        return UIKPlayer.TryNavigateUIByDirection(this, _direction);
    }

    public bool TryNavigateUIByDirection(UIKInputDirection _direction)
    {
        return UIKPlayer.TryNavigateUIByDirection(this, _direction);
    }

    public bool TryTargetUI(UIKTarget _target)
    {
        return UIKPlayer.TryTargetUI(this, _target);
    }

    public bool TryUntargetUI(UIKTarget _target = null)
    {
        return UIKPlayer.TryUntargetUI(this, _target);
    }

    public bool TrySubmitUI(UIKTarget _target)
    {
        return UIKPlayer.TrySubmitUI(this, _target);
    }
}
