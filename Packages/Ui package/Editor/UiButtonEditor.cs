using OneDay.Ui;
using UnityEditor;
using UnityEditor.UI;

namespace OneDay.Ui.Editor
{
    [CustomEditor(typeof(UiButton), true)]
    [CanEditMultipleObjects]
    public class UiButtonEditor : ButtonEditor
    {
        private SerializedProperty onStateChangedProperty;
        private SerializedProperty labelProperty;
        private SerializedProperty buttonContainerProperty;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            onStateChangedProperty = serializedObject.FindProperty("OnStateTransition");
            labelProperty = serializedObject.FindProperty("label");
            buttonContainerProperty = serializedObject.FindProperty("buttonContainer");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(labelProperty);
            EditorGUILayout.PropertyField(onStateChangedProperty);
            EditorGUILayout.PropertyField(buttonContainerProperty);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}