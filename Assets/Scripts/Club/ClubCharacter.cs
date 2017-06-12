using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClubCharacter : define
{
	public kCC_Types type;
	public int ID;
	public Vector2 gridPoint;
	 
	string _texturepath {
		get {
			return "Textures/Club/Character/Guest/" + ID + "/";
		}
	}

	kDirection _direction = kDirection.LB;
	kCC_Actions _action;
	int _frameIndex = 0;
	List<Sprite> _actionCache = new List<Sprite> ();


	// Use this for initialization
	void Start ()
	{
		
	}

	public void CC_INIT ()
	{
		


		RegistNotification (this, kNotificationKeys.NextFrame, NextFrame);

		SetAction (kCC_Actions.move,kDirection.RT);
	}

	public void SetAction (kCC_Actions pAction, kDirection pDirection = kDirection.none)
	{
		_actionCache.Clear ();
		if (pDirection != kDirection.none) {
			SetDirection (pDirection);
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
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "move_" + ds + "_2"));
		}
		if (pAction == kCC_Actions.sit_normal) {
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "sit_normal"));
		}
		if (pAction == kCC_Actions.sit_speak) {
			_actionCache.Add (Resources.Load<Sprite> (_texturepath + "sit_speak"));
		}
	}

	public void SetDirection (kDirection pDirection)
	{
		if (pDirection == kDirection.none) {
			return;
		}
		_direction = pDirection;
		if (_direction == kDirection.RB || _direction == kDirection.RT) {
			GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			GetComponent<SpriteRenderer> ().flipX = false;
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

	// Update is called once per frame
	void Update ()
	{
		
	}
}
