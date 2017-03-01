using UnityEngine;
using System.Collections;

public enum kEI_CharacterMoveTypes{
	In,
	Out,
	Move
}


public enum kEI_CharacterAnimations{
	flash,
	fadeIn
}

public enum kEI_CharacterPositions{
	leftOutside,
	rightOutside,
	left,
	middle,
	right
}

public class EI_CharacterMove : EventItem {
	public kEI_CharacterMoveTypes type;
	public string ID;
	public kEI_CharacterPositions startPosition;
	public kEI_CharacterPositions endPosition;
	public GameObject characterObject;
	public kEI_CharacterEmotes characterEmote = kEI_CharacterEmotes.normal;

	void Start () {
		_itemType = kEI_Types.CharacterIn;
	}
	
	public override void Execute ()
	{
		base.Execute ();
		if (type == kEI_CharacterMoveTypes.In) {
			//init
			characterObject = GameObject.Instantiate (characterObject) as GameObject;
			_eventManager.characters.Add (ID, characterObject);
			characterObject.transform.SetParent (_eventManager.canvas);
			characterObject.transform.localPosition = Vector3.zero;
			characterObject.transform.localScale = Vector3.one;

			characterObject.GetComponent<Character> ().ChangeEmote (characterEmote);
		}

		Next ();
	}
}
