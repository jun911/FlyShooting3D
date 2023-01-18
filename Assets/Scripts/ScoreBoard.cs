using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private int score = 0;

    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amountToIncrease)
    {
        Debug.Log("call IncreaseScore scoreText:" + scoreText);
        score += amountToIncrease;
        scoreText.text = score.ToString();
    }
}