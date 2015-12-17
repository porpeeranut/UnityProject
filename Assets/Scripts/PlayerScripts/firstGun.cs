using UnityEngine;
using System.Collections;

public class firstGun : MonoBehaviour {

	public GameObject bullet;
	public GameObject bullet2;
	public bool isBullet2 = false;
	private GameObject player;
	public int magazine;

	void Start () {
	//	InvokeRepeating("Shooting",1.0f,0.1f);
		isBullet2 = true;
		magazine = 50;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		if (player == null) {
			Debug.Log("playernull camera");
			player = GameObject.FindGameObjectWithTag("Player");
		}
		if (isBullet2) {
			Invoke("bullet2Time", 10.0f);
		
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
				Debug.Log (isBullet2);
					if(isBullet2){
						GameObject gunBullet = (GameObject)Instantiate(bullet2, transform.position, transform.rotation);
					}else {
						GameObject gunBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
					}
						magazine -= 1;
				}
	} else {
			
			Debug.Log("Relaod");
			}
	}

	void addMagazine(int tmp){
		magazine =+ tmp;

	}

	void bullet2Time() {
		isBullet2 = false;

	}
}