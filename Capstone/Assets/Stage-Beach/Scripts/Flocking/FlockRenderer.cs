using UnityEngine;
using System.Collections;

public class FlockRenderer : MonoBehaviour
{
	private static Matrix4x4 _matrix_local_to_world;
	
	private static bool draw_direction_lines = false;
	void OnPostRender()
	{
		draw_direction_lines = Flock.draw_direction_lines;
		Lines();
	}

	void Lines()
	{
		if(draw_direction_lines == true || Flock.draw_neighborhood_lines == true)
		{
			int lines = 0;

			Flock.material.SetPass(0);
		
			GL.PushMatrix();
			GL.MultMatrix(Matrix4x4.identity);
			GL.Begin(GL.LINES);
			

			if(Flock.draw_direction_lines)
			{
				for(int i = 0; i < Flock.vertex_line_directions.Length; i++) 
				{
					Vector4 color = Vector4.Lerp(Flock.color_far, Flock.color_near , 2.0f * Flock.velocity[i/2]/Flock.velocity_multiplier);
					color.w = 1.0f;
					GL.Color(color);
					GL.Vertex3(Flock.vertex_line_directions[i].x, Flock.vertex_line_directions[i].y, Flock.vertex_line_directions[i].z);
					lines++;
				}
			}
			
			if(Flock.draw_neighborhood_lines)
			{
				Vector3 neighbor = Vector3.zero;
				
				for(int i = 0; i < Flock.COUNT; i++) 
				{
					if(lines < Flock.MAX_LINES)
					{
						int local_neighbor_count = Mathf.Min(Flock.vertex_indices[i, 0], Flock.MAX_NEIGHBOR_LINES);
					
						float l		= (float)local_neighbor_count/(float)Flock.max_neighbors;
						Vector4 color = Vector4.Lerp(Flock.color_far, Flock.color_near , l);
						
						for(int j = 0; j < local_neighbor_count; j++)
						{
							neighbor	= Flock.prior_position[Flock.vertex_indices[i, j]];
							
							if(Vector3.Distance(Flock.prior_position[i], neighbor) < Flock.neighborhood_radius)
							{
								GL.Color(color);
								GL.Vertex3(Flock.prior_position[i].x, Flock.prior_position[i].y, Flock.prior_position[i].z);
								GL.Vertex3(neighbor.x, neighbor.y, neighbor.z);
								
								lines+=2;
							}
						}
					}
				}
			}
			
			GL.End();
			GL.PopMatrix();
		}

	} 

}
