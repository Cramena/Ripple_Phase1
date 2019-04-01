using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAddForce : MonoBehaviour
{
	//-----PUBLIC-----
	//REFERENCES
	[Header("References:")]
	public Transform target;

	[Space()]

	//PARAMETERS
	[Header("Parameters:")]
	public Vector3 offset;

	[Header("Speed")]
	/*[Range(0, 1)]*/ public float followSpeed = 1;
	public bool randomSpeed;
	/*[Range(0, 0.99f)]*/ public float minSpeed;
	/*[Range(0.01f, 1f)]*/  public float maxSpeed;

	[Header("Distance before follow")]
	public float distanceBeforeFollow;
	public bool randomDistanceBeforeFollow;
	public float minDistanceBeforeFollow;
	public float maxDistanceBeforeFollow;

	[Header("Capped max speed")]
	public float maxVelocity;
	public bool randomMaxVelocity;
	public float minMaxVelocity;
	public float maxMaxVelocity;

	[Space()]
	public float drag;
	public bool randomDrag;
	public float minRandomDrag;
	public float maxRandomDrag;
	public float movingDrag = 0.5f;

	//-----PRIVATE-----
	//SELF REFERENCES
	Transform self;
	Rigidbody body;
	Rigidbody targetBody;
	//PARAMETERS
	Vector3 targetPos;
	Vector3 velocity;
	float currentSpeed;



	//new try
	float speed;
	Vector3 direction;
	float delay;
	float currentDrag;

	float distance;
	Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
		self = transform;
		body = GetComponent<Rigidbody>();
		targetBody = target.GetComponent<Rigidbody>();
		if (randomSpeed)
		{
			followSpeed = Random.Range(minSpeed, maxSpeed);
		}
		//if (randomAcceleration)
		//{
		//	acceleration = Random.Range(minAcceleration, maxAcceleration);
		//}
		if (randomDistanceBeforeFollow)
		{
			distanceBeforeFollow = Random.Range(minDistanceBeforeFollow, maxDistanceBeforeFollow);
		}
		if (randomDrag)
		{
			drag = Random.Range(minRandomDrag, maxRandomDrag);
		}
		if (randomMaxVelocity)
		{
			maxVelocity = Random.Range(minMaxVelocity, maxMaxVelocity);
		}
	}

    // Update is called once per frame
    void Update()
    {
		distance = Vector3.Distance(self.position, target.position + offset) - distanceBeforeFollow;
		DirectionManagement();
		AccelerationOrDecceleration();
	}

	void DirectionManagement()
	{
		direction = (target.position - self.position /*- (followSpeed * distance * direction)*/).normalized;
	}
	
	void AccelerationOrDecceleration()
	{
		if (Vector3.Distance(self.position, target.position + offset) > distanceBeforeFollow)
		{
			body.drag = movingDrag;
		}
		else
		{
			body.drag = drag;
		}
		speed = Mathf.Clamp(speed, 0, followSpeed);
	}

	void Move()
	{
		movement = followSpeed * distance * direction;
		movement = Vector3.ClampMagnitude(movement, maxVelocity);
		body.AddForce(movement, ForceMode.Acceleration);
	}

	private void FixedUpdate()
	{
		if (Vector3.Distance(self.position, target.position + offset) > distanceBeforeFollow)
		{
			Move();
		}
	}
}
