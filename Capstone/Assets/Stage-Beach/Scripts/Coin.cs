using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	//Create a reference to the CoinPoofPrefab
	public GameObject coinPoof;

	public void OnCoinClicked() {
		GameObject coinPoofClone = Instantiate(coinPoof, transform.position, Quaternion.Euler(-90, 0, 0 ));
		coinPoofClone.GetComponent<AudioSource> ().Play ();
		Destroy (gameObject);
	}
}