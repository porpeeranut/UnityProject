using UnityEngine;
using System.Collections;

public class firstGun : MonoBehaviour {

	public GameObject bullet;
	private GameObject player;
	public int magazine;

	void Start () {
	//	InvokeRepeating("Shooting",1.0f,0.1f);
		magazine = 50;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		if (player == null) {
			Debug.Log("playernull camera");
			player = GameObject.FindGameObjectWithTag("Player");
		}
		if (player.GetComponent<PlayerControl> ().aim) {
			if(Input.GetMouseButtonDown(0)){

				Shooting ();

			}
		
		}

	}

	void  Shooting(){
		if ( magazine > 0 ){
		RaycastHit screenRayInfo;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0)), out screenRayInfo, 500)) {
			transform.LookAt(screenRayInfo.point);
			GameObject gunBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
				magazine -= 1;
			}
	}
		else {
			
			Debug.Log("Relaod");
}
}

	void addMagazine(int tmp){
		magazine = + tmp;

	}
}