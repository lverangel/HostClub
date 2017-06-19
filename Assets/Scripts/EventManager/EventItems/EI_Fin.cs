using UnityEngine;
using System.Collections;

public class EI_Fin : EventItem {

	public override void Execute(){
		_eventManager.Fin ();
	}
}
