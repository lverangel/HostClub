using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventManager : define
{
	public int eventID;
	public GameObject eventObject;

	public Transform canvas;
	public Transform data;

	public GameObject sceneObject;
	public GameObject CGObject;
	public DialogBoard dialogBoard;
	public GameObject selectionBoard;
	public GameObject selectionItemObject;
	public Image nameImage;

	public float characterLeftX;
	public float characterMiddleX;
	public float characterRightX;
	public float characterY;
	public float characterDefaultSpeed;


	public Dictionary<string,GameObject> characters = new Dictionary<string,GameObject> ();


	void Start ()
	{
		
	}

	public void Execute (int pEventID = -1)
	{
		gameObject.SetActive (true);
		if (pEventID != -1) {
			eventID = pEventID;
		}

		Debug.Log ("Event ID:" + eventID);
		eventObject = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/EventManager/Events/" + eventID));
		eventObject.transform.SetParent (data);

		eventObject.GetComponent<Event> ().Execute ();
	}

	public void Fin(){
		Debug.Log ("Event Fin");
		for (int i = 0; i < data.childCount; i++) {
			DestroyImmediate (data.GetChild(i).gameObject);
		}
		foreach (KeyValuePair<string,GameObject> chara in characters) {
			DestroyImmediate (chara.Value);
		}
		characters.Clear ();
		gameObject.SetActive (false);
	}

	public void NextButton(){
		dialogBoard.OnClick ();
	}
}
