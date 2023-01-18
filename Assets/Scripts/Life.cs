using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] static int life = 5;

    TMP_Text lifeText;

    private void Awake()
    {
        lifeText = GetComponent<TMP_Text>();

        Init();    
    }

    private void Init()
    {
        UpdateText();
    }

    public void LoseLife()
    {
        life = (life > 0) ? --life : 0;
        UpdateText();
    }

    private void UpdateText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("LIFE: ");
        
        for (int i = 0; i < life; i++)
        {
            sb.Append("¢¾");
        }

        lifeText.text = sb.ToString();
    }
}
