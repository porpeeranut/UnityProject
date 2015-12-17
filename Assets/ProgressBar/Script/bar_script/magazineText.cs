using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class magazineText : MonoBehaviour {

	// Use this for initialization


	public Text txt;
	public float gunMagazine;
	public Image magbar;
	private float magratio;

	void Start () {

		txt = gameObject.GetComponent<Text>(); 
		//txt.text = "   "+(int)gunMagazine;
		magratio = GameObject.FindGameObjectWithTag ("spawnBullet").GetComponent<firstGun>().maxMag;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (txt.text);
		gunMagazine = GameObject.FindGameObjectWithTag ("spawnBullet").GetComponent<firstGun>().magazine;
		txt.text = "   "+(int)gunMagazine;
		magbar.fillAmount = gunMagazine / magratio;
		if (magbar.fillAmount <= 0.0f) 
		{
			magbar.fillAmount=0.0f;
		}
	}
}
