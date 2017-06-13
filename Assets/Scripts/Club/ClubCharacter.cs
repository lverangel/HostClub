using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClubCharacter : define
{
	public kCC_Types type;
	public int ID;
	public Vector2 grid;

	string _texturepath {
		get {
			return "Textures/Club/Character/Guest/" + ID + "/";
		}
	}

	kDirection _direction = kDirection.LB;
	kCC_Actions _action = kCC_Actions.stand;
	int _frameIndex = 0;
	List<Sprite> _actionCache = new List<Sprite> ();


	// Use this for initialization
	void Start ()
	{
		
	}

	public void CC_INIT ()
	{
		RegistNotification (this, kNotificationKeys.NextFrame, NextFrame);

		SetAction (kCC_Actions.stand, kDirection.LB);
	}

	public void SetAction (kCC_Actions pAction, kDirection pDirection = kDirection.LB)
	{
		_action = pAction;
		_actionCache.Clear ();
		if (pDirection != kDirection.none) {
			_direction = pDirection;
			if (_direction == kDirection.RB || _direction == kDirection.RT) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
		}
		string ds;
		if (_direction == kDirection.LB || _direction == kDirection.RB) {
			ds = "front";
		} else {
			ds = "back";
		}
		if (pAction == kCC_Actions.stand) {
			
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "stand_" + ds));
		}
		if (pAction == kCC_Actions.move) {
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "move_" + ds + "_1"));
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "move_" + ds + "_1"));
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "stand_" + ds));
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "stand_" + ds));
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "move_" + ds + "_2"));
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "move_" + ds + "_2"));
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "stand_" + ds));
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "stand_" + ds));
		}
		if (pAction == kCC_Actions.sit_normal) {
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "sit_normal"));
		}
		if (pAction == kCC_Actions.sit_speak) {
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "sit_speak"));
		}
	}

	void NextFrame (Hashtable pData)
	{
		if (_frameIndex >= _actionCache.Count) {
			_frameIndex = 0;
		}
		GetComponent<SpriteRenderer> ().sprite = _actionCache [_frameIndex];
		_frameIndex++;
	}

	void Move_ (List<Vector2> pToList, Action<Hashtable> pCallBack = null, Hashtable pCallBackData = null)
	{
		if (pToList.Count == 0) {
			SetAction (kCC_Actions.stand);
			if (pCallBack != null) {
				pCallBack (pCallBackData);
			}
			return;
		}
		kDirection d = kDirection.none;
		Vector2 to = pToList [0];
		if (to.x > grid.x) {
			d = kDirection.RT;
		}
		if (to.x < grid.x) {
			d = kDirection.LB;
		}
		if (to.y > grid.y) {
			d = kDirection.RB;
		}
		if (to.y < grid.y) {
			d = kDirection.LT;
		}
		SetAction (kCC_Actions.move, d);

		Vector3 tolp = _clubManager.chessBoard [to].localPosition;
		if (d == kDirection.LB || d == kDirection.LT) {
			this.GetComponent<SpriteRenderer> ().sortingOrder = ((int)grid.y - (int)grid.x) * 10 + 1;
		} else {
			this.GetComponent<SpriteRenderer> ().sortingOrder = ((int)grid.y - (int)grid.x) * 10 - 1;
		}
		pToList.Remove (to);
		gameObject.transform.DOLocalMove (tolp, _gameManager.frameSpeed * 6)
			.SetEase (Ease.Linear)
			.OnComplete (() => {
			grid = to;
			Move_ (pToList, pCallBack, pCallBackData);
		});
	}

	void FindSit ()
	{
		ClubManager.ClubNode sitNode = null;
		foreach (KeyValuePair<Vector2,ClubManager.ClubNode> v in _clubManager.chessBoard) {
			ClubManager.ClubNode node = v.Value;
			if (node.type == kCI_Types.chair && node.target == kCC_Types.guest) {
				if (node.obj.GetComponent<ClubItem> ().activeObject == null) {
					sitNode = node;
					break;
				}
			}
		}

		if (sitNode != null) {
			Vector2 movep = sitNode.obj.GetComponent<ClubItem> ().activeGrid;
			Vector2 sitp = sitNode.obj.GetComponent<ClubItem> ().grid;
			_clubManager.generatePath (grid, movep);
			Hashtable data = new Hashtable ();
			data.Add ("position", sitp);
			List<Vector2> path = _clubManager.generatePath (grid, movep);
			Move_ (path, Sit_, data);
		}
	}

	void Sit_ (Hashtable pData)
	{
		Vector2 sitp = (Vector2)pData ["position"];
		grid = sitp;
		ClubManager.ClubNode node = _clubManager.chessBoard [sitp];
		transform.localPosition = node.localPosition;
		node.obj.GetComponent<ClubItem> ().activeObject = gameObject;
		SetAction (kCC_Actions.sit_normal);
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
