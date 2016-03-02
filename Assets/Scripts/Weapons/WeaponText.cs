using UnityEngine;
using System.Collections;

public class WeaponText : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		StartCoroutine("BounceText");
	}

	IEnumerator BounceText() {
		float moveSpeed = 0.01f;
		int framesBeforeDirectionChange = 25;
		while (true) {
			for(int i = 0; i < framesBeforeDirectionChange; i++) {
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed);
				yield return null;
			}
			moveSpeed *= -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.forward = Camera.main.transform.forward;
	}
}
