using UnityEngine;
using System.Collections;


public class GravityAttractor : MonoBehaviour {

	public float gravity = -9.8f;
	public float gravityRange = 6;
	public float planetSurface = 4;
	public float atmosphereRadiouse = 10;

	void Awake () {
		tag = "Planet";
	}

	public void Attract(Rigidbody body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.transform.up;


		float distance = Vector3.Distance (transform.position, body.transform.position);


		// Apply downwards gravity to body
		float gravityForce;

		if(distance < gravityRange){
			gravityForce = gravity;
		} else {
			gravityForce = gravity / (1 + distance - gravityRange);
		}


		body.AddForce(gravityUp * gravityForce);


		if(distance < atmosphereRadiouse){
			atmBreak (body);
		}

		if(distance < gravityRange){
			// Allign bodies up axis with the centre of planet

			float distanceToSurface = distance - planetSurface;

			if (distanceToSurface > 0) {
				float rotationDistance = (gravityRange - planetSurface);
				float proportion = (rotationDistance - distanceToSurface) / rotationDistance;

				float angleRad = Vector3.Angle (localUp, gravityUp) * Mathf.Deg2Rad;

				Vector3 newDir = Vector3.RotateTowards(localUp, gravityUp, angleRad * proportion, 0.0F);

				body.rotation = Quaternion.FromToRotation (localUp, newDir) * body.rotation;
			} else {
				body.rotation = Quaternion.FromToRotation (localUp, gravityUp) * body.rotation;
			}
		}

	}  

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, atmosphereRadiouse);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, gravityRange);
		Gizmos.color = Color.gray;
		Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5F);
		Gizmos.DrawSphere(transform.position, planetSurface);
	}

	public void atmBreak(Rigidbody body){
		float atmBreaking = 3;
		var curSpeed = body.velocity.magnitude;
		var newSpeed = curSpeed - atmBreaking * Time.deltaTime;
		if (newSpeed < 0) {
			newSpeed = 0;
		}
		body.velocity = body.velocity.normalized * newSpeed;
	}
}
