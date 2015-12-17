using UnityEngine;
using System.Collections;

public class SpawnHpItem : MonoBehaviour {

	public GameObject spawnHP;

	public float delay;

	void Start () {
	
		StartCoroutine("SpawnBall",delay);
	}
	IEnumerator SpawnBall(float delayTime){
		while(true){

			float offsetX = Random.Range(-45.0f,45.0f);
			float offsetZ = Random.Range(-45.0f,45.0f);
			Vector3 spawnPosition = new 
				Vector3(transform.position.x+offsetX,transform.position.y,transform.position.z+offsetZ);
			Instantiate(spawnHP,spawnPosition,Quaternion.identity);
			yield return new WaitForSeconds(delayTime);
		}
	}
	void Update () {

	}
}
