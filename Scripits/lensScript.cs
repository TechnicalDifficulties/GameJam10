using UnityEngine;
using System.Collections;

public class lensScript : MonoBehaviour {

	public AudioSource audio1;
	public AudioSource audio2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void hurt(){
		GetComponent<AudioSource> ().Play ();
	}

	public void die(){
		GetComponent<AudioSource> ().Play ();
		GetComponent<SpriteRenderer> ().color = Color.clear;
		GetComponent<CircleCollider2D> ().enabled = false;
	}
}
