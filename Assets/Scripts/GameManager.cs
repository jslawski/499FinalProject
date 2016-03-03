using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager S;			//Singleton reference

	public GameObject damageTextPrefab;			//Used to instantiate damage display
	public GameObject weaponTextPrefab;			//Used to instantiate weapon text
	private GameObject currentEquipmentText;	//Used to keep track of the weapon text currently being displayed

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

	//Creates a new instance of weapon text, and keeps track of the instance that is currently
	//being displayed
	public void DisplayEquipmentText(Vector3 position, string text) {
		currentEquipmentText = Instantiate(weaponTextPrefab, position, new Quaternion()) as GameObject;
		currentEquipmentText.GetComponent<TextMesh>().text = text;
	}

	//Destroy the current instance of weapon text
	public void DestroyEquipmentText() {
		Destroy(currentEquipmentText);
	}
}
