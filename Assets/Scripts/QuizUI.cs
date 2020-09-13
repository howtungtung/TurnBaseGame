using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class QuizUI : MonoBehaviour
{
    [Serializable]
    public class PlayerAnswerEvent : UnityEvent<bool> { }
    public Text quizText;
    public Text[] optionTexts;
    public Button[] optionButtons;
    private Quiz quiz;
    public Color rightColor;
    public Color wrongColor;
    public Color normalColor;
    public PlayerAnswerEvent onPlayerAnswer;

    public void Show(Quiz quiz)
    {
        this.quiz = quiz;
        quizText.text = quiz.quiz;
        optionTexts[0].text = quiz.optionA;
        optionTexts[1].text = quiz.optionB;
        optionTexts[2].text = quiz.optionC;
        optionTexts[3].text = quiz.optionD;
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].targetGraphic.color = normalColor;
            optionButtons[i].interactable = true;
        }
        gameObject.SetActive(true);
    }

    public void OnAnswerSelect(int index)
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].interactable = false;
        }
        if (quiz.answer != index)
        {
            optionButtons[index].targetGraphic.color = wrongColor;
            onPlayerAnswer.Invoke(false);
        }
        else
        {
            optionButtons[index].targetGraphic.color = rightColor;
            onPlayerAnswer.Invoke(true);
        }
    }
}
