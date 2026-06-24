using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    private Material _material;

    private float _goalRed;
    private float _goalBlue;
    private float _goalGreen;

    public bool _overrideColor = false;
    private float _colorFade = 0.0f;
    private Color _targetColor = Color.white;

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
    private float _newColorDifference = 0.1f;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;

        _goalRed = Random.Range(0.0f, 1.0f);
        _goals.Add(_goalRed);
        _goalBlue = Random.Range(0.0f, 1.0f);
        _goals.Add(_goalBlue);
        _goalGreen = Random.Range(0.0f, 1.0f);
        _goals.Add(_goalGreen);
        
        goalDisplay.color = new Color(_goalRed, _goalGreen, _goalBlue, 1.0f);

        _checks.Add(_redLever);
        _checks.Add(_blueLever);
        _checks.Add(_greenLever);
    }

    void OnMouseDown()
    {
        if (FullCheck())
        {
            score += 1;
            _scoreText.text = "Score: " + score;

            if (Random.value < _goalRed)
            {
                _goalRed = Random.Range(0.0f, _goalRed - _newColorDifference);
            }
            else
            {
                _goalRed = Random.Range(_goalRed + _newColorDifference, 1.0f);
            }
            _goalRed = Mathf.Clamp(_goalRed, 0.0f, 1.0f);
            _goals[0] = _goalRed;

            if (Random.value < _goalBlue)
            {
                _goalBlue = Random.Range(0.0f, _goalBlue- _newColorDifference);
            }
            else
            {
                _goalBlue = Random.Range(_goalBlue + _newColorDifference, 1.0f);
            }
            _goalBlue = Mathf.Clamp(_goalBlue, 0.0f, 1.0f);
            _goals[1] = _goalBlue;

            if (Random.value < _goalGreen)
            {
                _goalGreen = Random.Range(0.0f, _goalGreen - _newColorDifference);
            }
            else
            {
                _goalGreen = Random.Range(_goalGreen + _newColorDifference, 1.0f);
            }
            _goalGreen = Mathf.Clamp(_goalGreen, 0.0f, 1.0f);
            _goals[2] = _goalGreen;

            goalDisplay.color = new Color(_goalRed, _goalGreen, _goalBlue, 1.0f);

            _colorFade = 1;
            _overrideColor = true;
            _targetColor = Color.green;
        }
        else
        {
            _colorFade = 1;
            _overrideColor = true;
            _targetColor = Color.red;
            Debug.Log("r: " + (_redLever._checkValue - _goalRed) + ", g:" + (_greenLever._checkValue - _goalGreen) + ", b:" + (_blueLever._checkValue - _goalBlue));
        }
    }

    void Update()
    {
        _colorFade -= _fadeSpeed;
        _colorFade = Mathf.Clamp(_colorFade, 0.0f, 1.0f);

        if (_colorFade == 0)
        {
            _overrideColor = false;
        }
        _material.color = Color.Lerp(new Color(_redLever._checkValue, _greenLever._checkValue, _blueLever._checkValue, 1.0f), _targetColor, _colorFade);
        
        if (!_overrideColor)
        {
            _material.SetColor("_BaseColor", new Color(_redLever._checkValue, _greenLever._checkValue, _blueLever._checkValue, 1.0f));
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