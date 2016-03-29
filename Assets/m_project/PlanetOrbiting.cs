using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlanetOrbiting : MonoBehaviour {

	public float planetSpeed = 2;

	GameObject sun;

	void Awake () {
		findSun();
		tag = "Orbiting";
	}

	void FixedUpdate () {
		transform.RotateAround (sun.transform.position, sun.transform.up, planetSpeed);
	}

	void OnDrawGizmosSelected() {
		findSun ();
		Handles.color = Color.red;
		float distance = Vector3.Distance (sun.transform.position, transform.position);

		Handles.DrawWireDisc(sun.transform.position, sun.transform.up, distance);
	}
		
	void findSun(){
		sun = GameObject.FindGameObjectWithTag ("Sun");
	}
}
