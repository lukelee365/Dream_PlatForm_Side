using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider coll){
		if (coll.name == "Rabbit_Parent") {
			gameObject.SendMessageUpwards ("ParentEnter", coll.gameObject);
		
		}
	}
}
