using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(CameraController), true, isFallback = true)]
public class CameraControllerEditor : Editor
{

    CameraController cameraController;
    private ReorderableList _myList;
    SerializedProperty m_speed;
    SerializedProperty m_smooth;
    SerializedProperty m_isLooping;
    SerializedProperty m_isWaittime;
    public void OnEnable()
    {

        cameraController = (CameraController)target;

        _myList = new ReorderableList(serializedObject, serializedObject.FindProperty("waypoints"), true, true, true, true);

        _myList.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Set Waypoints", EditorStyles.boldLabel);
        };

        _myList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = _myList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
    }

    public override void OnInspectorGUI()
    {
        m_speed = serializedObject.FindProperty("speed");
        m_smooth = serializedObject.FindProperty("smooth");
        m_isWaittime = serializedObject.FindProperty("waitingTime");
        m_isLooping = serializedObject.FindProperty("isLooping");
        serializedObject.Update();
        EditorGUILayout.Slider(m_speed, 0, 100, new GUIContent("Speed"));
        EditorGUILayout.Slider(m_smooth, 0, 100, new GUIContent("Smooth"));
        EditorGUILayout.Slider(m_isWaittime, 0, 5, new GUIContent("Wait Time"));
        EditorGUILayout.PropertyField(m_isLooping, new GUIContent("Loop"));
        _myList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
