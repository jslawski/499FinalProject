using UnityEngine;
using System.Collections;

public class ChestOpenZone : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			//JPS:   I really want to change this so that it only has to generate text once,
			//		 but putting GenerateText() in Start() might yield unreliable results in terms
			//		 of when certain values in thisEquipment are available.
			Vector3 textPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2); //JPS: Arbitrary 2 offset for now
			GameManager.S.DisplayEquipmentText(textPosition, "Press O to Open!");
		}
	}

	//Player can open the chest as long as they are in the "open" zone
	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (Input.GetKeyDown(KeyCode.O)) {
				GameManager.S.DestroyEquipmentText();
				GetComponentInParent<Chest>().GenerateTreasure();
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.S.DestroyEquipmentText();
		}
	}
}
