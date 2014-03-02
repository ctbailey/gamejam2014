using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class EnemyBehavior : MonoBehaviour {
	public BoxCollider playerFeet;
	public bool isDead = false;
	protected Animator animator;
	protected CharacterController controller;
	protected RoundRobinAudioCollection audioSources;
	float lastGroanTime = 0;
	public float groanDelay = 2;
	float attackTimer = 0;
	
	// Use this for initialization
	protected virtual void Start () {
		animator = gameObject.GetComponent<Animator>();
		controller = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		float timeSinceLastGroan = Mathf.Abs(Time.time - lastGroanTime);
		if(timeSinceLastGroan > groanDelay
			&& !isDead)
		{
			GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
			lastGroanTime = Time.time;
		}
		if(animator.GetBool("Attack") && Time.timeSinceLevelLoad - attackTimer > 1.5f)
		{
			SetAttack(false);
		}
	}
	protected virtual void OnTriggerEnter(Collider c)
	{	
		if(c.Equals(playerFeet)
			&& !isDead && !c.GetComponent<PlayerBehavior>().isDead)
		{
			OnDeath();
		}
	}
	protected virtual void OnDeath()
	{
		playerFeet.gameObject.GetComponent<PlayerBehavior>().KilledEnemy(this);
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
	
	public virtual void SetAttack(bool attack)
	{
		animator.SetBool("Attack", attack);
		attackTimer = Time.timeSinceLevelLoad;
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
