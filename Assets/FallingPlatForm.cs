using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
[ExecuteInEditMode]
public class FallingPlatForm : MonoBehaviour {
	public int row;
	public int colum;
	public int height;
	public float timeFallInterveral;
	public float delayTime;
	public GameObject cube;
	private GameObject newCube;
//	private GameObject[] gameObjectList;
	private int objIndex;

	// Use this for initialization
	void Start () {

		if (!Application.isPlaying) {
			Spawn ();
		} 
	}


	void Spawn(){
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < row; j++) {
				for(int z=0;z<colum;z++){
					newCube = Instantiate (cube, new Vector3 (transform.position.x + j, transform.position.y + i, transform.position.z + z), Quaternion.identity) as GameObject;
					newCube.transform.parent = gameObject.transform;

				}
			}
		}
	}

}
