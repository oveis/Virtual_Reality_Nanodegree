using UnityEngine;
using System.Collections;

public class ExampleParticles : MonoBehaviour 
{
	private ParticleSystem 				_particle_system;
	private ParticleSystem.Particle[] 	_particles;
	
	private const int count				= 512;

	private int	_time_index				= 0;
	private int	_last_index				= 0;

	
	public void Awake()
	{
		//start up the particle system when the script is loaded
		InitializeParticleSystem();
	}


	public void Update () 
	{
		//update the particle positions one frame at a time to create a trail
		_last_index 				= _time_index % count;
		_time_index 				= Time.frameCount % count;

		//create a smoothed position by finding the midpoint between this particle and the next
		Vector3	position			= Vector3.Lerp(gameObject.transform.position, _particles[_last_index].position, 0.5f);
		
		//create a size by finding the distance between this particle and the next (to create a nicely contoured trail)
		float size					= Vector3.Distance(_particles[_last_index].position, position) * 5.0f;

		//create a color from the current gameobject position
		Vector4 color				= new Vector4(position.x, position.y, position.z, 0.0f);
 		color						= color * 0.5f + Vector4.one * 0.5f;
		color						= Vector4.Normalize(color);
		 
		//set the position between the last position and the new one so they end up in the middle
		_particles[_time_index].position 	= position;
		_particles[_time_index].startSize	= size; 
		_particles[_time_index].startColor	= (Color)color;

		FadeParticles ();

		_particle_system.SetParticles(_particles, _particles.Length);
	}


	void InitializeParticleSystem()
	{
		//create a particle system and configure it
		gameObject.AddComponent<ParticleSystem>();
		_particle_system 						= gameObject.GetComponent<ParticleSystem>();
		_particle_system.startLifetime	 		= 0.0f;
		_particle_system.simulationSpace		= ParticleSystemSimulationSpace.World;
		_particle_system.playOnAwake 			= true;
		_particle_system.startDelay				= 0.0f;
		_particle_system.loop 					= false;

		//tell the particle system renderer to sort by distance
		gameObject.GetComponent<ParticleSystemRenderer>().sortMode = ParticleSystemSortMode.Distance;

		_particles 								= new ParticleSystem.Particle[count];
		
		_particle_system.Emit(count);
	}


	void FadeParticles () 
	{
		float unit 			= 1.0f/(float)(count);
		float time_index	= unit*(float)_time_index;
		for(int i = 0; i < count; i++)
		{
			//set the particles opacity by how far their time index is from the current loop index (wrapping around the full count of particles)
			float loop_index 	= unit*(float)i;
			float opacity 		= 1.0f-Mathf.Repeat(time_index-Mathf.Abs(loop_index-unit)-unit, 1.0f);
			
			Color new_color		= _particles[i].startColor;
			new_color.a			= opacity;

			_particles[i].startColor	= new_color;
		}
	}
}