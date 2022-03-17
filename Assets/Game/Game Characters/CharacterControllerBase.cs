using UnityEngine;

[System.Serializable]
public class CharacterMovementValues
{
    //Movement Variables
    [SerializeField] float _maxMovementSpeed = 3.0f;
    [SerializeField] float _acceleration = 0.1f;
    [SerializeField] float _deceleration = 0.125f;
    [SerializeField] float _rotationSpeed = 85.0f;

    //Getters and Setters
    public float MaxMovementSpeed { get { return _maxMovementSpeed; } set { _maxMovementSpeed = value; } }
    public float Acceleration     { get { return _acceleration;     } set { _acceleration = value;     } }
    public float Deceleration     { get { return _deceleration;     } set { _deceleration = value;     } }
    public float RotationSpeed    { get { return _rotationSpeed;    } set { _rotationSpeed = value;    } }
}


public abstract class CharacterControllerBase : MonoBehaviour
{
    //Variable containing values for charcter movement
    [SerializeField] CharacterMovementValues _characterMovementValues;
    public CharacterMovementValues GetCharacterMovementValues() { return _characterMovementValues; }

    [SerializeField] float _currentMovementSpeed;
    public float GetCurrentMovementSpeed() { return _currentMovementSpeed; }
    
    //Character State Machine
    [SerializeField] protected StateMachine _characterStateMachine;

    //Default State
    Istate _defaultState;

    //Character Animator
    Animator _characterAnimator;

    //Character Animator Getter
    public Animator GetAnimator() { if (_characterAnimator != null) return _characterAnimator; else return null; }

    public void Awake()
    {
        //Assign animator
        _characterAnimator = GetComponent<Animator>();

        //Assign Default State
        _defaultState = new StandingState();

        //Create State machine using this script and the default state as parameters
        _characterStateMachine = new StateMachine(this, _defaultState);

    }

    /*******************************************************************************
     * 
     *                  CHARACTER CONTROLLER MOVEMENT METHODS
     *
     * ****************************************************************************/

    

    public static Quaternion RotateInMovingDirection(Vector3 movingDirection)
    {
        //Find angle of joystick axes
        float angle = Mathf.Atan2(movingDirection.x, movingDirection.z);
        //Convert angle
        angle = Mathf.Rad2Deg * angle;

        //Make sure player rotation takes camera rotation into consideration
        angle += Camera.main.transform.eulerAngles.y;

        //Set rotation quaternion to calculated angle
        Quaternion rotationDirection = Quaternion.Euler(0, angle, 0);

        return rotationDirection;
    }

    public void Accelerate()
    {
        if (_currentMovementSpeed < _characterMovementValues.MaxMovementSpeed) { _currentMovementSpeed += _characterMovementValues.Acceleration; }
    }

    public void Decelerate()
    {
        if (_currentMovementSpeed >= 0) _currentMovementSpeed -= _characterMovementValues.Deceleration;

        if (_currentMovementSpeed < 0) _currentMovementSpeed = 0.0f;
    }

}
