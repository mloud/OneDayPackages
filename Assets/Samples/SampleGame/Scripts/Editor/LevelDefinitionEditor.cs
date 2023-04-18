using UnityEditor;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    [CustomEditor(typeof(LevelDefinition))]
    public class LevelDefinitionEditor : Editor 
    {
        private SerializedProperty tableProperty;
        private SerializedProperty widthProperty;
        private SerializedProperty heightProperty;

        private void OnEnable()
        {
            tableProperty = serializedObject.FindProperty("Table");
            widthProperty = serializedObject.FindProperty("Width");
            heightProperty = serializedObject.FindProperty("Height");
        }
 
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(widthProperty);
            EditorGUILayout.PropertyField(heightProperty);

            if (tableProperty.arraySize == widthProperty.intValue * heightProperty.intValue)
            {
                for (int y = 0; y < heightProperty.intValue; y++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int x = 0; x < widthProperty.intValue; x++)
                    {
                        int index = FallingBlockUtils.GetIndex(x, y,
                            widthProperty.intValue);
                        var arrayElementProperty =
                            tableProperty.GetArrayElementAtIndex(index);
                      var toggleState = (arrayElementProperty.intValue == 1);

                      toggleState = EditorGUILayout.Toggle(toggleState);
                      
                      arrayElementProperty.intValue = toggleState ? 1 : 0;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                if (GUILayout.Button("Resize"))
                {
                    tableProperty.ClearArray();
                    for (int i = 0; i < widthProperty.intValue * heightProperty.intValue; i++)
                    {
                        tableProperty.InsertArrayElementAtIndex(i);
                    }
                }
            }

            // Automatically uses the according PropertyDrawer for the type
            EditorGUILayout.PropertyField(tableProperty);

         
            // Write back changed values and evtl mark as dirty and handle undo/redo
            serializedObject.ApplyModifiedProperties();
                     
        }
    }
}