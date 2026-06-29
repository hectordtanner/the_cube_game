using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private int startX = -100;
    [SerializeField]
    private int startY = -100;
    private Vector2 startPos;

    [SerializeField]
    private int endX = -180;
    [SerializeField]
    private int endY = -180;
    private Vector2 endPos;

    private float cycle = 1.0f;
    [SerializeField]
    private float speed = 0.01f;

    private RectTransform rectTransform;

    void Start()
    {
        startPos = new Vector2(startX, startY);
        endPos = new Vector2(endX, endY);

        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2((cycle * startX) + ((1.0f - cycle)* endX), (cycle * startY) + ((1.0f - cycle)* endY));
            cycle -= speed;
            if ((cycle < 0.0f)||(cycle > 1.0f))
            {
                speed *= -1;
                cycle = Mathf.Clamp(cycle, 0.0f, 1.0f);
            }
        }
        else
        {
            Debug.Log("rect is null");
        }
    }
}
