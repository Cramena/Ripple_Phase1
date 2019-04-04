using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesBody : MonoBehaviour
{

	//-----PUBLIC-----
	[Header("Parameters:")]
	public float size;
	public bool randomSize;
	public float minSize;
	public float maxSize;

	//-----PRIVATE-----
	//SELF REFERENCES
	Transform self;

    // Start is called before the first frame update
    void Start()
    {
		self = transform;
		if (randomSize)
		{
			size = Random.Range(minSize, maxSize);
		}
		self.localScale = new Vector3(size, size, size);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
