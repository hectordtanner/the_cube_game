using UnityEngine;
using TMPro;

public class ScoreTimer : MonoBehaviour
{
    
    public float pointTimer;

    public float pointTimerStart;

    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        pointTimer = 2 * pointTimerStart;
    }

    void Update()
    {
        pointTimer -= Time.deltaTime;
        timerText.text = ("Time: " + pointTimer.ToString("F2") + "s");
    }
}
