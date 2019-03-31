using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesRotate : MonoBehaviour
{

	//-----PUBLIC-----
	//REFERENCES
	[Header("References:")]
	public Transform target;
	[Space()]
	[Header("Parameters:")]
	[Range(0.01f, 1)] public float turnSpeed = 0.2f;
	public bool randomTurnSpeed;
	[Range(0.01f, 1)] public float minTurnSpeed;
	[Range(0.01f, 1)] public float maxTurnSpeed;

	//-----PRIVATE-----
	//SELF REFERENCES
	Transform self;

    // Start is called before the first frame update
    void Start()
    {
		self = transform;
		if(randomTurnSpeed)
		{
			turnSpeed = Random.Range(minTurnSpeed, maxTurnSpeed);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		if (target == null) return;
		self.rotation = Quaternion.Slerp(self.rotation, target.rotation, turnSpeed);
	}
}
