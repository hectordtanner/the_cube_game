using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogue;

    [SerializeField]
    private string[] lines;

    [SerializeField]
    private float scrollSpeed;

    private int index;

    [SerializeField]
    private float smoothTime;

    private Vector2 velocity = Vector2.zero;

    private RectTransform rectTransform;

    [SerializeField]
    private Vector2 endPos;

    [SerializeField]
    private Vector2 startPos;

    private int tutorialStage = 0;

    void Start()
    {
        index = 0;
        dialogue.text = string.Empty;
        StartCoroutine(TypeLine());
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPos;
    }

    void Update()
    {
        switch (tutorialStage)
        {
            case 0:
                if (Mouse.current.rightButton.isPressed && Mouse.current.delta.ReadValue().magnitude != 0.0f && dialogue.text == lines[index])
                {
                    StartCoroutine(MoveBox(endPos, CloseTutorial));
                }
                break;

            case 1:
                break;
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            dialogue.text += c;
            yield return new WaitForSeconds(scrollSpeed);
        }
    }

    void CloseTutorial()
    {   
        gameObject.SetActive(false);
        dialogue.text = string.Empty;
    }

    void OpenNextTutorial()
    {
        gameObject.SetActive(true);
        index++;
        MoveBox(startPos, null);
    }

    IEnumerator MoveBox(Vector2 target, Action onComplete)
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, target) >= 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, target, ref velocity, smoothTime);
            yield return null;
        }

        onComplete?.Invoke();
    }
}
