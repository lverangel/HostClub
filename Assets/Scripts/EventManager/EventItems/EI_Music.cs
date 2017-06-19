using UnityEngine;
using System.Collections;

public class EI_Music : EventItem
{
	[Tooltip ("音乐操作")]
	public kEI_MusicTypes type;
	[Tooltip ("选择音频文件")]
	public AudioClip audioClip;

	public override void Execute ()
	{
		base.Execute ();
		if (type == kEI_MusicTypes.play) {
			_gameManager.PlayMusic (audioClip);
		} else if (type == kEI_MusicTypes.stop) {
			_gameManager.StopMusic ();
		}
		Next ();
	}
}
