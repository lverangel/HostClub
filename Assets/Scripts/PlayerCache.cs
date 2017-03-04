using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]

public class LocalStatistics
{
	public int coinTotal       = 0;//拾取金币
}

[Serializable] 
public class SaveDataPlayer //存储棋盘等内部属性
{
	public bool isNewGame;
	public int inGamePlayerDataTest;
}


public class PlayerCache:define
{
	public SaveDataPlayer _playerData = new SaveDataPlayer();

	void Awake ()
	{
		
	}

	void Start ()
	{

	}

	public void NewGame ()
	{
		_playerData.isNewGame = false;
	}
}