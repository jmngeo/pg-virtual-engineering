using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameEndReportManager : MonoBehaviourPun
{
    [Header("Screens")]
    public GameObject IntroScreen;
    public GameObject FinalReportScreen;
    public GameObject OverallRatingScreen;
    public GameObject RoleSelectionScreen;

    [Header("Buttons")]
    public Button NextButton;
    public Button PreviousButton;

    [Header("Intro Screen Elements")]
    public GameObject Description1;
    public GameObject Description2;

    [Header("Total: 4 Rows")]
    public GameObject Total_ResourceInvestedRow;
    public GameObject Total_PointsGainedFromResourceInvestmentRow;
    public GameObject Total_PointsGainedFromPhaseEndEventRow;
    public GameObject Total_TotalPointsRow;

    [Header("Phase1: 4 Rows")]
    public GameObject Phase1_ResourceInvestedRow;
    public GameObject Phase1_PointsGainedFromResourceInvestmentRow;
    public GameObject Phase1_PointsGainedFromPhaseEndEventRow;
    public GameObject Phase1_TotalPointsRow;

    [Header("Phase2: 4 Rows")]
    public GameObject Phase2_ResourceInvestedRow;
    public GameObject Phase2_PointsGainedFromResourceInvestmentRow;
    public GameObject Phase2_PointsGainedFromPhaseEndEventRow;
    public GameObject Phase2_TotalPointsRow;

    [Header("Phase3: 4 Rows")]
    public GameObject Phase3_ResourceInvestedRow;
    public GameObject Phase3_PointsGainedFromResourceInvestmentRow;
    public GameObject Phase3_PointsGainedFromPhaseEndEventRow;
    public GameObject Phase3_TotalPointsRow;

    [Header("Phase4: 4 Rows")]
    public GameObject Phase4_ResourceInvestedRow;
    public GameObject Phase4_PointsGainedFromResourceInvestmentRow;
    public GameObject Phase4_PointsGainedFromPhaseEndEventRow;
    public GameObject Phase4_TotalPointsRow;

    [Header("OverallQuality Screen Elements")]
    public TextMeshProUGUI OverallQualityText;


    List<GameObject> Screens;

    // List of all the method quality values
    List<int> MethodQualities = new List<int>() { InvestmentManager.Method1Quality, InvestmentManager.Method2Quality, InvestmentManager.Method3Quality, InvestmentManager.Method4Quality, InvestmentManager.Method5Quality, InvestmentManager.Method6Quality, InvestmentManager.Method7Quality, InvestmentManager.Method8Quality, InvestmentManager.Method9Quality };

    public static GameEndReportManager instance;


    float[] PhaseWeights = new float[4];

    //create a 3D array to store the data and initialize it to 0
    //1st dimension: 5 phases
    //2nd dimension: 4 rows
    //3rd dimension: 3 columns
    float[,,] FinalReportTable = new float[5, 4, 3];

    float TotalOptimumForQualityMeasurement;
    float TotalReachedValueForQualityMeasurement;

    float TotalOptimumForQualityMeasurement_PhaseData;
    float TotalReachedValueForQualityMeasurement_PhaseData;

    float TotalOptimumForQualityMeasurement_PhaseEnd;
    float TotalReachedValueForQualityMeasurement_PhaseEnd;
    public int OverallQuality;
    float OptimumPointsTotal;
    // Start is called before the first frame update

    // For Calculation of Phase Investment Points
    float[] WeightsPerMethodPerPhase = new float[9];
    float[] TempOptimumValuesPerMethodPerPhase = new float[9];

    float[] TempPointsPerPhase = new float[4];
    float[] OptimumPointsPerMethodPerPhase = new float[9];

    float[] TempPercentageValues = new float[4] { 0.25f, 0.50f, 0.75f, 1.00f };
    float[] PercentageModifiersPerMethodPerPhase = new float[9];

    float[] PointsFromResourceInvestment = new float[9];

    float[] PhaseResourceInvestmentQualityData = new float[5];

    float[] PhaseEndEventOptimalQualityData = new float[4] { 10f, 10f, 10f, 10f };
    float[] PhaseEndEventReachedQualityData = new float[5];
    float[] AllInvestmentsperMethod = new float[9];

    float modifier;
    void Start()
    {
        Screens = new List<GameObject>() { IntroScreen, FinalReportScreen, RoleSelectionScreen, OverallRatingScreen };

        //setting background color
        GlobalVariables.setBackgroundColorForCanvas();
        GlobalVariables.setBackgroundColor(Screens, "PhaseEndBackground");

        PreviousButton.interactable = false;
        SetScreen(IntroScreen);

        if (GlobalVariables.PhaseCounter == 5)
        {
            Description1.SetActive(true);
            Description2.SetActive(false);
        }
        else
        {
            Description1.SetActive(false);
            Description2.SetActive(true);
        }

        Invoke("SetValuesInFinalReportTable", 1.5f);
    }

    private void Awake()
    {
        instance = this;
    }

    void SetScreen(GameObject screen)
    {
        IntroScreen.SetActive(false);
        FinalReportScreen.SetActive(false);
        OverallRatingScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void OnNextButtonClick()
    {
        Debug.Log("Screens.Count: " + Screens.Count);
        for (int i = 0; i < Screens.Count; i++)
        {
            if (Screens[i].activeSelf)
            {

                if (i + 1 < Screens.Count)
                {
                    Screens[i].SetActive(false);
                    Screens[i + 1].SetActive(true);

                    if (i + 1 == Screens.Count - 1)
                    {
                        NextButton.interactable = false;
                    }
                    PreviousButton.gameObject.SetActive(true);
                    PreviousButton.interactable = true;
                }
                else
                {
                    NextButton.interactable = false;
                }
                break;
            }
        }
    }

    public void OnPreviousButtonClick()
    {
        for (int i = 0; i < Screens.Count; i++)
        {
            if (Screens[i].activeSelf)
            {
                if (i - 1 >= 0)
                {
                    NextButton.interactable = true;
                    Screens[i].SetActive(false);
                    Screens[i - 1].SetActive(true);
                }
                if (i - 1 == 0)
                {
                    PreviousButton.interactable = false;
                }
                break;
            }
        }
    }

    public void SetValuesInFinalReportTable()
    {
        calculatePhaseWeights();
        calculatePointsfromInvestment();
        calculatePointsFromPhaseEndEvent();
        populateFinalReportTableArray();


        GameObject[] Phase1Table = new GameObject[4] { Phase1_ResourceInvestedRow, Phase1_PointsGainedFromResourceInvestmentRow, Phase1_PointsGainedFromPhaseEndEventRow, Phase1_TotalPointsRow };
        GameObject[] Phase2Table = new GameObject[4] { Phase2_ResourceInvestedRow, Phase2_PointsGainedFromResourceInvestmentRow, Phase2_PointsGainedFromPhaseEndEventRow, Phase2_TotalPointsRow };
        GameObject[] Phase3Table = new GameObject[4] { Phase3_ResourceInvestedRow, Phase3_PointsGainedFromResourceInvestmentRow, Phase3_PointsGainedFromPhaseEndEventRow, Phase3_TotalPointsRow };
        GameObject[] Phase4Table = new GameObject[4] { Phase4_ResourceInvestedRow, Phase4_PointsGainedFromResourceInvestmentRow, Phase4_PointsGainedFromPhaseEndEventRow, Phase4_TotalPointsRow };
        GameObject[] TotalTable = new GameObject[4] { Total_ResourceInvestedRow, Total_PointsGainedFromResourceInvestmentRow, Total_PointsGainedFromPhaseEndEventRow, Total_TotalPointsRow };

        GameObject[][] FinalReportTableUI = new GameObject[5][] { TotalTable, Phase1Table, Phase2Table, Phase3Table, Phase4Table };

        string value = "";

        for (int i = 0; i < FinalReportTableUI.Length; i++)
        {
            for (int j = 0; j < FinalReportTableUI[i].Length; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    value = (Mathf.Round(FinalReportTable[i, j, k] * 10f) / 10f).ToString();
                    setTextInGameObject(FinalReportTableUI[i][j], k + 1, value);
                }
            }
        }

        //for calculating the overall quality and getting quality text from DB
        OverallQuality = calculateOverallQuality();
        GameEndReportDB.instance.fnFinalReportData(OverallQuality.ToString());

        Invoke("SetValuesInOverallQualityScreen", 1.5f);

    }

    void setTextInGameObject(GameObject gameObject, int childNumber, string text)
    {
        gameObject.transform.GetChild(childNumber - 1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }

    void calculatePhaseWeights()
    {
        PhaseWeights[0] = float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_1) / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].total_optimum_sum);
        PhaseWeights[1] = float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_2) / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].total_optimum_sum);
        PhaseWeights[2] = float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_3) / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].total_optimum_sum);
        PhaseWeights[3] = float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_4) / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].total_optimum_sum);
    }
    // For Row 1 following data is needed
    // FinalReportPhaseData
    // WeightsPerMethodPerPhase - InvestmentDB.threshold_values;
    // OptimumPointsPerMethodPerPhase
    // MethodQualities
    // PercentageModifiersPerMethodPerPhase
    // Points from Resource Investment

    void calculatePointsfromInvestment()
    {
        // calculate weights per method per phase

        for (int i = 0; i < InvestmentDB.threshold_values.data.Count; i++)
        {


            TempOptimumValuesPerMethodPerPhase[0] += float.Parse(InvestmentDB.threshold_values.data[i].StakeholderAnalyses_High);
            TempOptimumValuesPerMethodPerPhase[1] += float.Parse(InvestmentDB.threshold_values.data[i].EnvironmentModel_High);
            TempOptimumValuesPerMethodPerPhase[2] += float.Parse(InvestmentDB.threshold_values.data[i].ApplicationsScenario_High);
            TempOptimumValuesPerMethodPerPhase[3] += float.Parse(InvestmentDB.threshold_values.data[i].FunctionsHierarchy_High);
            TempOptimumValuesPerMethodPerPhase[4] += float.Parse(InvestmentDB.threshold_values.data[i].ActivityDiagram_High);
            TempOptimumValuesPerMethodPerPhase[5] += float.Parse(InvestmentDB.threshold_values.data[i].MorphologicalBox_High);
            TempOptimumValuesPerMethodPerPhase[6] += float.Parse(InvestmentDB.threshold_values.data[i].UtilityAnalysis_High);
            TempOptimumValuesPerMethodPerPhase[7] += float.Parse(InvestmentDB.threshold_values.data[i].LogicalArchitecture_High);
            TempOptimumValuesPerMethodPerPhase[8] += float.Parse(InvestmentDB.threshold_values.data[i].FMEA_High);


        }

        // debug TempOptimumValuesPerMethodPerPhase
        for (int i = 0; i < TempOptimumValuesPerMethodPerPhase.Length; i++)
        {
            Debug.Log("TempOptimumValuesPerMethodPerPhase" + TempOptimumValuesPerMethodPerPhase[i]);
        }

        for (int i = 0; i < WeightsPerMethodPerPhase.Length; i++)
        {
            if (i <= 2)
            {
                WeightsPerMethodPerPhase[i] = TempOptimumValuesPerMethodPerPhase[i] / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_1);
                Debug.Log("WeightsPerMethodPerPhase" + WeightsPerMethodPerPhase[i]);
            }
            else if (i > 2 && i <= 4)
            {
                WeightsPerMethodPerPhase[i] = TempOptimumValuesPerMethodPerPhase[i] / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_2);
                Debug.Log("WeightsPerMethodPerPhase" + WeightsPerMethodPerPhase[i]);
            }
            else if (i > 4 && i <= 6)
            {
                WeightsPerMethodPerPhase[i] = TempOptimumValuesPerMethodPerPhase[i] / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_3);
                Debug.Log("WeightsPerMethodPerPhase" + WeightsPerMethodPerPhase[i]);
            }
            else if (i > 6 && i <= 8)
            {
                WeightsPerMethodPerPhase[i] = TempOptimumValuesPerMethodPerPhase[i] / float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_4);
                Debug.Log("WeightsPerMethodPerPhase" + WeightsPerMethodPerPhase[i]);
            }

        }

        // Setting the point values per phase which is PhaseWeights * OptimumPointsPerPhase
        TempPointsPerPhase[0] = PhaseWeights[0] * float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_1);
        TempPointsPerPhase[1] = PhaseWeights[1] * float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_2);
        TempPointsPerPhase[2] = PhaseWeights[2] * float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_3);
        TempPointsPerPhase[3] = PhaseWeights[3] * float.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_4);

        // Debug Log TempPointsPerPhase
        for (int i = 0; i < TempPointsPerPhase.Length; i++)
        {
            Debug.Log("TempPointsPerPhase " + TempPointsPerPhase[i]);
        }

        for (int i = 0; i < OptimumPointsPerMethodPerPhase.Length; i++)
        {
            if (i <= 2)
            {
                OptimumPointsPerMethodPerPhase[i] = TempPointsPerPhase[0] * WeightsPerMethodPerPhase[i];
            }
            if (i > 2 && i <= 4)
            {
                OptimumPointsPerMethodPerPhase[i] = TempPointsPerPhase[1] * WeightsPerMethodPerPhase[i];
            }
            if (i > 4 && i <= 6)
            {
                OptimumPointsPerMethodPerPhase[i] = TempPointsPerPhase[2] * WeightsPerMethodPerPhase[i];
            }
            if (i > 6 && i <= 8)
            {
                OptimumPointsPerMethodPerPhase[i] = TempPointsPerPhase[3] * WeightsPerMethodPerPhase[i];
            }
        }


        for (int i = 0; i < InvestmentDB.instance.s.data.Count; i++)
        {
            AllInvestmentsperMethod[0] += float.Parse(InvestmentDB.instance.s.data[i].method_1);
            AllInvestmentsperMethod[1] += float.Parse(InvestmentDB.instance.s.data[i].method_2);
            AllInvestmentsperMethod[2] += float.Parse(InvestmentDB.instance.s.data[i].method_3);
            AllInvestmentsperMethod[3] += float.Parse(InvestmentDB.instance.s.data[i].method_4);
            AllInvestmentsperMethod[4] += float.Parse(InvestmentDB.instance.s.data[i].method_5);
            AllInvestmentsperMethod[5] += float.Parse(InvestmentDB.instance.s.data[i].method_6);
            AllInvestmentsperMethod[6] += float.Parse(InvestmentDB.instance.s.data[i].method_7);
            AllInvestmentsperMethod[7] += float.Parse(InvestmentDB.instance.s.data[i].method_8);
            AllInvestmentsperMethod[8] += float.Parse(InvestmentDB.instance.s.data[i].method_9);

        }

        for (int i = 0; i < PercentageModifiersPerMethodPerPhase.Length; i++)
        {
            if (AllInvestmentsperMethod[i] != 0)
            {
                PercentageModifiersPerMethodPerPhase[i] = TempPercentageValues[MethodQualities[i]];
                Debug.Log("OptimumPointsPerMethodPerPhase: " + OptimumPointsPerMethodPerPhase[i]);
            }

        }

        OptimumPointsTotal = 0;
        for (int i = 0; i < OptimumPointsPerMethodPerPhase.Length; i++)
        {
            OptimumPointsTotal += OptimumPointsPerMethodPerPhase[i];
        }



        for (int i = 0; i < PointsFromResourceInvestment.Length; i++)
        {

            PointsFromResourceInvestment[i] = OptimumPointsPerMethodPerPhase[i] * PercentageModifiersPerMethodPerPhase[i];
            Debug.Log("PointsFromResourceInvestment: " + PointsFromResourceInvestment[i]);

            PhaseResourceInvestmentQualityData[0] += PointsFromResourceInvestment[i];
            Debug.Log("PhaseResourceInvestmentQualityData[0]: " + PhaseResourceInvestmentQualityData[0]);
            if (i <= 2)
            {
                PhaseResourceInvestmentQualityData[1] += PointsFromResourceInvestment[i];
                Debug.Log("PhaseResourceInvestmentQualityData[1]: " + PhaseResourceInvestmentQualityData[1]);
            }
            if (i > 2 && i <= 4)
            {
                PhaseResourceInvestmentQualityData[2] += PointsFromResourceInvestment[i];
                Debug.Log("PhaseResourceInvestmentQualityData[2]: " + PhaseResourceInvestmentQualityData[2]);
            }
            if (i > 4 && i <= 6)
            {
                PhaseResourceInvestmentQualityData[3] += PointsFromResourceInvestment[i];
                Debug.Log("PhaseResourceInvestmentQualityData[3]: " + PhaseResourceInvestmentQualityData[3]);
            }
            if (i > 6 && i <= 8)
            {
                PhaseResourceInvestmentQualityData[4] += PointsFromResourceInvestment[i];
                Debug.Log("PhaseResourceInvestmentQualityData[4]: " + PhaseResourceInvestmentQualityData[4]);
            }

        }


    }

    void calculatePointsFromPhaseEndEvent()
    {
        // Calculating the modifier for the Phase End Event as Maximum points gained from Phase Investment/4 /10, 4 are the number of phases and 10 is the number of points per phase in the phase end event
        modifier = (OptimumPointsTotal / 4) / 10;

        // Debug log the PhaseResourceInvestmentQualityData[0] 
        Debug.Log("OptimumPointsTotal" + OptimumPointsTotal);

        // Debug log the modifier
        Debug.Log("Modifier " + modifier);

        // debug log the length of PhaseEndEventReachedQualityData.Length
        Debug.Log("PhaseEndEventReachedQualityData.Length" + PhaseEndEventReachedQualityData.Length);
        for (int i = 0; i < GameEndReportDB.instance.objFinalReportPhaseEndData.data.Count; i++)
        {
            Debug.Log(i);
            PhaseEndEventReachedQualityData[i + 1] = int.Parse(GameEndReportDB.instance.objFinalReportPhaseEndData.data[i].score) * modifier;
            PhaseEndEventReachedQualityData[0] += PhaseEndEventReachedQualityData[i + 1];
            Debug.Log("PhaseEndEventReachedQualityData[i]" + PhaseEndEventReachedQualityData[i + 1]);
            Debug.Log("PhaseEndEventReachedQualityData[0]" + PhaseEndEventReachedQualityData[0]);
        }



    }
    void populateFinalReportTableArray()
    {
        int[] FinalReportPhaseData = new int[5] {
            int.Parse(GameEndReportDB.instance.objFinalReportPhaseData.data[0].total_sum),
            int.Parse(GameEndReportDB.instance.objFinalReportPhaseData.data[0].sum_phase_1),
            int.Parse(GameEndReportDB.instance.objFinalReportPhaseData.data[0].sum_phase_2),
            int.Parse(GameEndReportDB.instance.objFinalReportPhaseData.data[0].sum_phase_3),
            int.Parse(GameEndReportDB.instance.objFinalReportPhaseData.data[0].sum_phase_4)
        };

        int[] OptimumQualityData = new int[5] {
            int.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].total_optimum_sum),
            int.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_1),
            int.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_2),
            int.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_3),
            int.Parse(GameEndReportDB.instance.objOptimumQualityData.data[0].sum_phase_4)
        };

        int[] FinalReportPhaseEndData = new int[4];
        for (int i = 1; i < GlobalVariables.PhaseCounter; i++)
        {
            FinalReportPhaseEndData[i - 1] = int.Parse(GameEndReportDB.instance.objFinalReportPhaseEndData.data[i - 1].score);
        }

        int[] OptimumQualityPhaseEndData = new int[4] { 10, 10, 10, 10 };
        // float PhaseEndEventDataModifier = ;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (j == 0)
                {
                    FinalReportTable[i, j, 0] = FinalReportPhaseData[i];
                    FinalReportTable[i, j, 1] = OptimumQualityData[i];
                    FinalReportTable[i, j, 2] = OptimumQualityData[i] - FinalReportPhaseData[i];
                }
                else if (j == 1)
                {
                    if (i > 0)
                    {
                        float PhaseDataVal = FinalReportPhaseData[i];
                        if (FinalReportPhaseData[i] > OptimumQualityData[i])
                        {
                            PhaseDataVal = OptimumQualityData[i];
                        }

                        FinalReportTable[i, j, 0] = PhaseResourceInvestmentQualityData[i];
                        FinalReportTable[i, j, 1] = TempPointsPerPhase[i - 1];
                        FinalReportTable[i, j, 2] = FinalReportTable[i, j, 1] - FinalReportTable[i, j, 0];
                    }
                    else
                    {
                        float SumOptimumQuality = 0.00f;

                        for (int z = 0; z < 4; z++)
                        {
                            SumOptimumQuality += TempPointsPerPhase[z];
                        }
                        TotalReachedValueForQualityMeasurement_PhaseData = PhaseResourceInvestmentQualityData[0];
                        TotalOptimumForQualityMeasurement_PhaseData = SumOptimumQuality;

                        FinalReportTable[i, j, 0] = TotalReachedValueForQualityMeasurement_PhaseData;
                        FinalReportTable[i, j, 1] = SumOptimumQuality;
                        FinalReportTable[i, j, 2] = FinalReportTable[i, j, 1] - FinalReportTable[i, j, 0];
                    }
                }
                else if (j == 2)
                {
                    if (i > 0)
                    {
                        FinalReportTable[i, j, 0] = PhaseEndEventReachedQualityData[i];
                        FinalReportTable[i, j, 1] = OptimumQualityPhaseEndData[i - 1] * modifier;
                        FinalReportTable[i, j, 2] = FinalReportTable[i, j, 1] - FinalReportTable[i, j, 0];
                    }
                    else
                    {
                        float SumOptimumQuality = 0.00f;
                        for (int z = 0; z < 4; z++)
                        {
                            SumOptimumQuality += (OptimumQualityPhaseEndData[z] * modifier);
                        }
                        TotalReachedValueForQualityMeasurement_PhaseEnd = PhaseEndEventReachedQualityData[0];
                        TotalOptimumForQualityMeasurement_PhaseEnd = SumOptimumQuality;

                        FinalReportTable[i, j, 0] = TotalReachedValueForQualityMeasurement_PhaseEnd;
                        FinalReportTable[i, j, 1] = SumOptimumQuality;
                        FinalReportTable[i, j, 2] = FinalReportTable[i, j, 1] - FinalReportTable[i, j, 0];
                    }
                }
                else if (j == 3)
                {
                    FinalReportTable[i, j, 0] = FinalReportTable[i, j - 1, 0] + FinalReportTable[i, j - 2, 0];
                    FinalReportTable[i, j, 1] = FinalReportTable[i, j - 1, 1] + FinalReportTable[i, j - 2, 1];
                    FinalReportTable[i, j, 2] = FinalReportTable[i, j - 1, 2] + FinalReportTable[i, j - 2, 2];
                }
            }
        }

        TotalReachedValueForQualityMeasurement = TotalReachedValueForQualityMeasurement_PhaseData + TotalReachedValueForQualityMeasurement_PhaseEnd;
        TotalOptimumForQualityMeasurement = TotalOptimumForQualityMeasurement_PhaseData + TotalOptimumForQualityMeasurement_PhaseEnd;

    }

    int calculateOverallQuality()
    {
        // TotalOptimumForQualityMeasurement;
        // TotalReachedValueForQualityMeasurement;

        //divide TotalOptimumForQualityMeasurement into 4 parts and check where TotalReachedValueForQualityMeasurement lies
        float OptimumQualityQuarter = TotalOptimumForQualityMeasurement / 10;
        float[] OptimumQualityQuarterArray = new float[10] { OptimumQualityQuarter, OptimumQualityQuarter * 2, OptimumQualityQuarter * 3, OptimumQualityQuarter * 4, OptimumQualityQuarter * 5, OptimumQualityQuarter * 6, OptimumQualityQuarter * 7, OptimumQualityQuarter * 8, OptimumQualityQuarter * 9, OptimumQualityQuarter * 10 };

        int OverallQuality = 0;

        //check where TotalReachedValueForQualityMeasurement lies
        if (TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[0])
        {
            OverallQuality = 0;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[0] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[1])
        {
            OverallQuality = 0;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[1] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[2])
        {
            OverallQuality = 0;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[2] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[3])
        {
            OverallQuality = 1;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[3] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[4])
        {
            OverallQuality = 1;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[4] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[5])
        {
            OverallQuality = 1;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[5] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[6])
        {
            OverallQuality = 2;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[6] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[7])
        {
            OverallQuality = 2;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[7] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[8])
        {
            OverallQuality = 3;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[8] && TotalReachedValueForQualityMeasurement <= OptimumQualityQuarterArray[9])
        {
            OverallQuality = 3;
        }
        else if (TotalReachedValueForQualityMeasurement > OptimumQualityQuarterArray[9])
        {
            OverallQuality = 3;
        }

        return OverallQuality;

    }

    void SetValuesInOverallQualityScreen()
    {
        //setting the values in the overall rating screen
        OverallQualityText.text = GameEndReportDB.instance.objFinalReportData.data[0].game_report.ToString();
    }

    public void onExitGameButton()
    {
        Application.Quit();
    }
}
