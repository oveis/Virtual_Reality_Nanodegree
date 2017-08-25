using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour
{
    // Create a boolean value called "locked" that can be checked in OnDoorClicked()
	public bool locked;
    // Create a boolean value called "opening" that can be checked in Update()
	public bool opening;

	// AudioClips for Doors.
	public AudioClip doorLockedSound;
	public AudioClip doorOpenedSound;
	public AudioSource audioSource;

	// Door obejcts to rotate.
	public GameObject rightDoor;
	public GameObject leftDoor;

    void Update() {
        // If the door is opening and it is not fully raised
		if (opening && !locked) {
			// Animate the door raising up
			// Jinil: Raising up the door does not make sense to me.
			//        Instead of raising it up, rotate each doors 90 degree, so it looks realistic.
			audioSource.clip = doorOpenedSound;
			audioSource.Play ();
			rightDoor.transform.Rotate (0, 20f * Time.deltaTime, 0, Space.World);
			leftDoor.transform.Rotate (0, -20f * Time.deltaTime, 0, Space.World);

			// Stop when the doors are opened as 90 degree. Then, destroy Door object, so user can go in.
			if (rightDoor.transform.rotation.z < 0f) {
				opening = false;
				Destroy (gameObject);
			}
		}
    }

    public void OnDoorClicked() {
        // If the door is clicked and unlocked
		if (!locked) {
			// Set the "opening" boolean to true
			opening = true;
		}
        // (optionally) Else
		else {
			// Play a sound to indicate the door is locked
			audioSource.clip = doorLockedSound;
			audioSource.Play();
		}
    }

    public void Unlock()
    {
        // You'll need to set "locked" to false here
		locked = false;
    }
}
