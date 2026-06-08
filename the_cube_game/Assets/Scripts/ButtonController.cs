using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    private Material _material;

    private float _goalRed;
    public bool _overrideColor = false;
    private float _colorFade = 0.0f;
    private Color _targetColor = Color.white;

    public int score = 0;

    [SerializeField]
    private float _lenience = 0.1f;

    [SerializeField]
    private Image goalDisplay;

    [SerializeField]
    private LeverOneController _colorData;

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
        goalDisplay.color = new Color(_goalRed, 1.0f, 1.0f, 1.0f);
    }

    void OnMouseDown()
    {
        if (((_goalRed - _lenience) < _material.color.r) && (_material.color.r < (_goalRed + _lenience)))
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

            goalDisplay.color = new Color(_goalRed, 1.0f, 1.0f, 1.0f);
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
        _colorFade -= _fadeSpeed;
        _colorFade = Mathf.Clamp(_colorFade, 0.0f, 1.0f);
        if (_colorFade == 0)
        {
            _overrideColor = false;
        }
        _material.color = Color.Lerp(new Color(_colorData.redColorValue, 1.0f, 1.0f, 1.0f), _targetColor, _colorFade);
    }
}