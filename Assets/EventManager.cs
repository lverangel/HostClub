using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : define {
	public int eventID;
	public GameObject eventObject;
	public Transform canvas;
	public Transform data;

	public GameObject sceneObject;
	public GameObject dialogObject;


	public Dictionary<string,GameObject> characters = new Dictionary<string,GameObject>();


	void Start () {
		_eventManager = this;
		Execute ();
	}

	public void Execute(int pEventID = -1){
		if (pEventID != -1) {
			eventID = pEventID;
		}

		Debug.Log ("Find event:" + "Prefabs/EventSystem/Events/" + eventID);
		eventObject = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/EventSystem/Events/" + eventID));
		eventObject.transform.SetParent (data);

		eventObject.GetComponent<Event> ().Execute ();
	}
}
