// Create a class for Global Variables to called anywhere in the project
// Path: Assets\Scripts\GlobalVariables.cs

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance;

    public static int roundCounter = 0;
    public static bool isInPhaseEndEvent = false;
    public static int PhaseCounter = 1;

    public static string[] MethodNames = new string[] {
        "StakeholderAnalyses",
        "EnvironmentModel",
        "ApplicationsScenario",
        "FunctionsHierarchy",
        "ActivityDiagram",
        "MorphologicalBox",
        "UtilityAnalysis",
        "LogicalArchitecture",
        "FMEA"
        };

    public static string connection_url = "http://uc2.eu5.org/";
    // public static string connection_url = "http://localhost/";

    // static Color BgColor = new Color32(154, 203, 232, 255);
    // static Color BgColor = new Color32(193, 216, 251, 255);
    static Color BgColor = new Color32(255, 255, 255, 255);


    // private void Awake()
    // {
    //     if (instance != null && instance != this)
    //         gameObject.SetActive(false);
    //     else
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

    public static void incrementRoundCounter()
    {
        if (roundCounter < 8)
        {
            roundCounter++;
        }

        else
        {
            //TODO:: show end of game screen
        }

    }

    public static void setBackgroundColorForCanvas()
    {
        BgColor.a = 1;
        GameObject background = GameObject.Find("Canvas").gameObject;
        // changeBackgroundColor[i].Image.sprite = loadSprite();
        background.GetComponent<Image>().color = BgColor;
        background.GetComponent<Image>().sprite = loadSprite("canvasBackground");
    }

    public static void setBackgroundColorForCanvasChild(string screentype)
    {
        BgColor.a = 1;
        GameObject background = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        // changeBackgroundColor[i].Image.sprite = loadSprite();
        background.GetComponent<Image>().color = BgColor;
        background.GetComponent<Image>().sprite = loadSprite(screentype);
    }

    public static void setBackgroundColor(List<GameObject> changeBackgroundColor, string screentype)
    {

        // BgColor.a = 0.75f;
        BgColor.a = 1;

        //for every object in the array, change the background color
        for (int i = 0; i < changeBackgroundColor.Count; i++)
        {
            //check if the Length returns length of the array
            changeBackgroundColor[i].GetComponent<Image>().sprite = loadSprite(screentype);
            changeBackgroundColor[i].GetComponent<Image>().color = BgColor;
        }
    }

    public static Sprite loadSprite(string screentype)
    {
        // if (screentype == "NoBackground")
        //     return Resources.Load<Sprite>("background_fraunhofer");
        if (screentype == "canvasBackground")
            return Resources.Load<Sprite>("Fraunhofer_plain");
        if (screentype == "PhaseEndBackground")
            return Resources.Load<Sprite>("PhaseEnd");
        else if (screentype == "introBackground")
            return Resources.Load<Sprite>("intro");
        // else if (screentype == "rolesBackground")
        //     return Resources.Load<Sprite>("roles");
        else if (screentype == "investmentsBackground")
            return Resources.Load<Sprite>("Investments");
        else if (screentype == "descriptionsBackground")
            return Resources.Load<Sprite>("descriptions");
        else
            return Resources.Load<Sprite>("background_fraunhofer");
    }
}