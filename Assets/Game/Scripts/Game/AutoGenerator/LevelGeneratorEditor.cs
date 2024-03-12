using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class RectangleGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Bắt đầu quá trình cập nhật serializedObject

        LevelGenerator generator = (LevelGenerator)target;



        SerializedProperty levelAssetProp = serializedObject.FindProperty("levelAsset");
        EditorGUILayout.PropertyField(levelAssetProp , new GUIContent("Level Asset"));

        GUILayout.Space(10);

        // Sử dụng SerializedProperty để cập nhật giá trị của biến trong serializedObject
        SerializedProperty rowProp = serializedObject.FindProperty("row");
        rowProp.intValue=EditorGUILayout.IntField("Row" , rowProp.intValue);

        SerializedProperty colProp = serializedObject.FindProperty("col");
        colProp.intValue=EditorGUILayout.IntField("Col" , colProp.intValue);

        SerializedProperty difficultProp = serializedObject.FindProperty("difficult");
        difficultProp.intValue=EditorGUILayout.IntField("Difficult" , difficultProp.intValue);

        GUILayout.Space(30);
        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }
        GUILayout.Space(30);
        SerializedProperty randomTime = serializedObject.FindProperty("randomTime");
        randomTime.intValue=EditorGUILayout.IntField("randomTime" , randomTime.intValue);
        if (GUILayout.Button("Random Generate"))
        {
            generator.RandomGenerate();
        }

        // Kết thúc quá trình cập nhật serializedObject
        serializedObject.ApplyModifiedProperties();

        // Đảm bảo rằng các thay đổi được ghi lại
        if (GUI.changed)
        {
            EditorUtility.SetDirty(generator);
        }
    }
}
