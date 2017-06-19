using UnityEngine;
using System.Collections;

public class AutoFullScreen : define {

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().sizeDelta = _scaledScreenSize;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
