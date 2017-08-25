using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourLogic : MonoBehaviour {

	public GameObject introCard;

	public void ClickKey() {
		introCard.SetActive (false);
	}
}
