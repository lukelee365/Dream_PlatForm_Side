using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatForm : MonoBehaviour {

	public GameObject[] wayPoints;
	public float speed;
	private int goalIndex;


	// Use this for initialization
	void Start () {
		goalIndex =  0;

	}

	// Update is called once per frame
	void Update () {
		//Get to Way Point
		Looping();


	}

	/// <summary>
	/// Chasing the goal.
	/// </summary>
	void Looping(){
		float distBetweenwayPoint = Vector3.Distance(transform.position,wayPoints[goalIndex].transform.position);
		if (distBetweenwayPoint < 0.1) {
			if (goalIndex < wayPoints.Length) {
				if (goalIndex == wayPoints.Length - 1) {
					goalIndex = 0;
				} else {
					goalIndex++;
				}
			}

		} else {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position,wayPoints[goalIndex].transform.position , step);
		
		}
	}

}
