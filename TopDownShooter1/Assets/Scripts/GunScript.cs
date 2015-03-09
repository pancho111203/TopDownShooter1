using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	public LayerMask whatToHit;
	public Transform bullet;
	private Transform firePos;

	// Use this for initialization
	void Awake () {

		firePos = transform.Find("FirePosition");
		if(firePos == null){
			firePos = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(Vector2 mousePos){
		Transform instanceBullet = (Transform)Instantiate(bullet,firePos.position,firePos.rotation );
		
		RaycastHit2D hit = Physics2D.Raycast((Vector2)firePos.position,mousePos-(Vector2)firePos.position,100,whatToHit); // hit is the object hit by the raycast, null if none
		
		if(hit != null){
			((BulletScript)instanceBullet.GetComponent<BulletScript>()).setCollision(hit);
		}
		
	}
}
