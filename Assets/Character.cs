using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum kEI_CharacterEmotes
{
	normal,
	smile,
	cry
}

public class Character : MonoBehaviour
{
	public GameObject bodyObject;
	public GameObject emoteObject;

	public Sprite body;
	public Sprite emoteNormal;
	public Sprite emoteSmile;
	public Sprite emoteCry;

	// Use this for initialization
	void Start ()
	{
		bodyObject.GetComponent<Image> ().sprite = body;
	}

	public void ChangeEmote (kEI_CharacterEmotes pEmote)
	{
		Debug.Log ("Change Emote:" + pEmote);
		Sprite emote;
		switch (pEmote) {
		case kEI_CharacterEmotes.normal:
			emote = emoteNormal;
			break;
		case kEI_CharacterEmotes.smile:
			emote = emoteSmile;
			break;
		case kEI_CharacterEmotes.cry:
			emote = emoteCry;
			break;
		default:
			emote = emoteNormal;
			break;
		}
			
		emoteObject.GetComponent<Image> ().sprite = emote;

			
	}

}
