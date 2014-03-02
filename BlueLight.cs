using UnityEngine;
using System.Collections;

public class BlueLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(Mathf.PingPong(Time.time / 6f + .5f, 1) - .5f, 0, 0);
	}
}
