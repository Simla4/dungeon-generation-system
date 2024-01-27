using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.MPE;

public class RoomNodeGraphEditor : EditorWindow
{
    #region Vairables

    private GUIStyle roomNodeStyle;

    private static RoomNodeGraphSO currentRoomNodeGraph;
    private RoomNodeTypeListSO roomNodeTypeList;

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
        
        //Load room node types
        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }
    
    /// <summary>
    /// Open the room node graph editor window if a room node graph scriptable object asset is double clicked in the inspectoe
    /// </summary>
    [OnOpenAsset(0)]
    public static bool OnDoubleClickedAsset(int instanceID, int line)
    {
        //for load to room type
        var roomNodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RoomNodeGraphSO;

        if (roomNodeGraph != null)
        {
            OpenEditorWindow();
            currentRoomNodeGraph = roomNodeGraph;
            return true;
        }

        return false;
    }
    
        
    /// <summary>
    /// Draw Editor GUI
    /// </summary>
    private void OnGUI()
    {
        if (currentRoomNodeGraph != null)
        {
            ProcessEvent(Event.current);

            DrawRoomNodes();
        }
    }

    private void ProcessEvent(Event currentEvent)
    {
        ProcessRoomNodeGraphEvent(currentEvent);
    }

    private void ProcessRoomNodeGraphEvent(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProccessMouseDownEvent(currentEvent);
                break;
                
            default:
                break;
        }
    }

    /// <summary>
    /// when right clicked mose down, show the context menu
    /// </summary>
    private void ProccessMouseDownEvent(Event currentEvent)
    {
        // did you right click?
        if (currentEvent.button == 1)
        {
            ShowContextMenu(currentEvent.mousePosition);
        }
    }

    private void ShowContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu();
        
        menu.AddItem(new GUIContent("Create Room Node"), false, CreateRoomNode, mousePosition);
        
        //if you right click, show context menu
        menu.ShowAsContext();
    }

    private void CreateRoomNode(object mousePosOject)
    {
        CreateRoomNode(mousePosOject, roomNodeTypeList.roomNodeTypeList.Find(x => x.isNone));
    }

    private void CreateRoomNode(object mousePosObject, RoomNodeTypeSO roomNodeType)
    {
        var mousePos = (Vector2)mousePosObject;

        var roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();
        
        currentRoomNodeGraph.roomNodeList.Add(roomNode);
        roomNode.Initialize(new Rect(mousePos, new Vector2(nodeWidht, nodeHeight)), currentRoomNodeGraph, roomNodeType);
        
        AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);
        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// Draw room nodes in the graph window
    /// </summary>
    private void DrawRoomNodes()
    {
        foreach (var roomNode in currentRoomNodeGraph.roomNodeList)
        {
            roomNode.Draw(roomNodeStyle);
        }

        GUI.changed = true;
    }

    #endregion
}
