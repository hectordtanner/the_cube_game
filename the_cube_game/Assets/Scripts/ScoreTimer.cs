using UnityEngine;
using TMPro;

public class ScoreTimer : MonoBehaviour
{
    
    public float timer = 1.0f;

    public float timerStart;

    private TextMeshProUGUI timerText;

    [SerializeField]
    private GameObject container;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timer = 2 * timerStart;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = ("Time: " + timer.ToString("F2") + "s");
        if (timer <= 0)
        {
            container.SetActive(true);
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }
    }
}
