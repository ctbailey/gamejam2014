using UnityEngine;
using System.Collections;

public class InputMover : MonoBehaviour {
	
	public CharacterController controller;
	public float gravity = 0.1f;
	public float startingJumpSpeed = 1.0f;
	public float horizontalSpeed = 50.0f;
	private float currentJumpSpeed = 0f;
	private int count = 0;
	
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")
			&& controller.isGrounded)
		{
			Jump();
		}
		Vector3 verticalTranslation = Update_MoveVertical();
		Vector3 horizontalTranslation = Update_MoveHorizontal();
		controller.Move(horizontalTranslation + verticalTranslation);
	}
	Vector3 Update_MoveHorizontal()
	{
		float horizontal = Input.GetAxis("Horizontal") * horizontalSpeed;
        horizontal *= Time.deltaTime;
        return new Vector3(horizontal, 0, 0);
	}
	Vector3 Update_MoveVertical()
	{
		if(!controller.isGrounded)
		{
			currentJumpSpeed -= gravity;	
		}
		
		Vector3 verticalMove = new Vector3(0, currentJumpSpeed, 0) * Time.deltaTime;
		return verticalMove;
	}
	void Jump()
	{	
		Debug.Log("Jump " + count);
		count++;
		currentJumpSpeed = startingJumpSpeed;
	}
}
