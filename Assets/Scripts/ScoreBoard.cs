using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int score = 0;

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        Debug.Log("score:" + score);
    }
}
