using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UnityStandardAssets.Characters.FirstPerson{

public class FireABeam : MonoBehaviour {
		
		public Transform origin;
		public float forceScaler;
		public float upForceScaler;
		public float connectRange;
		public float minRangeToDrop;
		public LayerMask interactionLayer;
		private Vector3 desPoint;
		private Rigidbody playerRg;
		public GameObject Lightningpref;
		public Texture2D crosshairTexture;
		public float scaler;
		public GameObject otherHeart;
		private GameObject heartMask;
		GameObject hitPeople = null;
		RigidbodyFirstPersonController playerScript;
		public int sceneIndex;
		Rect position;
		bool connected;
		bool onlyCheckOnce;
		bool onlyCheckOnce2;
		// Use this for initialization
		void Awake(){
			otherHeart.SetActive (false);
			heartMask = GameObject.Find ("HeartMask");
			heartMask.SetActive (false);
		}
		void Start () {
			onlyCheckOnce = true;
			onlyCheckOnce2 = true;
			position = new Rect( ( Screen.width - crosshairTexture.width*scaler ) / 2, ( Screen.height - crosshairTexture.height*scaler ) / 2, crosshairTexture.width*scaler, crosshairTexture.height*scaler );
			playerRg = GetComponentInParent<Rigidbody> ();
			playerScript = GetComponentInParent<RigidbodyFirstPersonController> ();

		}
		
		// Update is called once per frame
		void Update () {
			RaycastHit hit ;

			if (Input.GetButtonDown ("Fire1")) {
				if (Physics.Raycast (transform.position, transform.forward, out hit, connectRange,interactionLayer)&&!connected) {
					//print ("Found an object - distance: " + hit.collider.name + hit.distance);
					if (hit.collider.tag != "Obstacles") {
						connected = true;
						desPoint = hit.point;
						if (hit.collider.tag == "People")
							hitPeople = hit.collider.gameObject;
					}
				}

				//rg.AddForce(Camera.main.transform.forward*force);
			}
			if (Input.GetButtonUp ("Fire1")) {
				connected = false;
				otherHeart.SetActive (false);
				playerScript.advancedSettings.airControl = true;
				playerRg.useGravity = true;
				//Disable other people's heart
				if (hitPeople != null) {

					HeartBeat otherHeartScript = hitPeople.GetComponent<HeartBeat> ();
					otherHeartScript.heart = null;
				}
				hitPeople = null;
				//lRend.SetPosition (1, origin.position);
			}
			//hit to listen to other people's heart
			if(hitPeople!=null){
				otherHeart.SetActive (true);
				HeartBeat otherHeartScript = hitPeople.GetComponent<HeartBeat> ();
				otherHeartScript.heart = otherHeart;
				otherHeartScript.heartAnim = otherHeart.GetComponent<Animator> ();
				//If hit Rose
				if (hitPeople.name == "Rose"&&onlyCheckOnce) {
					FindHer ();
				}
				if (hitPeople.name == "NPC"&&onlyCheckOnce2) {
					//DO something
					onlyCheckOnce2 = false;
				}
			}
	
			if (connected) {// connect to something
				playerScript.advancedSettings.airControl = false;
				playerRg.useGravity = false;
				//lighting
				GameObject ligthing = Instantiate(Lightningpref,this.transform.position, Quaternion.identity);
				LineRenderer lRender = ligthing.GetComponent<LineRenderer> ();
			
				lRender.SetPosition(0, origin.position);
				lRender.SetPosition(1, desPoint);
	//			Lightning lRender = ligthing.GetComponent<Lightning> ();
	//			lRender.origin = transform.position;
	//			lRender.dest = desPoint;

				Vector3 forceDir = desPoint- transform.position;
				Vector3.Normalize (forceDir);
				//playerRg.velocity = forceDir*speed;
				playerRg.AddForce(forceDir*forceScaler,ForceMode.Acceleration);
				if (Vector3.Distance (desPoint, transform.position) < minRangeToDrop) {
						
						playerScript.advancedSettings.airControl = true;
						playerRg.useGravity = true;
						playerRg.Sleep ();
						playerRg.WakeUp ();
					if(!playerScript.Grounded){//add force to land when on air
						playerRg.AddForce(transform.up*upForceScaler,ForceMode.Impulse);
						playerRg.AddForce(transform.forward*upForceScaler*1.3f,ForceMode.Impulse);

					}
						//playerRg.AddForce(transform.up*upForceScaler,ForceMode.Impulse);
						connected = false;
				}
			}


		}
		public void DisableConnection(){
			connected = false;
		}
		void  OnGUI (){
			RaycastHit hit ;
			if (Physics.Raycast (transform.position, transform.forward, out hit, connectRange,interactionLayer)&&!connected) {
				GUI.DrawTexture(position, crosshairTexture);	
			}
		}
		//What happen after finding Rose
		void FindHer(){
			onlyCheckOnce = false;
			heartMask.SetActive (true);
			//go to next scene;
			StartCoroutine(GoToNextScene());
		
		}
		IEnumerator GoToNextScene(){
			yield return new WaitForSeconds (2.5f);
			SceneManager.LoadScene (sceneIndex);
			Debug.Log ("Try Load Scene2");
		}


	}

}
