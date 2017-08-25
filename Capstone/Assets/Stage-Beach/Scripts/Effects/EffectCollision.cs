using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EffectCollision : MonoBehaviour 
{
	private const int PARTICLE_COUNT 	= 4;

	public ParticleSystem			particle_system;
	private static ParticleSystem[]	_particle_system = new ParticleSystem[PARTICLE_COUNT];

	private int	_index					= 0;


	void Awake () 
	{
		for(int i = 0; i < PARTICLE_COUNT; i++)
		{
			_particle_system[i]						= Instantiate(particle_system);
			_particle_system[i].transform.position	= Vector3.zero;
			_particle_system[i].transform.rotation	= Quaternion.identity;
			_particle_system[i].transform.parent	= gameObject.transform;
			_particle_system[i].playOnAwake			= false;
			_particle_system[i].Pause();
		}
	}


	void OnCollisionEnter(Collision collision) 
	{
		if(collision.contacts.Length > 0)
		{
			ContactPoint contact						= collision.contacts[0];
			Quaternion rotation							= Quaternion.FromToRotation(Vector3.up, contact.normal);
			
			_particle_system[_index].transform.position = contact.point;
			_particle_system[_index].transform.rotation = rotation;
			
			_particle_system[_index].Play();
			
			_index										= _index++ % PARTICLE_COUNT;
		}
	}
}
