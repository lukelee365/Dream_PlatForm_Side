using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {
	public GameObject[] gameObjList;
	private int objIndex;
	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyOneByOne (4f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public IEnumerator DestroyOneByOne(float t){
		while (gameObjList.Length > 0) {
			if (gameObjList [objIndex] == null) {
				objIndex++;
			} else {
				yield return new WaitForSeconds (t);
				// if null skip
				if (gameObjList [objIndex] != null) {
					GameObject last = gameObjList [objIndex];
					//gameObjectList.Remove (last);
					Rigidbody rg = last.AddComponent<Rigidbody> ();
					rg.useGravity = true;
					objIndex++;
				}
		

			}
		} 
	}
}
