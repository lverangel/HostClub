using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(EI_CGController))]
public class Inspector_EI_CGController : Editor
{
	private SerializedObject obj;
	private SerializedProperty jumpTo, sound, delayTime, type, CG, zoomShowTime, zoomSpeed, zoomFromPosition, zoomFromScale, zoomToPosition, zoomToScale;

	void OnEnable ()
	{
		obj = new SerializedObject (target);
		jumpTo = obj.FindProperty ("jumpTo");
		sound = obj.FindProperty ("sound");
		delayTime = obj.FindProperty ("delayTime");

		type = obj.FindProperty ("type");
		CG = obj.FindProperty ("CG");
		zoomShowTime = obj.FindProperty ("zoomShowTime");
		zoomSpeed = obj.FindProperty ("zoomSpeed");
		zoomFromPosition = obj.FindProperty ("zoomFromPosition");
		zoomFromScale = obj.FindProperty ("zoomFromScale");
		zoomToPosition = obj.FindProperty ("zoomToPosition");
		zoomToScale = obj.FindProperty ("zoomToScale");
	}

	public override void OnInspectorGUI ()
	{
		obj.Update ();
		EditorGUILayout.PropertyField (jumpTo);
		EditorGUILayout.PropertyField (sound);
		EditorGUILayout.PropertyField (delayTime);

		GUILayout.Label ("------------------------------------------------------------------------");
		EditorGUILayout.PropertyField (type);
		if (type.enumValueIndex == 0) {
			EditorGUILayout.PropertyField (CG);
		} else if (type.enumValueIndex == 1) {
			EditorGUILayout.PropertyField (CG);
			EditorGUILayout.PropertyField (zoomShowTime);
			EditorGUILayout.PropertyField (zoomSpeed);
			EditorGUILayout.PropertyField (zoomFromPosition);
			EditorGUILayout.PropertyField (zoomFromScale);
			EditorGUILayout.PropertyField (zoomToPosition);
			EditorGUILayout.PropertyField (zoomToScale);
		}
		if (type.enumValueIndex == 2) {
		}

		obj.ApplyModifiedProperties ();
	}
}

