using UnityEngine;
using System.Collections;

public class WeaponText : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		StartCoroutine("BounceText");
	}

	IEnumerator BounceText() {
		float amplitude = 0.25f;
		float timeBeforeDirectionChange = 1.5f;
		float timeElapsed = 0;

		Vector3 startPos = transform.position;

		while (true) {
			timeElapsed += Time.deltaTime;

			transform.position = startPos + Vector3.forward * amplitude * Mathf.Sin(2 * Mathf.PI * timeElapsed / timeBeforeDirectionChange);

			yield return 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.forward = Camera.main.transform.forward;
	}
}
