using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	public Character thisPlayer;

	public List<Image> buffIcons = new List<Image>();		//The icons showing which buffs are on the player
	public List<Image> debuffIcons = new List<Image>();		//The icons showing which debuffs are on the player

	public Text manaText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		manaText.text = ((int)thisPlayer.mana).ToString() + "/" + ((int)thisPlayer.maxMana).ToString();
	}

	public Image AddStatusEffectIcon(GameObject newIconPrefab, bool isBuff) {
		//Create the gameobject from the passed in prefab, and grab a reference to the image component
		GameObject newGO = Instantiate(newIconPrefab);

		//Make a child of the status effect bar
		newGO.transform.SetParent(transform);
		//Fix the rect that gets messed up for some reason
		RectTransform newRect = newGO.GetComponent<RectTransform>();
		newRect.offsetMax = newRect.offsetMin = Vector2.zero;

		Image newIcon = newGO.GetComponent<Image>();

		//Add it to the correct buff or debuff bar
		if (isBuff) {
			buffIcons.Add(newIcon);
		}
		else {
			debuffIcons.Add(newIcon);
		}

		//Return the newly created icon
		return newIcon;
	}
	public void RemoveStatusEffectIcon(Image iconToBeRemoved, bool wasBuff) {
		if (wasBuff) {
			if (buffIcons.Contains(iconToBeRemoved)) {
				buffIcons.Remove(iconToBeRemoved);
				Destroy(iconToBeRemoved.gameObject);
			}
		}
		else {
			if (debuffIcons.Contains(iconToBeRemoved)) {
				debuffIcons.Remove(iconToBeRemoved);
				Destroy(iconToBeRemoved.gameObject);
			}
		}
	}
}
