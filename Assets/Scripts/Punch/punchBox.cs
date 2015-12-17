using UnityEngine;
using System.Collections;

public class punchBox : MonoBehaviour {

	float startTime;
	float lifeTime;
	float timer;
	void Start () {
		lifeTime = 1.0f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider obj){
		
		if((obj.gameObject.CompareTag("Human")) || (obj.gameObject.CompareTag("Solider"))){
			obj.GetComponent<EnemyHealth>().GetDamage(1);
		}
		if (obj.gameObject.CompareTag ("Soldier")) {
			obj.GetComponent<SoldierController>().foundPlayer();
		}
		Destroy(gameObject);
	}
}
