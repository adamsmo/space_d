using UnityEngine;
using System.Collections;
using AssemblyCSharp;

[RequireComponent (typeof (Rigidbody))]
public class PlayerControler : MonoBehaviour {

	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 6;
	public float jumpForce = 220;
	public LayerMask groundedMask;

	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	new Rigidbody rigidbody;


	GameObject sun;

	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody> ();
		findSun();

		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void Update() {

		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);

		// Jump
		if (Input.GetButtonDown("Jump")) {
			if (grounded) {
				rigidbody.AddForce(transform.up * jumpForce);
			}
		}

		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, .5f, groundedMask)) {
			grounded = true;
		}
		else {
			grounded = false;
		}
	}

	void FixedUpdate() {
		PlanetUpdate();

		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);


		// jetpack
		if (Input.GetButton("Submit") && rigidbody.velocity.magnitude < 2) {
			rigidbody.AddForce(transform.up.normalized * 15);
		}
	}

	void PlanetUpdate () {
		GameObject[] orbitings = GameObject.FindGameObjectsWithTag ("Orbiting");
		GameObject nearestOrbiting = Utils.getNearestGameObject (orbitings, this.transform);
		float planetSpeed = nearestOrbiting.GetComponent<PlanetOrbiting> ().planetSpeed;


		GameObject[] atractors = GameObject.FindGameObjectsWithTag ("Planet");
		GameObject atractor = Utils.getNearestGameObject (atractors, this.transform);
		GravityAttractor planet = atractor.GetComponent<GravityAttractor>();



		float distance = Vector3.Distance (planet.transform.position, this.transform.position);

		if(distance < planet.atmosphereRadiouse){
			transform.RotateAround (sun.transform.position, sun.transform.up, planetSpeed);
		}

		planetGravity (planet);
	}

	void planetGravity(GravityAttractor planet){
		Rigidbody body = this.rigidbody;
		Vector3 gravityUp = (body.position - planet.transform.position).normalized;
		Vector3 localUp = body.transform.up;
		float distance = Vector3.Distance (planet.transform.position, this.transform.position);

		if(distance < planet.gravityRange){
			// Allign bodies up axis with the centre of planet

			float distanceToSurface = distance - planet.planetSurface;

			if (distanceToSurface > 0) {
				float rotationDistance = (planet.gravityRange - planet.planetSurface);
				float proportion = (rotationDistance - distanceToSurface) / rotationDistance;

				float angleRad = Vector3.Angle (localUp, gravityUp) * Mathf.Deg2Rad;

				Vector3 newDir = Vector3.RotateTowards(localUp, gravityUp, angleRad * proportion, 0.0F);

				body.rotation = Quaternion.FromToRotation (localUp, newDir) * body.rotation;
			} else {
				body.rotation = Quaternion.FromToRotation (localUp, gravityUp) * body.rotation;
			}
		}

		float gravityForce;

		if(distance < planet.gravityRange){
			gravityForce = planet.gravity;
		} else {
			gravityForce = planet.gravity / (1 + distance - planet.gravityRange);
		}


		body.AddForce(gravityUp * gravityForce);


		if(distance < planet.atmosphereRadiouse){
			atmBreak (body);
		}
	}

	public void atmBreak(Rigidbody body){
		float atmBreaking = 0.025f;
		var curSpeed = body.velocity.magnitude;
		var newSpeed = curSpeed - atmBreaking;
		if (newSpeed < 0) {
			newSpeed = 0;
		}
		body.velocity = body.velocity.normalized * newSpeed;
	}

	void findSun(){
		sun = GameObject.FindGameObjectWithTag ("Sun");
	}
}