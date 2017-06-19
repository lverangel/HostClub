using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class EI_CGController : EventItem
{
	[Tooltip ("CG模式")]
	public kEI_CGTypes type;
	[Tooltip ("CG图片")]
	public Sprite CG;
	[Tooltip ("缩放前停留时间")]
	public float zoomShowTime = 1;
	[Tooltip ("缩放速度")]
	public float zoomSpeed = 1;
	[Tooltip ("缩放前位置")]
	public Vector2 zoomFromPosition;
	[Tooltip ("缩放前倍率")]
	public float zoomFromScale = 1;
	[Tooltip ("缩放后位置")]
	public Vector2 zoomToPosition;
	[Tooltip ("缩放后倍率")]
	public float zoomToScale = 1;

	public override void Execute ()
	{
		GameObject CGObject = _eventManager.CGObject;
		if (type == kEI_CGTypes.show || type == kEI_CGTypes.showZoom) {
			CGObject.GetComponent<Image> ().sprite = CG;
			CGObject.GetComponent<Image> ().SetNativeSize ();
			CGObject.SetActive (true);

			if (type == kEI_CGTypes.showZoom) {
				CGObject.transform.localScale = new Vector3 (zoomFromScale, zoomFromScale);
				CGObject.transform.localPosition = new Vector3 (zoomFromPosition.x, zoomFromPosition.y);

				CGObject.transform.DOScale (zoomToScale, zoomSpeed)
					.SetDelay (zoomShowTime)
					.OnComplete (Next);
				CGObject.transform.DOLocalMove (new Vector3 (zoomToPosition.x, zoomToPosition.y), zoomSpeed)
					.SetDelay (zoomShowTime);
			} else {
				Next ();
			}
		} else if (type == kEI_CGTypes.move) {
			CGObject.transform.DOScale (zoomToScale, zoomSpeed)
				.SetDelay (zoomShowTime)
				.OnComplete (Next);
			CGObject.transform.DOLocalMove (new Vector3 (zoomToPosition.x, zoomToPosition.y), zoomSpeed)
				.SetDelay (zoomShowTime);
		} else {
			_eventManager.CGObject.SetActive (false);
			Next ();
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
