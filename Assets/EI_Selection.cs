using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class EI_Selection : EventItem
{
	public string[] text;
	public GameObject[] chooseJump;

	private List<GameObject> _selectionList = new List<GameObject> ();

	public override void Execute ()
	{
		_eventManager.selectionBoard.SetActive (true);
		for (int i = 0; i < text.Length; i++) {
			GameObject item = GameObject.Instantiate (_eventManager.selectionItemObject) as GameObject;
			_selectionList.Add (item);
			item.GetComponentInChildren<Text> ().text = text [i];
			item.name = i.ToString ();
			item.GetComponent<Button> ().onClick.AddListener (delegate () {
				jumpTo = chooseJump[Convert.ToInt32 (item.name)];
				_eventManager.selectionBoard.SetActive (false);
				Next ();
				foreach(GameObject o in _selectionList){
					DestroyImmediate(o);
				}
			});
			item.transform.SetParent (_eventManager.selectionBoard.transform);
			item.transform.localScale = Vector3.one;
			item.transform.localPosition = Vector3.zero;
		}
	}
}
