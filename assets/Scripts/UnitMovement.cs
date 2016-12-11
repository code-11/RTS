using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitMovement : MonoBehaviour {

	SelectionDisplay selector;
	GraphMaster underGraph;
	// Use this for initialization
	void Start () {
		selector = GetComponent<SelectionDisplay> ();
		underGraph =GameObject.FindObjectOfType<GraphMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1) && selector.getSelectedUnits ().Count > 0) {
			RaycastHit2D hit = new RaycastHit2D ();
			Ray ray= Camera.main.ScreenPointToRay (Input.mousePosition);
			//Debug.Log ("performing raycast with "+ray);

			hit = Physics2D.GetRayIntersection (ray, 100.0f);
			if(hit.collider!=null){
				foreach (GameObject unitObj in selector.getSelectedUnits()) {
					Unit unit = unitObj.GetComponent<Unit> ();
					GameObject hitObj = hit.collider.gameObject;
					GameObject locObj = unit.getLocation ().gameObject;
					Queue <GameObject> path = underGraph.PathTo (locObj,hitObj );
					unit.setOrders (path);
				}
			}
		}
	}
}
