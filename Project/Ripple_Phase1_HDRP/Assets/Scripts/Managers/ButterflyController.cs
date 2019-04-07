using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

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
	//-----STATIC-----
	public static ButterflyController instance;

	//-----PUBLIC-----
	[Header("References")]
	public CinemachineFreeLook freelookCam;
	public CinemachineVirtualCamera lockOnCam;
	[Space()]
	//PARAMETERS
	[Header("Parameters:")]
	public float deadzone = 0.2f;
	//EVENTS
	public UnityEvent ActivateEvent;
	public UnityEvent DeactivateEvent;


	//-----PUBLIC NON SERIALIZED
	[System.NonSerialized] public Vector3 cameraRelativeInput;

	//-----PRIVATE-----
	//REFERENCES
	Transform cameraTransform;
	//SELF REFERENCES
	ButterflyBehavior butterfly;
	PlayerData data;
	//PARAMETERS
	Vector3 input;
	bool lockOn;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
    {
		data = GetComponent<PlayerData>();
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
			TogglePollen();
		}
	}

	void TogglePollen()
	{
		if (data.state == PlayerState.Default)
		{
			data.state = PlayerState.HasPollen;
			if (ActivateEvent != null) ActivateEvent.Invoke();
		}
		else
		{
			data.state = PlayerState.Default;
			if (DeactivateEvent != null) DeactivateEvent.Invoke();
		}
	}

	void ToggleCamera()
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
}
