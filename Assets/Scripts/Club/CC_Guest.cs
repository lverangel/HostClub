using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_Guest : ClubCharacter {

	// Use this for initialization
	void Start () {
		CC_INIT();
		SetAction (kCC_Actions.stand, kDirection.LB);
		Invoke ("FindSit", 1.0f);
	}

	void FindSit ()
	{
		ClubManager.ClubNode sitNode = null;
		foreach (KeyValuePair<Vector2,ClubManager.ClubNode> v in _clubManager.chessBoard) {
			ClubManager.ClubNode node = v.Value;
			if (node.type == kCI_Types.chair && node.target == kCC_Types.guest) {
				if (node.obj.GetComponent<CI_Chair> ().activeObject == null) {
					node.obj.GetComponent<CI_Chair> ().activeObject = gameObject;
					sitNode = node;
					break;
				}
			}
		}

		if (sitNode != null) {
			Vector2 movep = sitNode.obj.GetComponent<CI_Chair> ().activeGrid;
			Vector2 sitp = sitNode.obj.GetComponent<CI_Chair> ().grid;
			_clubManager.generatePath (grid, movep);
			Hashtable data = new Hashtable ();
			data.Add ("position", sitp);
			List<Vector2> path = _clubManager.generatePath (grid, movep);
			Move_ (path, Sit_, data);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
