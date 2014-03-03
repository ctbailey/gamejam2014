using UnityEngine;
using System.Collections;

public class ZombiePatrolBehavior : EnemyBehavior {
	
	private float horizontalStartLocation;
	public float patrolRadius = 10;
	public float moveSpeed = 2;
	public bool moveRight = true;
	public bool shouldRotate = false;
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		horizontalStartLocation = transform.position.x;
		SetWalk(true);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		Update_Patrol();
	}
	void Update_Patrol()
	{
		if(!isDead && !animator.GetBool("Attack") && !hide)
		{
			PatrolMove();
		}
	}
	void PatrolMove()
	{
		if(ReachedEdgeOfPatrolZone())
		{
			moveRight = !moveRight; // Switch directions
			transform.Rotate(Vector3.up, 180);
		}
		float moveDistance = moveSpeed;
		if(!moveRight)
		{
			moveDistance = -moveSpeed;
		}
		transform.position = new Vector3(moveDistance * Time.deltaTime + transform.position.x, transform.position.y, 0);
	}
	bool ReachedEdgeOfPatrolZone()
	{
		float distanceFromStart = Mathf.Abs(transform.position.x - horizontalStartLocation);
		return distanceFromStart >= patrolRadius;
	}
}
