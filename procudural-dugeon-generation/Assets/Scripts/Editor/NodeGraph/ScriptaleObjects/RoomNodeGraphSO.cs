using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Node", menuName = "ScriptaleObjects/Dungeon Generation/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector] public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public List<RoomNodeSO> roomNodeList;
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionart;
    
}
