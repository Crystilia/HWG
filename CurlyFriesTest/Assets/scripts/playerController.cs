using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public GameObject Player;
	public float speed;
	public float moveV;
	public float mouseSensitivity;
	public float clampAngle;

	private Rigidbody rb;
	private float rotX= 0.0f;
	private float rotY= 0.0f;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}


	void Update () 
	{

		Movement ();
		Look ();
	}

	void Movement()
	{
		if (Input.GetKey ("d") || Input.GetKey ("right")) 
		{
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}

		if (Input.GetKey ("a") || Input.GetKey ("left")) 
		{
			transform.Translate (Vector3.left * speed* Time.deltaTime);
		}

		if (Input.GetKey ("w") || Input.GetKey ("up")) 
		{
			transform.Translate (Vector3.forward * speed* Time.deltaTime);
		}

		if (Input.GetKey ("s") || Input.GetKey ("down")) 
		{
			transform.Translate (Vector3.back * speed* Time.deltaTime);
		}
	}

	void Look()
	{
		float mouseX = Input.GetAxis ("Mouse X");
		float mouseY = Input.GetAxis ("Mouse Y");

		rotY += mouseX * mouseSensitivity * Time.deltaTime;
		rotX += mouseY * mouseSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp (rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
		transform.rotation = localRotation;
	}


}
