using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : CharacterState
{
    public StandingState()
    {
        _animatorVariable = "IsStanding";
    }

    public override void Execute(CharacterControllerBase characterController)
    {
        characterController.transform.Translate(Vector3.forward * characterController.GetCurrentMovementSpeed() * Time.deltaTime, Space.Self);

        characterController.GetAnimator().SetFloat("Movement Speed", characterController.GetCurrentMovementSpeed());

        //Check If Input from theLeft stick has been detected, if so move character
        if (InputManager.Instance.IsLeftStickInUse())
        {
            characterController.Accelerate();

            characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation, CharacterControllerBase.RotateInMovingDirection( new Vector3(InputManager.Instance.LeftStickValues().x, 0.0f, InputManager.Instance.LeftStickValues().y)), characterController.GetCharacterMovementValues().RotationSpeed * Time.deltaTime);

            
        }
        else
        {
            characterController.Decelerate();
        }
    }
}
