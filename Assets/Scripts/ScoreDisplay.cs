//using System;
//using UnityEngine;
//using UnityEngine.UI;

//public class ScoreDisplay : MonoBehaviour
//{
//    public Text scoreText; // Đây là public để bạn có thể kéo và thả trong Unity Editor
//    public Text highestScoreText;
//    private int score = 0; // Điểm số hiện tại
//    private int  highestScore = 0; // Điểm số cao nhất


//    public void Awake()
//    {
//        highestScore = PlayerPrefs.GetInt("HighestScore", highestScore);
//        if (highestScoreText != null)
//        {
//            highestScoreText.text = highestScore.ToString();
//        }
//    }


//    public void ScoreExtra()
//    {
//        score++;
//        if (scoreText != null)
//        {
//            scoreText.text = score.ToString();
//        }
//        PlayerPrefs.SetInt("Score", score);
//        PlayerPrefs.Save();
//    }

//    public void HighestScore()
//    {
//        score = PlayerPrefs.GetInt("Score", score);
//        //highestScore = PlayerPrefs.GetInt("HighestScore", highestScore);
//        if (score > highestScore)
//        {
//            Debug.Log(score);
//            highestScore = score;
//            PlayerPrefs.SetInt("HighestScore", highestScore);
//            PlayerPrefs.Save(); 
//            if (highestScoreText != null)
//            {
//                highestScoreText.text = highestScore.ToString();
//            }
//        }
//        scoreText.text = score.ToString();
//    }


//}

using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText; // Đây là public để bạn có thể kéo và thả trong Unity Editor
    public Text highestScoreText;
    public GameObject medal;
    private int score = 0; // Điểm số hiện tại
    private int highestScore = 0; // Điểm số cao nhất

    

    public void ScoreExtra()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    public void HighestScore()
    {
        GameObject scoreDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay");
        SpriteRenderer medalGold = medal.GetComponent<SpriteRenderer>();

        if (scoreDisplay != null)
        {
            ScoreDisplay scoreDisplayComponent = scoreDisplay.GetComponent<ScoreDisplay>();
            score = scoreDisplayComponent.score;
        }
        if(score != 0)
        {
            score = PlayerPrefs.GetInt("Score", score);
        }
        
        highestScore = PlayerPrefs.GetInt("HighestScore", highestScore);
        
        if (score > highestScore)
        {
            highestScore = score;
            medalGold.sortingOrder = 1;
            PlayerPrefs.SetInt("HighestScore", highestScore);
            PlayerPrefs.Save();
        }
        if (highestScoreText != null)
        {
            highestScoreText.text = highestScore.ToString();
        }
        scoreText.text = score.ToString();
    }


}
