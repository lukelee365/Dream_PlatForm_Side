using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorForGoat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider coll){
		if(coll.tag =="Player")
		gameObject.SendMessageUpwards ("PlayerEnter");
	}
	void OnTriggerExit(Collider coll){
		if(coll.tag =="Player")
		gameObject.SendMessageUpwards ("PlayerExit");
	}
}
