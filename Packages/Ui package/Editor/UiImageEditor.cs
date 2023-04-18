using UnityEditor;
using UnityEditor.UI;

namespace OneDay.Ui.Editor
{
    [CustomEditor(typeof(UiImage), true)]
    [CanEditMultipleObjects]
    public class UiImageEditor : ImageEditor
    {
        private SerializedProperty imageAssetReference;
        private SerializedProperty imageAlpha;
        private SerializedProperty unloadOnDisableProperty;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            imageAssetReference = serializedObject.FindProperty("ImageAssetReference");
            imageAlpha = serializedObject.FindProperty("ImageAlpha");
            unloadOnDisableProperty = serializedObject.FindProperty("unloadOnDisable");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(imageAssetReference);
            EditorGUILayout.PropertyField(imageAlpha);
            EditorGUILayout.PropertyField(unloadOnDisableProperty);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}