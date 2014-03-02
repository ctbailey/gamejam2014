using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public BoxCollider playerFeet;
	public bool isDead = false;
	protected Animator animator;
	protected CharacterController controller;
	
	// Use this for initialization
	protected virtual void Start () {
		animator = gameObject.GetComponent<Animator>();
		controller = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
	protected virtual void OnTriggerEnter(Collider c)
	{	
		if(c.Equals(playerFeet)
			&& !isDead)
		{
			OnDeath();
		}
	}
	protected virtual void OnDeath()
	{
		Debug.Log("Enemy died!");
		GetComponent<SphereCollider>().enabled = false;
		GetComponent<CharacterController>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;
		isDead = true;
		SetFall(true);
	}
	
	protected virtual void SetWalk(bool walk)
	{
		animator.SetBool("Walk", walk);
	}
	
	protected virtual void SetFall(bool fall)
	{
		controller.height = .25f;
		controller.center = new Vector3(0,-.325f,0);
		transform.position = new Vector3(transform.position.x, transform.position.y, .5f);
		animator.SetBool("Fall", fall);
	}
	
	public virtual void SetHide(bool hide)
	{
		animator.SetBool("Hide", hide);
	}
}
