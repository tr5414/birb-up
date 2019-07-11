using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{

    public GameObject cam;
    public Camera cameraScript;

    public GameObject player;

    public float defaultSize;
    public float horizontalCameraBuffer = 5; //Distance from horizontal edge of camera you are before it zooms out. This allows you to move within the camera bounds without the zoom changing 

    // Start is called before the first frame update
    void Start()
    {
        if (!cam) cam = Camera.main.transform.gameObject;
        if (!cameraScript) cameraScript = cam.GetComponent<Camera>();
        if (!player) player = GameObject.Find("Player");
        defaultSize = cameraScript.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        //X
        print("player pos.x:"+ player.transform.position.x + " cam pos.x + ortho size:" + cam.transform.position.x + cameraScript.orthographicSize);
        print("cam ortho size:" + cameraScript.orthographicSize + " default size (The smallest it should go) :" + defaultSize);
        if (Mathf.Abs(player.transform.position.x) + horizontalCameraBuffer > cam.transform.position.x + cameraScript.orthographicSize)
        {
            print("expanding camera x");
            cameraScript.orthographicSize += 0.1f;

            //if you are below camera bounds, minimize camera view. DO NOT do so though, if the camera is at it's defaultSize (So you don't get super zoomed in)
        }else if (Mathf.Abs(player.transform.position.x) - horizontalCameraBuffer < cam.transform.position.x + cameraScript.orthographicSize
          && cameraScript.orthographicSize > defaultSize)
        {
            print("contracting camera x");
            cameraScript.orthographicSize -= 0.1f;
        }
        
        /*

    //Y
        //if the player.y is above the camera viewport height, expand out the size
        if (player.transform.position.y > cam.transform.position.y + cameraScript.orthographicSize)
        {
            cameraScript.orthographicSize+= 0.1f;

        //if you are below camera bounds, minimize camera view. DO NOT do so though, if the camera is at it's defaultSize (So you don't get super zoomed in)
        } else if(player.transform.position.y < cam.transform.position.y + cameraScript.orthographicSize 
            && cameraScript.orthographicSize > defaultSize)
        {
            cameraScript.orthographicSize-= 0.1f;
        }

    */
    }
}
