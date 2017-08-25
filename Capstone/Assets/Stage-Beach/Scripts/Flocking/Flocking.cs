using UnityEngine;
using System.Collections;

public class Flocking : MonoBehaviour
{
	//Exposed UI Parameters
	public int max_neighbors							= 16;
	public float neighborhood_radius 					= 32.0f;
	public float boundary								= 32.0f;
	public float avoidance								= 1.0f;
	public float alignment								= 32.0f;
	public float cohesion								= 1.0f;
	public float gather									= 0.01f;
	public float home									= 0.01f;
	public float lerp									= 0.01f;

	public int update_interval							= 32;
	public float comparison_frequency					= 0.05f;
	public float direction_frequency					= 0.025f;

	public int camera_index								= 0;
	public bool	attach_camera							= false;

	public bool draw_neighborhood_lines					= true;
	public bool draw_direction_lines					= true;
	public bool debug_origin							= false;

	public float velocity								= 1.0f;
	public float velocity_max							= 1.0f;

	public bool interactive_origin						= false;
	public Vector3 origin								= Vector3.zero;


	public Vector4 color_near							= new Vector4(1.2f, 0.8f, 0.6f, 0.1f);	
	public Vector4 color_far							= new Vector4(0.5f, 0.5f, 1.125f, 0.125f);

	private static Vector4 _mouse						= Vector4.zero;

	void Awake () 
	{
//		Camera.main.clearFlags			= CameraClearFlags.Color;
//		Camera.main.backgroundColor		= Color.black;

//		GvrEye[] eye = GameObject.FindObjectsOfType<GvrEye>();

//		for(int i = 0; i < eye.Length; i++)
		{
//			eye[i].gameObject.AddComponent<FlockRenderer>();
		}
	}
	void Start () 
	{
		SetFlockParameters();
	
		Flock.Initialize();
 		
	
	//	InvokeRepeating("Compare", 0.01f, comparison_frequency);
	//	InvokeRepeating( "Direct", 0.01f, direction_frequency);
	}


	void Update()
	{
		if(Time.time < .125f)
		{
			Attract();
			origin 							= new Vector3(0.0f, 512.0f, 0.0f);
		}

		Flock.Compare();
		Flock.Direct();
		Flock.Update();
		
		UpdateMouseInput();
		
		SetFlockParameters();

		if(GvrViewer.Instance.Triggered)
		{
			Attract();
		}

		home 		= Mathf.Lerp(home, -0.0f, 0.125f);
	}


	void Compare()
	{
		Flock.Compare();
	}


	void Direct()
	{
		Flock.Direct();
	}


	void UpdateMouseInput()
	{
		_mouse.x						= UnityEngine.Input.mousePosition.x/Screen.width;
		_mouse.y						= UnityEngine.Input.mousePosition.y/Screen.height;
		_mouse.z						= UnityEngine.Input.GetAxis("Mouse X");
		_mouse.w						= UnityEngine.Input.GetAxis("Mouse Y");
	}


	void Attract()
	{
		origin 							= Camera.main.transform.position+Camera.main.transform.forward * 16.0f;
		home							+= 64.0f;
	}

	void SetFlockParameters()
	{
		Flock.neighborhood_radius 		= neighborhood_radius; 		
		Flock.max_neighbors				= max_neighbors;				
		Flock.boundary					= boundary;				
		Flock.avoidance					= avoidance;					
		Flock.alignment					= alignment;					
		Flock.cohesion					= cohesion;					
		Flock.gather					= gather;					
		Flock.home						= home;						
		Flock.lerp						= lerp;						
		Flock.update_interval			= update_interval;			
		Flock.comparison_frequency		= comparison_frequency;		
		Flock.direction_frequency		= direction_frequency;		
		Flock.attach_camera				= attach_camera;				
		Flock.camera_index				= camera_index;				
		Flock.draw_neighborhood_lines	= draw_neighborhood_lines;	
		Flock.draw_direction_lines		= draw_direction_lines;		
		Flock.debug_origin				= debug_origin;				
		Flock.velocity_multiplier		= velocity;
		Flock.velocity_max				= velocity_max;		
		Flock.interactive_origin		= interactive_origin;
		Flock.origin					= origin;
		Flock.color_near				= color_near;
		Flock.color_far					= color_far;
		Flock.mouse						= _mouse;
	}
}
