using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CI_Chair : ClubItem {
	public Vector2 activeGrid;
	public kCC_Types activeTarget;
	public GameObject activeObject;
	public GameObject serviceTarget;

	void Start () {
		CI_init ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUpAsButton() {
		Debug.Log ("mouseup");
	}
}
