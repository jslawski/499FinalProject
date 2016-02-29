using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusEffect : MonoBehaviour {
	public Character thisCharacter;				//The character this status effect is associate with
	public GameObject statusEffectIconPrefab;	//Used to instantiate the statusEffectIcon
	protected Image statusEffectIcon;			//The UI image showing the remaining time on the status effect

	protected float duration;
	protected float timeRemaining;

	// Use this for initialization
	protected virtual void Start () {
		thisCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;

		//Create a separate instance of the material to change the _Percent value on
		//Without this, it affects the global material, thus changing every status effect icon
		Material mat = Instantiate(statusEffectIcon.material);
		mat.SetFloat("_Percent", 1 - (timeRemaining / duration));
		statusEffectIcon.material = mat;

		if (timeRemaining <= 0) {
			EffectExpire();
		}
	}

	virtual protected void EffectExpire() {
		thisCharacter.statusEffects.Remove(this);
		Destroy(this);
	}
}
