using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphMaster : MonoBehaviour {

	private MapNode root;

	//Testing properties, remove later
	public Unit unit;
	public GameObject start;
	public GameObject end;


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
		if (Input.GetKeyDown ("t")) {
			PathTo (start, end);
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


	public string displayPath(Queue<GameObject> path){
		string toDisplay="[";
		foreach (GameObject el in path){
			toDisplay+=el+",";
		}
		toDisplay+="]";
		return toDisplay;
	}

	public Queue<GameObject> PathTo(GameObject start,GameObject end){
		Queue<GameObject> thePath=GraphSearch (start, new Queue<GameObject> (), end);
		// Do a trick to remove the first element
		List<GameObject> theListPath=new List<GameObject>(thePath); 
		theListPath.RemoveAt (0);
		Queue<GameObject> pathWithoutStart = new Queue<GameObject> ();
		foreach(GameObject el in theListPath){
			if (el != null) {
				pathWithoutStart.Enqueue (el);
			}
		}
		return pathWithoutStart;
		//Debug.Log(displayPath (thePath));
	}

	public Queue<GameObject> GraphSearch(GameObject curLoc, Queue<GameObject> accumPath,GameObject goal){
		accumPath = new Queue<GameObject> (accumPath);
		accumPath.Enqueue (curLoc);
		if (goal == curLoc) {
			return accumPath;
		} else {
			int shortestPathLength = int.MaxValue;
			Queue<GameObject> shortestPath = new Queue<GameObject>();
			foreach (GameObject neighbor in curLoc.GetComponent<MapNode>().getNeighbors()) {
				//If I've been there before, don't go there again
				if (!accumPath.Contains (neighbor)) {
					//Debug.Log("In "+curLoc.name+" looking at "+neighbor.name+" "+displayPath(accumPath));
					Queue<GameObject> possiblePath = GraphSearch (neighbor, accumPath, goal);
					if (possiblePath.Count>0 && possiblePath.Count<shortestPathLength) {
						shortestPath = possiblePath;
						shortestPathLength = possiblePath.Count;
					}
				}
			}
			return shortestPath;
		}
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
