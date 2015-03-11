using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {


	public float fireRate = 0; //weapons fire rate; if 0, it is single shot; if 1, it shoots once per second ...
	public float damage = 10;
	public LayerMask whatToHit;
	public float safeZone = 2f; // distance from the center of the player to where you can click to shoot
	public Transform bullet;

	public bool activeWeapon = false;

	private float timeToShoot = 0;
	private Transform firePos;
	private Vector2 mousePos;
	private Camera cam;
	private Transform player;

	// Use this for initialization
	void Awake () {

		player = transform.parent;

		cam = Camera.main;

		firePos = transform.Find("FirePosition");
		if(firePos == null){
			firePos = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player!=null&&activeWeapon){
			if(fireRate==0){	// SINGLE SHOT
				if(Input.GetButtonDown("Fire1")){ 
					ShootCheck();
				}
			}else{	// MULTI SHOT
				if(Input.GetButton("Fire1") && timeToShoot<= Time.time){ 
					ShootCheck();
					timeToShoot = Time.time + 1/fireRate;
				}
			}
		}


	}

	private void ShootCheck(){
		mousePos = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x,cam.ScreenToWorldPoint(Input.mousePosition).y);
		if((mousePos-(Vector2)player.position).magnitude>safeZone){
			Shoot();
		}
	}

	private void Shoot(){



		Transform instanceBullet = (Transform)Instantiate(bullet,firePos.position,firePos.rotation );
	

		RaycastHit2D hit = Physics2D.Raycast((Vector2)firePos.position,mousePos-(Vector2)firePos.position,100,whatToHit); // hit is the object hit by the raycast, null if none
		
		if(hit){
			((BulletScript)instanceBullet.GetComponent<BulletScript>()).setCollision(hit);
		}
		
	}

	public void pickedUp(Transform play){
		player = play;
		activeWeapon = true;
	}

	public void setActive(bool b){
		activeWeapon=b;
	}
}
