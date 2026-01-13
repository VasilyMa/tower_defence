using UnityEngine;

public class TooltipBoxAttribute : PropertyAttribute
{
    public string Text;
    public TooltipBoxAttribute(string text)
    {
        Text = text;
    }
}