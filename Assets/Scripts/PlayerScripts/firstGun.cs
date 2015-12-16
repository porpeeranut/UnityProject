using UnityEngine;
using System.Collections;

public class firstGun : MonoBehaviour {

	public GameObject bullet;
	public float initialSpeed;

	
	void Start () {	
		
		InvokeRepeating("Shooting",1.0f,0.1f);
		initialSpeed = 50.0f;

	}
	
	
	void  Shooting(){

		Vector3 tmpVec = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		//tmpVec += transform.up * 0.5f;
		GameObject gunBullet = null;
		gunBullet = (GameObject)Instantiate(bullet, tmpVec, transform.rotation);
		gunBullet.GetComponent<Rigidbody> ().velocity = transform.forward * initialSpeed;
	}
}

