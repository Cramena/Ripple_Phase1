using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum RotationDirection
{
	None,
	Left,
	Right
}

public enum MoveDirection
{
	Forward,
	Backward,
	Left,
	Right,
}

public class ButterflyController : MonoBehaviour
{
	//-----PUBLIC-----
	[Header("References")]
	public CinemachineFreeLook freelookCam;
	public CinemachineVirtualCamera lockOnCam;
	[Space()]
	//PARAMETERS
	[Header("Parameters:")]
	public float deadzone = 0.2f;

	//-----PUBLIC NON SERIALIZED
	[System.NonSerialized] public Vector3 cameraRelativeInput;

	//-----PRIVATE-----
	//REFERENCES
	Transform cameraTransform;
	//SELF REFERENCES
	ButterflyBehavior butterfly;
	//PARAMETERS
	Vector3 input;
	bool lockOn;

    // Start is called before the first frame update
    void Start()
    {
		butterfly = GetComponent<ButterflyBehavior>();
		cameraTransform = GetComponentInChildren<CinemachineFreeLook>().transform;
	}

	// Update is called once per frame
	void Update()
    {
		GetInput();
	}

	void GetInput()
	{
		input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		input = input.normalized * (Mathf.Clamp(input.magnitude - deadzone, 0, 1) / (1 - deadzone));
		cameraRelativeInput = cameraTransform.TransformDirection(input);

		if (Input.GetButtonDown("Fire1"))
		{
			if (lockOn)
			{
				lockOn = false;
				cameraTransform = freelookCam.transform;
				freelookCam.enabled = true;
				lockOnCam.enabled = false;
				
				butterfly.InitializeCamera();
			}
			else
			{
				cameraTransform = lockOnCam.transform;
				lockOn = true;
				freelookCam.enabled = false;
				lockOnCam.enabled = true;
				butterfly.InitializeCamera();
			}
		}
		#region Old code
		/*//Move forward
		if (Input.GetKeyDown(KeyCode.Z))
		{
			butterfly.StartMovingForward();
		}
		else if (Input.GetKeyUp(KeyCode.Z))
		{
			butterfly.StartDeccelerating();
		}

		//Move backward
		if (Input.GetKeyDown(KeyCode.S))
		{
			butterfly.StartMovingForward();
		}
		else if (Input.GetKeyUp(KeyCode.S))
		{
			butterfly.StartDeccelerating();
		}

		//Strafe left
		if (Input.GetKey(KeyCode.Q))
		{
			butterfly.Strafe(MoveDirection.Left);
		}

		if (Input.GetKey(KeyCode.D))
		{
			butterfly.Strafe(MoveDirection.Right);
		}
		//Turn left
		//if (Input.GetKeyDown(KeyCode.Q))
		//{
		//	butterfly.StartRotating(RotationDirection.Left);
		//}
		//else if (Input.GetKeyUp(KeyCode.Q))
		//{
		//	CheckOtherDirection(RotationDirection.Left);
		//}

		////Turn right
		//if (Input.GetKeyDown(KeyCode.D))
		//{
		//	butterfly.StartRotating(RotationDirection.Right);
		//}
		//else if (Input.GetKeyUp(KeyCode.D))
		//{
		//	CheckOtherDirection(RotationDirection.Right);
		//}
		*/
		#endregion
	}

	/// <summary>
	/// If the other button is pressed, rotates in the opposite direction. Otherwise, stops rotating.
	/// </summary>
	/// <param name="_direction"></param>
	//void CheckOtherDirection(RotationDirection _direction)
	//{
	//	if (_direction == RotationDirection.Left && Input.GetKey(KeyCode.D))
	//	{
	//		butterfly.StartRotating(RotationDirection.Right);
	//	}
	//	else if (_direction == RotationDirection.Right && Input.GetKey(KeyCode.Q))
	//	{
	//		butterfly.StartRotating(RotationDirection.Left);
	//	}
	//	else
	//	{
	//		butterfly.StartRotationEnd();
	//	}
	//}
}
