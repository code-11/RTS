using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public MapNode location;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool checkUnitLocationConsistancy(){
		if (location == null) {
			return true;
		}
		if (location.hasUnit (gameObject)) {
			return true;
		}else{
			Debug.Log(gameObject + " has location "+ location+" but that location didn't know about that unit");
			return false;
		}
	}

	public void setLocation(MapNode newNode){
		location = newNode;
	}

	public MapNode getLocation(){
		return location;
	}
}
