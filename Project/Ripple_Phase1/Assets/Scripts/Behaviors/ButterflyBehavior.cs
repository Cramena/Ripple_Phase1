﻿using System.Collections;
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
	public AnimationCurve accelerationCurve;
	public AnimationCurve deccelerationCurve;
	public float acceleration;
	public float maxSpeed;
	public float accelerationSpeed = 1;
	public float deccelerationSpeed = 1;
	public float turnSpeed = 1;

	//-----PUBLIC NON SERIALIZED-----

	//-----PRIVATE-----
	//REFERENCES
	ButterflyController controller;
	//SELF REFERENCES
	Rigidbody body;
	Transform self;
	//PARAMETERS
	MoveState moveState;
	Quaternion turnRotation;
	Vector3 startDirection;
	Vector3 direction;
	Vector3 movement;
	Vector3 lastDirection;
	float currentSpeed;
	float accelerationTimer;
	float deccelerationTimer;
	float maxDeccelerationSpeed;

	//-----DEBUG-----
	float debugSpeed;

	private void Start()
	{
		body = GetComponent<Rigidbody>();
		self = transform;
		controller = GetComponent<ButterflyController>();
	}

	private void Update()
	{
		GetInput();
		CheckState();
	}

	private void FixedUpdate()
	{
		ManageState();
	}

	void ManageState()
	{
		switch (moveState)
		{
			case MoveState.Moving:
				if (direction.magnitude != 0)
				{
					lastDirection = direction;
				}
				Rotate();
				Move();
				break;
			case MoveState.Deccelerating:
				Deccelerate();
				break;
			default:
				break;
		}
	}

	void GetInput()
	{
		direction = controller.cameraRelativeInput;
	}

	void CheckState()
	{
		if (body.velocity.magnitude == 0 && direction.magnitude == 0 && moveState != MoveState.Idle)
		{
			StartIdle();
		}
		else if (body.velocity.magnitude != 0 && direction.magnitude == 0 && moveState != MoveState.Deccelerating)
		{
			StartDeccelerating();
		}
		else if (direction.magnitude != 0 && moveState != MoveState.Moving)
		{
			StartMoving();
		}
		debugSpeed = body.velocity.magnitude;
	}

	void StartMoving()
	{
		moveState = MoveState.Moving;
		startDirection = controller.cameraRelativeInput;
		accelerationTimer = 0;
	}

	void StartDeccelerating()
	{
		moveState = MoveState.Deccelerating;
		deccelerationTimer = 0;
		maxDeccelerationSpeed = currentSpeed;
	}

	void StartIdle()
	{
		moveState = MoveState.Idle;
		body.velocity = Vector3.zero;
	}

	void Rotate()
	{
		turnRotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.z) * 180 / Mathf.PI, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, turnRotation, turnSpeed);
	}

	void Deccelerate()
	{
		deccelerationTimer += Time.deltaTime * deccelerationSpeed;
		deccelerationTimer = Mathf.Clamp(deccelerationTimer, 0, 1);
		movement = lastDirection * deccelerationCurve.Evaluate(deccelerationTimer) * maxDeccelerationSpeed;
		body.velocity = movement;
	}

	void Move()
	{
		accelerationTimer += Time.deltaTime * accelerationSpeed;
		accelerationTimer = Mathf.Clamp(accelerationTimer, 0, 1);
		currentSpeed = accelerationCurve.Evaluate(accelerationTimer) * maxSpeed;
		movement = direction * currentSpeed;
		body.velocity = Vector3.Lerp(body.velocity, movement, acceleration);
	}
}
