using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlayerBehavior : MonoBehaviour {
	
	public CharacterController controller;
	protected Animator animator;
	public float gravity = 1;
	public float currentJumpSpeed = 15;
	public float horizontalSpeed = 20.0f;
	public PlayerFootstepsAudio footstepsAudio;
	public PlayerJumpAudio jumpAudio;
	public float footstepTimeInterval = 0.1f;
	private float startJumpSpeed = 0f;
	private float lastFootstepTime;
		float deadTimer = 0;
	float restartTimer = 0;
	public bool isDead = false;
	bool meshSwitch = false;
	
	public Mesh zombieMesh;
	
	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
	    footstepsAudio = GetComponentInChildren<PlayerFootstepsAudio>();
		jumpAudio = GetComponentInChildren<PlayerJumpAudio>();
		lastFootstepTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDead)
		{
			Vector3 horizontalTranslation = Update_MoveHorizontal();
	        Vector3 verticalTranslation = Update_MoveVertical();
			if(Input.GetButtonDown("Jump")
				&& controller.isGrounded)
			{
				Jump();
			}
			else if(animator.GetBool("Jump"))
			{
				SetJump(false);
			}
			controller.Move(horizontalTranslation + verticalTranslation);
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		}
		else
		{
			if(Time.timeSinceLevelLoad - deadTimer > 1.5f && !meshSwitch)
			{
				meshSwitch = true;
				transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh = zombieMesh;
				SetHide(false);
				SetRun(false);
				restartTimer = Time.timeSinceLevelLoad;
			}
			if(meshSwitch && Time.timeSinceLevelLoad - restartTimer > 2f)
			{
				Application.LoadLevel(0);
			}
		}
	}
	Vector3 Update_MoveHorizontal()
	{
		float horizontal;// = Input.GetAxis("Horizontal") * horizontalSpeed;//(transform.position.y > -5) ? Input.GetAxis("Horizontal") * horizontalSpeed : 0;
		if(transform.position.y > -5)
		{
			horizontal = Input.GetAxis("Horizontal") * horizontalSpeed;
		}
		else
		{
			horizontal = 0;
			Application.LoadLevel(0);
		}
        horizontal *= Time.deltaTime;
		transform.rotation = Quaternion.Euler(0, 90 * -Input.GetAxis("Horizontal") + 180, 0);
		if(Mathf.Abs(horizontal) > 0)
		{
			TryPlayFootstep();
		}
		
		if(Input.GetAxis("Horizontal") > .1 || Input.GetAxis("Horizontal") < -.1 )
		{
			SetRun(true);
		}
		else
		{
			SetRun(false);
		}
		
        return new Vector3(horizontal, 0, 0);
	}
	Vector3 Update_MoveVertical()
	{
		if(!controller.isGrounded)
		{
			startJumpSpeed -= gravity;
		}
		Vector3 verticalMove = new Vector3(0, startJumpSpeed * Time.deltaTime, 0);
		return verticalMove;
	}
	void Jump()
	{
		jumpAudio.PlayJump();	
		startJumpSpeed = currentJumpSpeed;
		SetJump(true);
	}
	void OnTriggerEnter(Collider c)
	{
		EnemyBehavior enemy = Util.GetEnemyBehavior(c.gameObject);
		if(enemy != null)
		{
			if(!enemy.isDead)
			{
				OnDeath();	
				if(!enemy.isDead && !isDead)
				{
					OnDeath();
					enemy.SetAttack(true);
				}
			}
		}
	}
	public void KilledEnemy(EnemyBehavior enemy)
	{
		Debug.Log("Killed enemy!");
		Jump();	
	}
	void OnDeath()
	{
		Debug.Log ("Player died!");	
		SetHide(true);
		isDead = true;
		deadTimer = Time.timeSinceLevelLoad;
	}
	
	protected virtual void SetRun(bool run)
	{
		animator.SetBool("Run", run);
	}
	
	public virtual void SetHide(bool hide)
	{
		animator.SetBool("Hide", hide);
	}
	
	protected virtual void SetJump(bool jump)
	{
		animator.SetBool("Jump", jump);
	}
	void TryPlayFootstep()
	{
		float timeSinceLastFootstep = Mathf.Abs(Time.time - lastFootstepTime);
		if(timeSinceLastFootstep > footstepTimeInterval
			&& controller.isGrounded)
		{
			footstepsAudio.PlayFootstep();
			lastFootstepTime = Time.time;
		}
	}
}
