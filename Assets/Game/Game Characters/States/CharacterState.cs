using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : Istate
{
    protected string _animatorVariable = "";

    public void Enter(CharacterControllerBase characterController)
    {
        SetAnimatorBool(characterController, _animatorVariable, true);
    }

    public abstract void Execute(CharacterControllerBase characterController);
   

    public void Exit(CharacterControllerBase characterController)
    {
        SetAnimatorBool(characterController, _animatorVariable, false);
    }

    public void SetAnimatorBool(CharacterControllerBase characterController, string varName, bool value)
    {
        int i = Animator.StringToHash(varName);
        characterController.GetAnimator().SetBool(varName, value);
    }

}
