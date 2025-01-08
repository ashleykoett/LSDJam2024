using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType { Small, Large }
    public DoorType DoorSize; // Specifies if the door is small or large
    public bool IsConnected; // Tracks whether this door is connected to another
}
