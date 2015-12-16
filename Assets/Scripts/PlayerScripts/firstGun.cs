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
		RaycastHit screenRayInfo;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0)), out screenRayInfo, 500)) {
			transform.LookAt(screenRayInfo.point);
			GameObject gunBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
			gunBullet.GetComponent<Rigidbody> ().velocity = transform.forward * initialSpeed;
		}
	}
}