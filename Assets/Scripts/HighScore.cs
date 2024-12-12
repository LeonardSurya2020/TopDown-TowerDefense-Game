using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    // Start is called before the first frame update
    void Start()
    {
        float elapsedTime = PlayerPrefs.GetFloat("HighScore", 0);
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
