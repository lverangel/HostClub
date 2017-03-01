using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public enum kEI_Types
{
	ChangeScene,
	CharacterIn
}

public class EventItem : define
{
	protected kEI_Types _itemType;
	public AudioClip Music;
	public AudioClip Sound;

	void Start ()
	{
	
	}

	public void Next ()
	{
		_eventManager.eventObject.GetComponent<Event> ().Next ();
	}

	public virtual void Execute ()
	{
		
	}
}
