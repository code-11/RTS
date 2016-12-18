using UnityEngine;
using System.Collections;

public class OnFight : MonoBehaviour,Tickable {

	public Unit attacker;
	public Unit defender;
	public Unit winner;
	public Unit loser;
	public int timeLeft=-1;

	public void construct(Unit attacker, Unit defender){
		int duration = calculateDuration ();
		this.attacker = attacker;
		this.defender = defender;
		timeLeft = duration;
		GameObject.FindObjectOfType<TimeMaster> ().subscribe (this);
	}

	public void discreteUpdate(){
		if (timeLeft > 0) {
			timeLeft -= 1;
			applyDailyResult ();
			Debug.Log ("Applying Daily Result");
		} else {
			tearDown ();
		}
	}

	private void tearDown(){
		foreach(Unit unit in new Unit[]{attacker,defender}){
			unit.inFight = false;
		}
		Debug.Log ("Fight over, cleaning up");
		applyEndResult ();
		forceRetreat ();
		GameObject.FindObjectOfType<TimeMaster> ().unsubscribe (this);
		Destroy (gameObject);
	}

	private void forceRetreat(){
		MapNode curLoc= loser.getLocation();
		GameObject retreatLoc = curLoc.randomNeighbor ();
		loser.moveTo (retreatLoc);
	}

	private void applyDailyResult(){
	}
	private void applyEndResult(){
		winner = attacker;
		loser = defender;
	}

	private int calculateDuration(){
		return 10;
	}
}
