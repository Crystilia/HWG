using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

	GameObject mainCamera;
	GameObject carriedObject;
	public Transform guide;
	public bool carrying;
	public float distance;
	public float smooth;
	public float throwForce = 10;



	void Start () {

		mainCamera = GameObject.FindWithTag("MainCamera");
	}

	void Update () {

		if(carrying) {
			carry(carriedObject);
			checkDrop();
		}
		else {
			pickup();
		}
	}

	void carry (GameObject carriedObject) {

		carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
	}

	void pickup () {

		if(Input.GetKeyDown(KeyCode.Mouse0)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null) {
					carrying = true;
					Debug.Log("Carrying");
					carriedObject = p.gameObject;
					p.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}
	}

	void checkDrop() {

		if(Input.GetKeyDown(KeyCode.Mouse0)) {
			dropObject();
		}
	}

	void dropObject() {

		carrying = false;
		carriedObject.GetComponent<Rigidbody>().isKinematic = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * throwForce;
		carriedObject = null;
	}
}
