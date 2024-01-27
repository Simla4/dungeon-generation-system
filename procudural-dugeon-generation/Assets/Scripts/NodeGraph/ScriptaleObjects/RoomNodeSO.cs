using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Room Node", menuName = "ScriptaleObjects/Dungeon Generation/Room Node")]

public class RoomNodeSO : ScriptableObject
{
    public string id;

    [HideInInspector] public List<string> childRoomList = new List<string>();
    [HideInInspector] public List<string> parentRoomList = new List<string>();
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
    [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
    [HideInInspector] public Dictionary<string, RoomNodeTypeSO> roomNodeTypeDictionary;

    public RoomNodeTypeSO roomNodeType;

    #region Editor code

#if UNITY_EDITOR

    [HideInInspector] public Rect rect;

    public void Initialize(Rect rect, RoomNodeGraphSO roomNodeGraph, RoomNodeTypeSO roomNodeType)
    {
        this.rect = rect;
        this.id = Guid.NewGuid().ToString();
        this.name = "Room Node";
        this.roomNodeGraph = roomNodeGraph;
        this.roomNodeType = roomNodeType;

        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }

    public void Draw(GUIStyle nodeStyle)
    {
        GUILayout.BeginArea(rect, nodeStyle);
        
        EditorGUI.BeginChangeCheck();
        var sellected = roomNodeTypeList.roomNodeTypeList.FindIndex(x => x == roomNodeType);
        var sellection = EditorGUILayout.Popup("", sellected, GetRoomNodeTypeToDisplay());
        
        roomNodeType = roomNodeTypeList.roomNodeTypeList[sellection];
        
        if(EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(this);
        
        GUILayout.EndArea();
    }

    private string[] GetRoomNodeTypeToDisplay()
    {
        var roomArray = new string[roomNodeTypeList.roomNodeTypeList.Count];
        var roomNodeTypeListCount = roomNodeTypeList.roomNodeTypeList.Count;

        for (int i = 0; i < roomNodeTypeListCount; i++)
        {
            if (roomNodeTypeList.roomNodeTypeList[i].disableInNodeGraphEditor)
            {
                roomArray[i] = roomNodeTypeList.roomNodeTypeList[i].roomNodeTypeName;
            }
        }

        return roomArray;
    }

#endif

    #endregion
}
