using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

//[ExecuteInEditMode]

[RequireComponent(typeof(PlayerCore))]

public class GameManager : define
{
	public bool deletePlayerPrefs = false;
	public static GameManager instance = null;
	public PlayerCore playerCore = null;
	public EventManager eventManager = null;
	public ClubManager clubManager = null;

	public string Localize = "en";

	public List<Type> lastScene = new List<Type> ();

//	public MaskLayer maskLayer;
	public GameObject mouseResponser;

//	public GameScene gameScene;

	public int randomSeed = -1;

	public AudioMixer SoundMixer;
	public AudioMixer MusicMixer;

	public float dt = 0;
	public float frameSpeed = 0.3f;

	void Awake ()
	{
		playerCore = GetComponent<PlayerCore> ();
		DontDestroyOnLoad (gameObject);

		if (instance == null) {
			instance = this;
			this.name = "GameManager";
			Debug.Log ("GameManager init");

			return;

		} else if (instance != this)
		{
			Destroy (gameObject);   
		}


	}

	void Start()
	{
		if(deletePlayerPrefs)
		{
			IOHelper.deletAll(Application.persistentDataPath + "/Save");
		}

		randomSeed = (int)getTimeStamp();
		playerDataInit ();

		if (!_isScreenConfigInited) {
			_realScreenSize = new Vector2 (Screen.width, Screen.height);
			float screenSizeRatio = _screenSize.x / _screenSize.y;
			float realScreenSizeRatio = _realScreenSize.x / _realScreenSize.y;
			
			float scaledScreenSizeWidth = 0.0f;
			float scaledScreenSizeHeight = 0.0f;
			
			if (realScreenSizeRatio >= screenSizeRatio) {
				scaledScreenSizeHeight = _screenSize.y;
				_scaleFactor = _screenSize.y / _realScreenSize.y;
				_scaleFactorY = _scaleFactor;
				_scaleFactorX = _screenSize.x / _realScreenSize.x;
				scaledScreenSizeWidth = _realScreenSize.x * _scaleFactor;
			} else {
				scaledScreenSizeWidth = _screenSize.x;
				_scaleFactor = _screenSize.x / _realScreenSize.x;
				_scaleFactorX = _scaleFactor;
				_scaleFactorY = _screenSize.y / _realScreenSize.y;

				scaledScreenSizeHeight = _realScreenSize.y * _scaleFactor;
			}
			
			_scaledScreenSize = new Vector2 (scaledScreenSizeWidth, scaledScreenSizeHeight);
			
			//			Screen.SetResolution((int)_scaledScreenSize.x,(int)_scaledScreenSize.y,false);
			
			_isScreenConfigInited = true;
			
			Debug.Log ("_realScreenSize:" + _realScreenSize.ToString () 
			           + " _scaledScreenSize:" + _scaledScreenSize.ToString () 
			           + " _scaleFactor:" + _scaleFactor.ToString ()
			           + " _scaleFactorX:" + _scaleFactorX.ToString ()
			           + " _scaleFactorY:" + _scaleFactorY.ToString ()
			           );
			
			
		}

//		RegistNotification (this, kNotificationKeys.ShowAlertByNotification, ShowAlertByNotification);
	}

	public void playerDataInit()
	{
		Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

	}

	void Update ()
	{
		if (mouseResponser != null) {
			mouseResponser.transform.position = define.getTouchPoint ();

			if (Input.GetMouseButtonDown (0)) {
//			mouseResponser.GetComponent<ParticleSystem>().Play();
				StartCoroutine (onMouseResponse_ ());
			}

			if (Input.GetMouseButtonUp (0)) {
				mouseResponser.GetComponent<ParticleSystem> ().Stop ();
			}
		}

		dt += Time.deltaTime;

		if (dt > frameSpeed) {
			dt = 0;
			PostNotification(kNotificationKeys.NextFrame,null);
		}
	}

	private IEnumerator onMouseResponse_(){
		yield return  new WaitForEndOfFrame ();
		mouseResponser.GetComponent<ParticleSystem>().Play();
	}

	void ShowAlertByNotification(Hashtable pParams){
		foreach(DictionaryEntry entry in pParams){
			Debug.Log (entry.Value);
//			_maskLayer.ShowAlert ((kAlertTypes)entry.Key, (Hashtable)entry.Value);
		}
	}

	public  void PlaySound(string pSource){
		PlaySound (Resources.Load<AudioClip> (pSource));
	}

	public  void PlaySound(AudioClip pAudioClip){
		GameObject obj = GameObject.Instantiate(Resources.Load("Prefabs/SoundPlayer") as GameObject);
		obj.GetComponent<AudioSource> ().clip = pAudioClip;
		obj.GetComponent<AudioSource> ().Play ();
	}

	public  void PlayMusic(AudioClip pAudioClip){
		AudioSource audioSource = _gameManager.GetComponent<AudioSource> ();
		audioSource.clip = pAudioClip;
		audioSource.Play ();
	}

	public  void StopMusic(){
		AudioSource audioSource = _gameManager.GetComponent<AudioSource> ();
		audioSource.Stop ();
	}
		
}
