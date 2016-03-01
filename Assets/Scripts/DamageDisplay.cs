using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour {
	public Gradient damageColor;					//Higher damage == far end of this gradient
	float minColorDamage = 10f;					//Amount of damage done that corresponds to Gradient(0)
	float maxColorDamage = 250f;					//Amount of damage done that corresponds to Gradient(1)

	TextMesh damageText;

	float lifetime = 1f;							//Number of seconds the damage text stays around
	float distanceTraveledUp = 1f;					//Distance the damage text lists upward
	float distanceTraveledSide = 0.25f;				//Distance the damage text waves back and forth
	float verticalOffsetVariation = 0.5f;			//Distance above or below standard the damage text can spawn

	// Use this for initialization
	void Awake () {
		damageText = GetComponent<TextMesh>();
	}

	IEnumerator Start() {
		float timeElapsed = 0;
		float verticalOffset = Random.Range(-verticalOffsetVariation, verticalOffsetVariation);
		float waveOffset = Random.Range(0f, 1f);

		Vector3 startPos = transform.position;
		Color startColor = damageText.color;
		Color endColor = startColor;
		endColor.a = 0;

		while (timeElapsed < lifetime) {
			float percent = timeElapsed/lifetime;

			Vector3 curPos = transform.position;
			//Text should travel upwards
			curPos.y = Mathf.Lerp(startPos.y + verticalOffset, startPos.y + distanceTraveledUp, percent);
			//Text should wave back and forth
			curPos.x = startPos.x + distanceTraveledSide * Mathf.Sin((waveOffset + percent)*Mathf.PI);
			transform.position = curPos;

			//Text should fade away
			damageText.color = Color.Lerp(startColor, endColor, percent);

			timeElapsed += Time.deltaTime;
			yield return 0;
		}
		//Destroy the damage text after lifetime seconds have passed
		Destroy(this.gameObject);
	}

	public void SetDamageValue(float damageDone) {
		damageText.text = "-" + Mathf.Round(damageDone).ToString();
		damageText.color = damageColor.Evaluate(Mathf.InverseLerp(minColorDamage, maxColorDamage, damageDone));
    }
	
	// Update is called once per frame
	void Update () {
		transform.forward = Camera.main.transform.forward;
	}
}
