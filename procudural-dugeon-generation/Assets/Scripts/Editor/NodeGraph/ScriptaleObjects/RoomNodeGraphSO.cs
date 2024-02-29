using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Node", menuName = "ScriptaleObjects/Dungeon Generation/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector] public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public List<RoomNodeSO> roomNodeList = new List<RoomNodeSO>();
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionary = new Dictionary<string, RoomNodeSO>();


    #region Editor

#if UNITY_EDITOR
    
    [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom = null;
    [HideInInspector] public Vector2 linePos;

    private void Awake()
    {
        LoadRoomNodeDictionary();
    }

    public void LoadRoomNodeDictionary()
    {
        roomNodeDictionary.Clear();
        
        foreach (var node in roomNodeList)
        {
            roomNodeDictionary[node.id] = node;
        }   
    }

    public void SetNodeToDrawConnectionLineFrom(Vector2 position, RoomNodeSO node)
    {
        roomNodeToDrawLineFrom = node;
        linePos = position;
    }

    public void OnValidate()
    {
        LoadRoomNodeDictionary();
    }

#endif

    #endregion
}
