#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TooltipBoxAttribute))]
public class TooltipBoxDecoratorDrawer : DecoratorDrawer
{
    private const float Padding = 4f;

    public override float GetHeight()
    {
        TooltipBoxAttribute a = (TooltipBoxAttribute)attribute;

        GUIStyle style = new GUIStyle(EditorStyles.helpBox)
        {
            richText = true,
            wordWrap = true
        };
         
        float viewWidth = EditorGUIUtility.currentViewWidth;
        float contentWidth = viewWidth - Padding * 2f - 20f; 

        float height = style.CalcHeight(
            new GUIContent(a.Text),
            contentWidth
        );

        return height + Padding * 2f;
    }

    public override void OnGUI(Rect position)
    {
        TooltipBoxAttribute a = (TooltipBoxAttribute)attribute;

        GUIStyle style = new GUIStyle(EditorStyles.helpBox)
        {
            richText = true,
            wordWrap = true
        };
         
        Rect contentRect = new Rect(
            position.x + Padding,
            position.y + Padding,
            position.width - Padding * 2f,
            position.height - Padding * 2f
        );

        GUI.Label(contentRect, a.Text, style);
    }
}
#endif 