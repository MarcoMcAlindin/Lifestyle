using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraTransformPositions
{
    [Header("Camera Transforms")]
    [SerializeField] public Transform[] cameraTransforms = new Transform[3];
   
    [SerializeField] public int transformIndex = 1;

    public void SwitchCameraTransform()
    {
        transformIndex++;
    }
}


public class CameraController : MonoBehaviour
{
    [SerializeField] CameraTransformPositions _cameraTransformPositions;
    public CameraTransformPositions GetCameraTransformPositions() { return _cameraTransformPositions; }

    PlayerController _player;
    public PlayerController GetPlayer() { return _player; }

    //Variables to do with target camera is looking at
    [Header("Look Target")]
    [SerializeField] public Vector3 _lookTargetposition;

    //Default Camera behaviour var
    ICameraBehaviour _currentCameraBehaviour;

    [SerializeField]private Vector3 _offsetWithPlayer;
    public Vector3 GetOffsetWithPlayer() { return _offsetWithPlayer; }

    [Header("Camera Movement Stats")]
    [SerializeField] public float _cameraRotationSpeed = 125.0f;
    [SerializeField] public float _cameraTranslationSpeed = 2.0f;

    [SerializeField] public float _timeToPositionReset = 5.0f;

    private void Awake()
    {
        //Assign player variable 
        _player = FindObjectOfType<PlayerController>();

        //Calculate initial offset with player
        _offsetWithPlayer = transform.position - _player.transform.position;

        //Set default look Target Position (roughly player Head)
        _lookTargetposition = new Vector3(_player.transform.position.x, _player.transform.position.y + 1.5f, _player.transform.position.z);

        

        //Assign default behaviour 
        _currentCameraBehaviour = new CameraDefaultBehaviour();
    }

    private void LateUpdate()
    {
        _currentCameraBehaviour.ExecuteBehaviour(this);
    }

    //Update Position of the look target
    public void UpdateLookTargetPosition(Vector3 position)
    {
        _lookTargetposition = position;
    }

    public void UpdatePlayerOffset()
    {
        _offsetWithPlayer = transform.position - _player.transform.position;
    }


    //ROTATION FUNCTIONS

    //Rotate Around X axis using right stick value as parameter
    public void RotateAroundXAxis(float inputValue)
    {
        //Check input value is not around null values
        if (!Mathf.Approximately(inputValue, 0f))
        {
            //Check if camera y position in between the min and max values
            if (transform.position.y > 0.5f && transform.position.y < 5.5f)
            {
                //Rotate around player using negative of input value
                transform.RotateAround(_lookTargetposition, transform.right, _cameraRotationSpeed / 1.5f * Time.deltaTime * -inputValue);
            }

            //Check camera y position is larger than max or smaller than min 
            if (transform.position.y < 1.5f || transform.position.y > 5.5f)
            {

                //Rotate around player on y axis using positive input value
                transform.RotateAround(_lookTargetposition, transform.right, _cameraRotationSpeed / 1.5f * Time.deltaTime * inputValue);
            }
        }
    }

    /*****************************************
     * Rotate Camera around x Axis of player *
     *****************************************/
    public void RotateAroundYAxis(float inputValue)
    {
        //Check input value is not around null values
        if (!Mathf.Approximately(inputValue, 0f))
        {
            //Rotate around
            transform.RotateAround(_lookTargetposition, Vector3.up, _cameraRotationSpeed * Time.deltaTime * inputValue);
        }
    }

    public void ResetToDefaultCameraPosition()
    {
        transform.position = Vector3.Lerp(transform.position, GetCameraTransformPositions().cameraTransforms[GetCameraTransformPositions().transformIndex].position, Time.deltaTime * _cameraTranslationSpeed);
    }
}
