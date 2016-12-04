using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapNode : MonoBehaviour {

	public List<GameObject> connectedNodes;
	public List<GameObject> units;
	private LineRenderer connectionDrawer;

	// Use this for initialization
	void Start () {
		connectionDrawer = gameObject.AddComponent<LineRenderer> ();
		connectionDrawer.material.color = Color.black;	
		connectionDrawer.sortingOrder = 1;
		connectionDrawer.SetWidth (0.02F, 0.02F);
		connectionDrawer.SetVertexCount (2);
		showConnections ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool hasConnectionTo(GameObject other){
		return connectedNodes.Contains(other);
	}

	public bool hasUnit(GameObject someUnit){
		return units.Contains (someUnit);
	}

	public void forgetUnit(GameObject someUnit){
		units.Remove (someUnit);
	}

	public void learnUnit(GameObject someUnit){
		units.Add (someUnit);
	}

	public GameObject randomNeighbor(){
		int randIndex = Random.Range (0, connectedNodes.Count);
		return connectedNodes [randIndex];
	}

	public bool unitLocationConsistancyCheck(){
		foreach (GameObject obj in units) {
			if (obj == null) {
				continue;
			}
			if (!obj.GetComponent<Unit> ().location == this) {
				Debug.Log ("Location" + obj + " had a reference to unit " + obj + " but it didn't");
				return false;
			}
		}
		return true;
	}

	public bool graphConsistancyCheck(){
		foreach(GameObject obj in connectedNodes){
			if (obj == null) {
				continue;
			}
			if (!(obj.GetComponent<MapNode>()).hasConnectionTo (gameObject)) {
				Debug.Log (obj + " should have a connection to " + gameObject + " but doesn't");
				return false;
			}
		}
		return true;
	}

	public void showConnections(){
		foreach(GameObject obj in connectedNodes){
			if(obj ==null){
				continue;
			}
			connectionDrawer.SetPosition (0, transform.position);
			connectionDrawer.SetPosition (1, obj.transform.position);
		}
	}
}
