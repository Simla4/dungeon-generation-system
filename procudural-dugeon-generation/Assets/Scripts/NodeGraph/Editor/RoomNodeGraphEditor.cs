using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class RoomNodeGraphEditor : EditorWindow
{
    #region Vairables

    private GUIStyle roomNodeStyle;

    private const float nodeWidht = 160f;
    private const float nodeHeight = 75;
    private const int nodeBorder = 12;
    private const int nodePadding = 25;

    #endregion

    #region CallBacks

    private void OnEnable()
    {
        NodeStyle();
    }

    #endregion

    #region OtherMethods

    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
    private static void OpenEditorWindow()
    {
        GetWindow<RoomNodeGraphEditor>("Romm Node Graph Editor");
    }

    private void NodeStyle()
    {
        roomNodeStyle = new GUIStyle();

        roomNodeStyle.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
        roomNodeStyle.normal.textColor = Color.white;
        roomNodeStyle.border = new RectOffset(nodeBorder, nodeBorder, nodeBorder, nodeBorder);
        roomNodeStyle.padding = new RectOffset(nodePadding, nodePadding, nodePadding, nodePadding);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(new Vector2(100, 100), new Vector2(nodeWidht, nodeHeight)), roomNodeStyle);
        EditorGUILayout.LabelField("node 1");
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(new Vector2(200, 300), new Vector2(nodeWidht, nodeHeight)), roomNodeStyle);
        EditorGUILayout.LabelField("node 2");
        GUILayout.EndArea();
    }

    #endregion
}
