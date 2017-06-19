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
		if (activeTarget == kCC_Types.waiter) {
			gameObject.AddComponent<BoxCollider2D> ().size = new Vector2(200,300);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUpAsButton() {
		if (activeObject == null) {
			CC_Guest guest = serviceTarget.GetComponent<CI_Chair> ().activeObject.GetComponent<CC_Guest> ();
				if (guest.action == kCC_Actions.sit_normal) {
				activeObject = _clubManager.selectedWaiter;
				CC_Waiter waiter = activeObject.GetComponent<CC_Waiter> ();
				waiter.grid = grid;
				waiter.setDirection (direction);
				activeObject.transform.localPosition = _clubManager.chessBoard [grid].localPosition;
				guest.SetAction (kCC_Actions.sit_speak);
			}
		}
	}
}
