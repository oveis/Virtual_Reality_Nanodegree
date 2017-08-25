using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandEvents : MonoBehaviour {

	public GameObject coconut1;
	public GameObject coconut2;
	public GameObject coconut3;

	private bool isTrunkClicked = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (isTrunkClicked) {
			if (coconut2.transform.position.y > 0.6f) {
				coconut2.transform.Translate (0, -5f * Time.deltaTime, 0, Space.World);
			}
			if (coconut3.transform.position.y > 0.6f) {
				coconut3.transform.Translate (0, -5f * Time.deltaTime, 0, Space.World);
			}
		}
	}

	public void TrunkClicked() {
		isTrunkClicked = true;
	}
}
