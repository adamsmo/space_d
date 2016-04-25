using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FaceCamera : MonoBehaviour {
	void Update(){
		//pick player not camera for camera rotation
		//pick camera for bilboard only

		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		Camera bhCamera = Utils.filter<Camera>(
			(element) => {
				return element.gameObject.name == "bh_camera";
			}
			, Camera.allCameras)[0];

		bhCamera.transform.LookAt(bhCamera.transform.position + player.transform.rotation * Vector3.down,
			player.transform.rotation * Vector3.forward);

		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.down,
			Camera.main.transform.rotation * Vector3.back);
	}
}
