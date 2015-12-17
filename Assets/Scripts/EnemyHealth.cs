using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public int maxHealth;
	public bool isDead;
	Animator animator;

	void Awake () {
		health = maxHealth;
		isDead = false;
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		//Debug.Log (health);
	}
	
	public void GetDamage(int damage){
		health -= damage;
		if (health < 0)
			health = 0;
		if(health == 0 && !isDead){
			Die();
			//		Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	public void AddHealth(int boost){
		health += boost;
		if(health >= maxHealth){
			health = maxHealth;
		}
	}
	
	void Die(){
		animator.SetBool ("die", true);
		isDead = true;
		//Destroy(gameObject);
	}
}
