using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int health;
	public int maxHealth;
	public bool isDead;
	private Animator playerAnimator;

	void Awake () {
		playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		maxHealth = 100;
		health = maxHealth;
		isDead = false;
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
		playerAnimator.SetBool ("Die", true);
		isDead = true;
		//Destroy(gameObject);
	}
}
