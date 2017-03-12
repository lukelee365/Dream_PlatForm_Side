using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithThings : MonoBehaviour {
	public LayerMask interactionLayer;
	public GameObject PlaceHolderforFruit;
	public GameObject UI_Flower1;
	public GameObject UI_Flower2;
	public GameObject UI_Flower3;
	public GameObject objFlower1;
	public GameObject objFlower2;
	public GameObject objFlower3;
	public Texture2D crosshairTexture;
	private GameObject inHand;
	private AudioSource Sound_Collect;
	private bool flower1;
	private bool flower2;
	private bool flower3;
	Rect position;
	// Use this for initialization
	void Start () {
		position = new Rect( ( Screen.width - crosshairTexture.width*0.15f ) / 2, ( Screen.height - crosshairTexture.height*0.15f ) / 2, crosshairTexture.width*0.15f, crosshairTexture.height*0.15f );
		UI_Flower1.SetActive (false);
		UI_Flower2.SetActive (false);
		UI_Flower3.SetActive (false);
		Sound_Collect = GetComponent<AudioSource> ();
		objFlower1.SetActive (false);
		objFlower2.SetActive (false);
		objFlower3.SetActive (false);
		flower1 = false;
		flower2 = false;
		flower3 = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit ;

		if (Input.GetButtonDown ("Fire1")) {
			if (inHand != null) {
				ThrowInHand ();
			}
			if (Physics.Raycast (transform.position, transform.forward, out hit, 6f, interactionLayer)) {
				if (hit.collider.name == "Cue") {
					//GrabIt
					Sound_Collect.Play ();
					hit.collider.tag = "Cue";
					hit.collider.transform.position = PlaceHolderforFruit.transform.position;
					hit.collider.transform.transform.parent = transform.parent;
					inHand = hit.collider.gameObject;
					Rigidbody rg = inHand.GetComponent<Rigidbody> ();
					ParticleSystem ps = inHand.GetComponentInChildren<ParticleSystem> ();
					ps.Stop ();
					rg.isKinematic = true;
					rg.useGravity = false;
				} else if (hit.collider.tag == "Fruit") {
					Sound_Collect.Play ();
					Sound_Collect.Play ();
					hit.collider.transform.position = PlaceHolderforFruit.transform.position;
					hit.collider.transform.transform.parent = transform.parent;
					inHand = hit.collider.gameObject;
					Rigidbody rg = inHand.GetComponent<Rigidbody> ();
					ParticleSystem ps = inHand.GetComponentInChildren<ParticleSystem> ();
					ps.Stop ();
					rg.isKinematic = true;
					rg.useGravity = false;
					//Grabit
				} else if (hit.collider.name == "Goat") {
					// Flip it Back
					Sound_Collect.Play ();
				
					hit.collider.gameObject.BroadcastMessage ("FlipGoatBack");
				} else if (hit.collider.name == "Flower1") {
					UI_Flower1.SetActive (true);
					Sound_Collect.Play ();
					Destroy (hit.collider.gameObject);
					flower1 = true;
				} else if (hit.collider.name == "Flower2") {
					UI_Flower2.SetActive (true);
					Sound_Collect.Play ();
					flower2 = true;
					Destroy (hit.collider.gameObject);
				} else if (hit.collider.name == "Flower3") {
					UI_Flower3.SetActive (true);
					Sound_Collect.Play ();
					Destroy (hit.collider.gameObject);
					flower3 = true;
				}

			}


		}
	}

	void ThrowInHand(){
		inHand.transform.parent = null;
		Rigidbody rg = inHand.GetComponent<Rigidbody> ();
		ParticleSystem ps = inHand.GetComponentInChildren<ParticleSystem> ();
		ps.Play ();
		rg.isKinematic = false;
		rg.useGravity = true;
		inHand = null;
	}
	public void ShowTheFlower(){
		if (flower1) {
			objFlower1.SetActive (true);
		}
		if (flower2) {
			objFlower2.SetActive (true);
		}
		if (flower3) {
			objFlower3.SetActive (true);
		}
	}

	void  OnGUI (){
		RaycastHit hit ;
		if (Physics.Raycast (transform.position, transform.forward, out hit, 6,interactionLayer)) {
			GUI.DrawTexture(position, crosshairTexture);	
		}
	}

}
