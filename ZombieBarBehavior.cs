using UnityEngine;
using System.Collections;

public class ZombieBarBehavior : EnemyBehavior {
	
	float jumpTime = 0;
	// Use this for initialization
	
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		float dist = transform.position.x - playerFeet.transform.position.x;
		if(dist < 2f && dist > 1 && !animator.GetBool("Attack") && !hide)
		{
			animator.SetBool("Attack", true);
			jumpTime = Time.timeSinceLevelLoad;
		}
		else if((dist >= 2f || dist <= 1) && animator.GetBool("Attack"))
		{
			animator.SetBool("Attack", false);
			jumpTime = Time.timeSinceLevelLoad;
		}
		
		if(animator.GetBool("Attack"))
		{
			float lerp = Mathf.Lerp(1.1f, 2, (Time.timeSinceLevelLoad - jumpTime) * 2.5f);
			transform.position = new Vector3(transform.position.x, lerp, transform.position.z);
		}
		else
		{
			float lerp = Mathf.Lerp(2, 1.1f, (Time.timeSinceLevelLoad - jumpTime) * 1.5f);
			transform.position = new Vector3(transform.position.x, lerp, transform.position.z);
		}
	}
	
	protected override void OnTriggerEnter(Collider c)
	{
		
	}
}
