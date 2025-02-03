using UnityEngine;
using UnityEngine.Events;

public class PassthroughScript : MonoBehaviour

{

    public Collider passthroughTrigger;

    public bool hasTriggered;

    public GameObject [] possibleRooms;


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
        possibleRooms[Random.Range(0,possibleRooms.Length)].SetActive(true);
        hasTriggered = true;
    }
}
