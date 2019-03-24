using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoveState
{
	Idle,
	Moving,
	Deccelerating
}

public class ButterflyBehavior : MonoBehaviour
{
	//-----PUBLIC-----
	//PARAMETERS
	[Header("Parameters")]
	public float maxSpeed;
	public AnimationCurve accelerationCurve;
	public float accelerationRate = 1;
	public float deccelerationRate = 1;

	//-----PRIVATE-----
	//SELF REFERENCES
	Rigidbody body;
	Transform self;
	//PARAMETERS
	MoveState moveState = MoveState.Idle;
	float speedTimer;

	//DEBUG TEMP
	float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
		body = GetComponent<Rigidbody>();
		self = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (moveState != MoveState.Idle)
		{
			MoveForward();
		}
	}

	private void Update()
	{
		switch (moveState)
		{
			case MoveState.Deccelerating:
				if (speedTimer > 0)
				{
					speedTimer -= Time.deltaTime * deccelerationRate;
				}
				else
				{
					speedTimer = 0;
					StopMoving();
				}
				break;
			case MoveState.Moving:
				if (speedTimer < 1)
				{
					speedTimer += Time.deltaTime * accelerationRate;
				}
				else
				{
					speedTimer = 1;
				}
				break;
			default:
				break;
		}

		currentSpeed = body.velocity.magnitude;
	}

	#region Move state switching
	public void StartMovingForward()
	{
		moveState = MoveState.Moving;
	}

	public void StartDeccelerating()
	{
		moveState = MoveState.Deccelerating;
	}

	void StopMoving()
	{
		moveState = MoveState.Idle;
		body.velocity = Vector3.zero;
	}
	#endregion

	#region Speed modifying
	void MoveForward()
	{
		body.velocity = self.forward * accelerationCurve.Evaluate(speedTimer) * maxSpeed;
	}
	#endregion
}
