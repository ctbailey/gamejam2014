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
		}
	}
	
	void SetDiscoLights(bool b)
	{
		GameObject[] discoLights = GameObject.FindGameObjectsWithTag("DiscoLights");
		
		for(int i = 0; i < discoLights.Length; i++)
		{
			discoLights[i].SetActive(b);
		}
		
//		for(int i = 0; i < discoLights.transform.childCount; i++)
//		{
//			for(int j = 0; j < discoLights.transform.GetChild(i).childCount; j++)
//			{
//				Transform trans = discoLights.transform.GetChild(i).GetChild(j);
//				if(trans.name == "Point light")
//				{
//					//trans.gameObject.GetComponent<Light>().intensity = b;
//					//Debug.Log(trans.gameObject.GetComponent<Light>().intensity);
//					Destroy(trans.gameObject);
//				}
//			}
//		}
	}
}
