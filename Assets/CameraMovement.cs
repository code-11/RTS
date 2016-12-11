using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		int rate = 6;
		if (Input.GetKey ("w")) {
			transform.position += new Vector3 (0, rate, 0) * Time.fixedDeltaTime;
		}if (Input.GetKey ("d")) {
			transform.position += new Vector3 (rate,0,0)* Time.fixedDeltaTime;
		}if (Input.GetKey ("s")) {
			transform.position += new Vector3 (0,-rate,0)* Time.fixedDeltaTime;
		}if (Input.GetKey ("a")) {
			transform.position += new Vector3 (-rate,0,0)* Time.fixedDeltaTime;
		}
		float scrollWheelVel = Input.GetAxis ("Mouse ScrollWheel");
		if (scrollWheelVel != 0) {
			int zoomMultiplier = 40;
			var camera = Camera.main;
			float newCameraSize = (camera.orthographicSize - scrollWheelVel * zoomMultiplier * Time.fixedDeltaTime);
			if (newCameraSize> .1) {
				camera.orthographicSize = newCameraSize;
			}
		}
	}
}
