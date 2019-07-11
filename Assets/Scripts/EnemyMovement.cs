using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float distanceMin = 3;
    public float distanceMax = 5;
    public float waitTime = 2;
    bool moving;
    int direction = 1;
    bool delayDone = false;
    bool cycleReady = true;
    float speed = 4;
    float distance;
    float time = 0;
    float timeDone = 0;
    bool collidedLast; // object hit another object last movement cycle

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") {
            moving = false;
            collidedLast = true;
            float diff = transform.position.x - collision.gameObject.transform.position.x;
            if (diff>0) {
                direction = 1;
            } else {
                direction = -1;
            }
            cycleReady = true;
            delayDone = false;
            timeDone = Time.time + waitTime;
        }
    }
            // Update is called once per frame
    void Update()
    {
        ////////////////////
        /// Movement over time
        ////////////////////
        if (cycleReady && timeDone<Time.time) { // waits a set amount of time before starting next wait cycle
            delayDone = true;
            cycleReady = false;
        }
        //if (Input.GetKeyDown("w")&&!moving) {
        if (delayDone&&!moving) { // sets the distance, direction, and end time of the movement cycle
            distance = Random.Range(distanceMin, distanceMax);
            if (!collidedLast) {
                direction = 1;
                if (Random.Range(0, 2) == 1) {
                    direction *= -1;
                }
            } else {
                collidedLast = false;
            }
            moving = true;
            time = Time.time + (distance / speed); 
        }
        if (moving) { // handles movement overtime and leads to the start of the cycle
            if (time > Time.time) {
                Vector3 temp = transform.position;
                temp.x += direction * speed / 100;
                transform.position = temp;
            } else {
                moving = false;
                delayDone = false;
                cycleReady = true;
                timeDone = Time.time + waitTime;
            }
        }
        /////////////////////
    }
}
