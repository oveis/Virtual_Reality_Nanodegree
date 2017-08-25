using UnityEngine;
using System.Collections;

public class Scene4 : MonoBehaviour 
{
	public GameObject 		boat_object;
	public Material 		ocean_material;

	private bool 			_rendering	= false;


	void Start()
	{
		Camera.main.clearFlags			= CameraClearFlags.Color;
		Camera.main.backgroundColor		= new Color(12.0f, 37.0f, 49.0f, 255.0f) * (.125f/255.0f);
		RenderSettings.ambientMode		= UnityEngine.Rendering.AmbientMode.Flat;
	}


	void Update () 
	{
	 	AnimateBoat();

		if(!_rendering && GameObject.Find("Main Camera Left") != null)
		{
			InitializeFlockRenderering();
			_rendering = true;
		}
	}


	void AnimateBoat() 
	{
		float time			= Time.time * 2.0f;

		float x 			= Mathf.Sin(time * 2.4f + 0.5f)*.025f;
		float y 			= Mathf.Sin(time * 1.4f + 0.5f)*.0125f;
		float z 			= Mathf.Sin(time * 1.1f + 0.3f)*.0625f;

		Vector3 rotation 	= new Vector3(x, y, z);

		Vector3 position 	= gameObject.transform.position;
		position.y 			+= z * 0.0625f; 

		boat_object.transform.Rotate(rotation);
		
		gameObject.transform.position = position;

		boat_object.transform.Rotate(rotation * 0.05f);
	}

	void InitializeFlockRenderering() 
	{
		GameObject.Find("Main Camera Left").AddComponent<FlockRenderer>();
		GameObject.Find("Main Camera Right").AddComponent<FlockRenderer>();
	}
}
