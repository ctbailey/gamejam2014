using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlayerBehavior : MonoBehaviour {
	
	public CharacterController controller;
	
	public float gravity = 1;
	public float currentJumpSpeed = 15;
	public float horizontalSpeed = 20.0f;
	public PlayerFootstepsAudio footstepsAudio;
	public PlayerJumpAudio jumpAudio;
	public float footstepTimeInterval = 0.1f;
	private float startJumpSpeed = 0f;
	private float lastFootstepTime;
	
	// Use this for initialization
	void Start () {
	    footstepsAudio = GetComponentInChildren<PlayerFootstepsAudio>();
		jumpAudio = GetComponentInChildren<PlayerJumpAudio>();
		lastFootstepTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 horizontalTranslation = Update_MoveHorizontal();
        Vector3 verticalTranslation = Update_MoveVertical();
		if(Input.GetButtonDown("Jump")
			&& controller.isGrounded)
		{
			Jump();
		}
		controller.Move(horizontalTranslation + verticalTranslation);
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
		}
        horizontal *= Time.deltaTime;
		transform.rotation = Quaternion.Euler(0, 90 * -Input.GetAxis("Horizontal") + 180, 0);
		if(Mathf.Abs(horizontal) > 0)
		{
			TryPlayFootstep();
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
	}
	void OnTriggerEnter(Collider c)
	{
		EnemyBehavior enemy = Util.GetEnemyBehavior(c.gameObject);
		if(enemy != null)
		{
			if(!enemy.isDead)
			{
				OnDeath();	
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
