using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
	public class Tuturial : MonoBehaviour
	{
		private EnableFire eF;
		private TuturialFadeOut textFadeOut;

	// Use this for initialization
	void Start ()
		{
			eF = GetComponentInChildren<EnableFire> ();
			textFadeOut = GameObject.Find ("TutorialText").GetComponent<TuturialFadeOut> ();
		}
	
		// Update is called once per frame
		void OnTriggerEnter(Collider coll){
			if (coll.name == "Tutorial1") {
				eF.DisableFire ();
				StartCoroutine (textFadeOut.FadeTextToZeroAlpha ("Press space jump to the rotating stuff", 4f));
			} else if (coll.name == "Tutorial2") {
				eF.ENableFire ();
				StartCoroutine (textFadeOut.FadeTextToZeroAlpha ("Using mouse click to connect to edge of platfrom to go there", 7f));
			} else if (coll.name == "Tutorial3") {
				StartCoroutine (textFadeOut.FadeTextToZeroAlpha ("Try to combine jump and connect to reach further", 8f));
			} else if(coll.name == "Tutorial4") {
				StartCoroutine (textFadeOut.FadeTextToZeroAlpha ("Try to release mouse in mid way of connection to fly further", 8f));

			}else if(coll.name == "Tutorial5") {
				StartCoroutine (textFadeOut.FadeTextToZeroAlpha ("She is there!", 8f));

			}else if(coll.name == "Tutorial6") {
				StartCoroutine (textFadeOut.FadeTextToZeroAlpha ("Watching Obstacles", 8f));

			}

		}
	}
}