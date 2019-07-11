using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeCollectable : MonoBehaviour
{
    public GameObject[] spawnPoints; //array of valid spawn points for collectables
    public List<GameObject> openSpawnPoints; //list of points available on its use
    public int index;
    public HopeSpawnPoint spawnPoint;
    public PlayerMovement playerMovement;
    public string spawnZoneTag;
    
    // Start is called before the first frame update
    void Start()
    {
        //if(spawnPoints == null)
        spawnPoints = GameObject.FindGameObjectsWithTag(spawnZoneTag);
        foreach(GameObject hopeSpawn in spawnPoints)
        {
            if (!hopeSpawn.GetComponent<HopeSpawnPoint>().occupied) openSpawnPoints.Add(hopeSpawn);
        }
      
        if(spawnPoints.Length == 0)
        {
            Debug.Log("no more available spawn points with tag: " + spawnZoneTag);
        }
        else
        {
            index = Random.Range(0, openSpawnPoints.Count);
            spawnPoint = openSpawnPoints[index].GetComponent<HopeSpawnPoint>();
            transform.position = openSpawnPoints[index].transform.position;
            spawnPoint.occupied = true;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.gainedHope();
            Destroy(gameObject);
        }

    }
}
