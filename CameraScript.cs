using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Texture background;
	public Transform player;
	bool show;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		show = true;
	}
	
	// Update is called once per frame
	void Update () {
		float y = transform.position.y;
		if(player.position.y > -3)
		{
			y = player.position.y + 3f;
		}
		
		if(player.position.x < -175)
		{
			transform.position = new Vector3(-175, y, -10);
		}
		else if(player.position.x > 175)
		{
			transform.position = new Vector3(175, y, -10);
		}
		else
		{
			transform.position = new Vector3(player.position.x, y, -10);
		}
	}
	
	void OnGUI()
	{
		
		if(show)
		{
			GUI.Box(new Rect(20,20,Screen.width - 40,Screen.height - 40), background);
			GUI.Box(new Rect(20,20,Screen.width - 40,Screen.height - 40), "Zombies in the Disco! Navigate to the end to hit the button and end this nightmare. \nDirections:\nA = Left\nD = Right\nSpace Bar = Jump\nDon't get dead as disco. ");
			if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50), "Do it to it!"))
			{
				show = false;
				Time.timeScale = 1;
			}
		}
	}
}
