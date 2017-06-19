using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRectAutoFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int i = transform.childCount;
		this.GetComponent<RectTransform>().sizeDelta = new Vector2(i*168 + (i-1)*8 + 16,240);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
