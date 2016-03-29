﻿using UnityEngine;
using System;
using System.Collections;
using AssemblyCSharp;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {

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
		GameObject[] atractors = GameObject.FindGameObjectsWithTag ("Planet");

		GameObject atractor = Utils.getNearestGameObject (atractors, this.transform);

		planet = atractor.GetComponent<GravityAttractor>();
	}
}