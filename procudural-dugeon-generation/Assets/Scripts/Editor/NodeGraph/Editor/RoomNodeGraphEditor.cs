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
    private RoomNodeSO currentRoomNode = null;

    private const float nodeWidht = 160f;
    private const float nodeHeight = 75;
    private const int nodeBorder = 12;
    private const int nodePadding = 25;
    private const float lineWidth = 5f;

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
            DrawDraggedLine();
            
            ProcessEvent(Event.current);

            DrawRoomNodes();
        }
    }

    private void DrawDraggedLine()
    {
        var node = currentRoomNodeGraph.roomNodeToDrawLineFrom;
        var linePos = currentRoomNodeGraph.linePos;

        
        if (linePos != Vector2.zero)
        {
            Handles.DrawBezier(node.rect.center, linePos, node.rect.center, 
                linePos, Color.white,null, lineWidth);
        }
    }

    private void ProcessEvent(Event currentEvent)
    {
        if (currentRoomNode == null || currentRoomNode.isLeftClikDragging == false)
        {
            currentRoomNode = IsMouseOverRoomNode(currentEvent);
        }

        if (currentRoomNode == null || currentRoomNodeGraph.roomNodeToDrawLineFrom != null)
        {
            ProcesRoomNodeGraphEvent(currentEvent);
        }
        else
        {
            currentRoomNode.ProcessEvent(currentEvent);
        }
    }

    private RoomNodeSO IsMouseOverRoomNode(Event currentEvent)
    {
        var currentRoomNodeGraphList = currentRoomNodeGraph.roomNodeList;
        /* if current mouse position equals whatever node position  */
        for (int i = currentRoomNodeGraphList.Count - 1; i >= 0; i--)
        {
            if (currentRoomNodeGraphList[i].rect.Contains(currentEvent.mousePosition))
            {
                return currentRoomNodeGraphList[i];
            }
        }

        return null;
    }

    private void ProcesRoomNodeGraphEvent(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProccessMouseDownEvent(currentEvent);
                break;
            case EventType.MouseDrag:
                ProccessMouseDragEvent(currentEvent);
                break;
            case EventType.MouseUp:
                ProceesMouseUpEvent(currentEvent);
                break;
                
            default:
                break;
        }
    }

    private void ProccessMouseDragEvent(Event currentEvent)
    {
        if (currentEvent.button == 1)
        {
            ProcessRightClikDragEvent(currentEvent);
        }
    }

    private void ProcessRightClikDragEvent(Event currentEvent)
    {
        if (currentRoomNodeGraph.linePos != null)
        {
            DragConnecionLine(currentEvent.delta);
            GUI.changed = true;
        }
    }

    private void DragConnecionLine(Vector2 delta)
    {
        currentRoomNodeGraph.linePos += delta;
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

    private void ProceesMouseUpEvent(Event currentEvent)
    {
        var roomNodeToDrawLineFrom = currentRoomNodeGraph.roomNodeToDrawLineFrom;
        if (currentEvent.button == 1 && roomNodeToDrawLineFrom != null)
        {
            var roomNode = IsMouseOverRoomNode(currentEvent);

            if (roomNodeToDrawLineFrom.AddChildRoomNodeToRoomNoode(roomNode.id))
            {
                roomNodeToDrawLineFrom.AddParentRoomNodeToRoomNode(roomNode.id);
            }
            
            ClearLineDrag();
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

    private void ClearLineDrag()
    {
        currentRoomNodeGraph.roomNodeToDrawLineFrom = null;
        currentRoomNodeGraph.linePos = Vector2.zero;
        GUI.changed = true;
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
