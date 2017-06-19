using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EI_ChangeScene : EventItem {
	[Tooltip("拖入场景图片")]
	public Sprite scene;
	[Tooltip("场景进入动画模式")]
	public kEI_SceneAnimations animationType;

	public override void Execute(){
		base.Execute ();
		_eventManager.sceneObject.GetComponent<Image> ().sprite = scene;
		Next();
	}
}
