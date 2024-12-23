using UnityEngine;
using UnityEngine.Events;

public class GenericInteraction : MonoBehaviour
{
    [SerializeField]
     private bool isInteractable = true;  // Determines if the interaction can be activated
    public bool IsInteractable => isInteractable;  // Public property for accessing isInteractable status

    public UnityEvent OnInteract;  // Event triggered when interaction occurs

    [SerializeField]
    private Outline outline;

    [Header ("Outline Settings")]

    public Color outlineColor = Color.white;

    [Range(0f, 20f)]
    public float outlineWidth = 12f;

    void Start()
    {
      OutlineSetup();
    }

    public void MakeActiveInteraction()
    {
        // Logic for when this interaction becomes active
        Debug.Log("Active interaction: " + gameObject.name);
        outline.enabled = true;
    }

    public void MakeInactiveInteraction()
    {
        // Logic for when this interaction becomes inactive
        Debug.Log("Dropped interaction: " + gameObject.name);
        outline.enabled = false;
    }

    public void Interact()
    {
        if (OnInteract != null)
        {
            OnInteract.Invoke();  // Trigger the interaction event
            Debug.Log("Interacted with: " + gameObject.name);
        }
    }

    public void OutlineSetup(){
        outline = gameObject.AddComponent<Outline>();
        outline.precomputeOutline = true;
        outline.enabled = false;
        outline.outlineWidth = outlineWidth;
        outline.OutlineColor = outlineColor;

    }
}
