using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassthroughScript : MonoBehaviour

{

    public Collider passthroughTrigger;

    public bool hasTriggered;

    public bool activeAdjacent;

    public GameObject [] possibleRooms;

    public List<RoomIdentifier> connectedRooms;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextRoom()
    
    {
        if(!hasTriggered)
        {
            int newRoom = Random.Range(0,possibleRooms.Length);
            possibleRooms[newRoom].SetActive(true);
            RoomIdentifier thisRoom = possibleRooms [newRoom].GetComponent<RoomIdentifier>();
            connectedRooms.Add(thisRoom);
            hasTriggered = true;
        }
    }
}
