using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerFloater = 0; // how much the player goes up per frame
    public float jumpInc = 0.3f; // what playerFloater is set to initially
    int jumpsLeft = 0;
    public int totalJumps = 1;
    public float jumpFadeOff = 0.01f;
    bool grounded = false;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") {
            jumpsLeft = totalJumps;
            grounded = true;
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
        if (playerFloater >= -0.5&&!grounded) {
            Vector3 pos = transform.position;
            pos.y += playerFloater;
            transform.position = pos;
            playerFloater -= jumpFadeOff;
        }

        if (Input.GetKey("d")){
            Vector3 pos = transform.position;
            pos.x += 0.075f;
            transform.position = pos;
        }
        if (Input.GetKey("a")){
            Vector3 pos = transform.position;
            pos.x -= 0.075f;
            transform.position = pos;
        }
    }
}
