using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
	private PlayerControl playerCtrl;
	private Animator anim;
	public GameObject player;

	public LineRenderer laser;
	public float curLaserWidth = 0.1f;
	private float curLaserAlpha = 1f;
	private Color currentColor = Color.red;
	private bool red = false;
	private bool green = false;
	private bool blue = false;

	public Vector3 hitPoint;
	public float distance;

	public bool toggle = false;

	public Texture2D hold;
	public Texture2D toggleUI;

	public GameObject holdUI;
	void Awake()
	{
		hold = Resources.Load ("UI_ToggleBox_Hold") as Texture2D;
		toggleUI = Resources.Load ("UI_ToggleBox_Toggle") as Texture2D;
		// Setting up the references.
		anim = GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}

	void Update ()
	{
		checkColor ();
		//laser.GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b,curLaserAlpha);
	}

	void checkColor () {
		if (Input.GetKeyDown (KeyCode.R))
			toggle = !toggle;
		if (!toggle) {
			holdUI.GetComponent<Image> ().sprite = Sprite.Create(hold, new Rect(0,0,hold.width, hold.height), new Vector2(0.5f, 0.5f));
			if (Input.GetKey (KeyCode.Alpha1) || Input.GetMouseButton(0))
				red = true;
			else
				red = false;
			if (Input.GetKey (KeyCode.Alpha2)|| Input.GetMouseButton(2))
				green = true;
			else
				green = false;
			if (Input.GetKey (KeyCode.Alpha3)|| Input.GetMouseButton(1))
				blue = true;
			else
				blue = false;
		} else {
			holdUI.GetComponent<Image> ().sprite = Sprite.Create(toggleUI, new Rect(0,0,toggleUI.width, toggleUI.height), new Vector2(0.5f, 0.5f));
			if (Input.GetKeyDown (KeyCode.Alpha1)|| Input.GetMouseButtonDown(0))
				red = !red;
			if (Input.GetKeyDown (KeyCode.Alpha2)|| Input.GetMouseButtonDown(2))
				green = !green;
			if (Input.GetKeyDown (KeyCode.Alpha3)|| Input.GetMouseButtonDown(1))
				blue = !blue;
		}
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
		if (hit.collider.gameObject.tag != "Ground") {
			doReflection (hit, hit2, transform.position, 1);

			if (currentColor != Color.clear) {
				if (hit2.collider.gameObject.tag == "Tear") {
					if (checkSweatColor (hit2.collider.gameObject.GetComponent<tearScript> ().color)) {
					} else {
						hit2.collider.gameObject.GetComponent<tearScript> ().hurt = true;

					}
				} else if (hit2.collider.gameObject.tag == "PrepTear") {
					if (checkSweatColor (hit2.collider.gameObject.GetComponent<prepTearScript> ().color)) {
					} else {
						Debug.Log ("hurting1");
						hit2.collider.gameObject.GetComponent<prepTearScript> ().hurt = true;
						//hit2.collider.gameObject.GetComponent<prepTearScript> ().hurting = false;
					}
				} else if (hit2.collider.gameObject.tag == "Sweat") {
					if (checkSweatColor (hit2.collider.gameObject.GetComponent<sweatScript> ().color)) {
					} else {
						hit2.collider.gameObject.GetComponent<sweatScript> ().die ();
					}
				} else if (hit2.collider.gameObject.tag == "Balloon") {
					if (checkSweatColor (hit2.collider.gameObject.GetComponent<balloonScript> ().color)) {
					} else {
						hit2.collider.gameObject.GetComponent<balloonScript> ().die ();
					}
				} else if (hit2.collider.gameObject.tag == "Hand") {
					hit2.collider.gameObject.GetComponent<handScript> ().lasered = true;
					hit2.collider.gameObject.GetComponent<handScript> ().hurt = true;
				}
			}
		}

	}
		
	void doReflection(RaycastHit2D hit, RaycastHit2D hit2, Vector3 origin, int bounce){
		if (currentColor != Color.clear) {
			if (hit.collider.gameObject.tag == "Tear") {
				if (checkSweatColor (hit.collider.gameObject.GetComponent<tearScript>().color)) {
					laser.SetVertexCount (bounce + 2);
					laser.SetPosition (bounce + 1, hit2.point);
				} else {
					hit.collider.gameObject.GetComponent<tearScript>().hurt = true;
				}
			}
			else if (hit.collider.gameObject.tag == "PrepTear") {
				if (checkSweatColor (hit.collider.gameObject.GetComponent<prepTearScript>().color)) {
					laser.SetVertexCount (bounce + 2);
					laser.SetPosition (bounce + 1, hit2.point);
				} else {
					Debug.Log ("hurting2");
					hit.collider.gameObject.GetComponent<prepTearScript>().hurt = true;
					//hit.collider.gameObject.GetComponent<prepTearScript> ().hurting = false;
				}
			}
			if (hit.collider.gameObject.tag == "Sweat") {
				if (checkSweatColor (hit.collider.gameObject.GetComponent<sweatScript>().color)) {
					laser.SetVertexCount (bounce + 2);
					laser.SetPosition (bounce + 1, hit2.point);
				} else {
					hit.collider.gameObject.GetComponent<sweatScript> ().die ();
				}
			} else if (hit.collider.gameObject.tag == "Balloon") {
				if (checkSweatColor (hit.collider.gameObject.GetComponent<balloonScript> ().color)) {
					laser.SetVertexCount (bounce + 2);
					laser.SetPosition (bounce + 1, hit2.point);
				
				} else {
					hit.collider.gameObject.GetComponent<balloonScript> ().die ();
				}
			} else if (hit.collider.gameObject.tag == "Hand") {
				hit.collider.gameObject.GetComponent<handScript> ().lasered = true;
				hit.collider.gameObject.GetComponent<handScript> ().hurt = true;
			} else if (hit.collider.gameObject.tag == "Lens") {
				laser.SetVertexCount (bounce + 2);
				laser.SetPosition (bounce + 1, hit2.point);
			}
		}
	}
		

	void changeColor (Color c) {
		laser.SetColors (c, c);
	}

	bool checkSweatColor (Color c) {
		//Debug.Log ("checkSweatColor");
		//Debug.Log ("prep color =" + c);
		//Debug.Log ("laser color =" + currentColor);
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
				Debug.Log ("hit!");
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
