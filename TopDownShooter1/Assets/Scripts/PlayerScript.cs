using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {



	public float speed = 5.0f;
	public LayerMask whatToHit;
	public float rotOffset = 0f;
	public float closestPointToRotate = 1.5f; // distance from mouse to player where it rotates to mouse pos
	public Transform bullet;

	private Transform ownT;
	private Rigidbody2D ownR;
	private Camera cam;
	private Transform firePos;

	private float moveH = 0f;
	private float moveV = 0f;

	private Vector2 mousePos;
	private Vector2 firingPos;
	private bool shootNext = false;


	// Use this for initialization
	void Awake () {
		ownT = GetComponent<Transform>();
		ownR = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		firePos = ownT.Find("FirePosition");
		if(firePos == null){
			firePos = ownT;
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//input detection
		moveH = Input.GetAxis("Horizontal");
		moveV = Input.GetAxis("Vertical");

		//movement calculations
		moveH = moveH * Time.deltaTime * speed;
		moveV = moveV * Time.deltaTime * speed;


		//moving
		ownR.velocity=new Vector2(moveH,moveV);

		//shooting direction
		mousePos = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x,cam.ScreenToWorldPoint(Input.mousePosition).y);
		firingPos = new Vector2(firePos.position.x,firePos.position.y);


		//continous rotation towards mouse position 
		Vector2 diff = mousePos - (new Vector2(transform.position.x,transform.position.y));

		if(diff.magnitude>closestPointToRotate){

			//shoot detection
			if(Input.GetButtonDown("Fire1")){ shootNext = true;}

			diff.Normalize();

			float rotZ = Mathf.Atan2(diff.y,diff.x)* Mathf.Rad2Deg; // get angle of the vector to apply it to the player (find the angle in degrees)
			transform.rotation = Quaternion.Euler(0f,0f,rotZ+rotOffset);
		}

		if(shootNext){
			Shoot ();
			shootNext=false;
		}

	}

	void Shoot(){
		Transform instanceBullet = (Transform)Instantiate(bullet,firePos.position,firePos.rotation );

		RaycastHit2D hit = Physics2D.Raycast(firingPos,mousePos-firingPos,100,whatToHit); // hit is the object hit by the raycast, null if none

	}
}
