using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesRotate : MonoBehaviour
{

	//-----PUBLIC-----
	//REFERENCES
	[Header("References:")]
	public Transform target;
    public Rigidbody targetBody;
	[Space()]
	[Header("Parameters:")]
	[Range(0.01f, 1)] public float turnSpeed = 0.2f;
	public Vector3 rotationOffset;
	public bool randomTurnSpeed;
	[Range(0.01f, 1)] public float minTurnSpeed;
	[Range(0.01f, 1)] public float maxTurnSpeed;

	//-----PRIVATE-----
	//SELF REFERENCES
	Transform self;
	Quaternion trueRotationOffset;
	Quaternion tempRotation;
    

    // Start is called before the first frame update
    void Start()
    {
		self = transform;
		trueRotationOffset = Quaternion.Euler(rotationOffset);
        //targetBody = target.GetComponent<Rigidbody>();
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
		//if (target == null) return;
		//self.rotation = Quaternion.LookRotation(target.position - self.position);
		//self.rotation = Quaternion.Slerp(self.rotation, target.rotation * trueRotationOffset, turnSpeed);
		tempRotation = Quaternion.LookRotation(targetBody.velocity.normalized);
        self.rotation = Quaternion.Slerp(self.rotation, tempRotation * trueRotationOffset, turnSpeed);
    }
}
