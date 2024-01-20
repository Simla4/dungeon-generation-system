using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Room Node", menuName = "ScriptaleObjects/Dungeon Generation/Room Node")]

public class RoomNodeSO : ScriptableObject
{
    public string id;

    [HideInInspector] public List<string> childRoomLis;
    [HideInInspector] public List<string> parentRoomList;
    [HideInInspector] public List<RoomNodeTypeSO> roomNodeTypeList;
    [HideInInspector] public Dictionary<string, RoomNodeTypeSO> roomNodeTypeDictionary;
}
