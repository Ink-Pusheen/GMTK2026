using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField] bool bAllowInteraction = true; //Can the player currently take input?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main; //Grab the camera
    }

    // Update is called once per frame
    void Update()
    {
        //Left mouse interaction
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!bAllowInteraction) return; //Return if interaction is disabled

            Vector2 mouse = Mouse.current.position.ReadValue();

            Vector3 mousePos = mainCam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, mainCam.transform.position.z));
            mousePos.z = 0;

            Ray newRay = mainCam.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(newRay, out hit, 9999))
            {
                iInteraction interacted = hit.collider.GetComponent<iInteraction>();

                if (interacted is not null)
                {
                    interacted.LeftClickInteraction();
                }
            }
        }

        //Right mouse interaction
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (!bAllowInteraction) return; //Return if interaction is disabled

            Vector2 mouse = Mouse.current.position.ReadValue();

            Vector3 mousePos = mainCam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, mainCam.transform.position.z));
            mousePos.z = 0;

            Ray newRay = mainCam.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(newRay, out hit, 9999))
            {
                iInteraction interacted = hit.collider.GetComponent<iInteraction>();

                if (interacted is not null)
                {
                    interacted.RightClickInteraction();
                }
            }
        }
    }


    //Public function to flip the interaction state
    public void SwapInteractionState()
    {
        bAllowInteraction = !bAllowInteraction;
    }
}
