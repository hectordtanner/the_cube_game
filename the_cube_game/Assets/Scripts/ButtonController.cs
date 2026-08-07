using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ButtonController : MonoBehaviour
{
    private Material material;

    private bool overrideColor = false;
    private float colorFade = 0.0f;
    private Color targetColor = Color.white;
    private Color buttonColor = Color.white;

    private List<float> goals = new List<float>();
    private List<CheckController> leverChecks = new List<CheckController>();

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

    private int wrongCount = 0;

    [SerializeField]
    private int hintThreshold;

    private string[] colours = new string[] {"red", "green", "blue"};

    private int lowestWrong;

    [SerializeField]
    private GameObject ghostLever;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;

        goals.Add(Random.Range(0.0f, 1.0f));
        goals.Add(Random.Range(0.0f, 1.0f));
        goals.Add(Random.Range(0.0f, 1.0f));
        
        goalDisplay.color = new Color(goals[0], goals[1], goals[2], 1.0f);
        
        leverChecks.Add(redLever);
        leverChecks.Add(greenLever);
        leverChecks.Add(blueLever);

        ghostLever.SetActive(false);
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
            wrongCount = 0;
            ghostLever.SetActive(false);
        }
        else
        {
            colorFade = 1;
            overrideColor = true;
            targetColor = Color.red;
            wrongCount += 1;
            
            if (wrongCount > hintThreshold)
            {
                lowestWrong = FindLowestWrong();
                ghostLever.transform.position = new Vector3(2.0f, (goals[lowestWrong] * 2 - 1), leverChecks[lowestWrong].positionZ);
                ghostLever.SetActive(true);
                wrongCount = 0;
            }
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

        for (int i = 0; i < leverChecks.Count; i++)
        {
            lenience = 0.05f * (2 * (goals.Where((x, idx) => x > goals[i] || (x == goals[i] && idx < i)).Count()) + 1);
            if (!(goals[i] - lenience <= leverChecks[i].checkValue && leverChecks[i].checkValue <= goals[i] + lenience))
            {
                return false;
            }
        }
        return true;
    }

    int FindLowestWrong()
    {
        int lowestIndex = -1;
        float lowestValue = float.MaxValue;

        for (int i = 0; i < goals.Count; i++)
        {
            lenience = 0.05f * (2 * (goals.Where((x, idx) => x > goals[i] || (x == goals[i] && idx < i)).Count()) + 1);
            if (!(goals[i] - lenience <= leverChecks[i].checkValue && leverChecks[i].checkValue <= goals[i] + lenience))
            {
                if (goals[i] < lowestValue)
                {
                    lowestValue = goals[i];
                    lowestIndex = i;
                }
            }
        }
        return lowestIndex;
    }
}