using UnityEngine;
using System.Collections;

public class Tphuman : MonoBehaviour {

	public int tp;
	public int maxtp;
	public bool isTransform;
	
	
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
		
		Destroy(gameObject);
		
	}
}
