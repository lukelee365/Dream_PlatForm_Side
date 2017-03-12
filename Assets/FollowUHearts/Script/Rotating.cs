using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {
	public float radius;
	public float speed;
	public Vector3 RotateAxis;
	private GameObject parentObject;

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
	}
	
	// Update is called once per frame
	void Update () {
		parentObject.transform.Rotate (RotateAxis*speed);
	}
}
