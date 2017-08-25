using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //Create a reference to the KeyPoofPrefab and Door
	public GameObject keyPoof;
	public Door door;
	public bool keyCollected;

	void Update()
	{
		transform.position = new Vector3 (0.8f, 3 + Mathf.Sin (Time.time)/4, 71);
	}

	public void OnKeyClicked()
	{
        // Instatiate the KeyPoof Prefab where this key is located
        // Make sure the poof animates vertically
		GameObject keyPoofClone = Instantiate(keyPoof, transform.position, Quaternion.Euler(-90, 0, 0));

		// Play a sound via script.
		keyPoofClone.GetComponent<AudioSource> ().Play ();

        // Call the Unlock() method on the Door
		door.Unlock();

        // Set the Key Collected Variable to true
		keyCollected = true;

        // Destroy the key. Check the Unity documentation on how to use Destroy
		Destroy (gameObject);
    }

}
