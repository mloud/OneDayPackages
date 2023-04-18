using OneDay.PackageTools;
using UnityEditor;

namespace PackageTools.Editor
{
    [CustomEditor(typeof(PackageJsonFiles))]
    [CanEditMultipleObjects]
    public class PackageJsonFilesEditor : UnityEditor.Editor 
    {
        SerializedProperty packageFilesProperty;
    
        private void OnEnable()
        {
            packageFilesProperty = serializedObject.FindProperty("packageFiles");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Package group");
            EditorGUILayout.Space();
            serializedObject.Update();
            EditorGUILayout.PropertyField(packageFilesProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}