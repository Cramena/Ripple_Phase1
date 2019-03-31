using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesNoise : MonoBehaviour
{
	//-----PUBLIC-----
	//PARAMETERS
	[Header("Parameters:")]
	public float maxDistance = 0.25f;
	public float noiseDuration = 0.25f;
	public float noiseDurationRange = 0.1f;
	public float noiseSpeed = 1;
	public float directionLerpSpeed = 0.1f;

	//-----PRIVATE-----
	//SELF REFERENCES
	Transform self;
	//PARAMETERS
	Vector3 lastDirection;
	Vector3 targetDirection;
	Vector3 noiseDirection;
	float noiseTimer;
	float currentNoiseFrequency;

	void Start()
	{
		self = transform;
	}

	void FixedUpdate()
	{
		if (noiseTimer > 0)
		{
			noiseTimer -= Time.fixedDeltaTime;
		}
		else
		{
			noiseTimer = noiseDuration + Random.Range(-noiseDurationRange, noiseDurationRange);
			currentNoiseFrequency = noiseTimer;
			NoiseMove();
		}

		if (self.localPosition.magnitude > maxDistance)
		{
			StopMovement();
		}
		UpdateDirection();
		Move();


	}

	void StopMovement()
	{
		lastDirection = noiseDirection;
		targetDirection = -self.localPosition.normalized;
	}

	void NoiseMove()
	{
		float xMove = Random.Range(-1f, 1f);
		float yMove = Random.Range(-1f, 1f);
		float zMove = Random.Range(-1f, 1f);

		lastDirection = noiseDirection;
		targetDirection = new Vector3(xMove, yMove, zMove);
	}

	void UpdateDirection()
	{
		noiseDirection = Vector3.Lerp(lastDirection, targetDirection, directionLerpSpeed * currentNoiseFrequency);
	}

	void Move()
	{
		self.position += noiseDirection.normalized * noiseSpeed * Time.deltaTime;
	}
}
