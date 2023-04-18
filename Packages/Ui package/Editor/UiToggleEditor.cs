using UnityEditor;
using UnityEditor.UI;

namespace OneDay.Ui.Editor
{
    [CustomEditor(typeof(UiToggle), true)]
    [CanEditMultipleObjects]
    public class UiToggleEditor : ToggleEditor
    {
        private SerializedProperty OffTransformProperty;
        private SerializedProperty OnTransformProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            OffTransformProperty = serializedObject.FindProperty("OffTransform");
            OnTransformProperty = serializedObject.FindProperty("OnTransform");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            
            EditorGUILayout.PropertyField(OffTransformProperty);
            EditorGUILayout.PropertyField(OnTransformProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}