using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBoard : define
{

	public GameObject textLabel;
	public GameObject arrow;
	public GameObject nameImage;
	public EI_Dialog EI_Dialog;

	void Start ()
	{
		ToggleArrow (false);
	}

	public void SetText (string pText)
	{
		textLabel.GetComponent<Text> ().text = pText;
	}

	public void SetNameImage (Sprite pImage)
	{
		nameImage.GetComponent<Image> ().sprite = pImage;
	}

	public void ToggleArrow (bool pSw)
	{
		arrow.SetActive (pSw);
	}

	public void OnClick(){
		if (EI_Dialog) {
			EI_Dialog.OnClick ();
		}
	}
}
