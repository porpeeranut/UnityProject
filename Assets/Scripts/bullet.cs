using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	float startTime;
	float lifeTime;
	float timer;
	public float initialSpeed;

	void Start () {
		lifeTime = 1.5f;
		startTime = Time.time;
		GetComponent<Rigidbody> ().velocity = transform.forward * initialSpeed;
	}

	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider obj){
<<<<<<< HEAD
		if((obj.gameObject.CompareTag("Human")) || (obj.gameObject.CompareTag("Solider"))){
=======
		Debug.Log ("ttt");
		if((obj.gameObject.CompareTag("Human")) || (obj.gameObject.CompareTag("Soldier"))){
>>>>>>> 761bd4b2f2b299425ddeaf585e7c55f34a7f83bd
			obj.GetComponent<EnemyHealth>().GetDamage(2);
		}
		if (obj.gameObject.CompareTag ("Soldier")) {
			obj.GetComponent<SoldierController>().foundPlayer();
		}
		Destroy(gameObject);
	}
}
