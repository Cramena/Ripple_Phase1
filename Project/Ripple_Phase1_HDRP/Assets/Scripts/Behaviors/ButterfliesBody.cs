using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesBody : MonoBehaviour
{

	//-----PUBLIC-----
	//REFERENCES
	[Header("References:")]
	public ParticleSystem pollenParticle;
	public ParticleSystem releasePollenParticle;
	public Animator anim;

	[Space()]

	//PARAMETERS
	[Header("Parameters:")]
	public float size;
	public bool randomSize;
	public float minSize;
	public float maxSize;

	[Space()]

	public float animSpeed;
	public bool randomAnimSpeed;
	public float minAnimSpeed;
	public float maxAnimSpeed;


	//-----PRIVATE-----
	//SELF REFERENCES
	Transform self;

    // Start is called before the first frame update
    void Start()
    {
		ButterflyController.instance.ActivateEvent.AddListener(ActivatePollen);
		ButterflyController.instance.DeactivateEvent.AddListener(DeactivatePollen);
		self = transform;
		//anim = GetComponent<Animator>();
		if (randomSize)
		{
			size = Random.Range(minSize, maxSize);
		}
		if (randomAnimSpeed)
		{
			animSpeed = Random.Range(minAnimSpeed, maxAnimSpeed);
		}
		self.localScale = new Vector3(size, size, size);
		anim.speed = animSpeed;
	}

	public void DeactivatePollen()
	{
		pollenParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		releasePollenParticle.Play();
	}

	public void ActivatePollen()
	{
		pollenParticle.Play();
	}
}
