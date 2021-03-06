using UnityEngine;
using System.Collections;

public class PoreScript : MonoBehaviour {

	private GameObject sweatResource;
	private GameObject sweat;

	private float fallDir = .25f;

	void Awake () {
		sweatResource = Resources.Load ("Sweat") as GameObject;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown (KeyCode.R))
		//	dropSweat (fallDir, Color.white);
	}

	public void dropSweat (float dir, Color color) {
		fallDir = dir;
		sweat = (GameObject)Instantiate (sweatResource, transform.position, transform.rotation);
		sweat.GetComponent<sweatScript> ().setFallDir (fallDir);
		sweat.GetComponent<sweatScript> ().color = color;
	}
}
