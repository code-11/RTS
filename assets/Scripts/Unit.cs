using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public MapNode location;
	private GameObject selectionHaloObj;
	public Sprite haloSprite;
	private Queue<GameObject> goal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		evalOrder ();
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

	public void onSelected(){
		//Only allow to be selected once
		if (transform.childCount == 0) {
			selectionHaloObj = new GameObject (); 
			//Center on parent
			selectionHaloObj.name = "Selection Halo";
			selectionHaloObj.transform.SetParent (gameObject.transform);
			selectionHaloObj.transform.localPosition = new Vector3 (0, 0, 0);
			selectionHaloObj.transform.localScale = new Vector3 (1, 1, 1);
			selectionHaloObj.AddComponent<SpriteRenderer> ();
			SpriteRenderer haloRenderer = selectionHaloObj.GetComponent<SpriteRenderer> ();
			haloRenderer.sprite = haloSprite;
			haloRenderer.color = Color.green;
		} else {
			Debug.Log (gameObject.name + " already has a halo");
			Debug.Log (gameObject.transform.GetChild (0).name);
		}
	}

	public void onDeselected(){
		if (transform.childCount >= 1) {
			selectionHaloObj.transform.parent = null;
			Destroy (selectionHaloObj);
		}
	}

	//Moves a unit to an adjacent node
	public void moveTo(GameObject newLocation){
		MapNode curLocation = location;

		curLocation.forgetUnit (gameObject);
		setLocation (newLocation.GetComponent<MapNode>());
		newLocation.GetComponent<MapNode>().learnUnit (gameObject);
		gameObject.transform.position= newLocation.transform.position;
	}

	private void evalOrder(){
		if (goal!=null && goal.Count>0){
			moveTo(goal.Dequeue());
		}
	}

	public void setOrders(Queue<GameObject> order){
		goal=order;
	}

	public void setLocation(MapNode newNode){
		location = newNode;
	}

	public MapNode getLocation(){
		return location;
	}
}
