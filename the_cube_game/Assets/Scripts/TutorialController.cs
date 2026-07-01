using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    void Start()
    {
        dialogue.text = string.Empty;
        StartDialogue();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Mouse.current.rightButton.isPressed && Mouse.current.delta.ReadValue().magnitude != 0.0f && dialogue.text == lines[index])
        {
            NextTutorial(new Vector2(100, 100));
        }
    }
    
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            dialogue.text += c;
            yield return new WaitForSeconds(scrollSpeed);
        }
    }

    void NextTutorial(Vector2 endPos)
    {   
        MoveBox(endPos);
        gameObject.SetActive(false);
        index++;
        dialogue.text = string.Empty;
    }

    void MoveBox(Vector2 endPos)
    {
        while (rectTransform.anchoredPosition != endPos)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, endPos, ref velocity, smoothTime);
        }
    }
}
