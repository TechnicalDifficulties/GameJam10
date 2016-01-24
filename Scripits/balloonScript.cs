using UnityEngine;
using System.Collections;

public class balloonScript : MonoBehaviour {
	public Color color;
	private bool dead = false;
	public bool isYellow = false;
	// Use this for initialization
	void Start () {
		if (!isYellow) {
			color = GetComponent<SpriteRenderer> ().color;
		} else {
			color = Color.yellow;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (dead && !GetComponent<AudioSource>().isPlaying) {
			Destroy (gameObject);
		}
	}

	public void die(){
		//steam = (GameObject)Instantiate (steamResource, transform.position, transform.rotation);
		//steam.GetComponent<SpriteRenderer> ().color = color;
		if (!GetComponent<AudioSource> ().isPlaying && !dead)
			GetComponent<AudioSource> ().Play ();
		GetComponent<SpriteRenderer>().color = Color.clear;
		dead = true;
		//Destroy (gameObject);
	}
}
