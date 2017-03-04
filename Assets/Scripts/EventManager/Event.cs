using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Event : define {
//	public List<GameObject> EventOrders;
	private int _EventIndex = -1;

	// Use this for initialization
	void Start () {
		
	}

	public void Execute(){
		Next ();
	}

	public void Next(){
		_EventIndex ++;
		Debug.Log ("Event Index:" + _EventIndex);
		if (_EventIndex < transform.childCount) {
			transform.GetChild (_EventIndex).GetComponent<EventItem> ().Execute ();
		} else {
			Debug.Log ("Event Fin");
		}
	}
}
