using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoveState
{
	Idle,
	Moving,
	Deccelerating
}

enum RotateState
{
	Idle,
	Moving,
	Deccelerating
}

public class ButterflyBehavior : MonoBehaviour
{
	class Movement
	{
		MoveState movementState = MoveState.Idle;
		public AnimationCurve movementAccelerationCurve;
		public AnimationCurve movementDeccelerationCurve;
		public float movementAccelerationRate = 1;
		public float movementDeccelerationRate = 1;
	}
	//-----PUBLIC-----
	//PARAMETERS
	[Header("PARAMETERS")]
	[Space()]
	[Header("Movement: ")]
	public float accelerationMaxSpeed;
	public AnimationCurve accelerationCurve;
	public AnimationCurve deccelerationCurve;
	public float accelerationRate = 1;
	public float deccelerationRate = 1;
	[Space()]
	[Header("Rotation: ")]
	public float rotationMaxSpeed;
	public AnimationCurve rotationAccelerationCurve;
	public float rotationAccelerationRate = 1;
	public float rotationDeccelerationRate = 1;

	//-----PRIVATE-----
	//SELF REFERENCES
	Rigidbody body;
	Transform self;
	//PARAMETERS
	MoveState moveState = MoveState.Idle;
	RotateState rotateState = RotateState.Idle;
	RotationDirection currentDirection = RotationDirection.None;

	//Movement parameters
	Vector3 movement;
	float speedTimer;
	float accelerationIndex;
	float deccelerationMaxSpeed;

	//Rotation parameters
	float rotationSpeedTimer;
	int directionFactor;

	//-----DEBUG-----
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
		if (rotateState != RotateState.Idle)
		{
			Rotate();
		}
		Move();
	}

	private void Update()
	{

		ManageMoveState();
		ManageRotationState();
		currentSpeed = body.velocity.magnitude;
	}

	#region Transform states management
	void ManageMoveState()
	{
		switch (moveState)
		{
			case MoveState.Deccelerating:
				if (accelerationIndex < 1)
				{
					accelerationIndex += Time.deltaTime * deccelerationRate;
				}
				else
				{
					accelerationIndex = 1;
					StopMoving();
				}
				break;
			case MoveState.Moving:
				if (accelerationIndex < 1)
				{
					accelerationIndex += Time.deltaTime * accelerationRate;
				}
				else
				{
					accelerationIndex = 1;
				}
				break;
			default:
				break;
		}


		//switch (moveState)
		//{
		//	case MoveState.Deccelerating:
		//		if (speedTimer > 0)
		//		{
		//			speedTimer -= Time.deltaTime * deccelerationRate;
		//		}
		//		else
		//		{
		//			speedTimer = 0;
		//			StopMoving();
		//		}
		//		break;
		//	case MoveState.Moving:
		//		if (speedTimer < 1)
		//		{
		//			speedTimer += Time.deltaTime * accelerationRate;
		//		}
		//		else
		//		{
		//			speedTimer = 1;
		//		}
		//		break;
		//	default:
		//		break;
		//}
	}

	void ManageRotationState()
	{
		switch (rotateState)
		{
			case RotateState.Deccelerating:
				if (Mathf.Abs(rotationSpeedTimer) > 0.1f)
				{
					rotationSpeedTimer -= Time.deltaTime * Mathf.Sign(rotationSpeedTimer) * rotationDeccelerationRate;
				}
				else
				{
					rotationSpeedTimer = 0;
					StopRotating();
				}
				break;
			case RotateState.Moving:
				if ((rotationSpeedTimer < 1 && currentDirection == RotationDirection.Right) ||
					(rotationSpeedTimer > -1 && currentDirection == RotationDirection.Left))
				{
					rotationSpeedTimer += Time.deltaTime * directionFactor * rotationAccelerationRate;
				}
				break;
			default:
				break;
		}
		rotationSpeedTimer = Mathf.Clamp(rotationSpeedTimer, -1, 1);
	}
	#endregion

	#region Move state switching
	public void StartMovingForward()
	{
		if (moveState == MoveState.Deccelerating)
		{
			accelerationIndex = GetCorrespondingIndex(accelerationIndex, deccelerationCurve, accelerationCurve);
		}
		else
		{
			accelerationIndex = 0;
		}
		moveState = MoveState.Moving;

	}

	public void StartDeccelerating()
	{
		deccelerationMaxSpeed = accelerationCurve.Evaluate(accelerationIndex) * accelerationMaxSpeed;
		accelerationIndex = 0;
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
		if (moveState == MoveState.Moving)
		{
			movement += self.forward * accelerationCurve.Evaluate(accelerationIndex) * accelerationMaxSpeed;
		}
		else if (moveState == MoveState.Deccelerating)
		{
			movement += self.forward * deccelerationCurve.Evaluate(accelerationIndex) * deccelerationMaxSpeed;
		}
	}

	#endregion

	#region Rotation state switching
	public void StartRotating(RotationDirection _direction)
	{
		if (_direction == currentDirection) return;

		currentDirection = _direction;
		rotateState = RotateState.Moving;

		if (currentDirection == RotationDirection.Left)
		{
			directionFactor = -1;
		}
		else
		{
			directionFactor = 1;
		}
	}

	public void StartRotationEnd()
	{
		rotateState = RotateState.Deccelerating;
	}

	public void StopRotating()
	{
		currentDirection = RotationDirection.None;
		rotateState = RotateState.Idle;
	}
	#endregion

	#region Rotation method
	void Rotate()
	{
		self.rotation *= Quaternion.AngleAxis(rotationAccelerationCurve.Evaluate(rotationSpeedTimer) * rotationMaxSpeed, Vector3.up);
	}
	#endregion


	float GetCorrespondingIndex(float _pointer, AnimationCurve _firstCurve, AnimationCurve _secondCurve)
	{
		float value = _firstCurve.Evaluate(_pointer);
		float index = 0;
		float comparedValue = _secondCurve.Evaluate(index);
		while (comparedValue < value)
		{
			index += 0.05f;
			comparedValue = _secondCurve.Evaluate(index);
		}
		return index;
	}

	void Move()
	{
		body.velocity = movement;
		movement = Vector3.zero;
	}

	public void Strafe(StrafeDirection _direction)
	{
		if (_direction == StrafeDirection.Left)
		{
			movement += -self.right * accelerationMaxSpeed;
		}
		if (_direction == StrafeDirection.Right)
		{
			movement += self.right * accelerationMaxSpeed;
		}
	}




}
