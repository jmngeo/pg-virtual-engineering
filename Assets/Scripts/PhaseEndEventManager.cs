using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class PhaseEndEventManager : MonoBehaviourPun
{

    public static PhaseEndEventManager instance;

    [Header("Screens")]
    public GameObject IntroScreen;
    public GameObject QuestionScreen;
    public GameObject PhaseEndEventScoreScreen;
    public GameObject PhaseEndEventScoreIntro;
    public GameObject PhaseEndEventScoreBox;
    public GameObject EndOfPhaseEndEventScreen;

    [Header("Questionnaire")]
    public GameObject QuestionBox;
    public GameObject Option1;
    public GameObject Option2;
    public GameObject Option3;
    public GameObject Option4;

    //declare button variables
    public Button NextMethodButton;
    public Button PreviousMethodButton;
    public Button SubmitButton;

    public TextMeshProUGUI QuestionNumberText;

    [Header("PhaseEndEventScore")]
    public TextMeshProUGUI PhaseEndEventScoreText;

    public TextMeshProUGUI PhaseEndEventScoreMethod1Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod2Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod3Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod4Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod5Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod6Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod7Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod8Text;
    public TextMeshProUGUI PhaseEndEventScoreMethod9Text;

    public TextMeshProUGUI PhaseEndEventMethod1QualityText;
    public TextMeshProUGUI PhaseEndEventMethod2QualityText;
    public TextMeshProUGUI PhaseEndEventMethod3QualityText;
    public TextMeshProUGUI PhaseEndEventMethod4QualityText;
    public TextMeshProUGUI PhaseEndEventMethod5QualityText;
    public TextMeshProUGUI PhaseEndEventMethod6QualityText;
    public TextMeshProUGUI PhaseEndEventMethod7QualityText;
    public TextMeshProUGUI PhaseEndEventMethod8QualityText;
    public TextMeshProUGUI PhaseEndEventMethod9QualityText;

    private int screenNumber = -1;
    public int score = 0;

    // Variables for getting the quality values from the db based on the investments made eg {Insufficient, Low, Medium, High}
    string Method1QualityText;
    string Method2QualityText;
    string Method3QualityText;
    string Method4QualityText;
    string Method5QualityText;
    string Method6QualityText;
    string Method7QualityText;
    string Method8QualityText;
    string Method9QualityText;

    // Variables for getting the text based on the investments made eg {"You did good...", "YOu did Bad.."} 
    string Method1Text;
    string Method2Text;
    string Method3Text;
    string Method4Text;
    string Method5Text;
    string Method6Text;
    string Method7Text;
    string Method8Text;
    string Method9Text;


    string[] quality_vals = new string[9] { "", "", "", "", "", "", "", "", "" };
    string[] method_vals = new string[9] { "", "", "", "", "", "", "", "", "" };

    private List<int> optionsSelected = Enumerable.Repeat(0, 10).ToList();

    public static Questionnaire q;
    public static PhaseEndReports r;

    string[] qualities = { "Insufficient", "Low", "Medium", "High" };


    // Color normalColor = new Color32(0, 182, 255, 255);
    // Color selectedColor = new Color32(0, 70, 99, 255);
    // Color normalColor = new Color32(31, 211, 168, 255);
    // 
    Color normalColor = new Color32(32, 137, 112, 255);

    // Color selectedColor = new Color32(16, 110, 88, 255);
    Color selectedColor = new Color32(10, 82, 65, 255);

    void Start()
    {
        //to set the background color of the screen
        GlobalVariables.setBackgroundColorForCanvas();
        GlobalVariables.setBackgroundColor(new List<GameObject>() { IntroScreen, QuestionScreen, PhaseEndEventScoreScreen, EndOfPhaseEndEventScreen }, "PhaseEndBackground");


        if (IntroScreen.activeSelf)
            PreviousMethodButton.interactable = false;
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetScreen()
    {
        //setting everything else to be inactive
        IntroScreen.SetActive(false);
        QuestionScreen.SetActive(false);

        if (screenNumber >= 0)
        {
            QuestionScreen.SetActive(true);

            //set QuestionBox's child object's text to q.data[screenNumber].question
            QuestionBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.data[screenNumber].question.ToString();

            //set the options' text to be the options from the data
            Option1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.data[screenNumber].option_1.ToString();
            Option2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.data[screenNumber].option_2.ToString();
            Option3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.data[screenNumber].option_3.ToString();

            // check if it is isBinaryQuestion from Data
            // if it is, then set the options to be active
            if (int.Parse(q.data[screenNumber].isBinary) == 0)
            {
                Option1.SetActive(true);
                Option2.SetActive(true);
                Option3.SetActive(true);
                Option4.SetActive(true);

                //set the options' text to be the options from the data                
                Option4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.data[screenNumber].option_4.ToString();
            }
            else
            {
                Option1.SetActive(true);
                Option2.SetActive(true);
                Option3.SetActive(true);
                Option4.SetActive(false);
            }
        }
        else
            IntroScreen.SetActive(true);

    }

    public void calculateScore()
    {


        for (int i = 0; i < optionsSelected.Count; i++)
        {
            if (optionsSelected[i] == int.Parse(q.data[i].answer))
                score++;
        }

        Debug.Log("Score: " + score);
        PhaseEndEventDB.instance.SendPhaseEndScoreValues();
    }

    public void setPhaseEndEventScreen()
    {
        PhaseEndEventScoreScreen.SetActive(true);
        QuestionScreen.SetActive(false);
        IntroScreen.SetActive(false);

        Debug.Log("Navigation: PhaseEndEventScreen");

        int PhaseEndScore = score;

        photonView.RPC("SetScoreandQualityforAll", RpcTarget.All, PhaseEndScore, quality_vals, method_vals);

        Debug.Log("Method1QualityText: " + Method1QualityText);

        PhaseEndEventScoreText.text = " " + score + " / 10";

        if (GlobalVariables.PhaseCounter == 1)
        {
            PhaseEndEventScoreMethod1Text.text = Method1Text;
            PhaseEndEventScoreMethod2Text.text = Method2Text;
            PhaseEndEventScoreMethod3Text.text = Method3Text;

            PhaseEndEventMethod1QualityText.text = Method1QualityText;
            PhaseEndEventMethod2QualityText.text = Method2QualityText;
            PhaseEndEventMethod3QualityText.text = Method3QualityText;
        }
        else if (GlobalVariables.PhaseCounter == 2)
        {
            PhaseEndEventScoreMethod4Text.text = Method4Text;
            PhaseEndEventScoreMethod5Text.text = Method5Text;

            PhaseEndEventMethod4QualityText.text = Method4QualityText;
            PhaseEndEventMethod5QualityText.text = Method5QualityText;
        }
        else if (GlobalVariables.PhaseCounter == 3)
        {
            PhaseEndEventScoreMethod6Text.text = Method6Text;
            PhaseEndEventScoreMethod7Text.text = Method7Text;

            PhaseEndEventMethod6QualityText.text = Method6QualityText;
            PhaseEndEventMethod7QualityText.text = Method7QualityText;
        }
        else if (GlobalVariables.PhaseCounter == 4)
        {
            PhaseEndEventScoreMethod8Text.text = Method8Text;
            PhaseEndEventScoreMethod9Text.text = Method9Text;

            PhaseEndEventMethod8QualityText.text = Method8QualityText;
            PhaseEndEventMethod9QualityText.text = Method9QualityText;
        }
    }

    public void onNextButtonClick()
    {

        if (screenNumber <= q.data.Count - 1)
        {
            screenNumber++;
            setQuestionNumber();
            Debug.Log("screenNumber: " + screenNumber);
            setOptionsToNormalColor();
            SetScreen();
            EnableDisableNavButtons();
            setSelectedOptionsColor();
        }

    }

    //write a function to go to the previous method
    public void onPreviousButtonClick()
    {

        if (screenNumber >= 0)
        {
            screenNumber--;
            setQuestionNumber();
            Debug.Log("screenNumber: " + screenNumber);
            setOptionsToNormalColor();
            SetScreen();
            EnableDisableNavButtons();
            setSelectedOptionsColor();
        }
    }

    public void EnableDisableNavButtons()
    {

        if (screenNumber <= 0)
        {
            //disable back button
            PreviousMethodButton.interactable = false;
        }
        else
        {
            PreviousMethodButton.interactable = true;
        }

        if (screenNumber == q.data.Count - 1)
        {
            //disable NextMethodButton
            NextMethodButton.interactable = false;
            // make the submit button visible
            SubmitButton.gameObject.SetActive(true);
        }
        else
        {
            NextMethodButton.interactable = true;
            // make the submit button invisible
            SubmitButton.gameObject.SetActive(false);
        }
    }

    public void setOptionsToNormalColor()
    {
        Option1.GetComponent<Image>().color = normalColor;
        Option2.GetComponent<Image>().color = normalColor;
        Option3.GetComponent<Image>().color = normalColor;
        Option4.GetComponent<Image>().color = normalColor;
    }

    public void onOptionClick()
    {
        setOptionsToNormalColor();

        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        GameObject.Find(buttonName).GetComponent<Image>().color = selectedColor;


        //save the option selected to the data
        switch (buttonName)
        {
            case "Option1":
                optionsSelected[screenNumber] = 1;
                break;
            case "Option2":
                optionsSelected[screenNumber] = 2;
                break;
            case "Option3":
                optionsSelected[screenNumber] = 3;
                break;
            case "Option4":
                optionsSelected[screenNumber] = 4;
                break;
        }

    }

    public void setSelectedOptionsColor()
    {
        if (optionsSelected[screenNumber] == 0)
        {
            return;
        }
        else
        {
            switch (optionsSelected[screenNumber])
            {
                case 1:
                    Option1.GetComponent<Image>().color = selectedColor;
                    break;
                case 2:
                    Option2.GetComponent<Image>().color = selectedColor;
                    break;
                case 3:
                    Option3.GetComponent<Image>().color = selectedColor;
                    break;
                case 4:
                    Option4.GetComponent<Image>().color = selectedColor;
                    break;
            }
        }
    }

    public void setQuestionNumber()
    {
        QuestionNumberText.text = "Q. " + (screenNumber + 1) + " / " + q.data.Count;
    }



    public void PhaseEndEventScreenSetup()
    {
        // photonView.RPC("SetFinalScoreScreenforAll", RpcTarget.Others);
        NetworkManager.instance.photonView.RPC("SetFinalScoreScreenforAll", RpcTarget.All);
        setDataValuesForMaster();
        photonView.RPC("SetScoreandQualityforAll", RpcTarget.All, score, quality_vals, method_vals);
        // call setPhaseEndEventScreen for all
    }

    // This would be called first
    void setDataValuesForMaster()
    {
        if (GlobalVariables.PhaseCounter == 1)
        {
            Method1QualityText = qualities[int.Parse(r.data[0].quality)];
            Method2QualityText = qualities[int.Parse(r.data[1].quality)];
            Method3QualityText = qualities[int.Parse(r.data[2].quality)];

            quality_vals[0] = Method1QualityText;
            quality_vals[1] = Method2QualityText;
            quality_vals[2] = Method3QualityText;

            Method1Text = r.data[0].method_report.ToString();
            Method2Text = r.data[1].method_report.ToString();
            Method3Text = r.data[2].method_report.ToString();

            method_vals[0] = Method1Text;
            method_vals[1] = Method2Text;
            method_vals[2] = Method3Text;
        }
        else if (GlobalVariables.PhaseCounter == 2) //change r.data[] values accordingly
        {
            Method4QualityText = qualities[int.Parse(r.data[0].quality)];
            Method5QualityText = qualities[int.Parse(r.data[1].quality)];

            quality_vals[3] = Method4QualityText;
            quality_vals[4] = Method5QualityText;

            Debug.Log("Method4QualityText: " + quality_vals[3]);
            Debug.Log("Method5QualityText: " + quality_vals[4]);

            Method4Text = r.data[0].method_report.ToString();
            Method5Text = r.data[1].method_report.ToString();

            method_vals[3] = Method4Text;
            method_vals[4] = Method5Text;
        }
        else if (GlobalVariables.PhaseCounter == 3) //change r.data[] values accordingly
        {
            Method6QualityText = qualities[int.Parse(r.data[0].quality)];
            Method7QualityText = qualities[int.Parse(r.data[1].quality)];

            quality_vals[5] = Method6QualityText;
            quality_vals[6] = Method7QualityText;

            Method6Text = r.data[0].method_report.ToString();
            Method7Text = r.data[1].method_report.ToString();

            method_vals[5] = Method6Text;
            method_vals[6] = Method7Text;
        }
        else if (GlobalVariables.PhaseCounter == 4) //change r.data[] values accordingly
        {
            Method8QualityText = qualities[int.Parse(r.data[0].quality)];
            Method9QualityText = qualities[int.Parse(r.data[1].quality)];

            quality_vals[7] = Method8QualityText;
            quality_vals[8] = Method9QualityText;

            Method8Text = r.data[0].method_report.ToString();
            Method9Text = r.data[1].method_report.ToString();

            method_vals[7] = Method8Text;
            method_vals[8] = Method9Text;
        }
    }

    // this would be called second
    [PunRPC]
    void SetScoreandQualityforAll(int PhaseEndScore, string[] qualities, string[] method_vals)
    {

        score = PhaseEndScore;

        if (GlobalVariables.PhaseCounter == 1)
        {
            Method1QualityText = qualities[0];
            Method2QualityText = qualities[1];
            Method3QualityText = qualities[2];

            Method1Text = method_vals[0];
            Method2Text = method_vals[1];
            Method3Text = method_vals[2];


            PhaseEndEventScoreMethod1Text.text = Method1Text;
            PhaseEndEventScoreMethod2Text.text = Method2Text;
            PhaseEndEventScoreMethod3Text.text = Method3Text;

            PhaseEndEventMethod1QualityText.text = Method1QualityText;
            PhaseEndEventMethod2QualityText.text = Method2QualityText;
            PhaseEndEventMethod3QualityText.text = Method3QualityText;
        }
        else if (GlobalVariables.PhaseCounter == 2)
        {
            Method4QualityText = qualities[3];
            Method5QualityText = qualities[4];

            Method4Text = method_vals[3];
            Method5Text = method_vals[4];

            PhaseEndEventScoreMethod4Text.text = Method4Text;
            PhaseEndEventScoreMethod5Text.text = Method5Text;

            PhaseEndEventMethod4QualityText.text = Method4QualityText;
            PhaseEndEventMethod5QualityText.text = Method5QualityText;
        }
        else if (GlobalVariables.PhaseCounter == 3)
        {
            Method6QualityText = qualities[5];
            Method7QualityText = qualities[6];

            Method6Text = method_vals[5];
            Method7Text = method_vals[6];

            PhaseEndEventScoreMethod6Text.text = Method6Text;
            PhaseEndEventScoreMethod7Text.text = Method7Text;

            PhaseEndEventMethod6QualityText.text = Method6QualityText;
            PhaseEndEventMethod7QualityText.text = Method7QualityText;
        }
        else if (GlobalVariables.PhaseCounter == 4)
        {
            Method8QualityText = qualities[7];
            Method9QualityText = qualities[8];

            Method8Text = method_vals[7];
            Method9Text = method_vals[8];

            PhaseEndEventScoreMethod8Text.text = Method8Text;
            PhaseEndEventScoreMethod9Text.text = Method9Text;

            PhaseEndEventMethod8QualityText.text = Method8QualityText;
            PhaseEndEventMethod9QualityText.text = Method9QualityText;
        }

        PhaseEndEventScoreText.text = " " + score + " / 10";

        PhaseEndEventScoreScreen.SetActive(true);
        QuestionScreen.SetActive(false);
        IntroScreen.SetActive(false);
    }


    public void onPhaseEndScoreNextButtonClick()
    {
        IntroScreen.SetActive(false);
        QuestionScreen.SetActive(false);

        if (PhaseEndEventScoreIntro.activeSelf)
        {
            PhaseEndEventScoreIntro.SetActive(false);
            PhaseEndEventScoreBox.SetActive(true);
        }
        else
        {
            PhaseEndEventScoreScreen.SetActive(false);
            EndOfPhaseEndEventScreen.SetActive(true);
        }
    }

    public void onPhaseEndFinalScreenNextButtonClick()
    {
        GlobalVariables.PhaseCounter++;
        GlobalVariables.isInPhaseEndEvent = false;

        if (GlobalVariables.PhaseCounter < 5 && GlobalVariables.roundCounter < 8)
            SceneManager.LoadScene("phase" + GlobalVariables.PhaseCounter + "-" + "Introduction");

        if (GlobalVariables.PhaseCounter == 5 || GlobalVariables.roundCounter == 8)
            SceneManager.LoadScene("GameEndReport");
    }
}
