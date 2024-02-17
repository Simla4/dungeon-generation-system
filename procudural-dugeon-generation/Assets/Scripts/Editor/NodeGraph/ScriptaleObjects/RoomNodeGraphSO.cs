using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Node", menuName = "ScriptaleObjects/Dungeon Generation/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector] public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public List<RoomNodeSO> roomNodeList;
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionart;


    #region Editor

#if UNITY_EDITOR

    [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom = null;
    [HideInInspector] public Vector2 linePos;

    public void SetNodeToDrawConnectionLineFrom(Vector2 position, RoomNodeSO node)
    {
        roomNodeToDrawLineFrom = node;
        linePos = position;
    }

#endif

    #endregion
}
