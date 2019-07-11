using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{

    public GameObject cam;
    public Camera cameraScript;

    public GameObject player;

    public float defaultSize;
    public float horizontalCameraBuffer = 5; //Distance from horizontal edge of camera you have to be before it zooms out. This allows you to move within the camera bounds without the zoom changing 
    public float verticalCameraBuffer = 5; 

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
        if (Mathf.Abs(player.transform.position.x) + horizontalCameraBuffer > cam.transform.position.x + cameraScript.orthographicSize || Mathf.Abs(player.transform.position.y) + verticalCameraBuffer > cam.transform.position.y + cameraScript.orthographicSize)
        {
            print("expanding camera x");
            cameraScript.orthographicSize += 0.1f;

        //if you are below camera bounds, minimize camera view. DO NOT do so though, if the camera is at it's defaultSize (So you don't get super zoomed in)
        }
        else if (cameraScript.orthographicSize > defaultSize)
        {
            print("contracting camera x");
            cameraScript.orthographicSize -= 0.1f;
        }        

    }
}
