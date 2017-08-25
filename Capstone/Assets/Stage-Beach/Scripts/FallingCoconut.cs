using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoconut : MonoBehaviour {

	private bool isCoconutClicked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isCoconutClicked && transform.position.y > 0.6f) {
			transform.Translate (0, -4f * Time.deltaTime, 0, Space.World);
		}
	}

	public void CoconutClicked() {
		isCoconutClicked = true;
	}
}
