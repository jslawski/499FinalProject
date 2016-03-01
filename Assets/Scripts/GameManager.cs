using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager S;			//Singleton reference

	public GameObject damageTextPrefab;		//Used to instantiate damage display

	// Use this for initialization
	void Awake () {
		S = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Creates a new instance of damage text and tells the damage text to display the damage done
	public void DisplayDamageText(Vector3 position, float damageDone) {
		GameObject newDamageText = Instantiate(damageTextPrefab, position, new Quaternion()) as GameObject;
		DamageDisplay damageDisplay = newDamageText.GetComponent<DamageDisplay>();
		damageDisplay.SetDamageValue(damageDone);
	}
}
