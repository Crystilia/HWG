using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentryAI : MonoBehaviour {

	PickUpController PickUpController;

	GameObject sentryCamera;
	GameObject carriedObject;
	private NavMeshAgent agent;
	public Transform player;
	public Transform sphere;
	public Transform dropSpot;
	public bool carrying;
	public bool holding;
	public float distance;
	public float smooth;
	public float throwForce = 10;


	void Start () {

		sentryCamera = GameObject.FindWithTag("SentryCamera");
		PickUpController = GameObject.Find("PlayerCamera").GetComponent<PickUpController>();
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {

		holding = PickUpController.carrying;

		if(holding) {
			agent.SetDestination(player.position);
		}
		else if (carrying) {
			carry(carriedObject);
			agent.SetDestination(dropSpot.position);

			if (!agent.pathPending)
			{
				if (agent.remainingDistance <= agent.stoppingDistance)
				{
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
					{
						dropItem();
					}
				}
			}
		}
		else {
			agent.SetDestination(sphere.position);
		}
	}

	void OnTriggerEnter () {

		Pickupable p = sphere.GetComponent<Pickupable>();
		if(p != null) {
			carrying = true;
			Debug.Log("Agent Carrying");
			carriedObject = p.gameObject;
			p.GetComponent<Rigidbody>().isKinematic = true;
	}
}

	void carry (GameObject carriedObject) {

		carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, sentryCamera.transform.position + sentryCamera.transform.forward * distance, Time.deltaTime * smooth);
	}

	void dropItem() {

		carrying = false;
		carriedObject.GetComponent<Rigidbody>().isKinematic = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * throwForce;
		carriedObject = null;
	}
}
