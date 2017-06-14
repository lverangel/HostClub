using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]

public class EE_GridMarker : define
{

	// Use this for initialization
	public Font font;

	void Awake ()
	{
		Vector2 gd = GetComponent<ClubManager> ().gridDistance;
		Vector2 s = GetComponent<ClubManager> ().size;
		GameObject gmo;
		if (transform.Find ("GridMarker")) {
			gmo = transform.Find ("GridMarker").gameObject;
			GameObject.DestroyImmediate (gmo);
		}
		gmo = new GameObject ();
		Canvas canvas = gmo.AddComponent<Canvas> ();
		canvas.overrideSorting = true;
		canvas.sortingLayerName = "UI";
		gmo.AddComponent<CanvasGroup> ().alpha = 0.4f;
		gmo.name = "GridMarker";
		gmo.transform.SetParent (transform);
		gmo.transform.localScale = Vector3.one;
		for (int x = 0; x < s.x; x++) {
			for (int y = 0; y < s.y; y++) {
				GameObject lo = new GameObject ();
				lo.name = "{" + x + "," + y + "}";
				lo.AddComponent<RectTransform> ();
				Text t = lo.AddComponent<Text> ();
				t.text = "{" + x + "," + y + "}";
				t.font = font;
				t.alignment = TextAnchor.MiddleCenter;
				lo.transform.SetParent (gmo.transform);
				float xx = GetComponent<ClubManager> ().OriginPoint.x;
				float yy = GetComponent<ClubManager> ().OriginPoint.y;
				if (x > y) {
					xx += (x + y) * gd.x;
					yy += (x - y) * gd.y;
				} else if (x == y) {
					xx += (x + y) * gd.x;
					yy += 0;
				} else {
					xx += (x + y) * gd.x;
					yy += -(y - x) * gd.y;
				}
				lo.transform.localPosition = new Vector3 (xx, yy, 0);
				lo.transform.localScale = Vector3.one;
			}
		}
	}

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
