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
		audio1.Play ();
	}

	public void die(){
		audio2.Play ();
	}
}
