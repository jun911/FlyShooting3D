using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ScoreBoard scoreBoard;

    private void Awake()
    {
        instance = this;
    }
}