using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	public Character thisPlayer;
	public Text manaText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		manaText.text = ((int)thisPlayer.mana).ToString() + "/" + ((int)thisPlayer.maxMana).ToString();
	}
}
