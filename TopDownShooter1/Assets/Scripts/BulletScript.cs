using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float bulletSpeed = 120f;
	public float timeAlive = 1f;
	

	private Transform ownT;
	// Use this for initialization
	void Awake () {
		ownT = GetComponent<Transform>();
	}

	void Start(){

		Destroy (gameObject,timeAlive);
	}
	
	// Update is called once per frame
	void Update () {
		ownT.Translate(bulletSpeed*Time.deltaTime,0,0);


	}
	
}
