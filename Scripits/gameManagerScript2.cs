using UnityEngine;
using System.Collections;

public class gameManagerScript2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick(){
		Application.LoadLevel("scene");
	}

	public void onClick2(){
		Application.LoadLevel("instructions");
	}
}
