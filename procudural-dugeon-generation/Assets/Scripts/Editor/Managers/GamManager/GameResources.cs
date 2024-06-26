using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    #region Variables

    public RoomNodeTypeListSO roomNodeTypeList;

    #endregion

    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }

            return instance;
        }
    }

   /* #region Header Dungeon

    [Space(10)]
    [Header("DUNGEON")]
    #endregion

    #region Tooltip
    [Tooltip("Populate with the dungeon RoomNodeTypeListSO")]
    #endregion*/
}
