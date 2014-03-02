using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	
	public CharacterController controller;
	
	public float height = 1;
	public float gravity = 1;
	public float currentJumpSpeed = 15;
	public float horizontalSpeed = 20.0f;
	private float startJumpSpeed = 0f;
	
	// Use this for initialization
	void Start () {
	    
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
		{Debug.Log("Down"); Debug.Log(transform.position.y);
			horizontal = 0;
		}
        horizontal *= Time.deltaTime;
		transform.rotation = Quaternion.Euler(0, 90 * -Input.GetAxis("Horizontal") + 180, 0);
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
		startJumpSpeed = currentJumpSpeed;
	}
	void OnTriggerEnter(Collider c)
	{
		//Debug.Log("Player triggered!");	
	}
}
