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

    private bool isChangingTutorial = false;

    private bool isTyping = false;

    [SerializeField]
    private CheckController blueLever;

    [SerializeField]
    private CheckController redLever;

    [SerializeField]
    private CheckController greenLever;

    [SerializeField]
    private ButtonController button;

    void Start()
    {
        index = 0;
        dialogue.text = string.Empty;

        blueLever.gameObject.SetActive(false);
        redLever.gameObject.SetActive(false);
        greenLever.gameObject.SetActive(false);
        button.gameObject.SetActive(false);

        StartCoroutine(TypeLine());
        isTyping = false;

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
                    if (!isChangingTutorial)
                    {
                        isChangingTutorial = true;
                        StartCoroutine(MoveBox(endPos, CloseTutorial));
                    }
                }
                break;

            case 1:
                if (!isChangingTutorial)
                {
                    isChangingTutorial = true;
                    OpenNextTutorial();
                }
                break;

            case 2:

                blueLever.gameObject.SetActive(true);
                redLever.gameObject.SetActive(true);
                greenLever.gameObject.SetActive(true);

                if (!isTyping)
                {
                    StartCoroutine(TypeLine());
                }

                if ((blueLever.checkValue != 1 | redLever.checkValue != 1 | greenLever.checkValue != 1) && dialogue.text == lines[index])
                {
                    if (!isChangingTutorial)
                    {
                        isChangingTutorial = true;
                        StartCoroutine(MoveBox(endPos, CloseTutorial));
                    }
                }
                break;

            case 3:
                if (!isChangingTutorial)
                {
                    isChangingTutorial = true;
                    OpenNextTutorial();
                }
                isTyping = false;
                break;

            case 4:

                button.gameObject.SetActive(true);

                if (!isTyping)
                {
                    StartCoroutine(TypeLine());
                }

                if ((button.score > 0) && dialogue.text == lines[index])
                {
                    if (!isChangingTutorial)
                    {
                        isChangingTutorial = true;
                        StartCoroutine(MoveBox(endPos, CloseTutorial));
                    }
                }
                break;
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index])
        {
            dialogue.text += c;
            yield return new WaitForSeconds(scrollSpeed);
        }
    }

    void CloseTutorial()
    {   
        dialogue.text = string.Empty;
        tutorialStage ++;
        isChangingTutorial = false;
    }

    void OpenNextTutorial()
    {
        tutorialStage ++;
        index++;
        StartCoroutine(MoveBox(startPos, () => {isChangingTutorial = false;}));
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
