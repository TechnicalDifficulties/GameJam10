using UnityEngine;
using System.Collections;

public class handScript : MonoBehaviour {

	public bool lasered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (!lasered)
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (1.25f, 1f, 0f);
		else {
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, 0f, 0f);
		}
		lasered = false;
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			Destroy (col.gameObject);
			Debug.Log ("Player got squished");
		}

		if (col.gameObject.tag == "EyeBall") {
			Destroy (col.gameObject);
			Debug.Log ("Eye got squished");
		}
	}
}
