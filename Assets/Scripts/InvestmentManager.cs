using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InvestmentManager : MonoBehaviourPunCallbacks
{
    [Header("Screens")]
    public GameObject InvestmentIntroScreen;
    public GameObject InvestmentScreen;
    public GameObject InvestmentConfirmationScreen;
    public GameObject TotalPlayersInvestmentScreen;
    public TextMeshProUGUI RoundCounterText;

    [Header("Investment Intro Screen - Text Fields")]
    public TextMeshProUGUI InvestmentIntroScreenHeadingText;
    public TextMeshProUGUI InvestmentIntroScreenText;
    public GameObject NextIsToInvestmentsScreenBtn;
    public GameObject BackToPhaseDescScreenBtn;
    public GameObject promptsContainer;
    public GameObject RoleDescriptions;

    [Header("Previous Investment - Resources Text Fields")]
    public TextMeshProUGUI Method1PreviousInvestedResourcesText;
    public TextMeshProUGUI Method2PreviousInvestedResourcesText;
    public TextMeshProUGUI Method3PreviousInvestedResourcesText;
    public TextMeshProUGUI Method4PreviousInvestedResourcesText;
    public TextMeshProUGUI Method5PreviousInvestedResourcesText;
    public TextMeshProUGUI Method6PreviousInvestedResourcesText;
    public TextMeshProUGUI Method7PreviousInvestedResourcesText;
    public TextMeshProUGUI Method8PreviousInvestedResourcesText;
    public TextMeshProUGUI Method9PreviousInvestedResourcesText;

    [Header("Current Investment - Resources Text Fields")]
    public TextMeshProUGUI AvailableResourcesText;
    public TextMeshProUGUI Method1CurrentInvestedResourcesText;
    public TextMeshProUGUI Method2CurrentInvestedResourcesText;
    public TextMeshProUGUI Method3CurrentInvestedResourcesText;
    public TextMeshProUGUI Method4CurrentInvestedResourcesText;
    public TextMeshProUGUI Method5CurrentInvestedResourcesText;
    public TextMeshProUGUI Method6CurrentInvestedResourcesText;
    public TextMeshProUGUI Method7CurrentInvestedResourcesText;
    public TextMeshProUGUI Method8CurrentInvestedResourcesText;
    public TextMeshProUGUI Method9CurrentInvestedResourcesText;

    [Header("Total Investment - Resources Text Fields")]
    public TextMeshProUGUI Method1TotalInvestedResourcesText;
    public TextMeshProUGUI Method2TotalInvestedResourcesText;
    public TextMeshProUGUI Method3TotalInvestedResourcesText;
    public TextMeshProUGUI Method4TotalInvestedResourcesText;
    public TextMeshProUGUI Method5TotalInvestedResourcesText;
    public TextMeshProUGUI Method6TotalInvestedResourcesText;
    public TextMeshProUGUI Method7TotalInvestedResourcesText;
    public TextMeshProUGUI Method8TotalInvestedResourcesText;
    public TextMeshProUGUI Method9TotalInvestedResourcesText;

    public TextMeshProUGUI TotalInvestedResourcesText;
    public GameObject ContinuetoTotalPlayerResourcesScreenbutton;

    private static int AvailableResources;

    [Header("Total Players Investments Screen - Role Resources Rows")]
    public List<GameObject> RolesResourcesRows = new List<GameObject>();
    public GameObject TotalPlayersInvestmentScreenButtonsContainer;
    public GameObject TotalResourcesRow;


    // Default Method quality is level high i.e. 3
    public static int Method1Quality = 3;
    public static int Method2Quality = 3;
    public static int Method3Quality = 3;
    public static int Method4Quality = 3;
    public static int Method5Quality = 3;
    public static int Method6Quality = 3;
    public static int Method7Quality = 3;
    public static int Method8Quality = 3;
    public static int Method9Quality = 3;


    //create a list for Method1Quality for 6 players and initialize it with 3
    private List<int> Method1QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method2QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method3QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method4QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method5QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method6QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method7QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method8QualityList = Enumerable.Repeat(3, 6).ToList();
    private List<int> Method9QualityList = Enumerable.Repeat(3, 6).ToList();

    //to show sum of resources invested in each method in the total players investment screen
    int SumofMethod_1_Resources = 0;
    int SumofMethod_2_Resources = 0;
    int SumofMethod_3_Resources = 0;
    int SumofMethod_4_Resources = 0;
    int SumofMethod_5_Resources = 0;
    int SumofMethod_6_Resources = 0;
    int SumofMethod_7_Resources = 0;
    int SumofMethod_8_Resources = 0;
    int SumofMethod_9_Resources = 0;

    //a list to store 9 comparison flags of whether or not to show methods
    public static List<bool> showMethodsList = Enumerable.Repeat(true, 9).ToList();


    [Header("IntroTexts")]
    public List<GameObject> IntroTexts = new List<GameObject>();

    public Button NextMethodButton;
    public Button PreviousMethodButton;

    public static bool isInvestmentRoundStarted = false;

    private bool PlayerSubmit = false;
    private ExitGames.Client.Photon.Hashtable _playerCustomProperties = new ExitGames.Client.Photon.Hashtable();

    public static InvestmentManager instance;

    // Start is called before the first frame update
    void Start()
    {
        _playerCustomProperties["PlayerSubmit"] = false;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);

        Debug.Log(InvestmentScreen.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.name);

        //to set the background color of the screen
        GlobalVariables.setBackgroundColorForCanvas();
        GlobalVariables.setBackgroundColor(new List<GameObject>() { InvestmentIntroScreen, InvestmentScreen.transform.GetChild(0).gameObject, InvestmentConfirmationScreen.transform.GetChild(0).gameObject, TotalPlayersInvestmentScreen.transform.GetChild(0).gameObject, InvestmentScreen.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject }, "investmentsBackground");

        SetScreen(InvestmentIntroScreen);

        if (isInvestmentRoundStarted == false)
        {
            isInvestmentRoundStarted = true;

            GlobalVariables.incrementRoundCounter();
            if (GlobalVariables.roundCounter == 7)
            {
                promptsContainer.SetActive(true);
                promptsContainer.transform.GetChild(0).gameObject.SetActive(true);
                promptsContainer.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (GlobalVariables.roundCounter == 8)
            {
                promptsContainer.SetActive(true);
                promptsContainer.transform.GetChild(1).gameObject.SetActive(true);
                promptsContainer.transform.GetChild(0).gameObject.SetActive(false);
            }

            Invoke("SetPlayerResourcesPerWeek", 1.5f);
            // Invoke("updatePreviousInvestments", 1.5f);
        }

        //set the round counter text
        RoundCounterText.text = "Round: " + GlobalVariables.roundCounter.ToString();

    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        AvailableResourcesText.text = AvailableResources.ToString();
        Invoke("updatePreviousInvestments", 1.5f);

        if (InvestmentConfirmationScreen.activeSelf)
        {
            if (PhotonNetwork.IsMasterClient && AllPlayersReady)
                ContinuetoTotalPlayerResourcesScreenbutton.GetComponent<Button>().interactable = true;
            else
                ContinuetoTotalPlayerResourcesScreenbutton.GetComponent<Button>().interactable = false;
        }
    }

    public void updatePreviousInvestments()
    {
        Method1PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_1.ToString();
        Method2PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_2.ToString();
        Method3PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_3.ToString();

        if (GlobalVariables.PhaseCounter > 1)
        {
            Method4PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_4.ToString();
            Method5PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_5.ToString();

            if (GlobalVariables.PhaseCounter > 2)
            {

                Method6PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_6.ToString();
                Method7PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_7.ToString();

                if (GlobalVariables.PhaseCounter > 3)
                {
                    Method8PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_8.ToString();
                    Method9PreviousInvestedResourcesText.text = InvestmentDB.instance.investment_values_data.data[0].method_9.ToString();
                }
            }
        }
    }

    public void AddScore(string methodName)
    {
        if (AvailableResources > 0)
        {
            AvailableResources -= 1;
            switch (methodName)
            {
                case "Method1":
                    Method1CurrentInvestedResourcesText.text = (int.Parse(Method1CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method2":
                    Method2CurrentInvestedResourcesText.text = (int.Parse(Method2CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method3":
                    Method3CurrentInvestedResourcesText.text = (int.Parse(Method3CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method4":
                    Method4CurrentInvestedResourcesText.text = (int.Parse(Method4CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method5":
                    Method5CurrentInvestedResourcesText.text = (int.Parse(Method5CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method6":
                    Method6CurrentInvestedResourcesText.text = (int.Parse(Method6CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method7":
                    Method7CurrentInvestedResourcesText.text = (int.Parse(Method7CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method8":
                    Method8CurrentInvestedResourcesText.text = (int.Parse(Method8CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
                case "Method9":
                    Method9CurrentInvestedResourcesText.text = (int.Parse(Method9CurrentInvestedResourcesText.text) + 1).ToString();
                    break;
            }
        }
    }

    public void SubScore(string methodName)
    {
        switch (methodName)
        {
            case "Method1":
                if (int.Parse(Method1CurrentInvestedResourcesText.text) > 0)
                {
                    Method1CurrentInvestedResourcesText.text = (int.Parse(Method1CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method2":
                if (int.Parse(Method2CurrentInvestedResourcesText.text) > 0)
                {
                    Method2CurrentInvestedResourcesText.text = (int.Parse(Method2CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method3":
                if (int.Parse(Method3CurrentInvestedResourcesText.text) > 0)
                {
                    Method3CurrentInvestedResourcesText.text = (int.Parse(Method3CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method4":
                if (int.Parse(Method4CurrentInvestedResourcesText.text) > 0)
                {
                    Method4CurrentInvestedResourcesText.text = (int.Parse(Method4CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method5":
                if (int.Parse(Method5CurrentInvestedResourcesText.text) > 0)
                {
                    Method5CurrentInvestedResourcesText.text = (int.Parse(Method5CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method6":
                if (int.Parse(Method6CurrentInvestedResourcesText.text) > 0)
                {
                    Method6CurrentInvestedResourcesText.text = (int.Parse(Method6CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method7":
                if (int.Parse(Method7CurrentInvestedResourcesText.text) > 0)
                {
                    Method7CurrentInvestedResourcesText.text = (int.Parse(Method7CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method8":
                if (int.Parse(Method8CurrentInvestedResourcesText.text) > 0)
                {
                    Method8CurrentInvestedResourcesText.text = (int.Parse(Method8CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
            case "Method9":
                if (int.Parse(Method9CurrentInvestedResourcesText.text) > 0)
                {
                    Method9CurrentInvestedResourcesText.text = (int.Parse(Method9CurrentInvestedResourcesText.text) - 1).ToString();
                    AvailableResources += 1;
                }
                break;
        }
    }

    public void SetScreen(GameObject screen)
    {
        InvestmentIntroScreen.SetActive(false);
        InvestmentScreen.SetActive(false);
        InvestmentConfirmationScreen.SetActive(false);
        TotalPlayersInvestmentScreen.SetActive(false);

        //activate the requested Screen
        screen.SetActive(true);
    }

    public void SubmitInvestmentBtn()
    {
        // //send the investment data to the server - Github Co-pilot code suggestion
        // photonView.RPC("ReceiveInvestment", RpcTarget.AllBuffered, int.Parse(Method1InvestedResourcesText.text), int.Parse(Method2InvestedResourcesText.text), int.Parse(Method3InvestedResourcesText.text));
        //add previous investment to the current investment


        Method1TotalInvestedResourcesText.text = (int.Parse(Method1PreviousInvestedResourcesText.text) + int.Parse(Method1CurrentInvestedResourcesText.text)).ToString();
        Method2TotalInvestedResourcesText.text = (int.Parse(Method2PreviousInvestedResourcesText.text) + int.Parse(Method2CurrentInvestedResourcesText.text)).ToString();
        Method3TotalInvestedResourcesText.text = (int.Parse(Method3PreviousInvestedResourcesText.text) + int.Parse(Method3CurrentInvestedResourcesText.text)).ToString();

        TotalInvestedResourcesText.text = (int.Parse(Method1TotalInvestedResourcesText.text) + int.Parse(Method2TotalInvestedResourcesText.text) + int.Parse(Method3TotalInvestedResourcesText.text)).ToString();


        if (GlobalVariables.PhaseCounter > 1)
        {

            Method4TotalInvestedResourcesText.text = (int.Parse(Method4PreviousInvestedResourcesText.text) + int.Parse(Method4CurrentInvestedResourcesText.text)).ToString();
            Method5TotalInvestedResourcesText.text = (int.Parse(Method5PreviousInvestedResourcesText.text) + int.Parse(Method5CurrentInvestedResourcesText.text)).ToString();

            TotalInvestedResourcesText.text = (int.Parse(TotalInvestedResourcesText.text) + int.Parse(Method4TotalInvestedResourcesText.text) + int.Parse(Method5TotalInvestedResourcesText.text)).ToString();

            if (GlobalVariables.PhaseCounter > 2)
            {

                Method6TotalInvestedResourcesText.text = (int.Parse(Method6PreviousInvestedResourcesText.text) + int.Parse(Method6CurrentInvestedResourcesText.text)).ToString();
                Method7TotalInvestedResourcesText.text = (int.Parse(Method7PreviousInvestedResourcesText.text) + int.Parse(Method7CurrentInvestedResourcesText.text)).ToString();

                TotalInvestedResourcesText.text = (int.Parse(TotalInvestedResourcesText.text) + int.Parse(Method6TotalInvestedResourcesText.text) + int.Parse(Method7TotalInvestedResourcesText.text)).ToString();


                if (GlobalVariables.PhaseCounter > 3)
                {

                    Method8TotalInvestedResourcesText.text = (int.Parse(Method8PreviousInvestedResourcesText.text) + int.Parse(Method8CurrentInvestedResourcesText.text)).ToString();
                    Method9TotalInvestedResourcesText.text = (int.Parse(Method9PreviousInvestedResourcesText.text) + int.Parse(Method9CurrentInvestedResourcesText.text)).ToString();

                    TotalInvestedResourcesText.text = (int.Parse(TotalInvestedResourcesText.text) + int.Parse(Method8TotalInvestedResourcesText.text) + int.Parse(Method9TotalInvestedResourcesText.text)).ToString();

                }
            }
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            ContinuetoTotalPlayerResourcesScreenbutton.SetActive(false);
        }
        SetScreen(InvestmentConfirmationScreen);
        SetPlayerSubmitInPhoton();
    }

    public void BackToInvestmentsScreenBtn()
    {
        photonView.RPC("BackToInvestmentsScreenBtnAsPunRPC", RpcTarget.All);
        UnSetPlayerSubmitInPhoton();
    }

    [PunRPC]
    public void BackToInvestmentsScreenBtnAsPunRPC()
    {
        SetScreen(InvestmentScreen);
    }

    public void BackToInvestmentsScreenBtnOnInvestmentConfirmationScreen()
    {
        SetScreen(InvestmentScreen);
        UnSetPlayerSubmitInPhoton();
    }

    public void ContinueToTotalPlayerInvestmentsScreenBtn()
    {
        photonView.RPC("SendFinalInvestmentsAndChangeScene", RpcTarget.All);
    }

    [PunRPC]
    public void SendFinalInvestmentsAndChangeScene()
    {
        //send all players data to the server

        switch (GlobalVariables.PhaseCounter)
        {

            case 1:
                InvestmentDB.instance.SendFinalInvestmentsPhase1(Method1TotalInvestedResourcesText.text, Method2TotalInvestedResourcesText.text, Method3TotalInvestedResourcesText.text);
                break;
            case 2:
                InvestmentDB.instance.SendFinalInvestmentsPhase2(Method1TotalInvestedResourcesText.text, Method2TotalInvestedResourcesText.text, Method3TotalInvestedResourcesText.text, Method4TotalInvestedResourcesText.text, Method5TotalInvestedResourcesText.text);
                break;
            case 3:
                InvestmentDB.instance.SendFinalInvestmentsPhase3(Method1TotalInvestedResourcesText.text, Method2TotalInvestedResourcesText.text, Method3TotalInvestedResourcesText.text, Method4TotalInvestedResourcesText.text, Method5TotalInvestedResourcesText.text, Method6TotalInvestedResourcesText.text, Method7TotalInvestedResourcesText.text);
                break;
            case 4:
                InvestmentDB.instance.SendFinalInvestmentsPhase4(Method1TotalInvestedResourcesText.text, Method2TotalInvestedResourcesText.text, Method3TotalInvestedResourcesText.text, Method4TotalInvestedResourcesText.text, Method5TotalInvestedResourcesText.text, Method6TotalInvestedResourcesText.text, Method7TotalInvestedResourcesText.text, Method8TotalInvestedResourcesText.text, Method9TotalInvestedResourcesText.text);
                break;

        }

        //get all players data from the server and update the TotalPlayersInvestmentScreen with updated data
        //Using Invoke() to create a delay to allow the server to update the data to the screen
        Invoke("UpdateAndRefreshTotalInvestmentScreen", 1f);
    }

    public void GoToDevelopingMethodsScreen()
    {
        // QualityCalculator();
        // SceneManager.LoadScene("DevelopingMethods");

        //Select methods to be shown in the developing methods screen
        photonView.RPC("SelectMethodsToBeShownInDevelopingMethodsScreen", RpcTarget.All);

        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "phase" + GlobalVariables.PhaseCounter + "-" + "DevelopingMethods");
    }

    [PunRPC]
    void SelectMethodsToBeShownInDevelopingMethodsScreen()
    {
        previous_total_investment_values PreviousInvestmentValues = InvestmentDB.instance.previous_investment_values_data;
        // investment_values CurrentInvestmentValues = InvestmentDB.instance.s;

        //compare the previous investment values with the current investment values for each of the methods methods_1 to methods_9 string
        //if the current investment value is greater than the previous investment value, then set the ShowMethodsList[i] to true

        if (SumofMethod_1_Resources > int.Parse(PreviousInvestmentValues.data[0].method_1) || (GlobalVariables.PhaseCounter == 1))
            showMethodsList[0] = true;
        else
        { showMethodsList[0] = false; }

        if (SumofMethod_2_Resources > int.Parse(PreviousInvestmentValues.data[0].method_2) || (GlobalVariables.PhaseCounter == 1))
            showMethodsList[1] = true;
        else
        { showMethodsList[1] = false; }

        if (SumofMethod_3_Resources > int.Parse(PreviousInvestmentValues.data[0].method_3) || (GlobalVariables.PhaseCounter == 1))
            showMethodsList[2] = true;
        else
        { showMethodsList[2] = false; }

        if (SumofMethod_4_Resources > int.Parse(PreviousInvestmentValues.data[0].method_4) || (GlobalVariables.PhaseCounter == 2))
            showMethodsList[3] = true;
        else
        { showMethodsList[3] = false; }

        if (SumofMethod_5_Resources > int.Parse(PreviousInvestmentValues.data[0].method_5) || (GlobalVariables.PhaseCounter == 2))
            showMethodsList[4] = true;
        else
        { showMethodsList[4] = false; }

        if (SumofMethod_6_Resources > int.Parse(PreviousInvestmentValues.data[0].method_6) || (GlobalVariables.PhaseCounter == 3))
            showMethodsList[5] = true;
        else
        { showMethodsList[5] = false; }

        if (SumofMethod_7_Resources > int.Parse(PreviousInvestmentValues.data[0].method_7) || (GlobalVariables.PhaseCounter == 3))
            showMethodsList[6] = true;
        else
        { showMethodsList[6] = false; }

        if (SumofMethod_8_Resources > int.Parse(PreviousInvestmentValues.data[0].method_8) || (GlobalVariables.PhaseCounter == 4))
            showMethodsList[7] = true;
        else
        { showMethodsList[7] = false; }

        if (SumofMethod_9_Resources > int.Parse(PreviousInvestmentValues.data[0].method_9) || (GlobalVariables.PhaseCounter == 4))
            showMethodsList[8] = true;
        else
        { showMethodsList[8] = false; }

    }

    public void UpdateAndRefreshTotalInvestmentScreen()
    {
        SetScreen(TotalPlayersInvestmentScreen);
        if (PhotonNetwork.IsMasterClient)
        {
            TotalPlayersInvestmentScreenButtonsContainer.SetActive(true);
        }

        InvestmentDB.instance.setTotalInvestmentValues();

        Invoke("QualityCalculator", 1.5f);
    }

    public void UpdateTotalInvestmentValuesInUnityScreen(investment_values investments)
    {
        //get text value from RoleText gameobject inside the RolesResourcesRows[i] gameobject
        for (int i = 0; i < RolesResourcesRows.Count; i++)
        {
            string roleName = RolesResourcesRows[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
            roleName = roleName.Replace(" ", "");

            //loop each playerData and check if the playerRole is equal to the roleName
            for (int j = 0; j < investments.data.Count; j++)
            {
                // Debug.Log("RoleName: " + roleName + " | PlayerRole: " + investments.data[j].playerRole);
                if (roleName == investments.data[j].playerRole)
                {
                    //if the playerRole is equal to the roleName, then get the method_1, method_2 and method_3 values from the investments data and set them to the RolesResourcesRows[i] gameobject
                    RolesResourcesRows[i].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_1.ToString();
                    RolesResourcesRows[i].transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_2.ToString();
                    RolesResourcesRows[i].transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_3.ToString();

                    if (GlobalVariables.PhaseCounter > 1)
                    {
                        RolesResourcesRows[i].transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_4.ToString();
                        RolesResourcesRows[i].transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_5.ToString();

                        if (GlobalVariables.PhaseCounter > 2)
                        {
                            RolesResourcesRows[i].transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_6.ToString();
                            RolesResourcesRows[i].transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_7.ToString();

                            if (GlobalVariables.PhaseCounter > 3)
                            {
                                RolesResourcesRows[i].transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_8.ToString();
                                RolesResourcesRows[i].transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = investments.data[j].method_9.ToString();
                            }
                        }
                    }
                }
            }
        }

        //initializing the SumofMethodResources to 0
        SumofMethod_1_Resources = 0;
        SumofMethod_2_Resources = 0;
        SumofMethod_3_Resources = 0;
        SumofMethod_4_Resources = 0;
        SumofMethod_5_Resources = 0;
        SumofMethod_6_Resources = 0;
        SumofMethod_7_Resources = 0;
        SumofMethod_8_Resources = 0;
        SumofMethod_9_Resources = 0;

        //get the sum of investments.data[i] for each method_1,method_2, method_3, and set them to the TotalResourcesRow gameobject
        for (int i = 0; i < investments.data.Count; i++)
        {
            SumofMethod_1_Resources += int.Parse(investments.data[i].method_1);
            SumofMethod_2_Resources += int.Parse(investments.data[i].method_2);
            SumofMethod_3_Resources += int.Parse(investments.data[i].method_3);

            SumofMethod_4_Resources += int.Parse(investments.data[i].method_4);
            SumofMethod_5_Resources += int.Parse(investments.data[i].method_5);

            SumofMethod_6_Resources += int.Parse(investments.data[i].method_6);
            SumofMethod_7_Resources += int.Parse(investments.data[i].method_7);

            SumofMethod_8_Resources += int.Parse(investments.data[i].method_8);
            SumofMethod_9_Resources += int.Parse(investments.data[i].method_9);
        }

        TotalResourcesRow.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_1_Resources.ToString();
        TotalResourcesRow.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_2_Resources.ToString();
        TotalResourcesRow.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_3_Resources.ToString();

        if (GlobalVariables.PhaseCounter > 1)
        {
            TotalResourcesRow.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_4_Resources.ToString();
            TotalResourcesRow.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_5_Resources.ToString();

            if (GlobalVariables.PhaseCounter > 2)
            {
                TotalResourcesRow.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_6_Resources.ToString();
                TotalResourcesRow.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_7_Resources.ToString();

                if (GlobalVariables.PhaseCounter > 3)
                {
                    TotalResourcesRow.transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_8_Resources.ToString();
                    TotalResourcesRow.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = SumofMethod_9_Resources.ToString();

                }
            }
        }
    }

    public void QualityCalculator()
    {
        //reset the quality values to 3 after each round
        Method1Quality = 3;
        Method2Quality = 3;
        Method3Quality = 3;
        Method4Quality = 3;
        Method5Quality = 3;
        Method6Quality = 3;
        Method7Quality = 3;
        Method8Quality = 3;
        Method9Quality = 3;

        threshold_quality_values threshold_values = InvestmentDB.threshold_values;
        investment_values investments = InvestmentDB.instance.s;
        Debug.Log("Testing Investments: method 1: " + investments.data[0].method_1);
        // player_values playerData = InvestmentDB.instance.p;
        // Debug.Log("threshold_values.data.Count: " + threshold_values.data.Count);
        for (int i = 0; i < investments.data.Count; i++)
        {
            string roleName = investments.data[i].playerRole;

            int currentInvestmentsInMethod_1 = int.Parse(investments.data[i].method_1);
            int currentInvestmentsInMethod_2 = int.Parse(investments.data[i].method_2);
            int currentInvestmentsInMethod_3 = int.Parse(investments.data[i].method_3);
            int currentInvestmentsInMethod_4 = int.Parse(investments.data[i].method_4);
            int currentInvestmentsInMethod_5 = int.Parse(investments.data[i].method_5);
            int currentInvestmentsInMethod_6 = int.Parse(investments.data[i].method_6);
            int currentInvestmentsInMethod_7 = int.Parse(investments.data[i].method_7);
            int currentInvestmentsInMethod_8 = int.Parse(investments.data[i].method_8);
            int currentInvestmentsInMethod_9 = int.Parse(investments.data[i].method_9);


            //loop each playerData and check if the playerRole is equal to the roleName
            for (int j = 0; j < threshold_values.data.Count; j++)
            {

                if (roleName == threshold_values.data[j].Role)
                {

                    // ----------------Calculation for Method 1 ----------------

                    Method1QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_1, int.Parse(threshold_values.data[j].StakeholderAnalyses_High), int.Parse(threshold_values.data[j].StakeholderAnalyses_Medium), int.Parse(threshold_values.data[j].StakeholderAnalyses_Low), Method1Quality);

                    // ----------------Calculation for Method 2 ----------------

                    Method2QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_2, int.Parse(threshold_values.data[j].EnvironmentModel_High), int.Parse(threshold_values.data[j].EnvironmentModel_Medium), int.Parse(threshold_values.data[j].EnvironmentModel_Low), Method2Quality);

                    // ----------------Calculation for Method 3 ----------------

                    Method3QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_3, int.Parse(threshold_values.data[j].ApplicationsScenario_High), int.Parse(threshold_values.data[j].ApplicationsScenario_Medium), int.Parse(threshold_values.data[j].ApplicationsScenario_Low), Method3Quality);

                    // ----------------Calculation for Method 4 ----------------

                    Method4QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_4, int.Parse(threshold_values.data[j].FunctionsHierarchy_High), int.Parse(threshold_values.data[j].FunctionsHierarchy_Medium), int.Parse(threshold_values.data[j].FunctionsHierarchy_Low), Method4Quality);

                    // ----------------Calculation for Method 5 ----------------

                    Method5QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_5, int.Parse(threshold_values.data[j].ActivityDiagram_High), int.Parse(threshold_values.data[j].ActivityDiagram_Medium), int.Parse(threshold_values.data[j].ActivityDiagram_Low), Method5Quality);

                    // ----------------Calculation for Method 6 ----------------

                    Method6QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_6, int.Parse(threshold_values.data[j].MorphologicalBox_High), int.Parse(threshold_values.data[j].MorphologicalBox_Medium), int.Parse(threshold_values.data[j].MorphologicalBox_Low), Method6Quality);

                    // ----------------Calculation for Method 7 ----------------

                    Method7QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_7, int.Parse(threshold_values.data[j].UtilityAnalysis_High), int.Parse(threshold_values.data[j].UtilityAnalysis_Medium), int.Parse(threshold_values.data[j].UtilityAnalysis_Low), Method7Quality);

                    // ----------------Calculation for Method 8 ----------------

                    Method8QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_8, int.Parse(threshold_values.data[j].LogicalArchitecture_High), int.Parse(threshold_values.data[j].LogicalArchitecture_Medium), int.Parse(threshold_values.data[j].LogicalArchitecture_Low), Method8Quality);

                    // ----------------Calculation for Method 9 ----------------

                    Method9QualityList[i] = CalculateQualityForMethod_n(currentInvestmentsInMethod_9, int.Parse(threshold_values.data[j].FMEA_High), int.Parse(threshold_values.data[j].FMEA_Medium), int.Parse(threshold_values.data[j].FMEA_Low), Method9Quality);


                    break;
                    // Debug.Log("For Role " + roleName + ", Method1Quality: " + Method1Quality + "\nMethod2Quality: " + Method2Quality + " \nMethod4Quality: " + Method4Quality);
                }
            }
        }

        //Debug.Log the values in Method1QualityList, Method2QualityList and Method3QualityList arrays
        Debug.Log("Method1QualityList: \t" + Method1QualityList[0] + "\t" + Method1QualityList[1] + "\t" + Method1QualityList[2] + "\t" + Method1QualityList[3] + "\t" + Method1QualityList[4] + "\t" + Method1QualityList[5]);
        Debug.Log("Method2QualityList: \t" + Method2QualityList[0] + "\t" + Method2QualityList[1] + "\t" + Method2QualityList[2] + "\t" + Method2QualityList[3] + "\t" + Method2QualityList[4] + "\t" + Method2QualityList[5]);
        Debug.Log("Method3QualityList: \t" + Method3QualityList[0] + "\t" + Method3QualityList[1] + "\t" + Method3QualityList[2] + "\t" + Method3QualityList[3] + "\t" + Method3QualityList[4] + "\t" + Method3QualityList[5]);
        Debug.Log("Method4QualityList: \t" + Method4QualityList[0] + "\t" + Method4QualityList[1] + "\t" + Method4QualityList[2] + "\t" + Method4QualityList[3] + "\t" + Method4QualityList[4] + "\t" + Method4QualityList[5]);
        Debug.Log("Method5QualityList: \t" + Method5QualityList[0] + "\t" + Method5QualityList[1] + "\t" + Method5QualityList[2] + "\t" + Method5QualityList[3] + "\t" + Method5QualityList[4] + "\t" + Method5QualityList[5]);
        Debug.Log("Method6QualityList: \t" + Method6QualityList[0] + "\t" + Method6QualityList[1] + "\t" + Method6QualityList[2] + "\t" + Method6QualityList[3] + "\t" + Method6QualityList[4] + "\t" + Method6QualityList[5]);
        Debug.Log("Method7QualityList: \t" + Method7QualityList[0] + "\t" + Method7QualityList[1] + "\t" + Method7QualityList[2] + "\t" + Method7QualityList[3] + "\t" + Method7QualityList[4] + "\t" + Method7QualityList[5]);
        Debug.Log("Method8QualityList: \t" + Method8QualityList[0] + "\t" + Method8QualityList[1] + "\t" + Method8QualityList[2] + "\t" + Method8QualityList[3] + "\t" + Method8QualityList[4] + "\t" + Method8QualityList[5]);
        Debug.Log("Method9QualityList: \t" + Method9QualityList[0] + "\t" + Method9QualityList[1] + "\t" + Method9QualityList[2] + "\t" + Method9QualityList[3] + "\t" + Method9QualityList[4] + "\t" + Method9QualityList[5]);



        //find the minimum value in Method1QualityList, Method2QualityList and Method3QualityList arrays
        Method1Quality = Method1QualityList.Min();
        Method2Quality = Method2QualityList.Min();
        Method3Quality = Method3QualityList.Min();
        Method4Quality = Method4QualityList.Min();
        Method5Quality = Method5QualityList.Min();
        Method6Quality = Method6QualityList.Min();
        Method7Quality = Method7QualityList.Min();
        Method8Quality = Method8QualityList.Min();
        Method9Quality = Method9QualityList.Min();

    }

    // int SetPlayerResourcesPerWeek(string roleName, PlayerResources player_resource_data)
    // {
    //     for (int j = 0; j < player_resource_data.data.Count; j++)
    //     {
    //         if (roleName == player_resource_data.data[j].playerRole)
    //         {
    //             return int.Parse(player_resource_data.data[j].resources);
    //         }
    //     }
    //     return 0;
    // }
    void SetPlayerResourcesPerWeek()
    {
        string roleName = GameManager.instance.roleName;
        PlayerResources player_resource_data = InvestmentDB.player_resource_data;
        for (int j = 0; j < player_resource_data.data.Count; j++)
        {
            if (roleName == player_resource_data.data[j].playerRole)
            {
                AvailableResources = int.Parse(player_resource_data.data[j].resources);
                Debug.Log("AvailableResources in Round " + GlobalVariables.roundCounter + " = " + AvailableResources);
                break;
            }
        }
    }

    public void OnNextButtonClick()
    {
        for (int i = 0; i < IntroTexts.Count; i++)
        {
            if (IntroTexts[i].activeSelf)
            {
                IntroTexts[i].SetActive(false);
                if (i + 1 < IntroTexts.Count)
                {
                    IntroTexts[i + 1].SetActive(true);
                    PreviousMethodButton.gameObject.SetActive(true);
                    PreviousMethodButton.interactable = true;
                }
                else
                {
                    SetScreen(InvestmentScreen);
                }
                break;

            }
        }
    }

    public void OnPreviousButtonClick()
    {
        for (int i = 0; i < IntroTexts.Count; i++)
        {
            if (IntroTexts[i].activeSelf)
            {
                if (i - 1 >= 0)
                {
                    IntroTexts[i].SetActive(false);
                    IntroTexts[i - 1].SetActive(true);
                }
                if (i == 0)
                {
                    // Debug.Log("value of i: " + i);
                    SceneManager.LoadScene("phase" + GlobalVariables.PhaseCounter + "-" + "MethodsIntroduction");
                    // PreviousMethodButton.gameObject.SetActive(false);
                    // PreviousMethodButton.interactable = false;
                }
                break;
            }
        }
    }

    public int CalculateQualityForMethod_n(int CurrentInvestmentsforMethod_n, int threshold_value_forMethod_n_High, int threshold_value_forMethod_n_Medium, int threshold_value_forMethod_n_Low, int Method_n_Quality)
    {

        if ((CurrentInvestmentsforMethod_n >= threshold_value_forMethod_n_High) && (Method_n_Quality > 2))
        {
            return 3;
        }
        else if ((CurrentInvestmentsforMethod_n < threshold_value_forMethod_n_High) && (CurrentInvestmentsforMethod_n >= threshold_value_forMethod_n_Medium) && (Method_n_Quality > 1))
        {
            return 2;
        }
        else if ((CurrentInvestmentsforMethod_n < threshold_value_forMethod_n_Medium) && (CurrentInvestmentsforMethod_n >= threshold_value_forMethod_n_Low) && (Method_n_Quality > 0))
        {
            return 1;
        }
        else
        {
            return 0;
        }


    }

    public void showRoleDescription()
    {
        RoleDescriptions.SetActive(true);
        RoleDescriptions.transform.Find(GameManager.instance.roleName).gameObject.SetActive(true);
    }

    private bool AllPlayersReady
    {
        get
        {
            foreach (var photonPlayer in PhotonNetwork.PlayerList)
            {
                if ((bool)photonPlayer.CustomProperties["PlayerSubmit"] == false) return false;
            }

            return true;
        }
    }
    // private bool AllPlayersReady => PhotonNetwork.PlayerList.All(player => (bool)player.CustomProperties["PlayerSubmit"] == true);

    private void SetPlayerSubmitInPhoton()
    {
        PlayerSubmit = true;
        _playerCustomProperties["PlayerSubmit"] = PlayerSubmit;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
    }

    private void UnSetPlayerSubmitInPhoton()
    {
        PlayerSubmit = false;
        _playerCustomProperties["PlayerSubmit"] = PlayerSubmit;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
    }
}
