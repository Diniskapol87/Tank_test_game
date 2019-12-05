using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sceneWidth;
    public float sceneHeight;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void LateUpdate()
    {
        //движение камеры за персонажем
        Vector3 playerPos = Core.PlayerPosition;
        if (-sceneWidth / 2 + Screen.width / 100 < playerPos.x && playerPos.x < sceneWidth / 2 - Screen.width / 100)
            transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
        if (-sceneHeight / 2 + Screen.height / 100 < playerPos.z && playerPos.z < sceneHeight / 2 - Screen.height / 100)
            transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.z);
    }
}
