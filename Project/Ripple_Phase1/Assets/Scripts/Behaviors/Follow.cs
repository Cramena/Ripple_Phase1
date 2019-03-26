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
	[Range(0, 1)] public float followSpeed = 1;
	public Vector3 offset;
	Transform self;

    // Start is called before the first frame update
    void Start()
    {
		self = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		self.position = Vector3.Lerp(self.position, target.position + offset, followSpeed);
    }
}
