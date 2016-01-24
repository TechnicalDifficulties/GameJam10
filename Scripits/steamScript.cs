using UnityEngine;
using System.Collections;

public class steamScript : MonoBehaviour {

	private float deathSpeed = 1f;
	private Color color;
	public AudioSource source;
	// Use this for initialization
	void Start () {
		color = GetComponent<SpriteRenderer> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		if (color.a <= 0)
			Destroy (gameObject);
		color = GetComponent<SpriteRenderer> ().color;
		GetComponent<SpriteRenderer> ().color = new Vector4 (color.r, color.g, color.b, color.a - deathSpeed * Time.deltaTime);
	}
}
