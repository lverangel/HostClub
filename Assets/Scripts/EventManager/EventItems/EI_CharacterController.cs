using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EI_CharacterController : EventItem
{
	[Tooltip ("角色的运动模式，入场，出场，移动")]
	public kEI_CharacterMoveTypes type;
	[Tooltip ("默认速度的倍数")]
	public float speedFactor = 1.0f;
	[Tooltip ("角色ID，在后面的条目中用来定位角色对象")]
	public string ID = "角色1";
	[Tooltip ("角色起始位置")]
	public kEI_CharacterPositions startPosition = kEI_CharacterPositions.none;
	[Tooltip ("角色目标位置")]
	public kEI_CharacterPositions endPosition = kEI_CharacterPositions.none;
	[Tooltip ("拖入角色的Prefab")]
	public GameObject characterObject;
	[Tooltip ("更换角色表情")]
	public kEI_CharacterEmotes characterEmote = kEI_CharacterEmotes.none;

	public override void Execute ()
	{
		base.Execute ();

		//获取角色
		if (type == kEI_CharacterMoveTypes.createAndMoveInMoveIn) {
			
			characterObject = GameObject.Instantiate (characterObject) as GameObject;
			_eventManager.characters.Add (ID, characterObject);
			characterObject.transform.SetParent (_eventManager.canvas);
			characterObject.transform.localScale = Vector3.one;
		} else {
			characterObject = _eventManager.characters [ID];
		}

		if (type == kEI_CharacterMoveTypes.createAndMoveInMoveIn
		    || type == kEI_CharacterMoveTypes.moveOutAndDestory
		    || type == kEI_CharacterMoveTypes.movePosition) {
			moveCharacter_ ();
		} else {
			Next ();
		}


		if (characterObject && characterEmote != kEI_CharacterEmotes.none) {
			characterObject.GetComponent<Character> ().ChangeEmote (characterEmote);
		}

	}

	void moveCharacter_ ()
	{
		//起始位置定义
		Vector2 startPoint = Vector2.zero;
		Vector2 loPoint = new Vector2 (-_scaledScreenSize.x / 2 - characterObject.GetComponent<Character> ().bodyObject.GetComponent<RectTransform> ().sizeDelta.x / 2, _eventManager.characterY);
		Vector2 roPoint = new Vector2 (_scaledScreenSize.x / 2 + characterObject.GetComponent<Character> ().bodyObject.GetComponent<RectTransform> ().sizeDelta.x / 2, _eventManager.characterY);
		if (startPosition != kEI_CharacterPositions.none && type == kEI_CharacterMoveTypes.createAndMoveInMoveIn) {
			switch (startPosition) {
			case kEI_CharacterPositions.leftOutside:
				{
					startPoint = loPoint;
				}
				break;
			case kEI_CharacterPositions.rightOutside:
				{
					startPoint = roPoint;
				}
				break;
			case kEI_CharacterPositions.left:
				{
					startPoint = new Vector2 (_eventManager.characterLeftX, _eventManager.characterY);
				}
				break;
			case kEI_CharacterPositions.right:
				{
					startPoint = new Vector2 (_eventManager.characterRightX, _eventManager.characterY);
				}
				break;
			case kEI_CharacterPositions.middle:
				{
					startPoint = new Vector2 (_eventManager.characterMiddleX, _eventManager.characterY);
				}
				break;	
			}
			characterObject.transform.localPosition = startPoint;
			characterObject.GetComponent<Character> ().poistion = startPosition;
		} else {
			startPoint = characterObject.transform.localPosition;
		}

		//计算时间
		float durationTime = 0.0f;
		Vector2 endPoint = Vector2.zero;
		if (endPosition != kEI_CharacterPositions.none) {
			switch (endPosition) {
			case kEI_CharacterPositions.leftOutside:
				{
					endPoint = loPoint;
				}
				break;
			case kEI_CharacterPositions.rightOutside:
				{
					endPoint = roPoint;
				}
				break;
			case kEI_CharacterPositions.left:
				{
					endPoint = new Vector2 (_eventManager.characterLeftX, _eventManager.characterY);
				}
				break;
			case kEI_CharacterPositions.right:
				{
					endPoint = new Vector2 (_eventManager.characterRightX, _eventManager.characterY);
				}
				break;
			case kEI_CharacterPositions.middle:
				{
					endPoint = new Vector2 (_eventManager.characterMiddleX, _eventManager.characterY);
				}
				break;	
			}
		}
		durationTime = Mathf.Abs (endPoint.x - startPoint.x) / 100 / _eventManager.characterDefaultSpeed / speedFactor;

		Sequence mySequence = DOTween.Sequence ();
		mySequence
			.Append (characterObject.transform.DOLocalMove (endPoint, durationTime))
			.AppendCallback (() => {
			if (type == kEI_CharacterMoveTypes.createAndMoveInMoveIn
			    || type == kEI_CharacterMoveTypes.movePosition) {
				nextp_ ();
			} else {
				_eventManager.characters.Remove (ID);
				DestroyImmediate (characterObject);
				nextp_ ();
			}
		});
	}

	private void nextp_ ()
	{
		Next ();
	}
}
