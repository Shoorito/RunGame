using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text m_textScoreLabel               = null;
    public LifePanel m_lifePanel               = null;
    public PlayerController m_playerController = null;

    void Update()
    {
        int nScore = 0;

        nScore = calcScore();
        m_textScoreLabel.text = "Score : " + nScore + "m";

        m_lifePanel.updateLife(m_playerController.getLife());

        if(m_playerController.getLife() <= 0)
        {
            enabled = false;

            if (PlayerPrefs.GetInt("HighScore") < nScore)
                PlayerPrefs.SetInt("HighScore", nScore);

            Invoke("ReturnToTitle", 2.0f);
        }
    }

    int calcScore()
    {
        return (int)m_playerController.transform.position.z;
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
