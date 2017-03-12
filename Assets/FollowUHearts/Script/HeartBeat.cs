using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour {
	private AudioSource heartBeatAudioSource;
	public int audioIndex;
	public float heartBeatTime;
	public float minHearRate;
	public float maxHeartRate;
	public float maxDistBetweenTwo;
	public GameObject Target;
	public GameObject heart;
	private float timeStart;
	private float timer;
	[HideInInspector]
	public Animator heartAnim;
	private bool shouldHeartBeat;
	private bool checkOnce;
	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GameObject.Find("AudioManager").GetComponents<AudioSource> ();
		timeStart = Time.time;
		timer = Time.time + heartBeatTime;
		shouldHeartBeat = true;
		checkOnce = false;
		heartBeatAudioSource = audioSources [audioIndex];
		if (heart != null) {
			heartAnim = heart.GetComponent<Animator> ();
		}
//			StartCoroutine ("HeartBeatFrequnecy");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (heart != null) {
			ControlhearBeatTime ();
			HeartBeatFrequent ();
		}
			
		
	}
	public float DirtOfRose(){
		Vector3 dir =  Target.transform.position-transform.position;
		float angle = Vector3.Angle( dir, transform.forward );
		return angle;
	}

	public float DistBetween(){
		return Vector3.Distance (transform.position,Target.transform.position);
	}

	// control the Time Frequency
	public void ControlhearBeatTime(){
		float dist;
		if (DistBetween() >= maxDistBetweenTwo) {
			dist = maxDistBetweenTwo;
		} else {
			dist = DistBetween ();
		}
		//map the dist
		float r = Remap (dist/ maxDistBetweenTwo, 0, 1, minHearRate, maxHeartRate);
		// map the  direction
//		Debug.Log (DirtOfRose ());
		if (gameObject.tag == "MainCamera") {
			// 30 degree deference
			if (DirtOfRose () > 25f) {
				shouldHeartBeat = false;
				checkOnce = false;

			} else {
				shouldHeartBeat = true;
				if (!checkOnce) {
					
					heartAnim.SetTrigger ("HeartBeats");
					heartBeatAudioSource.Play ();
					checkOnce = true;
				}
			}

		}
		heartBeatTime = r;

	}
	//ReMapValuefrom A range to another
	float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	void HeartBeatFrequent(){
		//make a timer 
		float currentTime =  Time.time+timeStart;

		if (Time.time >= timer) {
			timer = currentTime + heartBeatTime;
			if (heart != null&&shouldHeartBeat) {
				heartAnim.SetTrigger ("HeartBeats");
				heartBeatAudioSource.Play ();
			}
		} 


	}

//	IEnumerator HeartBeatFrequnecy() {
//		float i = 0;
//		while (true) {
//				yield return new WaitForSeconds (heartBeatTime);
//			if (heart != null) {
//				heartAnim.SetTrigger ("HeartBeats");
//				heartBeatAudioSource.Play ();
//			}
//
//		}
//
//	}
}
