using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*relies on some means of selecting units, then does stuff with them.*/
public class SelectionDisplay : MonoBehaviour {

	private List<GameObject> selectedUnits=new List<GameObject> ();

	public void onSelectedUnits(List<GameObject> newSelectedUnits){
		//If nothing is in the selection, break early
		deselectUnits ();
		if (newSelectedUnits.Count != 0) {
			this.selectedUnits = newSelectedUnits;
			foreach (GameObject unit in this.selectedUnits) {
				Unit unitScript = unit.GetComponent<Unit> ();
				unitScript.onSelected ();
			}
		}
	}

	public void deselectUnits(){
		foreach (GameObject unit in selectedUnits) {
			Unit unitScript = unit.GetComponent<Unit> ();
			unitScript.onDeselected ();
		}
	}
	public void displaySelectedUnits( List<GameObject> theSelectedUnits){
		string toDisplay = "[";
		foreach(GameObject unitObj in theSelectedUnits){
			toDisplay += unitObj.name + ", ";
		}
		toDisplay+="]";
		Debug.Log (toDisplay);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("e")) {
			displaySelectedUnits (selectedUnits);
		}
	}
}
