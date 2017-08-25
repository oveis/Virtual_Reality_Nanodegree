using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKey : MonoBehaviour {

	public GameObject keyPoof;
	public Chest chest;
	public bool keyCollected;

	public void OnKeyClicked() {
		GameObject keyPoofClone = Instantiate(keyPoof, transform.position, Quaternion.Euler(-90, 0, 0));
		keyPoofClone.GetComponent<AudioSource> ().Play ();
		chest.Unlock ();
		Destroy (gameObject);
	}
}
