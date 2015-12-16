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
		Debug.Log ("xxx");
		if((obj.gameObject.CompareTag("Human")) || (obj.gameObject.CompareTag("Solider"))){
			obj.GetComponent<EnemyHealth>().GetDamage(2);
			Destroy(gameObject);
		}
	}
}
