using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    #region Singleton
    public static PlayerPrefsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public void SetCurrentScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
    }
    public int GetCurrentScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
}
