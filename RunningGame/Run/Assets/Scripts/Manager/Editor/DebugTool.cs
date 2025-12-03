#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class DebugToolEditor : EditorWindow
{
    [MenuItem("Tools/Debug Tool")]
    public static void ShowWindow()
    {
        GetWindow<DebugToolEditor>("Debug Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Debug Tool", EditorStyles.boldLabel);

        GameManager gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            EditorGUILayout.LabelField("GameManager", gameManager.name);
        }
        else
        {
            EditorGUILayout.HelpBox("GameManager Instance를 찾을 수 없습니다.", MessageType.Warning);
        }

        if (GUILayout.Button("Trigger Game Over"))
        {
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
            else
            {
                Debug.LogWarning("GameManager Instance가 존재하지 않습니다.");
            }
        }
    }
}
#endif
