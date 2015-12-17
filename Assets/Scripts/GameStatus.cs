using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

	void Update () {
		if (GameObject.FindGameObjectWithTag ("Human") == null) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool ("victory", true);
			Invoke("restartGame", 10f);
		}
	}

	void restartGame () {
		Application.LoadLevel("Home");
	}
}
