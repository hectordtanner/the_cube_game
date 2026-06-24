using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    private Material _material;

    private bool _overrideColor = false;
    private float _colorFade = 0.0f;
    private Color _targetColor = Color.white;
    private Color _buttonColor = Color.white;

    private List<float> _goals = new List<float>();
    private List<CheckController> _checks = new List<CheckController>();

    public int score = 0;

    [SerializeField]
    private float _lenience = 0.1f;

    [SerializeField]
    private Image goalDisplay;

    [SerializeField]
    private CheckController _redLever;

    [SerializeField]
    private CheckController _blueLever;

    [SerializeField]
    private CheckController _greenLever;

    [SerializeField]
    private float _fadeSpeed = 0.1f;

    [SerializeField]
    private float _newGoalDifference = 0.1f;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;

        _goals.Add(Random.Range(0.0f, 1.0f));
        _goals.Add(Random.Range(0.0f, 1.0f));
        _goals.Add(Random.Range(0.0f, 1.0f));
        
        goalDisplay.color = new Color(_goals[0], _goals[1], _goals[2], 1.0f);

        _checks.Add(_redLever);
        _checks.Add(_greenLever);
        _checks.Add(_blueLever);
    }

    void OnMouseDown()
    {
        if (FullCheck())
        {
            score += 1;
            _scoreText.text = "Score: " + score;
            
            for (int i = 0; i < _goals.Count; i++)
            {
                if (Random.value < _goals[i])
                {
                    _goals[i] = Random.Range(0.0f, _goals[i] - _newGoalDifference);
                }
                else
                {
                    _goals[i] = Random.Range(_goals[i] + _newGoalDifference, 1.0f);
                }
                _goals[i] = Mathf.Clamp(_goals[i], 0.0f, 1.0f);
            }

            goalDisplay.color = new Color(_goals[0], _goals[1], _goals[2], 1.0f);

            _colorFade = 1;
            _overrideColor = true;
            _targetColor = Color.green;
        }
        else
        {
            _colorFade = 1;
            _overrideColor = true;
            _targetColor = Color.red;
        }
    }

    void Update()
    {
        _buttonColor = new Color(_redLever._checkValue, _greenLever._checkValue, _blueLever._checkValue, 1.0f).linear;

        _colorFade -= _fadeSpeed;
        _colorFade = Mathf.Clamp(_colorFade, 0.0f, 1.0f);

        if (_colorFade == 0)
        {
            _overrideColor = false;
        }
        _material.color = Color.Lerp(_buttonColor, _targetColor, _colorFade);
        
        if (!_overrideColor)
        {
            _material.SetColor("_BaseColor", _buttonColor);
        }
    }

    bool FullCheck()
    {
        for (int i = 0; i < _checks.Count; i++)
        {
            if (!(_checks[i]._checkValue - _lenience < _goals[i] && _goals[i] < _checks[i]._checkValue + _lenience))
            {
                return false;
            }
        }
        return true;
    }
}