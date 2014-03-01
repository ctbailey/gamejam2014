using UnityEngine;
using System.Collections;

public class InputMover : MonoBehaviour {
	
	public CharacterController controller;
	public float gravity = 0.1f;
	public float jumpSpeed = 1.0f;
	public float horizontalSpeed = 50.0f;
	private float jumpAcceleration = 0f;
	
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
	}
	Vector3 Update_MoveHorizontal()
	{
		float horizontal = Input.GetAxis("Horizontal") * horizontalSpeed;
        horizontal *= Time.deltaTime;
        return new Vector3(horizontal, 0, 0);
	}
	Vector3 Update_MoveVertical()
	{
		jumpAcceleration -= gravity;
		Vector3 verticalMove = new Vector3(0, jumpAcceleration, 0);
		Debug.Log(verticalMove);
		return verticalMove;
	}
	void Jump()
	{	
		Debug.Log("Jumping");
		jumpAcceleration = jumpSpeed;
	}
}
