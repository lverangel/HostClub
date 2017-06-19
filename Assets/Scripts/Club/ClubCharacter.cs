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

	int _frameIndex = 0;

	protected kDirection _direction = kDirection.LB;
	public kCC_Actions action = kCC_Actions.stand;
	protected List<Sprite> _actionCache = new List<Sprite> ();


	// Use this for initialization
	void Start ()
	{
		
	}

	public void CC_INIT ()
	{
		RegistNotification (this, kNotificationKeys.NextFrame, NextFrame);
	}

	void NextFrame (Hashtable pData)
	{
		if (_actionCache.Count == 0)
			return;
		if (_frameIndex >= _actionCache.Count) {
			_frameIndex = 0;
		}
		GetComponent<SpriteRenderer> ().sprite = _actionCache [_frameIndex];
		if (_direction != kDirection.none) {

			if (_direction == kDirection.RB || _direction == kDirection.RT) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
		}

		_frameIndex++;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
