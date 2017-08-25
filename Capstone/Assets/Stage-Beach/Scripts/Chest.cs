using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	public bool locked;
	public bool opening;

	public GameObject chestUpperPart;
	public Teleportation teleportation;

	public AudioClip chestLockedSound;
	public AudioClip chestOpenedSound;
	public AudioSource audioSource;

	public Coin[] coins;
	
	// Update is called once per frame
	void Update () {
		if (opening && !locked) {
			audioSource.clip = chestLockedSound;
			audioSource.Play ();

			chestUpperPart.transform.Rotate (0, 0, 20f * Time.deltaTime, Space.World);
		}

		if (chestUpperPart.transform.rotation.x > 0.4f) {
			opening = false;
			for (int i=0; i<coins.Length; i++) {
				if (coins [i] != null) {
					coins[i].OnCoinClicked ();
				}
			}
			teleportation.SetAvailable ();
		}
	}

	public void OnChestClicked() {
		if (!locked) {
			opening = true;
		} else {
			audioSource.clip = chestLockedSound;
			audioSource.Play ();
		}
	}

	public void Unlock()
	{
		// You'll need to set "locked" to false here
		locked = false;
	}
}
