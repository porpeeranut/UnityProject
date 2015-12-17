using UnityEngine;
using System.Collections;

public class increaseMag : MonoBehaviour {

	// Use this for initialization
	public GameObject gunMagazine;
	void Start () {
		gunMagazine = GameObject.FindGameObjectWithTag ("spawnBullet");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider obj){
	
		if (obj.gameObject.CompareTag ("Player")) {
			gunMagazine.GetComponent<firstGun>().addMagazine(100);
			gunMagazine.GetComponent<firstGun>().isBullet2 = true;
		}

		//Instantiate(particleEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
