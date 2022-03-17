using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Istate
{
    void Enter(CharacterControllerBase characterController);

    void Execute(CharacterControllerBase characterController);

    void Exit(CharacterControllerBase characterController);

}