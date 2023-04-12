using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatisticsWindow : Singleton<LevelStatisticsWindow>
{

    //End Statistics
    public GameObject panel;
    public Text levelNameText;
    public Text secondsText, millisecondsText, smallPicked, smallAll, bigPicked, bigAll;

    protected override void Awake()
    {
        base.Awake();
        LevelLoader.SceneLoaded += OnLevelLoaded;
    }
    void OnDestroy() => LevelLoader.SceneLoaded -= OnLevelLoaded;

    void OnLevelLoaded(string name)
    {
        levelNameText.text = name;
        panel.SetActive(false);
    }

    public void DisplayLevelStatistics( LevelStatistics s )
    {
        panel.SetActive(true);

        levelNameText.text = s.name;

        secondsText.text = s.seconds.ToString();

        smallPicked.text = s.smallDiamondsPicked.ToString();
        smallAll.text = s.smallDiamondsAll.ToString();

        bigPicked.text = s.bigDiamondsPicked.ToString();
        bigAll.text = s.bigDiamondsAll.ToString();
    }

    public struct LevelStatistics
    {
        public string name;
        public int seconds;
        public int smallDiamondsPicked, smallDiamondsAll, bigDiamondsPicked, bigDiamondsAll;

        public LevelStatistics( string name, int seconds, int smallDiamondsPicked, int smallDiamondsAll, int bigDiamondsPicked, int bigDiamondsAll)
        {
            this.name = name;
            this.smallDiamondsPicked = smallDiamondsPicked;
            this.smallDiamondsAll = smallDiamondsAll;
            this.bigDiamondsPicked = bigDiamondsPicked;
            this.bigDiamondsAll = bigDiamondsAll;
            this.seconds = seconds;
        }
    }
}
