using UnityEngine;
using System.Collections;
using System.IO;

public class Scene2 : MonoBehaviour 
{
	public Material ocean_material;
	public Material cloud_material;

	void Start () 
	{
		RenderSettings.fog 			= true;
		RenderSettings.fogDensity 	= .2f;
		RenderSettings.fogColor 	= Color.white * 0.75f;

		Clouds.gameObject.SetActive(true);	
		Clouds.gameObject.transform.localScale 	= Vector3.one * 192.0f;
		Clouds.gameObject.transform.position 	= new Vector3(0.0f, 32.0f, 0.0f);
		Clouds.gameObject.transform.Rotate(new Vector3(0.0f, 90.0f, 180.0f));
		Clouds.gameObject.GetComponent<MeshRenderer>().material = cloud_material;	

		Ocean.gameObject.SetActive(true);	
		Ocean.gameObject.transform.position = new Vector3(0.0f, -1.7f, 0.0f);
		Ocean.gameObject.transform.localScale 	= Vector3.one * 32.0f;
		Ocean.gameObject.GetComponent<MeshRenderer>().material = ocean_material;	
		Ocean.audio_source.volume 				= 0.125f;
		Ocean.audio_source.Play();
	}

	
	void Update () 
	{
		Ocean.SetSoundPositionRelativeToViewer();
	}
}
