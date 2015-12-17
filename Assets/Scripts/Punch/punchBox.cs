using UnityEngine;
using System.Collections;

public class punchBox : MonoBehaviour {

	float startTime;
	float lifeTime;
	float timer;
	public AudioClip audioClip;
	AudioSource punchSound;

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
		
		if((obj.gameObject.CompareTag("Human")) || (obj.gameObject.CompareTag("Soldier"))){
			obj.GetComponent<EnemyHealth>().GetDamage(1);
			punchSound = GetComponent<AudioSource>();
			audioClip = punchSound.clip; 
			punchSound.Play();	
		}
		if (obj.gameObject.CompareTag ("Soldier")) {
			obj.GetComponent<SoldierController>().foundPlayer(true);

		}
		punchSound.Play ();
		Destroy(gameObject);
	}
}
