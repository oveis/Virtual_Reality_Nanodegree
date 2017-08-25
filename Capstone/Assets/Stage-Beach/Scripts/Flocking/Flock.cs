using UnityEngine;
using System.Collections;

public static class Flock
{
	public const int COUNT 							= 3000;
	public const int MAX_LINES 						= 65536;
	public const int MAX_NEIGHBOR_LINES 			= 64;

	private static Material _material = null;
	public static Material material
	{
		get
		{
			if(_material == null)
			{
				_material			= new Material(Shader.Find("Line"));
				_material.hideFlags = HideFlags.DontSave;
			}
			return _material;
		}
	}	

	public static int[,] 		vertex_indices			= new int[COUNT, MAX_NEIGHBOR_LINES];
	public static Vector3[] 	vertex_line_directions	= new Vector3[COUNT * 2];
	public static float[]		velocity				= new float[COUNT];
	public static Vector3[]		prior_position			= new Vector3[COUNT];
	public static Vector3[]		position				= new Vector3[COUNT];
	public static Vector3[]		prior_direction			= new Vector3[COUNT];
	public static Vector3[]		direction				= new Vector3[COUNT];
	public static Vector3[]		direction_avoidance		= new Vector3[COUNT];
	public static Vector3[]		direction_alignment		= new Vector3[COUNT];
	public static Vector3[]		direction_neighborhood	= new Vector3[COUNT];

	public static Vector3 center_of_flock				= Vector3.zero;

	public static Vector4 color_near					= new Vector4(1.2f, 0.8f, 0.0f, 1.0f);
	public static Vector4 color_far						= new Vector4(0.5f, 0.5f, 0.95f, 0.5f);

	public static Vector4 mouse							= Vector4.zero;

	//Exposed UI Parameters
	public static int max_neighbors						= 0;
	public static float neighborhood_radius 			= 0.0f;
	public static float boundary						= 0.0f;
	public static float avoidance						= 0.0f;
	public static float alignment						= 0.0f;
	public static float cohesion						= 0.0f;
	public static float gather							= 0.0f;
	public static float home							= 0.0f;
	public static float lerp							= 0.0f;
	public static float velocity_multiplier				= 0.0f;
	public static float velocity_max					= 0.0f;
	public static int update_interval					= 0;
	public static float comparison_frequency			= 0.0f;
	public static float direction_frequency				= 0.0f;
	public static int camera_index						= 0;
	public static bool	attach_camera					= false;									  
	public static bool draw_neighborhood_lines			= false;
	public static bool draw_direction_lines				= false;
	public static bool debug_origin						= false;
	public static bool interactive_origin				= false;
	public static Vector3 origin						= Vector3.zero;

	public static void Initialize () 
	{
		for(int i = 0; i < COUNT; i++)
		{
//			Vector3 random_position				= Random.insideUnitSphere * 512.0f;
//			random_position.y					+= 512.0f;
//			position[i]				= random_position;

			velocity[i]							= 0.05f;
			position[i]	 						= new Vector3(0.0f, 512.0f, 0.0f);
			prior_position[i]					= position[i];

			direction[i]						= Random.onUnitSphere;
			prior_direction[i]					= direction[i];
			direction_avoidance[i]				= direction[i];
			direction_alignment[i]				= direction[i];
			direction_neighborhood[i]			= direction[i];

			vertex_indices[i, 0]				= 0;
			
			for(int j = 1; j < MAX_NEIGHBOR_LINES; j++)
			{
				vertex_indices[i, j] = i;
			}
		}
	}

	public static void Update()
	{	
		update_interval 	= Mathf.Max(update_interval,  1);
		
		if(attach_camera)
		{
		//	Camera.main.transform._position	= _gameObject[camera_index].transform._position;
		//	Camera.main.transform.forward 	= _gameObject[camera_index].transform.direction;
		}	

		for(int i =0; i < COUNT; i++)
		{
			prior_direction[i]					= Vector3.Lerp(prior_direction[i], direction[i], lerp);
			prior_position[i]					= Vector3.Lerp(prior_position[i],   position[i], lerp);
		

			if(draw_direction_lines)
			{
				vertex_line_directions[2*i]			= prior_position[i]; 	
				vertex_line_directions[2*i+1]		= prior_position[i] - prior_direction[i] * velocity[i] * 0.5f;
					
				Debug.DrawLine(prior_position[i], prior_position[i] + prior_direction[i] * velocity[i], Color.black, 0.01f);
			}
		}
		

		if(interactive_origin)
		{
			origin.x += mouse.z;
			origin.z += mouse.w;
		}

		if(debug_origin)
		{
			Debug.DrawLine(Vector3.zero, origin, Color.white, 0.01f);
		}
	}	

