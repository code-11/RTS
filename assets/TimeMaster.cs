using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeMaster : MonoBehaviour {

	public float tickIntervalTime;
	private int ticks=0;

	List<Tickable> subscribed=new List<Tickable>(); //will call the discreteUpdate funciton of all objects in this list on every tick.

	// Use this for initialization
	void Start () {
		StartCoroutine (tick ());
	}

	public void subscribe(Tickable subscriber){
		subscribed.Add (subscriber);
	}

	private void evalSubscribed(){
		foreach(Tickable subscriber in subscribed ){
			subscriber.discreteUpdate();
		}
	}

	private IEnumerator tick(){
		while (true) {
			yield return new WaitForSeconds(tickIntervalTime);
			ticks += 1;
			evalSubscribed ();
		}
	}
}
