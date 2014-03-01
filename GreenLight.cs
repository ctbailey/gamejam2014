using UnityEngine;
using System.Collections;

public class GreenLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(0, 0, Mathf.PingPong(Time.time  / 4f, 1) - .5f);
	}
}
