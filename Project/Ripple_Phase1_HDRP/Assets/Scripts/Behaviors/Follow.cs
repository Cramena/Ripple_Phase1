using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
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

	[Header("Acceleration")]
	public float acceleration;
	public bool randomAcceleration;
	public float minAcceleration;
	public float maxAcceleration;

	[Header("Distance before follow")]
	public float distanceBeforeFollow;
	public bool randomDistanceBeforeFollow;
	public float minDistanceBeforeFollow;
	public float maxDistanceBeforeFollow;

	[Space()]
	public float maxDistance;
	public bool randomDistance;
	public float minRandomDistance;
	public float maxRandomDistance;

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
		if (randomAcceleration)
		{
			acceleration = Random.Range(minAcceleration, maxAcceleration);
		}
		if (randomDistanceBeforeFollow)
		{
			distanceBeforeFollow = Random.Range(minDistanceBeforeFollow, maxDistanceBeforeFollow);
		}
		if (randomDistance)
		{
			maxDistance = Random.Range(minRandomDistance, maxRandomDistance);
		}
	}

    // Update is called once per frame
    void Update()
    {
		//if (Vector3.Distance(self.position, target.position + offset) > distanceBeforeFollow)
		//{
		//	currentSpeed += Time.deltaTime * acceleration;
		//}
		//else
		//{
		//	currentSpeed -= Time.deltaTime * acceleration;
		//}
		//currentSpeed = Mathf.Clamp(currentSpeed, 0, followSpeed);


		//targetPos = Vector3.Lerp(self.position, target.position + offset, currentSpeed);
		//self.position = Vector3.Lerp(self.position, targetPos, 0.1f);

		//self.position = Vector3.Lerp(self.position, target.position + offset, followSpeed);


		DirectionManagement();
		DelayManagement();
		SpeedManagement();
	}

	void DirectionManagement()
	{
		direction = (target.position - self.position).normalized;
	}

	void DelayManagement()
	{
		delay = 1 - (maxDistance - Mathf.Clamp(Vector3.Distance(self.position, target.position), 0, maxDistance))/ maxDistance;
	}

	void SpeedManagement()
	{
		speed = followSpeed * delay;
	}
	void Move()
	{
		body.velocity = speed * direction;
	}

	private void FixedUpdate()
	{
		//self.position = Vector3.MoveTowards(self.position, target.position + offset, currentSpeed);
		//body.velocity = targetBody.velocity;
		//body.velocity = Vector3.Lerp(body.velocity, targetBody.velocity.magnitude * (self.position - target.position), currentSpeed * Time.fixedDeltaTime);


		//self.position = Vector3.SmoothDamp(self.position, target.position + offset, ref velocity, currentSpeed * Time.fixedDeltaTime);

		Move();
	}
}
