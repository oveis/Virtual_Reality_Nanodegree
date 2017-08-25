using UnityEngine;
using System.Collections;

public class Scene0 : MonoBehaviour 
{
	public GameObject main_object;
	public GameObject seat_object;
	public GameObject boat_object;
	public Material ocean_material;
	private Material ocean_collision_material;

	public float animation_speed = 1.5f;
	public float animation_scale = 1.5f;
//	private const string AUDIO_RESOURCE_PATH								= "Sounds/Ocean_Waves-Mike_Koenig";
//	private float _origional_boat_y_position;
	private Vector4 normal;
	
	void Start () {
		Ocean.gameObject.SetActive(true);	
		Ocean.gameObject.GetComponent<MeshRenderer>().material = ocean_material;
//		_origional_boat_y_position = boat_object.transform.position.y;

		ocean_collision_material = new Material(Shader.Find("OceanCollision"));
	}

	// Update is called once per frame
	void Update () 
	{	
	 	AnimateBoat();
		RideBoat();
	}
		
		
	void OnPreRender()
	{
		ocean_collision_material.SetVector("_Velocity", ocean_material.GetVector("_Velocity"));
		ocean_collision_material.SetVector("_Frequency", ocean_material.GetVector("_Frequency"));
		ocean_collision_material.SetVector("_Scale", ocean_material.GetVector("_Scale"));
		ocean_collision_material.SetVector("_Position", Vector4.zero);
		
		gameObject.GetComponent<Camera>().targetTexture	= RenderTexture.GetTemporary(1,1);
		Graphics.Blit(gameObject.GetComponent<Camera>().targetTexture, ocean_collision_material);
		Texture2D t						= new Texture2D(1,1);
		normal							= t.GetPixel(0, 0);
//		Debug.Log(normal.ToString());
	}

	void AnimateBoat() 
	{
		float time			= Time.time * animation_speed;

		float x 			= Mathf.Sin(time * 2.4f + 0.5f)*.025f;
		float y 			= Mathf.Sin(time * 1.4f + 0.5f)*.0125f;
		float z 			= Mathf.Sin(time * 1.1f + 0.3f)*.0625f;

		Vector3 rotation 	= new Vector3(x, y, z) * animation_scale;

		boat_object.transform.Rotate(rotation);
		
	}

	void RideBoat () 
	{		
		boat_object.transform.localPosition = new Vector3(boat_object.transform.localPosition.x, normal.y, boat_object.transform.localPosition.z);
	}
}
