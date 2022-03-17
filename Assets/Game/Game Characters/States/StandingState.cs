using UnityEngine;

public class StandingState : CharacterState
{
    public StandingState()
    {
        //Set variable name for animation transition
        _animatorVariable = "IsStanding";
    }

    public override void Execute(CharacterControllerBase characterController)
    {
        //Translate player using current movement speed
        characterController.transform.Translate(Vector3.forward * characterController.GetCurrentMovementSpeed() * Time.deltaTime, Space.Self);

        characterController.GetAnimator().SetFloat("Movement Speed", characterController.GetCurrentMovementSpeed());

        //Check If Input from theLeft stick has been detected, if so move character
        if (InputManager.Instance.IsLeftStickInUse())
        {
            //Accelerate player movement speed
            characterController.Accelerate();

            //Rotate player towards moving direction
            characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation, CharacterControllerBase.RotateInMovingDirection( new Vector3(InputManager.Instance.LeftStickValues().x, 0.0f, InputManager.Instance.LeftStickValues().y)), characterController.GetCharacterMovementValues().RotationSpeed * Time.deltaTime);

            
        }
        else
        {
            //Decelerate player movement speed
            characterController.Decelerate();
        }
    }
}
