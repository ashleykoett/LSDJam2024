using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour
{
    public int LevelBlocks = 10; // Number of rooms to generate
    public GameObject[] RoomPrefabs; // Array of room prefabs
    public float RoomSpacing = 10f; // Space between room origins

    private List<GameObject> generatedRooms = new List<GameObject>();

    void Start()
    {
        GenerateLevel();
        ArrangeRooms();
    }

    void GenerateLevel()
    {
        if (RoomPrefabs.Length == 0)
        {
            Debug.LogError("No room prefabs assigned.");
            return;
        }

        for (int i = 0; i < LevelBlocks; i++)
        {
            GameObject newRoom = InstantiateRoom();
            if (newRoom != null)
            {
                generatedRooms.Add(newRoom);
            }
        }
    }

    void ArrangeRooms()
    {
        foreach (GameObject newRoom in generatedRooms)
        {
            foreach (GameObject existingRoom in generatedRooms)
            {
                if (newRoom == existingRoom) continue;

                RoomData newRoomData = newRoom.GetComponent<RoomData>();
                RoomData existingRoomData = existingRoom.GetComponent<RoomData>();

                if (newRoomData == null || existingRoomData == null) continue;

                foreach (Door newDoor in newRoomData.Doors)
                {
                    if (newDoor.IsConnected) continue;

                    foreach (Door existingDoor in existingRoomData.Doors)
                    {
                        if (!existingDoor.IsConnected &&
                            existingDoor.DoorSize == newDoor.DoorSize)
                        {
                            AlignRoom(newRoom, newDoor, existingRoom, existingDoor);
                            ConnectTwoDoors(newDoor, existingDoor);
                            break;
                        }
                    }
                }
            }
        }
    }

    GameObject InstantiateRoom()
    {
        // Pick a random room prefab
        GameObject roomPrefab = RoomPrefabs[Random.Range(0, RoomPrefabs.Length)];

        // Generate a position and random rotation for the room
        Vector3 position = GetRandomPosition();
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0); // Randomize rotation (90-degree increments)

        // Instantiate the room
        GameObject newRoom = Instantiate(roomPrefab, position, rotation);
        newRoom.transform.parent = transform; // Keep hierarchy organized

        return newRoom;
    }

    Vector3 GetRandomPosition()
    {
        // Ensure rooms don't overlap excessively; use a grid system or random spacing
        return new Vector3(
            Random.Range(-LevelBlocks * RoomSpacing / 2, LevelBlocks * RoomSpacing / 2),
            0, // Keep all rooms on the same floor
            Random.Range(-LevelBlocks * RoomSpacing / 2, LevelBlocks * RoomSpacing / 2)
        );
    }

    void AlignRoom(GameObject newRoom, Door newDoor, GameObject existingRoom, Door existingDoor)
    {
        // Calculate the position difference between the two doors
        Vector3 offset = existingDoor.transform.position - newDoor.transform.position;

        // Move the new room by the offset
        newRoom.transform.position += offset;

        // Align the new room's rotation so the doors face each other
        Vector3 newDoorForward = newDoor.transform.forward;
        Vector3 existingDoorForward = existingDoor.transform.forward;

        float angleDifference = Vector3.SignedAngle(newDoorForward, -existingDoorForward, Vector3.up);
        newRoom.transform.RotateAround(newDoor.transform.position, Vector3.up, angleDifference);
    }

    void ConnectTwoDoors(Door door1, Door door2)
    {
        door1.IsConnected = true;
        door2.IsConnected = true;

        // Optionally align doors perfectly
        door2.transform.position = door1.transform.position;
    }
}
