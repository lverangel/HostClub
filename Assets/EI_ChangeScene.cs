using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum kEI_SceneAnimations{
	flash,
	fadeIn
}

public class EI_ChangeScene : EventItem {
	public Sprite scene;
	public kEI_SceneAnimations animationType;
	public float exitTime;

	// Use this for initialization
	void Start () {
		_itemType = kEI_Types.ChangeScene;		
	}

	public override void Execute(){
		base.Execute ();
		_eventManager.sceneObject.GetComponent<Image> ().sprite = scene;
		Invoke ("Next", exitTime);
	}
}
