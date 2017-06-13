using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_Guest : ClubCharacter {

	// Use this for initialization
	void Start () {
		CC_INIT();
		Invoke ("FindSit", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
