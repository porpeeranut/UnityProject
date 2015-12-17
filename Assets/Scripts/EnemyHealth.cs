using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public int maxHealth;
	public bool isDead;
	
	
	void Awake () {
		health = maxHealth;
		isDead = false;
	}
	
	void Update () {
		//Debug.Log (health);
	}
	
	public void GetDamage(int damage){
		
		health -= damage;
		if(health <= 0 && !isDead){
			Die();
			//		Application.LoadLevel(Application.loadedLevel);
		}
		
	}
	
	public void AddHealth(int boost){
		
		health += boost;
		if(health >= maxHealth){
			health = maxHealth;		}
		
	}
	
	void Die(){
		
		Destroy(gameObject);
		
	}
}
