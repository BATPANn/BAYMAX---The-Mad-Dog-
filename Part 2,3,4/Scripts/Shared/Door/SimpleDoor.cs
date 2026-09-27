using System.Collections;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{


    [Header("Door Settings")]
    public Vector3 openRotation = new Vector3(0, -90, 0);
    public float openSpeed = 50f;

    public bool DoorLocked = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip StartOpenSound;
    public AudioClip MovingSound;
    public AudioClip OpenedSound;
    public AudioClip ClosedSound;

    private Quaternion closedRot;
    private Quaternion openRot;
    private bool IsOpen = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRot = transform.localRotation;
        openRot = Quaternion.Euler(openRotation);

    }


    public void ToggleDoorState()
    {

        if(DoorLocked == true) return;

        StopAllCoroutines();
        IsOpen = !IsOpen; // Reverse Door state ( if open => close, if close => open) 

        StartCoroutine(MoveDoor_CO());

    }


    private IEnumerator MoveDoor_CO()
    {

        Quaternion targetRot = IsOpen ? openRot : closedRot;

        if(IsOpen && StartOpenSound)
        {
            audioSource.PlayOneShot(StartOpenSound);
        }

        if (MovingSound)
        {
            audioSource.clip = MovingSound;
            audioSource.loop = true;
            audioSource.Play();
        }


        while(Quaternion.Angle(transform.localRotation, targetRot) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRot, openSpeed * Time.deltaTime);
            yield return null; // wait for next frame
        }

        transform.localRotation = targetRot; // Set final Rotation, just in case 


        if (MovingSound)
        {
            audioSource.Stop();
        }


        AudioClip endSound = IsOpen ? OpenedSound : ClosedSound;

        if (endSound)
        {
            audioSource.PlayOneShot(endSound);
        }

    }



}
