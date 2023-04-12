using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyFlyStart : MonoBehaviour
{
    public GameObject startPanel;
    public Text startText;

    private void Awake() => LevelLoader.SceneRevealed += OnLevelStarted;

    void OnLevelStarted()
    {
        startPanel.SetActive(true);

        StartCoroutine(CountDown(3));
    }

    IEnumerator CountDown(int seconds)
    {
        while (seconds > 0)
        {
            startText.text = (--seconds).ToString();
            yield return new WaitForSeconds(1);
        }

        startPanel.SetActive(false);

        //PlayerController.Instance.EnableMovement();
        GameManager.Instance.SetState(GameManager.GameState.Flying);
    }
}
