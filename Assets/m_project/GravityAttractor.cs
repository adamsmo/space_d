using UnityEngine;
using System.Collections;


public class GravityAttractor : MonoBehaviour {

	public float gravity = -9.8f;
	public float gravityRange = 6;
	public float atmosphereRadiouse = 10;

	public void Attract(Rigidbody body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.transform.up;

		// Apply downwards gravity to body
		body.AddForce(gravityUp * gravity);


		float distance = Vector3.Distance (transform.position, body.transform.position);

		if(distance < atmosphereRadiouse){
			atmBreak (body);
		}


		// Allign bodies up axis with the centre of planet
		body.rotation = Quaternion.FromToRotation(localUp,gravityUp) * body.rotation;


	}  

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, atmosphereRadiouse);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, gravityRange);
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
