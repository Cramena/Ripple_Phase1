using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (Input.GetKeyDown(KeyCode.Z))
		{
			butterfly.StartMovingForward();
		}
		else if (Input.GetKeyUp(KeyCode.Z))
		{
			butterfly.StartDeccelerating();
		}
	}
}
