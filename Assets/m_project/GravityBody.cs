using UnityEngine;
using System;
using System.Collections;
using AssemblyCSharp;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {

	public GameObject[] atractors;

	GravityAttractor planet;
	new Rigidbody rigidbody;

	void Awake () {
		selectPlanet ();

		rigidbody = GetComponent<Rigidbody> ();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity
		selectPlanet();
		planet.Attract(rigidbody);
	}

	void selectPlanet(){
		atractors = GameObject.FindGameObjectsWithTag ("Planet");

		GameObject atractor = Utils.fold<GameObject>(
			(first, second) => {
				float distanceFirst = Vector3.Distance(first.transform.position, this.transform.position);
				float distanceSecond = Vector3.Distance(second.transform.position, this.transform.position);

				if(distanceFirst>distanceSecond){
					return second;
				}else{
					return first;
				}
			}, atractors);


		planet = atractor.GetComponent<GravityAttractor>();
	}
}