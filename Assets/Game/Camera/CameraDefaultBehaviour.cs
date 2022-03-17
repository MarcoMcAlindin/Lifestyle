using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDefaultBehaviour : ICameraBehaviour
{
    //Timer for camera repositioning
    float _positionResetTimer = 0.0f;

    public void ExecuteBehaviour(CameraController cameraController)
    {
        //Rotate camera towards look at position
        cameraController.transform.LookAt(cameraController._lookTargetposition);

        //Update look target position variable 
        cameraController.UpdateLookTargetPosition(new Vector3(cameraController.GetPlayer().transform.position.x, cameraController.GetPlayer().transform.position.y + 1.5f, cameraController.GetPlayer().transform.position.z));



        //----------INPUT-------------------

        //Check if Left Stick or right stick are in use
        if (InputManager.Instance.IsLeftStickInUse() || InputManager.Instance.IsRightStickInUse())
        {
            //Reset timer for camera default repositioning
            _positionResetTimer = 0.0f;

            if (InputManager.Instance.IsLeftStickInUse())
            {
                //set camera position using offset with player
                cameraController.transform.position = cameraController.GetPlayer().transform.position + cameraController.GetOffsetWithPlayer();

            }

            if (InputManager.Instance.IsRightStickInUse())
            {
                //Makes sure when left stick is released offset is no messed with by player's deceleration
                if (cameraController.transform.position + cameraController.GetOffsetWithPlayer() != cameraController.transform.position)
                {
                    //set camera position using offset with player
                    cameraController.transform.position = cameraController.GetPlayer().transform.position + cameraController.GetOffsetWithPlayer();
                }

                //Rotate camera around player using z and y axis
                cameraController.RotateAroundXAxis(InputManager.Instance.RightStickValues().y);
                cameraController.RotateAroundYAxis(InputManager.Instance.RightStickValues().x);
            }
        }
        else
        {
            //Incerement timer for repositioning 
            _positionResetTimer += Time.deltaTime;

            //Check if enough time has passed without any input 
            if (_positionResetTimer > cameraController._timeToPositionReset)
            {
                cameraController.ResetToDefaultCameraPosition();
            }
            else
            {
                cameraController.transform.position = cameraController.GetPlayer().transform.position + cameraController.GetOffsetWithPlayer();
            }
        }

        //------------------------------------


        //Update offset with payer after all camera trtansformations have been executed
        cameraController.UpdatePlayerOffset();
    }


}
