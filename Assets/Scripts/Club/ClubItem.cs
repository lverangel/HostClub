using UnityEngine;
using System.Collections;

public class ClubItem : define
{
	public kCI_Types type;
	public kDirection direction;
	public Vector2 grid;


	// Use this for initialization
	void Start ()
	{
		
	}

	public void CI_init(){
//		this.transform.localPosition = _clubManager.chessBoard [grid].localPosition;
		this.GetComponent<SpriteRenderer> ().sortingOrder = ((int)grid.y - (int)grid.x) * 10;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

