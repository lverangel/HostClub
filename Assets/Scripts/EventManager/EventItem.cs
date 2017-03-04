using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public enum kEI_Types
{
	ChangeScene,
	Music,
	CharacterController
}

public class EventItem : define
{
	[Tooltip ("条目结束时，跳转到指定条目，拖入条目GameObject")]
	public GameObject jumpTo;
	[Tooltip ("条目开始的同时播放一个音效")]
	public AudioClip sound;
	[Tooltip ("条目结束后延迟一段时间（秒）")]
	public float delayTime = 0.0f;

	public void Next ()
	{
		Invoke ("next_", delayTime);
	}

	private void next_ ()
	{
		_eventManager.eventObject.GetComponent<Event> ().Next ();
	}

	public virtual void Execute ()
	{
		if (sound != null) {
			_gameManager.PlaySound (sound);
		}
	}
}
