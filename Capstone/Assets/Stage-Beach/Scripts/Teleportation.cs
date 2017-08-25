using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour {

	public bool isAvailable;
	public string nextScene;

	public AudioClip findTreasureSound;
	public AudioSource audioSource;

	public void SetAvailable() {
		isAvailable = true;
	}

	public void ClickTeleport() {
		if (isAvailable) {
			SceneManager.LoadScene (nextScene);
		} else {
			audioSource.clip = findTreasureSound;
			audioSource.Play ();
		}
	}
}
