using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlyToPlayer : MonoBehaviour {
	public Transform Origin; 
	public Transform target;
	public float delay;
	public float speed;
	private bool flyBack;
	private bool onlyOnce;

	// Use this for initialization
	void Start () {

		flyBack = false;
		onlyOnce = false;

		Invoke ("TurnFlyBack", delay);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!onlyOnce) {
			if (flyBack) {
				
				FLyBackToPlayer ();
			} else {
				transform.parent = null;
				transform.position = Origin.position;
				transform.rotation = Origin.rotation;
			}
		}
	}

	void FLyBackToPlayer(){

		float distBetweenwayPoint = Vector3.Distance(transform.position,target.position);

		if (distBetweenwayPoint <=0.1f&&!onlyOnce) {
			transform.rotation = target.rotation;
				transform.parent = target.parent;
				onlyOnce = true;
		} else {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);

		}
	}
	void TurnFlyBack(){
		
		flyBack = true;
	}
}
