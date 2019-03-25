using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection
{
	None,
	Left,
	Right
}

public enum StrafeDirection
{
	None,
	Left,
	Right,
	Backward
}

public class ButterflyController : MonoBehaviour
{
	//-----PRIVATE-----
	//SELF REFERENCES
	ButterflyBehavior butterfly;

    // Start is called before the first frame update
    void Start()
    {
		butterfly = GetComponent<ButterflyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
		GetInput();
	}

	void GetInput()
	{
		//Move forward
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
			butterfly.Strafe(StrafeDirection.Left);
		}

		if (Input.GetKey(KeyCode.D))
		{
			butterfly.Strafe(StrafeDirection.Right);
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
	}

	/// <summary>
	/// If the other button is pressed, rotates in the opposite direction. Otherwise, stops rotating.
	/// </summary>
	/// <param name="_direction"></param>
	void CheckOtherDirection(RotationDirection _direction)
	{
		if (_direction == RotationDirection.Left && Input.GetKey(KeyCode.D))
		{
			butterfly.StartRotating(RotationDirection.Right);
		}
		else if (_direction == RotationDirection.Right && Input.GetKey(KeyCode.Q))
		{
			butterfly.StartRotating(RotationDirection.Left);
		}
		else
		{
			butterfly.StartRotationEnd();
		}
	}
}
