using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Questionnaire : MonoBehaviour
{

    public TextMeshProUGUI questionText;

    public Button nextButton;
    // Arrays to hold the GameObjects for each question's options
    public GameObject[] allquestion;

    public GameObject[] Allptions;
    public GameObject[] Q1Options;
    public GameObject[] Q2Options;
    public GameObject[] Q3Options;
    public GameObject[] Q4Options;
    public GameObject[] Q5Options;

    public List<string> userSelectedOptions = new List<string>();

    public Slider progressSlider;
    public int Qindex, OptionIndex;
    private int currentQuestionIndex = 0;
    public Button ContinueClick;
    public bool isOptionSelected;
    void Start()
    {
        Qindex = 0;
        OptionIndex = 0;
        changeQAndO(Qindex);
        ContinueClick.onClick.AddListener(() =>
        {
            changeQAndO(Qindex);
            OnNextQuestion(Qindex);
            isOptionSelected = false;
        });
        isOptionSelected = false;
    }

    public void OnOptionSelection(int index)
    {

        
        switch (Qindex)
        {
            case 0:
                print("0you have selected - " + Q1Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                userSelectedOptions.Add(Q1Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                isOptionSelected = true;
                break;
            case 1:
                print("1you have selected - " + Q2Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                userSelectedOptions.Add(Q2Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                isOptionSelected = true;
                break;
            case 2:
                print("2you have selected - " + Q3Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                userSelectedOptions.Add(Q3Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                isOptionSelected = true;
                break;
            case 3:
                print("3you have selected - " + Q4Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                userSelectedOptions.Add(Q4Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                isOptionSelected = true;
                break;
            case 4:
                print("4you have selected - " + Q5Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                userSelectedOptions.Add(Q5Options[index].transform.GetComponentInChildren<TextMeshProUGUI>().text);
                isOptionSelected = true;
                break;
        }
    }


    public void changeQAndO(int value)
    {
        if (value < allquestion.Length)
        {
            foreach (GameObject options in Allptions)
            {
                options.SetActive(false);
            }
            Allptions[value].SetActive(true);
            foreach (GameObject questions in allquestion)
            {
                questions.SetActive(false);
            }
            allquestion[value].SetActive(true);
        }
        else if(value==allquestion.Length)
        {
            UIManager.Instance.OnChangePanel(8);
        }
    }
    public void OnNextQuestion(int value)
    {
        if (isOptionSelected)
        {
            Qindex++;
            ContinueClick.onClick.RemoveAllListeners();
            ContinueClick.onClick.AddListener(() =>
            {
                changeQAndO(Qindex);
                OnNextQuestion(Qindex);
                isOptionSelected = false;
            });
        }
    }
}

/* // Questions and their respective options
 private string[] questions = {
     "Question 1: How Familiar are You With Ai Technology?",
     "Question 2: How did You first come into contact with AI?",
     "Question 3: Are You a fan of any of these movies?",
     "Question 4: How do you usually spend your free time?",
     "Question 5: What’s your love language?"
 };*/

/* private string[] options = {
      "I use AI across many applications", "I’ve interacted with AI occasionally", "This is one of my first experiences", "Unfamiliar",
      "i Tried ChatGPT, Bing or Bard", "i Used virtual assistants like Siri or google Assistant  ", "I played with AI companion apps", "i have no experience with AI",
      "Samantha : Caring, loving and evolving", "Joi : smart, flexible and loyal", "ava: curious, unpredictable and passionate", "i haven’t seen any of them",
      "Enjoying being alone", "Engaging in hobbies", "With friends and family", "meeting new people",
      "Physical closeness", "Deep Conversations", "Spending Time Together", "Gifts and tokens of Apperciation" 
 };
*/