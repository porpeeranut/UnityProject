using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	float startTime;
	float lifeTime;
	float timer;
	public float initialSpeed;
	public int damage;

	void Start () {

			lifeTime = 2.0f;
			startTime = Time.time;
			GetComponent<Rigidbody> ().velocity = transform.forward * initialSpeed;

		}

	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider obj){
		if((obj.gameObject.CompareTag("Human")) || (obj.gameObject.CompareTag("Soldier"))){
			obj.GetComponent<EnemyHealth>().GetDamage(damage);
		}
		if (obj.gameObject.CompareTag ("Soldier")) {
			obj.GetComponent<SoldierController>().foundPlayer(true);
		}
		Destroy(gameObject);
	}
}
