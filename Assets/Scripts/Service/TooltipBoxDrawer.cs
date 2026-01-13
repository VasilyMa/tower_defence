/*#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TooltipBoxAttribute))]
public class TooltipBoxDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var a = (TooltipBoxAttribute)attribute;

        GUIStyle richStyle = new GUIStyle(EditorStyles.helpBox);
        richStyle.richText = true;

        float textHeight = richStyle.CalcHeight(new GUIContent(a.Text), EditorGUIUtility.currentViewWidth);

        return textHeight + EditorGUI.GetPropertyHeight(property, label, true) + 6f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var a = (TooltipBoxAttribute)attribute;

        GUIStyle richStyle = new GUIStyle(EditorStyles.helpBox);
        richStyle.richText = true;

        Rect boxRect = new Rect(position.x, position.y, position.width,
            richStyle.CalcHeight(new GUIContent(a.Text), position.width));

        GUI.Label(boxRect, a.Text, richStyle);

        Rect fieldRect = new Rect(position.x, boxRect.yMax + 2, position.width,
            EditorGUI.GetPropertyHeight(property, label, true));

        EditorGUI.PropertyField(fieldRect, property, label, true);
    }
}

#endif*/