//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//
//[CustomEditor (typeof(EI_Dialog))]
//public class Inspector_EI_Dialog : Editor
//{
//	private SerializedObject obj;
//	private SerializedProperty jumpTo,sound,delayTime;
//
//	void OnEnable ()
//	{
//		obj = new SerializedObject (target);
//		jumpTo = obj.FindProperty ("jumpTo");
//		sound = obj.FindProperty ("sound");
//		delayTime = obj.FindProperty ("delayTime");
//
//		ID = obj.FindProperty ("ID");
//		text = obj.FindProperty ("text");
//
//		characterEmote = obj.FindProperty ("characterEmote");
//	}
//
//	public override void OnInspectorGUI ()
//	{
//		obj.Update ();
//		EditorGUILayout.PropertyField (jumpTo);
//		EditorGUILayout.PropertyField (sound);
//		EditorGUILayout.PropertyField (delayTime);
//
//		GUILayout.Label ("------------------------------------------------------------------------");
//		EditorGUILayout.PropertyField (type);
//		EditorGUILayout.PropertyField (ID);
//		if (type.enumValueIndex == 0) {
//			EditorGUILayout.PropertyField (speedFactor);
//			EditorGUILayout.PropertyField (characterEmote);
//			EditorGUILayout.PropertyField (startPosition);
//			EditorGUILayout.PropertyField (endPosition);
//			EditorGUILayout.PropertyField (characterObject);
//		} else if (type.enumValueIndex == 1) {
//			EditorGUILayout.PropertyField (speedFactor);
//			EditorGUILayout.PropertyField (characterEmote);
//			EditorGUILayout.PropertyField (endPosition);
//		}
//		if (type.enumValueIndex == 2) {
//			EditorGUILayout.PropertyField (speedFactor);
//			EditorGUILayout.PropertyField (characterEmote);
//			EditorGUILayout.PropertyField (endPosition);
//
//		}
//
//		obj.ApplyModifiedProperties ();
//	}
//}
//
