using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapNode : MonoBehaviour {

	public List<GameObject> connectedNodes;
	public List<GameObject> units;
	private List<LineRenderer> connectionDrawers;

	// Use this for initialization
	void Start () {
		connectionDrawers = new List<LineRenderer> ();
		int i = 0;
		foreach (GameObject el in connectedNodes) {
			GameObject child = new GameObject ();
			child.name = "Renderer " + i.ToString ();
			child.transform.parent = transform;

			LineRenderer connectionDrawer = child.AddComponent<LineRenderer> ();
			connectionDrawer.material.color = Color.black;	
			connectionDrawer.sortingOrder = 1;
			connectionDrawer.SetWidth (0.02F, 0.02F);
			connectionDrawer.SetVertexCount (2);
			connectionDrawers.Add (connectionDrawer);
			showConnection (el,connectionDrawer);

			i += 1;
		}

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

	public GameObject returnEnemyUnit(int attackerFaction){
		GameObject toReturn = null;
		foreach (GameObject unitObj in units) {
			if (unitObj.GetComponent<Unit> ().faction != attackerFaction) {
				return unitObj;
			}
		}
		return toReturn;
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

	public List<GameObject> getNeighbors(){
		return connectedNodes;
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

	public void showConnection(GameObject obj,LineRenderer connectionDrawer){
		if (obj != null) {
			connectionDrawer.SetPosition (0, transform.position);
			connectionDrawer.SetPosition (1, obj.transform.position);
		}
	}
}
