using UnityEngine;
using System.Collections;

public class GraphMaster : MonoBehaviour {

	private MapNode root;
	public Unit unit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) {
			checkConsistancy ();
		}
		if (Input.GetKeyDown ("e")) {
			randomlyMoveUnit ();
		}
	}

	public bool checkConsistancy(){
		MapNode[] allNodes = GameObject.FindObjectsOfType<MapNode> ();
		foreach (MapNode node in allNodes) {
			if (!node.graphConsistancyCheck ()) {
				return false;
			}
			if (!node.unitLocationConsistancyCheck ()) {
				return false;
			}
				
		} 
		Unit[] allUnits = GameObject.FindObjectsOfType<Unit>();
		foreach (Unit unit in allUnits) {
			if (!unit.checkUnitLocationConsistancy()) {
				return false;
			}
		}
		Debug.Log ("Passed consistancy check");
		return true;
	}

	public void randomlyMoveUnit(){
		MapNode curLocation = unit.getLocation ();
		GameObject newLocation = curLocation.randomNeighbor ();

		curLocation.forgetUnit (unit.gameObject);
		unit.setLocation (newLocation.GetComponent<MapNode>());
		newLocation.GetComponent<MapNode>().learnUnit (unit.gameObject);
		unit.gameObject.transform.position= newLocation.transform.position;
	}

}
