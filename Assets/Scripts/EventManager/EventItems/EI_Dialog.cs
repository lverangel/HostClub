using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EI_Dialog : EventItem {
	// Use this for initialization
	public kEI_CharacterEmotes characterEmote = kEI_CharacterEmotes.none;
	public string ID;

	public Sprite head;
	public bool isSpeakAnimation = true;
	public string[] text;


	public void Execute(){
		
		Next ();
	}
}
