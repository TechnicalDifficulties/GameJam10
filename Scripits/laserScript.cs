using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {
	private GameObject gun;

	void Awake (){
		gun = GameObject.Find ("/Gun");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		transform.position = gun.transform.position;
		//transform.rotation = gun.transform.rotation;
	}
}
