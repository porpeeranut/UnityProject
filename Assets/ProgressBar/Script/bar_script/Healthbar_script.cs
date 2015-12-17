using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Healthbar_script : MonoBehaviour {

	public Image healthbar;
	public bool coolingDown;
	public float waitTime = 30.0f;
	private GameObject playerObject;
	private float healthratio;
	void Start() {

		playerObject = GameObject.FindGameObjectWithTag("playerstatus");
		healthratio = playerObject.GetComponent<PlayerHealth>().maxHealth;
	}


	// Update is called once per frame
	void Update () 
	{

			//Reduce fill amount over 30 secondss
		healthbar.fillAmount = playerObject.GetComponent<PlayerHealth> ().health / healthratio;
	 if (healthbar.fillAmount < 0.2f) 
		{
			healthbar.color = Color.red;
		} else 
		{
			healthbar.color= Color.blue;
		}
	}
}
