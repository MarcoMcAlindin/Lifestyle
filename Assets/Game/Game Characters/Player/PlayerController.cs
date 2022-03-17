using UnityEngine;

public class PlayerController : CharacterControllerBase
{
    public void FixedUpdate()
    {
        _characterStateMachine.UpdateStateMachine();
    }
}
