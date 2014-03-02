using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

	public GameObject directionalLight;
	//public GameObject discoLights;

	// Use this for initialization
	void Start () {
		directionalLight.SetActive(false);
		SetDiscoLights(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.name == "Player")
		{
			directionalLight.SetActive(true);
			SetDiscoLights(false);
			GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
			for(int i = 0; i < zombies.Length; i++)
			{
				EnemyBehavior behavior = (zombies[i].GetComponent<EnemyBehavior>() != null) ? zombies[i].GetComponent<EnemyBehavior>() : (zombies[i].GetComponent<ZombieBarBehavior>() != null) ? zombies[i].GetComponent<ZombieBarBehavior>() : null;
				if(behavior != null)
				{
					behavior.SetHide(true);
				}
			}
		}
	}
	
	void SetDiscoLights(bool b)
	{
		GameObject[] discoLights = GameObject.FindGameObjectsWithTag("DiscoLights");
		
		for(int i = 0; i < discoLights.Length; i++)
		{
			discoLights[i].SetActive(b);
		}
	}
}
