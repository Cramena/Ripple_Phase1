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
	/*[Range(0, 1)]*/ public float followSpeed = 1;
	public Vector3 offset;
	public bool randomSpeed;
	/*[Range(0, 0.99f)]*/ public float minSpeed;
	/*[Range(0.01f, 1f)]*/  public float maxSpeed;
	public float distanceBeforeFollow;
	public bool randomDistanceBeforeFollow;
	public float minDistanceBeforeFollow;
	public float maxDistanceBeforeFollow;

	//-----PRIVATE-----
	//SELF REFERENCES
	Transform self;
	Rigidbody body;
	Rigidbody targetBody;
	//PARAMETERS
	Vector3 targetPos;
	float currentSpeed;

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
		if (randomDistanceBeforeFollow)
		{
			distanceBeforeFollow = Random.Range(minDistanceBeforeFollow, maxDistanceBeforeFollow);
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (Vector3.Distance(self.position, target.position + offset) > distanceBeforeFollow)
		{
			currentSpeed += Time.deltaTime;
		}
		else
		{
			currentSpeed -= Time.deltaTime;
		}
		currentSpeed = Mathf.Clamp(currentSpeed, 0, followSpeed);
		//targetPos = Vector3.Lerp(self.position, target.position + offset, currentSpeed);
		//self.position = Vector3.Lerp(self.position, targetPos, 0.1f);

		//self.position = Vector3.Lerp(self.position, target.position + offset, followSpeed);
	}

	private void FixedUpdate()
	{
		//self.position = Vector3.MoveTowards(self.position, target.position + offset, currentSpeed);
		//body.velocity = targetBody.velocity;
		body.velocity = Vector3.Lerp(body.velocity, targetBody.velocity, followSpeed);
	}
}
