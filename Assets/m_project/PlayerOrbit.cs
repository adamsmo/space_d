using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlayerOrbit : MonoBehaviour {

	GameObject sun;

	void Awake () {
		findSun();
	}

	void FixedUpdate () {
		GameObject[] orbitings = GameObject.FindGameObjectsWithTag ("Orbiting");
		GameObject nearestOrbiting = Utils.getNearestGameObject (orbitings, this.transform);
		float planetSpeed = nearestOrbiting.GetComponent<PlanetOrbiting> ().planetSpeed;


		GameObject[] atractors = GameObject.FindGameObjectsWithTag ("Planet");
		GameObject atractor = Utils.getNearestGameObject (atractors, this.transform);
		GravityAttractor planet = atractor.GetComponent<GravityAttractor>();



		float distance = Vector3.Distance (planet.transform.position, this.transform.position);

		if(distance < planet.atmosphereRadiouse){
			Debug.Log ("rotated");
			transform.RotateAround (sun.transform.position, sun.transform.up, planetSpeed);
		}
	}

	void findSun(){
		sun = GameObject.FindGameObjectWithTag ("Sun");
	}

}
