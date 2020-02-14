using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public Text m_textHighScoreLabel = null;

    public void Start()
    {
        m_textHighScoreLabel.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("RunGame");
    }
}
