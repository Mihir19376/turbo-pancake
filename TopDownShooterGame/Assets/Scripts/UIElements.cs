using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElements : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
