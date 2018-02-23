using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentryAI : MonoBehaviour {

	PickUpController PickUpController;

	private NavMeshAgent agent;
	public Transform player;
	public Transform tree;
	public bool holding;


	void Start () {

		PickUpController = GameObject.Find("Player").GetComponent<PickUpController>();
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {

		holding = PickUpController.carrying;

		if(holding) {
			agent.SetDestination(tree.position);
		}
		else {
			agent.SetDestination(player.position);
		}
	}
}
