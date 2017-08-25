using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseKey : MonoBehaviour {

	public GameObject keyPoof;

	public void OnKeyClicked() {
		GameObject keyPoofClone = Instantiate(keyPoof, transform.position, Quaternion.Euler(-90, 0, 0));
		keyPoofClone.GetComponent<AudioSource> ().Play ();
		Destroy (gameObject);
	}
}
