using UnityEngine;
using UnityEngine.UI;

public class Interaction_Welcome : MonoBehaviour
{


    public bool CanInteract = true;

    [Header("Camera & Ray settings")]
    public Camera Playercamera;
    public float InteractDistance = 3f;
    public LayerMask interactLayer = ~0;

    [Header("Crosshair")]
    public Image Crosshair_Img;
    public Color Cross_OnColor;
    public Color Cross_OffColor;

    public Vector3 Cross_OnScale;
    public Vector3 Cross_OffScale;
    
    private Ray ray;
    private RaycastHit hit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(Playercamera == null)
        {
            Playercamera = Camera.main; // if we didn't give the camera in the inspector, it automatically
            // gets the main camera for Player camera
        }

        // shoot a ray in the direction we are looking at
        ray = new Ray(Playercamera.transform.position, Playercamera.transform.forward);

        UpdateCursor(false); // make the cursor off at the start

    }

    // Update is called once per frame
    void Update()
    {
        
        if(CanInteract == false || Playercamera == null) return;

        ray.origin = Playercamera.transform.position;
        ray.direction = Playercamera.transform.forward;


        // For Crosshair
        if(Physics.Raycast(ray, out hit, InteractDistance, interactLayer))
        {

            if (hit.collider.CompareTag("Door"))
            {
                // Open Door
                UpdateCursor(true);
            }
            else
            {
                // hit none mentioned
                UpdateCursor(false);
            }

        }
        else
        {
            // hit nothing
            UpdateCursor(false);
        }


        // For Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }


    }


    private void Interact()
    {

        if(Physics.Raycast(ray, out hit, InteractDistance))
        {

            if (hit.collider.CompareTag("Door"))
            {
                // Open Door
                hit.collider.GetComponent<SimpleDoor>().ToggleDoorState();
            }
            else
            {
                // hit none mentioned
            }

        }
    }



    private void UpdateCursor(bool active)
    {
        
        if(active == true)
        {
            Crosshair_Img.color = Cross_OnColor;
            Crosshair_Img.transform.localScale = Cross_OnScale;
        }
        else
        {
            Crosshair_Img.color = Cross_OffColor;
            Crosshair_Img.transform.localScale = Cross_OffScale;
        }

    }

}
