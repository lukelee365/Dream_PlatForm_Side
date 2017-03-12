using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {
	public float radius;
	public float speed;
	public Vector3 RotateAxis;
	private GameObject parentObject;
	public float Delaytime;
	private bool rotate;

	// Use this for initialization
	void Start () {
		parentObject = new GameObject (gameObject.name + "Parent");
		parentObject.transform.position = transform.position;
		parentObject.transform.rotation = transform.rotation;
		parentObject.transform.parent = transform.parent;
		transform.parent = parentObject.transform;

		// Change pivot point
		transform.position += Vector3.forward * radius;
		parentObject.transform.position -= Vector3.forward * radius;
		rotate = false;
		Invoke ("TurnOn", Delaytime);
	}
	
	// Update is called once per frame
	void Update () {
		if(rotate)
		parentObject.transform.Rotate (RotateAxis*speed);
	}

	void TurnOn(){
		rotate = true;
	}
}
