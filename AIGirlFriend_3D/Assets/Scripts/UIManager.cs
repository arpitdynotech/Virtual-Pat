using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public List<GameObject> Panels = new List<GameObject>();
    public TMP_InputField name_field;
    public GameObject[] GenderList;
    public Button maleButton;
    public Button femaleButton;
    public Button otherButton;
    public Sprite maleSelectedSprite;
    public Sprite maleDeselectedSprite;
    public Sprite femaleSelectedSprite;
    public Sprite femaleDeselectedSprite;
    public Sprite otherSelectedSprite;
    public Sprite otherDeselectedSprite;
    private string selectedGender = "none";
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        OnChangePanel(1);
        // Set initial sprites
        UpdateButtonSprites();
    }

    public void OnChangePanel(int value)
    {
        foreach (GameObject go in Panels)
        {
           /* go.GetComponent<CanvasGroup>().alpha = 0f;
            go.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);*/
            go.SetActive(false);
        }
       /* Panels[value].GetComponent<CanvasGroup>().alpha = 0f;
        Panels[value].GetComponent<CanvasGroup>().DOFade(1f, 0.5f);*/
        Panels[value].gameObject.SetActive(true);
    }


    public void OnNameSave()
    {
        if (name_field.text != null && name_field.text != "")
        {
            string username = name_field.text;
            PlayerPrefs.SetString("username", username);
        }
        OnChangePanel(5);
        foreach(GameObject go in GenderList)
        {
            go.GetComponent<Image>().color=new Color(255,255,255,135);
        }
        GenderList[0].GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

    public void OnGenderSelection(int value)
    {
        foreach (GameObject go in GenderList)
        {
            go.GetComponent<Image>().color = new Color(255, 255, 255, 135);
        }
        GenderList[value].GetComponent<Image>().color = new Color(255, 255, 255, 255);
       
        if (value == 0)
        {
            selectedGender = "male";
        }
        if (value == 1)
        {
            selectedGender = "female";
        }
        if (value == 2)
        {
            selectedGender = "other";
        }
        PlayerPrefs.SetString("gender", selectedGender);
        // Update button sprites
        UpdateButtonSprites();
    }

    void UpdateButtonSprites()
    {
        // Reset all buttons' sprites
        maleButton.image.sprite = maleDeselectedSprite;
        femaleButton.image.sprite = femaleDeselectedSprite;
        otherButton.image.sprite = otherDeselectedSprite;

        // Set selected button's sprite
        switch (selectedGender)
        {
            case "male":
                maleButton.image.sprite = maleSelectedSprite;
                break;
            case "female":
                femaleButton.image.sprite = femaleSelectedSprite;
                break;
            case "other":
                otherButton.image.sprite = otherSelectedSprite;
                break;
            default:
                break;
        }
    }

    public void DateSelected()
    {
        SnapScrollHandler.instance.CurrentDaySelected();
        SnapScrollHandlerMonths.instance.CurrentDaySelected();
        SnapScrollHandleryears.instance.CurrentDaySelected();
        string day=SnapScrollHandler.instance.selectedday;
        string month = SnapScrollHandlerMonths.instance.selectedday;
        string year = SnapScrollHandleryears.instance.selectedday;
        PlayerPrefs.SetString("birthdate " , (day + "/" + month + "/" + year));
        string date = day + "/" + month + "/" + year;
        print("BirthDate -: "+date); 
        OnChangePanel(7);
    }

}