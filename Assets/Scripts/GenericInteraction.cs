using UnityEngine;
using UnityEngine.Events;

public class GenericInteraction : MonoBehaviour
{
    [SerializeField] private bool isInteractable = true;  // Determines if the interaction can be activated
    public bool IsInteractable => isInteractable;  // Public property for accessing isInteractable status

    public UnityEvent OnInteract;  // Event triggered when interaction occurs

    public void MakeActiveInteraction()
    {
        // Logic for when this interaction becomes active
        Debug.Log("Active interaction: " + gameObject.name);
    }

    public void MakeInactiveInteraction()
    {
        // Logic for when this interaction becomes inactive
        Debug.Log("Inactive interaction: " + gameObject.name);
    }

    public void Interact()
    {
        if (OnInteract != null)
        {
            OnInteract.Invoke();  // Trigger the interaction event
            Debug.Log("Interacted with: " + gameObject.name);
        }
    }
}
