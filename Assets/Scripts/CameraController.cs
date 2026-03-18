using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //check local player is exisiting and not dead
        if(PlayerController.me != null && !PlayerController.me.dead)
        {
            //pass info of player position to target posititon
            Vector3 targetPosition = PlayerController.me.transform.position; //position of player in that scene
            
            //the z value needs to be default of -10
            targetPosition.z = -10;

            //position of camera will be where player position is
            transform.position = targetPosition;
        }
    }
}
