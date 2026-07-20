using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    private Material material;

    private bool overrideColor = false;
    private float colorFade = 0.0f;
    private Color targetColor = Color.white;
    private Color buttonColor = Color.white;

    private List<float> goals = new List<float>();
    private List<CheckController> checks = new List<CheckController>();

    public int score = 0;

    [SerializeField]
    private float lenience = 0.1f;

    [SerializeField]
    private Image goalDisplay;

    [SerializeField]
    private CheckController redLever;

    [SerializeField]
    private CheckController blueLever;

    [SerializeField]
    private CheckController greenLever;

    [SerializeField]
    private float fadeSpeed = 0.1f;

    [SerializeField]
    private float newGoalDifference = 0.1f;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;

        goals.Add(Random.Range(0.0f, 1.0f));
        goals.Add(Random.Range(0.0f, 1.0f));
        goals.Add(Random.Range(0.0f, 1.0f));
        
        goalDisplay.color = new Color(goals[0], goals[1], goals[2], 1.0f);

        checks.Add(redLever);
        checks.Add(greenLever);
        checks.Add(blueLever);
    }

    void OnMouseDown()
    {
        if (FullCheck())
        {
            score ++;
            scoreText.text = "Score: " + score;
            
            for (int i = 0; i < goals.Count; i++)
            {
                if (Random.value < goals[i])
                {
                    goals[i] = Random.Range(0.0f, goals[i] - newGoalDifference);
                }
                else
                {
                    goals[i] = Random.Range(goals[i] + newGoalDifference, 1.0f);
                }
                goals[i] = Mathf.Clamp(goals[i], 0.0f, 1.0f);
            }

            goalDisplay.color = new Color(goals[0], goals[1], goals[2], 1.0f);

            colorFade = 1;
            overrideColor = true;
            targetColor = Color.green;
        }
        else
        {
            colorFade = 1;
            overrideColor = true;
            targetColor = Color.red;
        }
    }

    void Update()
    {
        buttonColor = new Color(redLever.checkValue, greenLever.checkValue, blueLever.checkValue, 1.0f).linear;

        colorFade -= fadeSpeed;
        colorFade = Mathf.Clamp(colorFade, 0.0f, 1.0f);

        if (colorFade == 0)
        {
            overrideColor = false;
        }
        material.color = Color.Lerp(buttonColor, targetColor, colorFade);
        
        if (!overrideColor)
        {
            material.SetColor("BaseColor", buttonColor);
        }
    }

    bool FullCheck()
    {
        for (int i = 0; i < checks.Count; i++)
        {
            if (!(checks[i].checkValue - lenience < goals[i] && goals[i] < checks[i].checkValue + lenience))
            {
                return false;
            }
        }
        return true;
    }
}