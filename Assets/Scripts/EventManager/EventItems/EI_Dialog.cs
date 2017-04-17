﻿using UnityEngine;
using UnityEngine.UI;
using System.Text;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class EI_Dialog : EventItem
{
	// Use this for initialization
	[Tooltip ("角色表情")]
	public kEI_CharacterEmotes characterEmote = kEI_CharacterEmotes.none;
	[Tooltip ("角色ID")]
	public string ID;
	[Tooltip ("角色姓名卡，不选择则使用角色默认卡片")]
	public Sprite nameImage = null;
	[Tooltip ("文字弹出间隔时间")]
	public float speed = 0.1f;
	[Tooltip ("是否启用开口动画")]
	public bool isSpeakAnimation = true;
	[Tooltip ("人物黑遮罩")]
	public kEI_DialogMasks dialogMask = kEI_DialogMasks.talkerLight;
	[Tooltip ("文字数组")]
	public string[] text;

	int _rowIndex;
	string _rowString = "";
	bool _isRowFin = false;

	int _wordIndex = 0;


	public override void Execute ()
	{
		_eventManager.dialogBoard.SetText ("");
		_eventManager.dialogBoard.gameObject.SetActive (true);
		_eventManager.dialogBoard.EI_Dialog = this;
		_eventManager.dialogBoard.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		Sequence mySequence = DOTween.Sequence ();
		mySequence
			.Append (_eventManager.dialogBoard.transform.DOScale (1, 0.3f).SetEase (Ease.OutExpo))
			.AppendCallback (() => {
			nextRow_ ();
		});

		Character chara = null;
		if (ID != "") {
			chara = _eventManager.characters [ID].GetComponent<Character> ();
			if (nameImage) {
				_eventManager.dialogBoard.SetNameImage (nameImage);
			} else if (chara.nameImage) {
				_eventManager.dialogBoard.SetNameImage (chara.nameImage);
			} else {
				_eventManager.dialogBoard.SetNameImage (null);
			}
			if (characterEmote != kEI_CharacterEmotes.none) {
				chara.ChangeEmote (characterEmote);
			}
		}

		if (nameImage) {
			_eventManager.dialogBoard.SetNameImage (nameImage);
		}

		if (dialogMask == kEI_DialogMasks.talkerLight) {
			if (!chara) {
				dialogMask = kEI_DialogMasks.allLight;
			} else {
				foreach (KeyValuePair<string,GameObject> pair in _eventManager.characters) {
					if (pair.Key == ID) {
						//变白
						pair.Value.GetComponent<Character>().SetColor(Color.white);
					} else {
						//变黑
						pair.Value.GetComponent<Character>().SetColor(Color.gray);
					}
				}
			}
		}

		if (dialogMask == kEI_DialogMasks.allLight) {
			foreach (KeyValuePair<string,GameObject> pair in _eventManager.characters) {
				//变白
				pair.Value.GetComponent<Character>().SetColor(Color.white);
			}
		}

		if (dialogMask == kEI_DialogMasks.allBlack) {
			foreach (KeyValuePair<string,GameObject> pair in _eventManager.characters) {
				//变黑
				pair.Value.GetComponent<Character>().SetColor(Color.gray);
			}
		}
	}

	bool nextRow_ ()
	{
		_isRowFin = false;
		_eventManager.dialogBoard.ToggleArrow (false);
		if (_rowIndex < text.Length) {
			_wordIndex = 1;
			_rowString = text [_rowIndex];
			_rowIndex++;
			goWord_ ();
		} else {
			return false;
		}
		return true;

	}

	void goWord_ ()
	{
		if (_isRowFin)
			return;
		if (_wordIndex <= _rowString.Length) {
			string s = _rowString.Substring (0, _wordIndex);
			_eventManager.dialogBoard.SetText (s);
			_wordIndex++;
			Invoke ("goWord_", speed);
		} else {
			rowFin_ ();
		}
	}

	void rowFin_ ()
	{
		_isRowFin = true;
		_eventManager.dialogBoard.ToggleArrow (true);
	}

	public void OnClick ()
	{
		if (_isRowFin) {
			if (!nextRow_ ()) {
				_eventManager.dialogBoard.EI_Dialog = null;
				_eventManager.dialogBoard.gameObject.SetActive (false);
				foreach (KeyValuePair<string,GameObject> pair in _eventManager.characters) {
					//变白
					pair.Value.GetComponent<Character>().SetColor(Color.white);
				}
				Next ();
			}
		} else {
			_eventManager.dialogBoard.SetText (_rowString);
			rowFin_ ();
		}
	}
}
