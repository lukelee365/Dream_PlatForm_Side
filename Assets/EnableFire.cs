using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
	public class EnableFire : MonoBehaviour
	{
	
		private Crosshair cH;
		private FireABeam fB;
		// Use this for initialization
		void Start ()
		{
			cH = GetComponent<Crosshair> ();
			fB = GetComponent<FireABeam> ();
//			cH.enabled = false;
//			fB.enabled = false;
//
		}
		public void ENableFire(){
			cH.enabled = true;
			fB.enabled = true;
		}
		public void DisableFire(){
			cH.enabled = false;
			fB.enabled = false;
		}
	}
}