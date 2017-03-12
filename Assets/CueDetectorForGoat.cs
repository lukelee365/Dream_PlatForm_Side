using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueDetectorForGoat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		if(coll.tag =="Cue")
			gameObject.SendMessageUpwards ("CueEnter");
	}
	void OnTriggerExit(Collider coll){
		if(coll.tag =="Cue")
			gameObject.SendMessageUpwards ("CueExit");
	}
}
