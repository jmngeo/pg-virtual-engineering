using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DevelopingMethodsManager : MonoBehaviourPun
{

    [Header("Methods")]
    public GameObject Methods;
    // public TextMeshProUGUI QualityText;

    //declare button variables
    public Button NextMethodButton;
    public Button PreviousMethodButton;

    public GameObject PromptScreen;

    int Method1Quality = 0;
    int Method2Quality = 0;
    int Method3Quality = 0;
    int Method4Quality = 0;
    int Method5Quality = 0;
    int Method6Quality = 0;
    int Method7Quality = 0;
    int Method8Quality = 0;
    int Method9Quality = 0;


    int[] methodsQuality = new int[9];

    // Start is called before the first frame update
    void Start()
    {
        //setting background color
        GlobalVariables.setBackgroundColorForCanvas();
        // GlobalVariables.setBackgroundColorForCanvasChild("NoBackground");
        // GlobalVariables.setBackgroundColor(new List<GameObject>() { Methods }, "NoBackground");

        string obj = GlobalVariables.MethodNames[0];

        //marking the end of current round of investments
        InvestmentManager.isInvestmentRoundStarted = false;

        Method1Quality = InvestmentManager.Method1Quality;
        Method2Quality = InvestmentManager.Method2Quality;
        Method3Quality = InvestmentManager.Method3Quality;
        Method4Quality = InvestmentManager.Method4Quality;
        Method5Quality = InvestmentManager.Method5Quality;
        Method6Quality = InvestmentManager.Method6Quality;
        Method7Quality = InvestmentManager.Method7Quality;
        Method8Quality = InvestmentManager.Method8Quality;
        Method9Quality = InvestmentManager.Method9Quality;


        Debug.Log("Method1Quality: " + Method1Quality + "\tMethod2Quality: " + Method2Quality + "\tMethod3Quality: " + Method3Quality);
        if (GlobalVariables.PhaseCounter > 1)
        {
            Debug.Log("Method4Quality: " + Method4Quality + "\tMethod5Quality: " + Method5Quality);
            if (GlobalVariables.PhaseCounter > 2)
            {
                Debug.Log("Method6Quality: " + Method6Quality + "\tMethod7Quality: " + Method7Quality);
                if (GlobalVariables.PhaseCounter > 3)
                    Debug.Log("Method8Quality: " + Method8Quality + "\tMethod9Quality: " + Method9Quality);
            }
        }

        Debug.Log("ShowMethods bool value for methods 1 to 9: " + "\t" + InvestmentManager.showMethodsList[0] + "\t" + InvestmentManager.showMethodsList[1] + "\t" + InvestmentManager.showMethodsList[2] + "\t" + InvestmentManager.showMethodsList[3] + "\t" + InvestmentManager.showMethodsList[4] + "\t" + InvestmentManager.showMethodsList[5] + "\t" + InvestmentManager.showMethodsList[6] + "\t" + InvestmentManager.showMethodsList[7] + "\t" + InvestmentManager.showMethodsList[8]);

        for (int i = 0; i < 9; i++)
        {
            if (InvestmentManager.showMethodsList[i] == false)
            {

                string MethodName = GlobalVariables.MethodNames[i];
                if ((Methods.transform.Find(MethodName)) != null)
                {
                    Debug.Log("Method to destroy: " + MethodName);
                    Destroy(Methods.transform.Find(MethodName).gameObject);
                }
                // for (int j = 0; j < Methods.transform.childCount; j++)
                // {
                //     GameObject method = Methods.transform.GetChild(j).gameObject;
                //     if(method.name == MethodName)
                //         method.SetActive(false);
                // }
            }
        }


        //set the quality text
        methodsQuality[0] = Method1Quality;
        methodsQuality[1] = Method2Quality;
        methodsQuality[2] = Method3Quality;
        methodsQuality[3] = Method4Quality;
        methodsQuality[4] = Method5Quality;
        methodsQuality[5] = Method6Quality;
        methodsQuality[6] = Method7Quality;
        methodsQuality[7] = Method8Quality;
        methodsQuality[8] = Method9Quality;


        PreviousMethodButton.interactable = false;
        Methods.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //swap the current Screen
    public void SetScreen(GameObject screen) //TODO:: iterate with GameObject<>
    {
        //setting all 3 methods to be inactive
        for (int i = 0; i < Methods.transform.childCount; i++)
        {
            GameObject method = Methods.transform.GetChild(i).gameObject;
            method.SetActive(false);
        }

        // inside each method, for each gameobject with image component, SetActive(false)
        foreach (Transform child in screen.transform)
        {
            if (child.GetComponent<Image>() != null)
            {
                // if (screen.name == "EnvironmentModel" && child.name == "Legend")
                // {
                //     continue;
                // }
                child.gameObject.SetActive(false);
            }
        }

        int quality = 0;

        //activate the requested Screen
        screen.SetActive(true);

        //setting the requested method to be active according to it's name (StakeholderAnalysis, EnvironmentModel, ApplicationScenarios) and it's quality
        if (screen.name == GlobalVariables.MethodNames[0])
            quality = Method1Quality;
        else if (screen.name == GlobalVariables.MethodNames[1])
            quality = Method2Quality;
        else if (screen.name == GlobalVariables.MethodNames[2])
            quality = Method3Quality;
        else if (screen.name == GlobalVariables.MethodNames[3])
            quality = Method4Quality;
        else if (screen.name == GlobalVariables.MethodNames[4])
            quality = Method5Quality;
        else if (screen.name == GlobalVariables.MethodNames[5])
            quality = Method6Quality;
        else if (screen.name == GlobalVariables.MethodNames[6])
            quality = Method7Quality;
        else if (screen.name == GlobalVariables.MethodNames[7])
            quality = Method8Quality;
        else if (screen.name == GlobalVariables.MethodNames[8])
            quality = Method9Quality;

        //use switch (Method1Quality) and find child gameObject by it's name (Level Insufficient, Level Low, Level Medium, Level High) and set it to be active
        switch (quality)
        {
            case 0:
                screen.transform.Find("Level Insufficient").gameObject.SetActive(true);
                break;
            case 1:
                screen.transform.Find("Level Low").gameObject.SetActive(true);
                break;
            case 2:
                screen.transform.Find("Level Medium").gameObject.SetActive(true);
                break;
            case 3:
                screen.transform.Find("Level High").gameObject.SetActive(true);
                break;
        }

    }

    public void onNextButtonClick()
    {
        if (!Methods.activeSelf)
        {
            Methods.SetActive(true);
            SetScreen(Methods.transform.GetChild(0).gameObject); //set the first method to be active

            //set the quality text to be the quality of the first method
            // QualityText.text = Method1Quality.ToString();
            return;
        }

        //to check if we are on the last method and then to move to Phase End Event Screen
        for (int i = 0; i < Methods.transform.childCount; i++)
        {
            GameObject method = Methods.transform.GetChild(i).gameObject;

            if (method.activeSelf)
            {
                if (i == Methods.transform.childCount - 1)
                {
                    // //Using Invoke() to create a delay to allow the server to update the data to the screen
                    // Invoke("UpdateAndRefreshTotalInvestmentScreen", 1f);

                    //show the prompt to move to Phase End Prompt Screen
                    PromptScreen.SetActive(true);

                    return;
                }
            }
        }

        //find the current method
        for (int i = 0; i < Methods.transform.childCount; i++)
        {
            GameObject method = Methods.transform.GetChild(i).gameObject;

            if (method.activeSelf)
            {
                //set the next method
                GameObject nextMethod = Methods.transform.GetChild(i + 1).gameObject;
                SetScreen(nextMethod);

                //set the previous method button to be interactable
                PreviousMethodButton.interactable = true;

                //if the next method is the last method, set the next method button to be not interactable
                if (i == Methods.transform.childCount - 2)
                {
                    if (GlobalVariables.isInPhaseEndEvent == true)
                    {
                        NextMethodButton.interactable = false;
                    }
                    else
                    {
                        NextMethodButton.interactable = true;
                    }
                }

                //set the quality text to be the quality of the next method
                // QualityText.text = methodsQuality[i + 1].ToString();
                break;
            }
        }



    }

    //write a function to go to the previous method
    public void onPreviousButtonClick()
    {

        //find the current method
        for (int i = 0; i < Methods.transform.childCount; i++)
        {
            GameObject method = Methods.transform.GetChild(i).gameObject;

            if (method.activeSelf)
            {
                //set the previous method
                GameObject previousMethod = Methods.transform.GetChild(i - 1).gameObject;
                SetScreen(previousMethod);

                //set the next method button to be interactable
                NextMethodButton.interactable = true;

                //if the previous method is the first method, set the previous method button to be not interactable
                if (i - 1 == 0)
                {
                    PreviousMethodButton.interactable = false;
                }
                // QualityText.text = methodsQuality[i - 1].ToString();
                break;
            }
        }
    }

    public void GoToPhaseEndPromptScreen()
    {
        //Move to PhaseEndEvent scene
        // SceneManager.LoadScene("phase" + GlobalVariables.PhaseCounter + "-" + "PhaseEndPrompt");
        SceneManager.LoadScene("PhaseEndPrompt");
    }
}



