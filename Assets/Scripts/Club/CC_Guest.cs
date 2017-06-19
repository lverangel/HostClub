using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CC_Guest : ClubCharacter {

	string _texturepath {
		get {
			return "Textures/Club/Character/Guest/" + ID + "/";
		}
	}

	kCC_GuestStatus _status;
	public kCC_GuestStatus status{
		get{
			return _status;
		}	
		set{
			setStatus (value);
		}
	}

	public SpriteRenderer statusIcon;

	// Use this for initialization
	void Start () {
		CC_INIT();
		SetAction (kCC_Actions.stand, kDirection.LB);
		Invoke ("FindSit", 1.0f);
	}

	void setStatus(kCC_GuestStatus pStatus){
		_status = pStatus;
		statusIcon.sprite = Resources.Load<Sprite> ("Textures/Club/Character/status/" + _status);
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

	public void SetAction (kCC_Actions pAction, kDirection pDirection = kDirection.none)
	{
		action = pAction;
		_actionCache.Clear ();
		if (pDirection != kDirection.none) {
			_direction = pDirection;
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



	protected void Move_ (List<Vector2> pToList, Action<Hashtable> pCallBack = null, Hashtable pCallBackData = null)
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
		this.GetComponent<SpriteRenderer> ().sortingOrder = ((int)to.y - (int)to.x) * 10 + 5;
		pToList.Remove (to);
		gameObject.transform.DOLocalMove (tolp, _gameManager.frameSpeed * 6)
			.SetEase (Ease.Linear)
			.OnComplete (() => {
				grid = to;
				Move_ (pToList, pCallBack, pCallBackData);
			});
	}

	protected void Sit_ (Hashtable pData)
	{
		Vector2 sitp = (Vector2)pData ["position"];
		grid = sitp;
		ClubManager.ClubNode node = _clubManager.chessBoard [sitp];
		transform.localPosition = node.localPosition;
		SetAction (kCC_Actions.sit_normal, node.obj.GetComponent<CI_Chair> ().direction);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
