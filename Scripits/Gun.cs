using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	private PlayerControl playerCtrl;
	private Animator anim;
	public GameObject player;

	public LineRenderer laser;
	public float curLaserWidth = 0.1f;
	private float curLaserAlpha = 1f;
	private Color currentColor = Color.red;
	private bool red = true;
	private bool green = false;
	private bool blue = false;

	public Vector3 hitPoint;
	public float distance;


	void Awake()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}

	void Update ()
	{
		checkColor ();
		laser.GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b,curLaserAlpha);
	}

	void checkColor () {
		if (Input.GetKeyDown (KeyCode.Alpha1))
			red = !red;
		if (Input.GetKeyDown (KeyCode.Alpha2))
			green = !green;
		if (Input.GetKeyDown (KeyCode.Alpha3))
			blue = !blue;
		if (red) {
			if (blue) {
				if (green) {
					changeColor (Color.white);
					currentColor = Color.white;
				} else {
					changeColor (Color.magenta);
					currentColor = Color.magenta;
				} 
			} else if (green) {
				changeColor (Color.yellow);
				currentColor = Color.yellow;
			} else {
				changeColor (Color.red);
				currentColor = Color.red;
			}
		} else if (blue) {
			if (green) {
				changeColor (Color.cyan);
				currentColor = Color.cyan;
			} else {
				changeColor (Color.blue);
				currentColor = Color.blue;
			}
		} else if (green) {
			changeColor (Color.green);
			currentColor = Color.green;
		} else {
			changeColor (Color.clear);
			currentColor = Color.clear;
		}
	}

	void FixedUpdate ()
	{
		if (player == null) {
			Destroy (gameObject);
			return;
		}
		laser.SetVertexCount (2);
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.position = player.transform.position;

		Vector3[] points = new Vector3[2];
		points [0] = transform.position;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right);

		Vector2 direction = Vector2.zero;
		laser.SetPosition (0, points[0]);
		points [1] = hit.point;
		laser.SetPosition (1, points[1]);


		direction = Vector3.Reflect ((Vector3)hit.point - transform.position, hit.normal);
		RaycastHit2D hit2 = Physics2D.Raycast (hit.point, direction);
		doReflection (hit, hit2, transform.position, 1);

		if (currentColor != Color.clear) {
			if (hit2.collider.gameObject.tag == "Tear") {
				if (checkSweatColor (hit2.collider.gameObject.GetComponent<tearScript>().color)) {
				} else {
					hit2.collider.gameObject.GetComponent<tearScript>().hurt = true;
				}
			}
			else if (hit2.collider.gameObject.tag == "PrepTear") {
				if (checkSweatColor (hit2.collider.gameObject.GetComponent<prepTearScript>().color)) {
				} else {
					hit2.collider.gameObject.GetComponent<prepTearScript>().hurt = true;
				}
			}
			else if (hit2.collider.gameObject.tag == "Sweat") {
				if (checkSweatColor (hit2.collider.gameObject.GetComponent<sweatScript>().color)) {
				} else {
					Destroy (hit2.collider.gameObject);
				}
			} else if (hit2.collider.gameObject.tag == "Hand") {
				hit2.collider.gameObject.GetComponent<handScript> ().lasered = true;
			}
		}

	}
		
	void doReflection(RaycastHit2D hit, RaycastHit2D hit2, Vector3 origin, int bounce){
		if (currentColor != Color.clear) {
			if (hit.collider.gameObject.tag == "Tear") {
				if (checkSweatColor (hit.collider.gameObject.GetComponent<tearScript>().color)) {
				} else {
					hit.collider.gameObject.GetComponent<tearScript>().hurt = true;
				}
			}
			else if (hit.collider.gameObject.tag == "PrepTear") {
				if (checkSweatColor (hit.collider.gameObject.GetComponent<prepTearScript>().color)) {
				} else {
					hit.collider.gameObject.GetComponent<prepTearScript>().hurt = true;
				}
			}
			if (hit.collider.gameObject.tag == "Sweat") {
				if (checkSweatColor (hit.collider.gameObject.GetComponent<sweatScript>().color)) {
					laser.SetVertexCount (bounce + 2);
					laser.SetPosition (bounce + 1, hit2.point);
				} else {
					Destroy (hit.collider.gameObject);
				}
			} else if (hit.collider.gameObject.tag == "Hand") {
				hit.collider.gameObject.GetComponent<handScript> ().lasered = true;
			} else if (hit.collider.gameObject.tag == "Lens") {
				laser.SetVertexCount (bounce + 2);
				laser.SetPosition (bounce + 1, hit2.point);
			}
		}
	}
		

	void changeColor (Color c) {
		laser.SetColors (c, c);
	}

	bool checkSweatColor (Color c){
		Debug.Log ("Checking Sweat Color");
		if (c == Color.white) {
			if (currentColor == Color.white) {
				return false;
			}
		} else if (c == Color.red) {
			if (currentColor == Color.blue || currentColor == Color.green ||
			    currentColor == Color.cyan) {
				return false;
			}
		} else if (c == Color.green) {
			if (currentColor == Color.blue || currentColor == Color.red ||
			    currentColor == Color.magenta) {
				return false;
			}
		} else if (c == Color.blue) {
			if (currentColor == Color.green || currentColor == Color.red ||
			    currentColor == Color.yellow) {
				return false;
			}
		} else if (c == Color.cyan) {
			if (currentColor == Color.red) {
				return false;
			}
		} else if (c == Color.magenta) {
			if (currentColor == Color.green) {
				return false;
			}
		} else if (c == Color.yellow) {
			if (currentColor == Color.blue) {
				return false;
			}
		} else {
			return true;
		}
		return true;
	}
}
