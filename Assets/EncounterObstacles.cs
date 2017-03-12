using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{

	public class EncounterObstacles : MonoBehaviour
	{
		private FireABeam fB;
		// Use this for initialization
		void Start ()
		{
			fB = GetComponentInChildren<FireABeam> ();
		}
	
		void OnCollisionEnter(Collision coll){
			if (coll.gameObject.tag == "Obstacles") {
				fB.DisableConnection ();
			}
		}
	}
}