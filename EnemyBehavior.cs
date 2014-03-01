using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public BoxCollider playerFeet;
	public bool isDead = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider c)
	{
		if(c.Equals(playerFeet)
			&& !isDead)
		{
			OnDeath();
		}
	}
	void OnDeath()
	{
		Debug.Log("Enemy died!");
		isDead = true;
	}
}
