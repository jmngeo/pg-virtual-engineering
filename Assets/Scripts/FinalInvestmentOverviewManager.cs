using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class FinalInvestmentOverviewManager : MonoBehaviourPun
{
    [Header("Screens")]
    public GameObject FinalInvestmentOverviewScreen;
    public GameObject RoleSelectionScreen;

    [Header("Role Text")]
    public TextMeshProUGUI RoleText;

    [Header("Method n - Quality thresholds")]
    public List<GameObject> Method1QualityThresholds = new List<GameObject>();
    public List<GameObject> Method2QualityThresholds = new List<GameObject>();
    public List<GameObject> Method3QualityThresholds = new List<GameObject>();
    public List<GameObject> Method4QualityThresholds = new List<GameObject>();
    public List<GameObject> Method5QualityThresholds = new List<GameObject>();
    public List<GameObject> Method6QualityThresholds = new List<GameObject>();
    public List<GameObject> Method7QualityThresholds = new List<GameObject>();
    public List<GameObject> Method8QualityThresholds = new List<GameObject>();
    public List<GameObject> Method9QualityThresholds = new List<GameObject>();

    [Header("Invested Values for 9 Methods")]
    public List<GameObject> InvestedValuesFor9Methods = new List<GameObject>();

    [Header("Difference for 9 Methods")]
    public List<GameObject> DifferenceFor9Methods = new List<GameObject>();

    public static FinalInvestmentOverviewManager instance;

    void Start()
    {
        //setting background color
        GlobalVariables.setBackgroundColorForCanvas();
        GlobalVariables.setBackgroundColor(new List<GameObject>() { FinalInvestmentOverviewScreen }, "PhaseEndBackground");
    }

    private void Awake()
    {
        instance = this;
    }

    public void onRoleButtonPress()
    {
        //get the name of the role button that was pressed 
        string roleName = EventSystem.current.currentSelectedGameObject.name;
        roleName = roleName.Remove(0, 3);

        SetScreen(FinalInvestmentOverviewScreen);
        SetRoleText(roleName);
        SetValuesInTable(roleName);
    }

    public void SetRoleText(string role)
    {
        RoleText.text = role;
    }

    void SetValuesInTable(string role)
    {
        for (int i = 0; i < InvestmentDB.threshold_values.data.Count; i++)
        {
            if (InvestmentDB.threshold_values.data[i].Role == role)
            {
                Method1QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].StakeholderAnalyses_Low;
                Method1QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].StakeholderAnalyses_Medium;
                Method1QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].StakeholderAnalyses_High;

                Method2QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].EnvironmentModel_Low;
                Method2QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].EnvironmentModel_Medium;
                Method2QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].EnvironmentModel_High;

                Method3QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].ApplicationsScenario_Low;
                Method3QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].ApplicationsScenario_Medium;
                Method3QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].ApplicationsScenario_High;

                Method4QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].FunctionsHierarchy_Low;
                Method4QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].FunctionsHierarchy_Medium;
                Method4QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].FunctionsHierarchy_High;

                Method5QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].ActivityDiagram_Low;
                Method5QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].ActivityDiagram_Medium;
                Method5QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].ActivityDiagram_High;

                Method6QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].MorphologicalBox_Low;
                Method6QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].MorphologicalBox_Medium;
                Method6QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].MorphologicalBox_High;

                Method7QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].UtilityAnalysis_Low;
                Method7QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].UtilityAnalysis_Medium;
                Method7QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].UtilityAnalysis_High;

                Method8QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].LogicalArchitecture_Low;
                Method8QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].LogicalArchitecture_Medium;
                Method8QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].LogicalArchitecture_High;

                Method9QualityThresholds[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].FMEA_Low;
                Method9QualityThresholds[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].FMEA_Medium;
                Method9QualityThresholds[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.threshold_values.data[i].FMEA_High;

                break;
            }
        }

        for (int i = 0; i < InvestmentDB.instance.s.data.Count; i++)
        {
            if (InvestmentDB.instance.s.data[i].playerRole == role)
            {
                InvestedValuesFor9Methods[0].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_1;
                InvestedValuesFor9Methods[1].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_2;
                InvestedValuesFor9Methods[2].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_3;
                InvestedValuesFor9Methods[3].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_4;
                InvestedValuesFor9Methods[4].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_5;
                InvestedValuesFor9Methods[5].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_6;
                InvestedValuesFor9Methods[6].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_7;
                InvestedValuesFor9Methods[7].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_8;
                InvestedValuesFor9Methods[8].GetComponent<TextMeshProUGUI>().text = InvestmentDB.instance.s.data[i].method_9;
                break;
            }

        }

        DifferenceFor9Methods[0].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method1QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[0].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[1].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method2QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[1].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[2].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method3QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[2].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[3].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method4QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[3].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[4].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method5QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[4].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[5].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method6QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[5].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[6].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method7QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[6].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[7].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method8QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[7].GetComponent<TextMeshProUGUI>().text)).ToString();
        DifferenceFor9Methods[8].GetComponent<TextMeshProUGUI>().text = (int.Parse(Method9QualityThresholds[2].GetComponent<TextMeshProUGUI>().text) - int.Parse(InvestedValuesFor9Methods[8].GetComponent<TextMeshProUGUI>().text)).ToString();


        for (int i = 0; i < DifferenceFor9Methods.Count; i++)
        {
            if (int.Parse(DifferenceFor9Methods[i].GetComponent<TextMeshProUGUI>().text) > 0)
            {
                DifferenceFor9Methods[i].GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                if (int.Parse(DifferenceFor9Methods[i].GetComponent<TextMeshProUGUI>().text) <= -5)
                    DifferenceFor9Methods[i].GetComponent<TextMeshProUGUI>().color = Color.red;
                else
                    DifferenceFor9Methods[i].GetComponent<TextMeshProUGUI>().color = Color.green;
            }
        }
    }

    public void SetScreen(GameObject screen)
    {
        FinalInvestmentOverviewScreen.SetActive(false);
        RoleSelectionScreen.SetActive(false);

        //activate the requested Screen
        screen.SetActive(true);
    }

    public void ResetValuesToZero()
    {
        for (int i = 0; i < InvestedValuesFor9Methods.Count; i++)
        {
            InvestedValuesFor9Methods[i].GetComponent<TextMeshProUGUI>().text = "0";
            DifferenceFor9Methods[i].GetComponent<TextMeshProUGUI>().text = "0";
        }

    }

}
