using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDay.CasualGame.Editor
{
    public class MenuItems : MonoBehaviour
    {
        [MenuItem("One Day/Play")]
        static void Play()
        {
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
            EditorApplication.isPlaying = true;
        }
    }
}