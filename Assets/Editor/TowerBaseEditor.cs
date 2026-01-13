using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerBase))]
public class TowerBaseEditor : Editor
{
    SerializedProperty viewObject;
    SerializedProperty projectileObject;
    SerializedProperty range;
    SerializedProperty damage;
    SerializedProperty speed;
    SerializedProperty rotation;
    SerializedProperty cost;
    SerializedProperty type;
    SerializedProperty radius;
    SerializedProperty height;
    SerializedProperty missileSpeed;

    private void OnEnable()
    {
        viewObject = serializedObject.FindProperty("ViewObject");
        projectileObject = serializedObject.FindProperty("ProjectileObject");
        range = serializedObject.FindProperty("Range");
        damage = serializedObject.FindProperty("Damage");
        speed = serializedObject.FindProperty("Speed");
        cost = serializedObject.FindProperty("Cost");
        type = serializedObject.FindProperty("Type");
        radius = serializedObject.FindProperty("Radius");
        height = serializedObject.FindProperty("Height");
        rotation = serializedObject.FindProperty("Rotation");
        missileSpeed = serializedObject.FindProperty("MissileSpeed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(viewObject);
        EditorGUILayout.PropertyField(projectileObject);
        EditorGUILayout.PropertyField(range);
        EditorGUILayout.PropertyField(damage);
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(rotation);
        EditorGUILayout.PropertyField(cost);
        EditorGUILayout.PropertyField(type);
        EditorGUILayout.PropertyField(missileSpeed);


        // Условное отображение
        if ((TowerType)type.enumValueIndex == TowerType.launcher)
        {
            EditorGUILayout.PropertyField(radius);
            EditorGUILayout.PropertyField(height);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
