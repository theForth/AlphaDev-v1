using UnityEngine;
using System.Collections;
/// <summary>
/// Draw gizmos. Attach this to any gameObject you want to draw a wired sphere or wired cube in your scene view.  You can change the
/// color of it too.  This script does not need to be enabled to do this so it's recommended to disable it since it doesn't need to be
/// used in game.
/// </summary>
public class DrawGizmos : MonoBehaviour
{
	public bool drawSphere = true;
	public bool drawCube = false;
	public float size = 2;
	public Color chosenColor = Color.white;
	
	void Start()
	{
		if(drawCube && drawSphere)
			drawCube = false;
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.color = chosenColor;
		if(drawCube)
			Gizmos.DrawWireCube(transform.position, new Vector3(size, size, size));
		if(drawSphere)
			Gizmos.DrawWireSphere(transform.position, size);
	}
}
