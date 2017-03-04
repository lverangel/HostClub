using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using LitJson;

[RequireComponent(typeof(PlayerCache))]
//[ExecuteInEditMode]

public class PlayerCore:MonoBehaviour
{

	public string dirPath;
	public PlayerCache playerCache;
	public class SaveDataMain //存储外围数据
	{
		public int Coin = 500;
		public int Gem = 100;

		public LocalStatistics _allStatistics = new LocalStatistics();
	}

	public  SaveDataMain _mainData = new SaveDataMain();

	void Awake ()
	{
		playerCache = GetComponent<PlayerCache> ();
	}

	void Start(){
		dirPath = Application.persistentDataPath + "/Save";


		//如果没有创建存档文件夹则是第一次进入游戏
		if(!IOHelper.IsDirectoryExists(dirPath))
		{
			Debug.Log("创建存档Player Profile not found,init.");
			//创建存档文件夹
			IOHelper.CreateDirectory (dirPath);
			initProfile();
			saveProfile();

		}
		else
		{
//			Debug.Log("提取存档Player Profile found,load.");
			loadProfile();
		
		}
			
	}
	public void initProfile ()
	{
//		_mainData.charaUnlocked["1"] = 1;
//		_mainData.charaUnlocked["2"] = 1;
//
//		for(int i = 1;i<=9;i++)
//		{
//			_mainData.runes.Add(i,3);
//		}
	}


	public void loadProfile ()
	{
		
		if (IOHelper.IsFileExists(dirPath + "/MainData.sav")) 
		{
			_mainData = (SaveDataMain)IOHelper.GetData( dirPath + "/MainData.sav",typeof(SaveDataMain));
		} 
		if(IOHelper.IsFileExists(dirPath + "/PlayerData.sav"))
		{
			playerCache._playerData = (SaveDataPlayer)IOHelper.GetData(dirPath + "/PlayerData.sav",typeof(SaveDataPlayer));
		
		}else
		{
//			playerCache.newGame(kprofession.Knight);//默认角色骑士
		}

	}



	public void saveProfile ()
	{
		Debug.Log("Save Profile");
		string filename = dirPath + "/MainData.sav";
		string fileplayer = dirPath + "/PlayerData.sav";

		IOHelper.SetData (filename,_mainData);//主属性
		IOHelper.SetData(fileplayer,playerCache._playerData);//棋盘和当前底层数据

	}

}


