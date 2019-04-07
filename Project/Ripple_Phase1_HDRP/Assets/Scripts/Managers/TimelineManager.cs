using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
	public PlayableDirector timeline;

    // Start is called before the first frame update
    void Start()
    {
		//timeline = GetComponent<PlayableDirector>();
    }

    public void StartTimeline()
	{
		timeline.Play();
	}
}
