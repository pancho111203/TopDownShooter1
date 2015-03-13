using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 5.0f;
	public float rotOffset = 0f;
	public GameObject activeWeapon;
	public int maxWeapons = 10; //max number of weapons that can be carried at once
	
	private CollIterator<GameObject> weaponsList;

	private Transform ownT;
	private Rigidbody2D ownR;
	private Camera cam;

	public float test;

	private float moveH = 0f;
	private float moveV = 0f;

	private Vector2 mousePos;


	// Use this for initialization
	void Awake () {
		ownT = GetComponent<Transform>();
		ownR = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		 
		weaponsList = new CollIterator<GameObject>(maxWeapons);

	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.N)){
			switchGunSeqUp();
		}
		if(Input.GetKeyDown(KeyCode.M)){
			switchGunSeqDown();
		}


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


		//continous rotation towards mouse position 
		Vector2 diff = mousePos - (new Vector2(ownT.position.x,ownT.position.y));

		diff.Normalize();

		float rotZ = Mathf.Atan2(diff.y,diff.x)* Mathf.Rad2Deg; // get angle of the vector to apply it to the player (find the angle in degrees)
		ownT.rotation = Quaternion.Euler(0f,0f,rotZ+rotOffset);

	}



	void OnTriggerEnter2D(Collider2D coll){
		if(coll.transform.tag=="Weapon"){
			if(ownT.Find(coll.gameObject.name)!=null){ // if he player already has a gun of this type  
				fillAmmo(coll);
			}else{
				pickGun(coll);
			}
		}
	}
	
	private void fillAmmo(Collider2D coll){ // fills the ammo of the picked up weapon if the player already has one of the same type
		// TODO
		Debug.Log("Fill Ammo(TODO)");
	}

	private void pickGun(Collider2D coll){ // picks up the weapon
		Transform newPos = ownT.Find("GunPosition");

		coll.transform.parent = ownT;
		coll.transform.position = newPos.position;
		coll.transform.rotation = newPos.rotation;

		bool emptyPlace = weaponsList.findFirstEmpty();
		if(emptyPlace){
			weaponsList.insert(coll.gameObject);
			if(activeWeapon!=null)activeWeapon.gameObject.SetActive(false);
			
			activeWeapon = coll.gameObject;
			activeWeapon.GetComponent<GunScript>().pickedUp(ownT);
		}

	}

	private void switchGun(int i){ // SWitch weapon giving an index
		if(weaponsList.size>i){
			GameObject newWep = weaponsList.getElement(i);
			if(newWep!=null){
				if(activeWeapon!=null)activeWeapon.gameObject.SetActive(false);
				newWep.SetActive(true);
				activeWeapon = newWep;
			}
		}


	}

	private void switchGunSeqUp(){ // SWitch weapon sequentially -- next gun
		GameObject newWep = weaponsList.getNext();
		if(newWep!=null){
			if(activeWeapon!=null)activeWeapon.gameObject.SetActive(false);
			newWep.SetActive(true);
			activeWeapon = newWep;
		}
	}

	private void switchGunSeqDown(){ // SWitch weapon sequentially -- previous gun
		GameObject newWep = weaponsList.getPrev();
		if(newWep!=null){
			if(activeWeapon!=null)activeWeapon.gameObject.SetActive(false);
			newWep.SetActive(true);
			activeWeapon = newWep;
		}
	}

}
