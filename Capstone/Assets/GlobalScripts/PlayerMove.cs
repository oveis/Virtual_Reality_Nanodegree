using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public GameObject player;
	public float sensitivity;

	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			player.transform.Translate (Vector3.forward * sensitivity * sensitivity);
		}
	}
}
