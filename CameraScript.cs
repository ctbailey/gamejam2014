using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(player.position.x < -175)
		{
			transform.position = new Vector3(-175, 2.5f, -10);
		}
		else if(player.position.x > 175)
		{
			transform.position = new Vector3(175, 2.5f, -10);
		}
		else
		{
			transform.position = new Vector3(player.position.x, 2.5f, -10);
		}
	}
}
