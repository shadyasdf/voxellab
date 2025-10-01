using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class VLPlayerManager : MonoBehaviour, UIKPlayerManager
{
    private List<UIKInputDevice> autoJoinInputDevices = new() { UIKInputDevice.MouseAndKeyboard };

    public List<UIKPlayer> players { get; set; } = new();
    private IDisposable anyButtonPressObserver;
    
    public static VLPlayerManager instance { get; private set; }

    
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        UIKPlayerManager.instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Bind to InputSystem events
        anyButtonPressObserver = InputSystem.onAnyButtonPress.Call(InputSystem_OnAnyButtonPress);
    }

    protected virtual void Start()
    {
        // Auto join input devices
        foreach (UIKInputDevice autoJoinInputDeviceType in autoJoinInputDevices)
        {
            foreach (InputDevice inputDevice in InputSystem.devices)
            {
                // If there is already a player using this device, skip it
                if (IsDeviceInUse(inputDevice))
                {
                    continue;
                }

                UIKInputDevice inputDeviceType = inputDevice.GetInputDeviceType();
                if (inputDeviceType != UIKInputDevice.NONE
                    && inputDeviceType.HasAnyFlags(autoJoinInputDeviceType))
                {
                    TryJoinInputDevice(inputDevice);
                    break;
                }
            }
        }
    }
    
    protected virtual void OnDestroy()
    {
        anyButtonPressObserver?.Dispose();
    }

    
    protected virtual void InputSystem_OnAnyButtonPress(InputControl _inputControl)
    {
        TryJoinInputDevice(_inputControl.device);
    }
    
    protected virtual void TryJoinInputDevice(InputDevice _inputDevice)
    {
        TryJoinInputDevice(new[] { _inputDevice });
    }

    protected virtual void TryJoinInputDevice(InputDevice[] _inputDevices)
    {
        // Attempt to instantiate a new player if this input is from a new device, and other conditions are met
        
        // Don't allow joining a new player if we already have one
        if (players.Count > 0)
        {
            return;
        }
        
        // Don't allow joining a player if any of the devices are in use
        foreach (InputDevice inputDevice in _inputDevices)
        {
            // Ignore presses on devices that are already used by a player
            if (PlayerInput.FindFirstPairedToDevice(inputDevice) != null)
            {
                return;
            }

            // Additional check to make sure device isn't in use
            if (IsDeviceInUse(inputDevice))
            {
                return;
            }
        }

        // If we're joining with the mouse or keyboard, join those devices together on the same PlayerInput
        if (_inputDevices.Contains(Mouse.current)
            || _inputDevices.Contains(Keyboard.current))
        {
            // Make sure there isn't a player using the Mouse or the Keyboard
            foreach (VLPlayer player in players.Cast<VLPlayer>())
            {
                if (player.inputDeviceType.HasFlag(UIKInputDevice.Mouse)
                    || player.inputDeviceType.HasFlag(UIKInputDevice.Keyboard))
                {
                    return;
                }
            }

            JoinPlayer(-1, -1, null, new InputDevice[2] { Mouse.current, Keyboard.current });
        }
        else
        {
            JoinPlayer(-1, -1, null, _inputDevices);
        }
    }

    public bool IsDeviceInUse(InputDevice _inputDevice)
    {
        foreach (VLPlayer player in players.Cast<VLPlayer>())
        {
            if (((UIKPlayer)player).GetInputDevices().Contains(_inputDevice))
            {
                return true;
            }
        }
        
        return false;
    }
    

    /// <summary>
    /// Gets the first player found in the player array, which we know to be the one and only player.
    /// </summary>
    /// <returns>The local player</returns>
    public VLPlayer GetLocalPlayer()
    {
        return players.FirstOrDefault() as VLPlayer;
    }

    public UIKPlayer JoinPlayer(int _playerIndex = -1, int _splitScreenIndex = -1, string _controlScheme = null, InputDevice[] _inputDevices = null)
    {
        return UIKPlayerManager.JoinPlayer(this, _playerIndex, _splitScreenIndex, _controlScheme, _inputDevices);
    }
}
