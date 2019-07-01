using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int jumpsLeft = 0; 
    float playerFloater = 0; // how much the player goes up per frame
    public float downwardGravityCap = -0.01f;
    public float jumpFadeOff = 0.01f; // how much playerFloater is decreased by each frame
    public float jumpInc = 0.3f; // what playerFloater is set to initially
    public int totalJumps = 1; // change to control how many jumps the player has
    bool grounded = false;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") {
            jumpsLeft = totalJumps;
            grounded = true;
        }

    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") {
            grounded = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")&&playerFloater<=0&&jumpsLeft>0){
            jumpsLeft--;
            playerFloater = jumpInc;
            grounded = false;
        }
        if (!grounded) {
            Vector3 pos = transform.position;
            pos.y += playerFloater;
            transform.position = pos;
            if(playerFloater >= downwardGravityCap)
                playerFloater -= jumpFadeOff;
        }

        if (Input.GetKey("d")|| Input.GetKey("right")) {
            Vector3 pos = transform.position;
            pos.x += 0.075f;
            transform.position = pos;
        }
        if (Input.GetKey("a")|| Input.GetKey("left")) {
            Vector3 pos = transform.position;
            pos.x -= 0.075f;
            transform.position = pos;
        }
    }
}
