using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNodeSO : ScriptableObject
{
    public string id;

    [HideInInspector] public List<string> childRoomLis;
    [HideInInspector] public List<string> parentRoomList;
    [HideInInspector] public List<RoomNodeTypeSO> roomNodeTypeList;
    [HideInInspector] public Dictionary<string, RoomNodeTypeSO> roomNodeTypeDictionary;
}
