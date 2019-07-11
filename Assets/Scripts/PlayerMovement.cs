using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int jumpsLeft = 0; 
    float playerFloater = 0; // how much the player goes up per frame

    float jumpHeldTime = 0.0f;
    bool jumpButtonDown = false;
    bool jumpWasAutoRelease = false;

    public float maxJumpHoldTime = 0.5f;

    public float downwardGravityCap = -0.01f;
    public float jumpFadeOff = 0.01f; // how much playerFloater is decreased per frame
    public float jumpIncBase = 0.3f; //Base value for playerfloater after jump
    public float jumpIncMax = 0.3f; //Initial max value for playerfloater after a jump
    public int totalJumps = 1; // change to control how many jumps the player has

    public int hopeHeld; //Number of hope collectables the player has obtained
    public int hopeTillHeightIncrease = 3;
    public int hopeTillJumpsIncrease = 3;

    bool grounded = false;

    public GameObject cam;


    void Start()
    {
        if (!cam) { cam = GameObject.Find("Main Camera"); } //Find camera in level if it is not specified in Inspector
    }

    void OnCollisionEnter(Collision collision)
    {


        if (!grounded)
        {
            ShakeCamera(this.GetComponent<Rigidbody>().velocity.y);
        }

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

    public void gainedHope()
    {
        hopeHeld++;
        hopeTillHeightIncrease--;
        hopeTillJumpsIncrease--;

        if(hopeTillHeightIncrease == 0)
        {
            jumpIncMax += 0.1f;
            hopeTillHeightIncrease = 3;
        }
        if(hopeTillJumpsIncrease == 0)
        {
            totalJumps++;
            hopeTillJumpsIncrease = 3;
        }
    }

    public void lostHope()
    {
        if (totalJumps > 0)
        { 
            hopeHeld--;
            hopeTillHeightIncrease++;
            hopeTillJumpsIncrease++;

            if (hopeTillHeightIncrease > 3)
            {
                jumpIncMax -= 0.1f;
                hopeTillHeightIncrease = 1;
            }
            if (hopeTillJumpsIncrease > 3)
            {
                totalJumps--;
                hopeTillJumpsIncrease = 1;
            }
        }
    }


    //Shake the camera based upon y velocity
    void ShakeCamera(float y)
    {
        cam.GetComponent<CameraShake>().ShakeCamera(Mathf.Abs(y) * 20.0f, 4.0f);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && playerFloater<=0&&jumpsLeft>0){
            jumpButtonDown = true;
            jumpWasAutoRelease = false;
        }

        if (jumpButtonDown)
        {
            jumpHeldTime += Time.deltaTime;
            if(jumpHeldTime > maxJumpHoldTime)
            {
                jumpWasAutoRelease = true;
                jump();
            }
        }

        if (Input.GetKeyUp("space") && playerFloater<=0 && jumpsLeft > 0)
        {
            if(!jumpWasAutoRelease)
            jump();
        }

        if (!grounded) {
            Vector3 pos = transform.position;
            pos.y += playerFloater;
            transform.position = pos;
            if (playerFloater >= downwardGravityCap)
            {
                playerFloater -= jumpFadeOff;
            }  
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

    void jump ()
    {
        jumpsLeft--;
        playerFloater = Mathf.Clamp( (jumpHeldTime / maxJumpHoldTime) * jumpIncMax, jumpIncBase, jumpIncMax);
        Debug.Log("PlayerFloater on jump: " + playerFloater);
        jumpHeldTime = 0.0f;
        jumpButtonDown = false;
        grounded = false;
    }
}
