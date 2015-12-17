using UnityEngine;
using System.Collections;

public class humanTransform : MonoBehaviour {
	
	private int tp;
	private int maxtp;
	public bool isTransform;
	public GameObject Zombie;
	
	
	void Awake () {
		
		maxtp = 10;
		tp = 0;		
		isTransform = false;
	}
	
	void Update () {
		//Debug.Log (health);
	}
	
	/*public void GetDamage(int damage){
		
		health -= damage;
		if(health <= 0 && !isDead){
			Die();
			//		Application.LoadLevel(Application.loadedLevel);
		}
	 }*/
	
	
	
	public void AddTp(int boost){
		
		tp += boost;
		if(tp >= maxtp){
			//	health = maxHealth;		
			Transformation();
		}
		
	}
	
	void Transformation(){
		
		isTransform = true;
		GameObject tmpZombie=(GameObject) Instantiate (Zombie, transform.position, transform.rotation);
		gameObject.GetComponent<zombie> ().activeZombie = 0;
		Destroy(gameObject);
		
	}
}