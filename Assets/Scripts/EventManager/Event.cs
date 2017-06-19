using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Event : define {
//	public List<GameObject> EventOrders;
	private int _eventIndex = -1;

	// Use this for initialization
	void Start () {
		
	}

	public void Execute(){
		Next ();
	}
		
	public void JumpTo(GameObject jumpTo){
		_eventIndex = jumpTo.transform.GetSiblingIndex();
		Debug.Log ("Event Index:" + _eventIndex);
		if (_eventIndex < transform.childCount) {
			transform.GetChild (_eventIndex).GetComponent<EventItem> ().Execute ();
		} else {
			Fin ();
		}
	}

	public void Next(){
		_eventIndex ++;
		Debug.Log ("Event Index:" + _eventIndex);
		if (_eventIndex < transform.childCount) {
			transform.GetChild (_eventIndex).GetComponent<EventItem> ().Execute ();
		} else {
			Fin ();
		}
	}

	public void Fin(){
		_eventManager.Fin ();
	}
}
