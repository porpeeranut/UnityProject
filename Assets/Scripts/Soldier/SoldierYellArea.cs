using UnityEngine;
using System.Collections;

public class SoldierYellArea : MonoBehaviour {

	float startTime;
	float lifeTime;
	float timer;
	
	void Start () {
		lifeTime = 0.5f;
		startTime = Time.time;
	}
	
	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider obj){
		if (obj.gameObject.CompareTag ("Soldier")) {
			obj.GetComponent<SoldierController>().foundPlayer(true);
		}
	}
}
