using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachine
{
    CharacterControllerBase _owner;

    Istate _currentState;

    [Header("Current State")]
    [SerializeField] protected string _name = "";

    public StateMachine(CharacterControllerBase owner, Istate defaultState)
    {
        _owner = owner;
        _currentState = defaultState;
        _currentState.Enter(owner);
        _name = _currentState.GetType().Name;
    }

    public void ChangeState(Istate newState)
    {
        //Check if state that were rtying to acces is same as active one, if true exit function
        if (newState.GetType() == _currentState.GetType()) return;

        //Call Exit function of current state
        _currentState.Exit(_owner);

        //Asign current state with new state
        _currentState = newState;

        //Set current State name variable
        _name = _currentState.GetType().Name;

        //Enter the current state that has just been changed to the new one
        _currentState.Enter(_owner);
    }

    public void UpdateStateMachine()
    {
        //Execute execute method
        _currentState.Execute(_owner);
    }

}
