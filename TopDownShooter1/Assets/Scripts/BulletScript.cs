using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float bulletSpeed = 500f;
	public float timeAlive = 1f;
	public float offset = 5f;
	public string enemyTag = "Enemy";

	private float traveledDistance = 0;
	
	private Vector2 prevPos;
	private Transform ownT;
	private RaycastHit2D hit;
	private float distanceToHit = 0;
	// Use this for initialization
	void Awake () {
		ownT = GetComponent<Transform>();
	}

	void Start(){
		prevPos = transform.position;
		Destroy (gameObject,timeAlive);
	}
	
	// Update is called once per frame
	void Update () {
		//move forward
		ownT.Translate(bulletSpeed*Time.deltaTime,0,0);

		//calculate traveled distance
		traveledDistance += ((Vector2)ownT.position - prevPos).magnitude;
		prevPos=ownT.position;

		//detect collision
		if (distanceToHit!=0 && traveledDistance+offset >= distanceToHit && hit){

			//COLLIDED!!!
			if(hit.transform.tag == enemyTag){
				Destroy(gameObject,0);
				Destroy(hit.transform.gameObject,0); // ENEMY KILL!! To be modified for a better kill implementation (gamemaster script?)
			} else if(hit.transform.tag == "Solid"){
				Destroy(gameObject,0);
			}


			distanceToHit = 0;
		}

	}

	public void setCollision(RaycastHit2D hit){
		this.hit = hit;
		distanceToHit = hit.distance;
	}

	
}
