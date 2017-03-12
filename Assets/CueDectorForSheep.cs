using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueDectorForSheep : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		if(coll.name =="Cue")
			gameObject.SendMessageUpwards ("CueEnter",coll.gameObject);
	}
}
