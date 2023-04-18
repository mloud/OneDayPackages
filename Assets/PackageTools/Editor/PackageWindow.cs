using PackageTools.Editor;
using UnityEditor;
using UnityEngine;

namespace OneDay.PackageTools.Editor
{
    public class PackageWindow : EditorWindow
    {
        private PackageJsonFiles JsonFiles
        {
            get
            {
                if (packageJsonFiles == null)
                {
                    packageJsonFiles = AssetDatabase.LoadAssetAtPath<PackageJsonFiles>("Assets/PackageTools/Settings/PackageRegistry.asset");
                }
                return packageJsonFiles;
            }
        }


        private PackageModels PackageModels
        {
            get
            {
                if (packageModels == null)
                {
                    packageModels = PackageModelsBuilder.Create(JsonFiles);
                }

                return packageModels;
            }
        }

        private PackageJsonFiles packageJsonFiles;
        private PackageModels packageModels;

        private Vector2 scrollPosition;
        private StringFilter filter;
        
        [MenuItem("PackageTools/Package Window")]
        static void Init()
        {
            var window = (PackageWindow) EditorWindow.GetWindow(typeof(PackageWindow));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Registered packages", EditorStyles.boldLabel);
            GUILayout.Space(20);

            filter ??= new StringFilter();
            EditorGUILayout.BeginHorizontal();
            filter.Mask  = EditorGUILayout.TextField("Filter", filter.Mask);
            if (GUILayout.Button("X"))
            {
                filter.Mask = null;
            }
            
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            foreach (var package in PackageModels.Packages)
            {
                if (!filter.IsFiltered(package.name) && !filter.IsFiltered(package.displayName))
                    continue;
                    
                EditorGUILayout.LabelField(package.name);
                EditorGUILayout.LabelField(package.displayName);
                package.version = EditorGUILayout.TextField("Version",package.version);

                if (GUILayout.Button("Save"))
                {
                    package.Save();
                }
                
                if (GUILayout.Button("Publish"))
                {
                    PackagePublisher.Publish(package);
                }
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
            }
            EditorGUILayout.EndScrollView();
        }
    }
}