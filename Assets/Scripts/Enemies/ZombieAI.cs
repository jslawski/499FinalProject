using UnityEngine;
using System.Collections;

//Utilizes the NavMeshAgent component on the enemy to navigate towards the player's position
public class ZombieAI : MonoBehaviour {
	NavMeshAgent agent;
    public Transform goal;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = goal.position;
	}
}
