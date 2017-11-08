using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public NejikoController nejiko;
    public Text scoreLabel;
    public Text moneyLabel;
    public LifePanel lifePanel;

	public void Update()
    {
        //お金更新
        int money = nejiko.Money();
        moneyLabel.text = "Money: " + money + "$";

        //スコア更新
        int score = CalcScore();
        scoreLabel.text = "Score: " + score + "m";

        //ライフパネルを更新
        lifePanel.UpdateLife(nejiko.Life());

        //ネジ子のライフが0になったらゲームオーバー
        if (nejiko.Life() <= 0)
        {
            //これ以降のUpdateはやめる
            enabled = false;

            //ハイスコア更新
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            //2秒後にReturnToTitleを呼び出す
            Invoke("ReturnToTitle", 2.0f);
        }

        if(nejiko.Position() >= 500.0f)
        {
            enabled = false;
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            //2秒後にReturnToTitleを呼び出す
            Invoke("ReturnToStageSerect", 2.0f);
        }
    }

    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }


    void ReturnToTitle()
    {
        Application.LoadLevel("Title");
    }

    void ReturnToStageSerect()
    {
        Application.LoadLevel("StageSerect");
    }
}
