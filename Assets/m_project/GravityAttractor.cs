using UnityEngine;
using System.Collections;


public class GravityAttractor : MonoBehaviour {

	public float gravity = 9.8f;
	public float gravityRange = 6;
	public float planetSurface = 4;
	public float atmosphereRadiouse = 10;

	void Awake () {
		tag = "Planet";
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


}
