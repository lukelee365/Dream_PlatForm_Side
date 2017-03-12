using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCheckForRabbit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider coll){
		if(coll.tag =="Fruit")
			gameObject.SendMessageUpwards ("FruitEnter",coll.gameObject);
	}
}
