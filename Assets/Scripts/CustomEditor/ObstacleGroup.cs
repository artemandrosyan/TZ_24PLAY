using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObstacleGroup", menuName = "ScriptableObjects/ObstacleGroup", order = 1)]
public class ObstacleGroup : SerializedScriptableObject
{
    public int Size = 5;

    [TableMatrix(HorizontalTitle = "Obstacle", SquareCells = true, DrawElementMethod = "DrawColoredEnumElement", HideColumnIndices = true, HideRowIndices =true, Transpose = true)]
    public bool[,] ObstaclesBlockField = new bool[5, 5];

    private static bool DrawColoredEnumElement(Rect rect, bool value)
    {
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            value = !value;
            GUI.changed = true;
            Event.current.Use();
        }
#if UNITY_EDITOR
        UnityEditor.EditorGUI.DrawRect(rect.Padding(1), value ? new Color(1f, 0f, 0f) : new Color(0, 0, 0, 0.5f));
#endif
        return value;
    }
  
}
