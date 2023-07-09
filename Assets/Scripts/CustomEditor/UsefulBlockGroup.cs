using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UsefulBlockGroup", menuName = "ScriptableObjects/UsefulBlockGroup", order = 1)]
public class UsefulBlockGroup : SerializedScriptableObject
{

    public int Size = 3;
    [TableMatrix(HorizontalTitle = "UsefulGroup", SquareCells = true, DrawElementMethod = "DrawColoredEnumElement", HideColumnIndices = true, HideRowIndices = true, Transpose = true)]
    public bool[,] UsefulBlockField = new bool[3, 3];

    private static bool DrawColoredEnumElement(Rect rect, bool value)
    {
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            value = !value;
            GUI.changed = true;
            Event.current.Use();
        }
#if UNITY_EDITOR
        UnityEditor.EditorGUI.DrawRect(rect.Padding(1), value ? new Color(1f, 1f, 0f) : new Color(0, 0, 0, 0.5f));
#endif

        return value;
    }

}