	public static void Direct()
	{
		for(int i = 0; i < COUNT; i++)
		{
			//
			Vector3 center_direction 	= Vector3.Normalize(center_of_flock - position[i]);
			Vector3 origin_direction 	= Vector3.Normalize(origin - position[i]);
			float distance_to_origin 	= Vector3.Distance(origin, position[i]);

			//Flocking Parameters
			direction[i]				+= direction_avoidance[i]		* avoidance;
			direction[i]				+= direction_alignment[i]		* alignment;
			direction[i]				+= direction_neighborhood[i]	* cohesion;

			//Additional Parameters
			direction[i]				+= center_direction				* gather;
			direction[i]				+= origin_direction				* home * distance_to_origin;


			direction[i] 				= Vector3.Normalize(direction[i]);

			position[i]					= position[i] + direction[i] * velocity[i];
			position[i]					= Vector3.ClampMagnitude(position[i], boundary);
		}
	}

	
	// Update is called once per frame
	public static void Compare()
	{
		Vector3 new_center_of_flock 		= Vector3.zero;
		
		int i  	= (int)(Random.value * COUNT);
		
		for(int index = 0; index < update_interval; index++)
		{
			i = (i + index) % (COUNT - 1);

			new_center_of_flock				+= Flock.position[i];

			Vector3 direction_to_neighbor	= Vector3.zero;
			Vector3 direction_of_neighbor	= Vector3.zero;
			Vector3 center_of_neighborhood	= Vector3.zero;
			
			int local_neighbor_count		= 0;

			float velocity_neighborhood		= velocity[i];
			float distance_sum				= 0.0f;
			float distance_minimum			= boundary;

			for(int j = 0; j < COUNT; j++)
			{
				if(i != j)
				{
					float distance				= Vector3.Distance(position[i], position[j]);
					distance_minimum			= distance < distance_minimum ? distance : distance_minimum;

					bool in_local_neighborhood 	= distance < neighborhood_radius;
				
					if(in_local_neighborhood && local_neighbor_count < max_neighbors)
					{
						local_neighbor_count++;
						distance_sum 					+= distance;

						velocity_neighborhood 			+= velocity[j];
						direction_to_neighbor			+= (position[i] - position[j])	/ (Mathf.Max(vertex_indices[j,0], 1.0f)/max_neighbors);
						direction_of_neighbor			+= direction[j] 				* (velocity_max-velocity[j]);
						center_of_neighborhood  		+= position[j];

						Vector4 debug_color				= Vector4.Lerp(color_near, color_far, distance/neighborhood_radius);

						if(draw_neighborhood_lines && local_neighbor_count < MAX_NEIGHBOR_LINES - 1)
						{	
							vertex_indices[i, local_neighbor_count] = j;
							
							Debug.DrawLine(position[i], position[j], debug_color);
						}
						
					}
				}
			}

			vertex_indices[i, 0] = local_neighbor_count;

			if(local_neighbor_count > 0)
			{
				center_of_neighborhood		= Vector3.Normalize(center_of_neighborhood) * (Mathf.Max(vertex_indices[i,0], 1.0f)/max_neighbors);
			
				direction_avoidance[i]		= Vector3.Normalize(direction_to_neighbor) * (1.0f + 1.0f/distance_sum);
			
				direction_alignment[i]		= Vector3.Normalize(direction_of_neighbor);
			
				direction_neighborhood[i]	= Vector3.Normalize(center_of_neighborhood - position[index]) * vertex_indices[i,0]/max_neighbors;

				velocity[i]					= Mathf.Min((distance_sum/local_neighbor_count) * velocity_multiplier, velocity_max);				
			}
			else
			{
				
				center_of_neighborhood			= direction[i];
			
				direction_avoidance[i]			= direction[i];
			
				direction_alignment[i]			= direction[i];
			
				direction_neighborhood[i]		= direction[i];

				velocity[i]						= Mathf.Min(distance_minimum * velocity_multiplier, velocity_max);
			}
		}

		center_of_flock = Vector3.Lerp(center_of_flock, new_center_of_flock, 0.025f);	
	}
}
