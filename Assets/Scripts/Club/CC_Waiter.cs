using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter
{
	static Hashtable _alldatas;
	public static Hashtable AllDatas{
		get {
			if(_alldatas == null){
				_alldatas = define.getTxtFile ("Data/Waiters").hashtableFromJson ();
			}
			return _alldatas;
		}
	}

	public string name;
	public string memo;
	public int ID = 0;
	public int level = 0;
	public Dictionary<kCC_WaitersAttributes,float> attributes = new Dictionary<kCC_WaitersAttributes, float> ();

	public Waiter ()
	{
	}

	static public Waiter createDefault (int pID)
	{
		Waiter ret = new Waiter ();
		ret.ID = pID;
		ret.level = 0;

		Hashtable waiter = (Hashtable)AllDatas [pID.ToString ()];
		Debug.Log (waiter.Count);
		Hashtable attributes = (Hashtable)waiter ["attributes"];

		Dictionary<kCC_WaitersAttributes,float> ati = new Dictionary<kCC_WaitersAttributes, float> ();
		foreach (DictionaryEntry entry in attributes) {
			ati.Add (define.ParseEnum<kCC_WaitersAttributes> (entry.Key.ToString ()), float.Parse (entry.Value.ToString ()));
		}

		ret.attributes = ati;

//		foreach (KeyValuePair<kCC_WaitersAttributes,float> kv in ret.attributes) {
//			Debug.Log (kv.Key + ":" + kv.Value);
//		}

		return ret;
	}

	public void levelUp ()
	{
		level++;

		Hashtable waiter = (Hashtable)AllDatas [ID.ToString ()];

		Hashtable attributes_growing = (Hashtable)waiter ["attributes_growing"];
		foreach (DictionaryEntry entry in attributes_growing) {
			attributes [define.ParseEnum<kCC_WaitersAttributes> (entry.Key.ToString ())] += float.Parse (entry.Value.ToString ());
		}
			
	}
}

public class CC_Waiter : ClubCharacter
{
	Waiter _waiter;

	string _texturepath {
		get {
			return "Textures/Club/Character/Waiter/";
		}
	}

	public void setDirection(kDirection pDirection){
		_direction = pDirection;
		if (_direction != kDirection.none) {
			if (_direction == kDirection.RB || _direction == kDirection.RT) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
		CC_INIT ();
		_waiter = _playerCore.mainData.Waiters [ID];
		transform.localPosition = new Vector3 (3000, 3000, 0);

		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.sprite = Resources.Load<Sprite>(_texturepath+ ID +"_1");
//		_actionCache.Add (sr.sprite);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
