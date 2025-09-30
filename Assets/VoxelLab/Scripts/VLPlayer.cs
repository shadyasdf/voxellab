using System;
using System.Collections.Generic;
using UIKit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VLPlayer : MonoBehaviour, UIKPlayer
{
    [HideInInspector] public UnityEvent<InputAction> OnInputActionTriggered = new();
    
    [SerializeField] private PlayerInput playerInput;
    
    public UIKSelectable selectedUI { get; set; }
    public UIKInputDevice inputDeviceType { get; protected set; }


    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        playerInput.onActionTriggered += PlayerInput_OnActionTriggered;
        playerInput.onControlsChanged += PlayerInput_OnControlsChanged;
        
        PlayerInput_OnControlsChanged(playerInput);
    }

    protected virtual void Start()
    {
        VLCanvas.instance.PushScreen(VLUI.screenName_menu);
    }

    
    protected virtual void PlayerInput_OnActionTriggered(InputAction.CallbackContext _context)
    {
        // The device used to trigger the action
        // _context.action.activeControl.device
        
        // Consume UI inputs before broadcasting them
        switch (_context.action.name)
        {
            case VLInput.actionUILeftClick:
                goto case VLInput.actionUISubmit;
            case VLInput.actionUISubmit:
                if (_context.action.WasPressedThisFrame()
                    && _context.action.triggered)
                {
                    if (TrySubmitUI(GetSelectedUI()))
                    {
                        return;
                    }
                }
                break;
            case VLInput.actionUIMove:
                if (_context.action.WasPerformedThisFrame())
                {
                    if (TryNavigateUIByDirection(_context.ReadValue<Vector2>()))
                    {
                        return;
                    }
                }
                break;
        }

        // Handle cursor locking
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            // If we're using a mouse device, and we're right-clicking, attempt to lock the cursor to the game
            if (_context.GetInputDeviceType().UsesCursor())
            {
                if (_context.action.name == VLInput.actionUIRightClick
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

                        return;
                    }
                }
            }
        }
        
        // Handle cursor unlocking
        if (Cursor.lockState != CursorLockMode.None)
        {
            if (_context.GetInputDeviceType().UsesCursor())
            {
                if (_context.action.name == VLInput.actionUIRightClick
                    &&  _context.action.WasReleasedThisFrame()
                    && UIKEventSystem.instance != null)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    
                    // Send an empty input action for look so anything listening to it, if they are caching incoming values, will cache 0,0
                    OnInputActionTriggered.Invoke(new InputAction(VLInput.actionLook));

                    return;
                }
            }
        }

        OnInputActionTriggered.Invoke(_context.action);
    }
    
    protected virtual void PlayerInput_OnControlsChanged(PlayerInput _playerInput)
    {
        if (_playerInput.devices.Count == 1)
        {
            if (_playerInput.devices[0] == null)
            {
                Debug.LogError("Player's playerInput device was null");
                return;
            }

            inputDeviceType = _playerInput.devices[0].GetInputDeviceType();
            return;
        }
        else if (_playerInput.devices.Count == 2)
        {
            bool hasMouse = false;
            bool hasKeyboard = false;

            foreach (InputDevice inputDevice in _playerInput.devices)
            {
                if (inputDevice.GetInputDeviceType() == UIKInputDevice.Mouse)
                {
                    hasMouse = true;
                    continue;
                }

                if (inputDevice.GetInputDeviceType() == UIKInputDevice.Keyboard)
                {
                    hasKeyboard = true;
                    continue;
                }
            }

            if (hasKeyboard && hasMouse)
            {
                inputDeviceType = UIKInputDevice.MouseAndKeyboard;
                return;
            }

            Debug.LogError("Failed to find valid InputDeviceType for given set of 2 devices");
            return;
        }
        else
        {
            Debug.LogError("Failed to find valid playerInput device");
            return;
        }
    }

    public InputDevice[] GetInputDevices()
    {
        return playerInput.devices.ToArray();
    }
    
    
    void UIKPlayer.OnSelectedUIChanged(UIKSelectable _oldSelectable, UIKSelectable _newSelectable)
    {
    }

    public UIKInputDevice GetInputDeviceType()
    {
        return inputDeviceType;
    }

    public UIKSelectable GetSelectedUI()
    {
        return selectedUI;
    }

    public bool TrySelectUI(UIKSelectable _selectable)
    {
        return UIKPlayer.TrySelectUI(this, _selectable);
    }

    public bool TryDeselectUI()
    {
        return UIKPlayer.TryDeselectUI(this);
    }

    public bool TryNavigateUIByDirection(Vector2 _direction)
    {
        return UIKPlayer.TryNavigateUIByDirection(this, _direction);
    }

    public bool TryNavigateUIByDirection(UIKInputDirection _direction)
    {
        return UIKPlayer.TryNavigateUIByDirection(this, _direction);
    }

    public bool TrySubmitUI(UIKSelectable _selectable)
    {
        return UIKPlayer.TrySubmitUI(this, _selectable);
    }
}
