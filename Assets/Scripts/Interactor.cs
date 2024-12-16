using UnityEngine;
using UnityEngine.InputSystem;  // Import for Input System (assuming you're using the Unity Input System package)

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayer;  // Layer mask for the raycast
    [SerializeField] [Range(0f, 20f)] private float raycastDistance = 5f;  // Max distance for the raycast
    [SerializeField] private InputAction interactInput;  // Input action for interaction

    [SerializeField] private GenericInteraction activeInteraction = null;

    private void OnEnable()
    {
        // Enable the input action
        interactInput.Enable();
        interactInput.performed += OnInteractInput;  // Subscribe to the input event
    }

    private void OnDisable()
    {
        // Disable the input action
        interactInput.Disable();
        interactInput.performed -= OnInteractInput;  // Unsubscribe from the input event
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        RaycastHit hit;
        // Perform raycast from the position forward
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, interactionLayer))
        {
            // Check if the object hit has a GenericInteraction component
            GenericInteraction interaction = hit.collider.GetComponent<GenericInteraction>();
            if (interaction != null && interaction != activeInteraction)
            {
                // Deactivate previous interaction
                if (activeInteraction != null)
                {
                    activeInteraction.MakeInactiveInteraction();
                }
                // Set new active interaction and activate it
                activeInteraction = interaction;
                activeInteraction.MakeActiveInteraction();
            }
        }
        else
        {
            // If raycast no longer hits, deactivate active interaction
            if (activeInteraction != null)
            {
                activeInteraction.MakeInactiveInteraction();
                activeInteraction = null;
            }
        }
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        // Check if there's an active interaction and if it's interactable
        if (activeInteraction != null && activeInteraction.IsInteractable)
        {
            activeInteraction.Interact();  // Trigger the interaction
        }
    }
}
