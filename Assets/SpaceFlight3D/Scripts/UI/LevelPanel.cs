using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    public UnityEngine.UI.Button button;
    public Text levelNameText;

    internal void Setup(string name)
    {
        levelNameText.text = name;
        button.onClick.AddListener(LoadLevel);
    }

    void LoadLevel()
    {
        GameManager.LoadLevel(levelNameText.text);
    }
}
