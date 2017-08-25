using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorEvents : MonoBehaviour {

	public bool horrorMode;
	public GameObject hiddenTV;
	public GameObject hiddenMonitorLeft;
	public GameObject hiddenMonitorRight;
	public GameObject deskChair;
	public GameObject teleport;

	public AudioClip horrorSound;
	public AudioSource audioSource;

	private Quaternion deskChairRot;
	private Vector3 teleportPos;

	// Use this for initialization
	void Start () {
		deskChairRot = deskChair.transform.rotation;
		teleportPos = teleport.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (horrorMode) {
			deskChair.transform.Rotate (0, 50f * Time.deltaTime, 0, Space.World);
			teleport.transform.position = new Vector3 (teleportPos.x, teleportPos.y + Mathf.Sin (Time.time)/4, teleportPos.z);
		} else {
			deskChair.transform.rotation = deskChairRot;
		}
	}

	void FixedUpdate() {

	}

	public void ToggleHorrorMode() {
		horrorMode = !horrorMode;

		hiddenTV.SetActive (horrorMode);
		hiddenMonitorLeft.SetActive (horrorMode);
		hiddenMonitorRight.SetActive (horrorMode);

		if (horrorMode) {
			audioSource.clip = horrorSound;
			audioSource.Play();
		}
	}
}
