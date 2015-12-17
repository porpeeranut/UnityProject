using UnityEngine;
using System.Collections;

public class firstGun : MonoBehaviour {

	public GameObject bullet;
	public GameObject bullet2;
	public bool isBullet2 = false;
	private GameObject player;
	public float magazine; 
	public float maxMag;
	public AudioClip audioClip;
	AudioSource magazineSound;

	void Awake () {
	//	InvokeRepeating("Shooting",1.0f,0.1f);
		isBullet2 = false;
		maxMag = 100;
		magazine = maxMag;
		player = GameObject.FindGameObjectWithTag("Player");
		magazineSound = GetComponent<AudioSource>();
		audioClip = magazineSound.clip; 
	}

	void Update () {
//		Debug.Log (magazine);
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

	void Shooting(){
		if ( magazine > 0 ){
			RaycastHit screenRayInfo;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0)), out screenRayInfo, 500)) {
				transform.LookAt(screenRayInfo.point);
				if(isBullet2){
					GameObject gunBullet = (GameObject)Instantiate(bullet2, transform.position, transform.rotation);
				}else {
					GameObject gunBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
				}
				magazine -= 1;
			}
		} else {
			magazineSound.Play();
			Debug.Log("Relaod");
		}
	}

	public void addMagazine(int tmp){
		magazine =+ tmp;
		if (magazine > maxMag) {
			magazine = maxMag;
		}
	}

	void bullet2Time() {
		isBullet2 = false;
	}
}