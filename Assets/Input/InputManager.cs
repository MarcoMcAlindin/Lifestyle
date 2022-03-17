using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    //Input actions holding button binding configuration
    [SerializeField] Lifestyle _inputActions;

    //Deadzone to control stick sensitivity
    static float _joystickDeadZone = 0.125f;

    public void Start()
    {
        //Assign and Enable new input actions asset
        _inputActions = new Lifestyle();
        _inputActions.Enable();
    }

    
    //------LEFT STICK---------- 

    //Get Left stick values from input actions
    public Vector2 LeftStickValues()
    {
        return _inputActions.Player.LeftStick.ReadValue<Vector2>();
    }

    //Check if left stick is being used and values are above deadzone
    public bool IsLeftStickInUse()
    {
        if (LeftStickValues().x > _joystickDeadZone || LeftStickValues().x < -_joystickDeadZone || LeftStickValues().y > _joystickDeadZone || LeftStickValues().y < -_joystickDeadZone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //-------RIGHT STICK----------

    //Get right stick values from input actions
    public Vector2 RightStickValues()
    {
        return _inputActions.Player.RightStick.ReadValue<Vector2>();
    }

    //Check if Right stick is being used and values are above deadzone
    public bool IsRightStickInUse()
    {
        if (RightStickValues().x > _joystickDeadZone || RightStickValues().x < -_joystickDeadZone || RightStickValues().y > _joystickDeadZone || RightStickValues().y < -_joystickDeadZone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //-----LEFT TRIGGER-------

    //Check If Left Trigger is Pressed
    public bool IsLeftTriggerPressed()
    {
        if (_inputActions.Player.LeftTrigger.IsPressed())
        {
            return true;
        }
        else if (_inputActions.Player.LeftTrigger.WasReleasedThisFrame() || !_inputActions.Player.LeftTrigger.IsPressed())
        {
            return false;

        }
        else
        {
            return false;
        }
    }

}
