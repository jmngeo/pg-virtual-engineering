using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;


public class InvestmentDB : MonoBehaviourPun
{
    private string playerName = "";
    private string playerID = "";

    //for methods
    private string method1 = "method_1";
    private string method2 = "method_2";
    private string method3 = "method_3";
    private string method4 = "method_4";
    private string method5 = "method_5";
    private string method6 = "method_6";
    private string method7 = "method_7";
    private string method8 = "method_8";
    private string method9 = "method_9";


    public int method1_Resources = 0;
    public int method2_Resources = 0;
    public int method3_Resources = 0;
    public int method4_Resources = 0;
    public int method5_Resources = 0;
    public int method6_Resources = 0;
    public int method7_Resources = 0;
    public int method8_Resources = 0;
    public int method9_Resources = 0;


    private string roomName = "";

    public static InvestmentDB instance;

    public investment_values s;
    // public player_values p;G


    public static threshold_quality_values threshold_values;

    public static PlayerResources player_resource_data;

    public InvestmentData investment_values_data;
    public previous_total_investment_values previous_investment_values_data;

    void Start()
    {
        GetDBValues();
        GetPreviousTotalInvestmentValues();
        // setTotalInvestmentValues();
        fetchThresholdQualityValues();
        fetchPlayerResourcesPerWeek();
    }

    private void Awake()
    {
        instance = this;
    }

    public void GetDBValues()
    {
        playerID = PhotonNetwork.LocalPlayer.UserId;
        playerName = PhotonNetwork.NickName;
        string uri = GlobalVariables.connection_url + "investment.php" + "?playerName=" + playerName + "\"&playerID=\"" + playerID + "\"&method1=\"" + method1 + "\"&method2=\"" + method2 + "\"&method3=\"" + method3 + "\"&method4=\"" + method4 + "\"&method5=\"" + method5 + "\"&method6=\"" + method6 + "\"&method7=\"" + method7 + "\"&method8=\"" + method8 + "\"&method9=\"" + method9;

        StartCoroutine(GetRequest(uri));
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);


                    //parse the string to int
                    Debug.Log("Investmetn Values:" + webRequest.downloadHandler.text);
                    investment_values_data = CreateFromJSON_InvestmentData(webRequest.downloadHandler.text);


                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }


    public void GetPreviousTotalInvestmentValues()
    {
        roomName = PhotonNetwork.CurrentRoom.Name;
        string uri = GlobalVariables.connection_url + "fetchTotalPreviousInvestments.php" + "?roomName=" + roomName;
        StartCoroutine(GetPreviousTotalInvestmentValuesFromDB(uri));
    }
    IEnumerator GetPreviousTotalInvestmentValuesFromDB(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);


                    //parse the string to int
                    Debug.Log("Previous Total Investment Values: " + webRequest.downloadHandler.text);
                    previous_investment_values_data = CreateFromJSON_PreviousInvestmentData(webRequest.downloadHandler.text);

                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }


    public void SendFinalInvestmentsPhase1(string method1, string method2, string method3)
    {
        StartCoroutine(updateDataPhase1(method1, method2, method3, GlobalVariables.PhaseCounter));
    }

    public void SendFinalInvestmentsPhase2(string method1, string method2, string method3, string method4, string method5)
    {
        StartCoroutine(updateDataPhase2(method1, method2, method3, method4, method5, GlobalVariables.PhaseCounter));
    }

    public void SendFinalInvestmentsPhase3(string method1, string method2, string method3, string method4, string method5, string method6, string method7)
    {
        StartCoroutine(updateDataPhase3(method1, method2, method3, method4, method5, method6, method7, GlobalVariables.PhaseCounter));
    }

    public void SendFinalInvestmentsPhase4(string method1, string method2, string method3, string method4, string method5, string method6, string method7, string method8, string method9)
    {
        StartCoroutine(updateDataPhase4(method1, method2, method3, method4, method5, method6, method7, method8, method9, GlobalVariables.PhaseCounter));
    }


    IEnumerator updateDataPhase1(string method1, string method2, string method3, int PhaseCountVal)
    {
        WWWForm form = new WWWForm();


        form.AddField("playerID", playerID);
        form.AddField("method_1", method1);
        form.AddField("method_2", method2);
        form.AddField("method_3", method3);
        form.AddField("PhaseCount", PhaseCountVal);

        using (
            UnityWebRequest www =
                UnityWebRequest.Post(GlobalVariables.connection_url + "finalPhaseValuesToDB.php", form)
        )

        {
            yield return www.SendWebRequest();
            // isNetworkError always comes true, else is to check for logs

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var phpoutput = www.downloadHandler.text;
                //strpos($phpoutput,"Player exists");
                UnityEngine.Debug.Log(phpoutput);
            }

            //for removing memory leak errors
            www.Dispose();
        }
    }

    IEnumerator updateDataPhase2(string method1, string method2, string method3, string method4, string method5, int PhaseCountVal)
    {
        WWWForm form = new WWWForm();


        form.AddField("playerID", playerID);
        form.AddField("method_1", method1);
        form.AddField("method_2", method2);
        form.AddField("method_3", method3);
        form.AddField("method_4", method4);
        form.AddField("method_5", method5);
        form.AddField("PhaseCount", PhaseCountVal);

        using (
            UnityWebRequest www =
                UnityWebRequest.Post(GlobalVariables.connection_url + "finalPhaseValuesToDB.php", form)
        )

        {
            yield return www.SendWebRequest();
            // isNetworkError always comes true, else is to check for logs

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var phpoutput = www.downloadHandler.text;
                //strpos($phpoutput,"Player exists");
                UnityEngine.Debug.Log(phpoutput);
            }

            //for removing memory leak errors
            www.Dispose();
        }
    }

    IEnumerator updateDataPhase3(string method1, string method2, string method3, string method4, string method5, string method6, string method7, int PhaseCountVal)
    {
        WWWForm form = new WWWForm();


        form.AddField("playerID", playerID);
        form.AddField("method_1", method1);
        form.AddField("method_2", method2);
        form.AddField("method_3", method3);
        form.AddField("method_4", method4);
        form.AddField("method_5", method5);
        form.AddField("method_6", method6);
        form.AddField("method_7", method7);
        form.AddField("PhaseCount", PhaseCountVal);

        using (
            UnityWebRequest www =
                UnityWebRequest.Post(GlobalVariables.connection_url + "finalPhaseValuesToDB.php", form)
        )

        {
            yield return www.SendWebRequest();
            // isNetworkError always comes true, else is to check for logs

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var phpoutput = www.downloadHandler.text;
                //strpos($phpoutput,"Player exists");
                UnityEngine.Debug.Log(phpoutput);
            }

            //for removing memory leak errors
            www.Dispose();
        }
    }

    IEnumerator updateDataPhase4(string method1, string method2, string method3, string method4, string method5, string method6, string method7, string method8, string method9, int PhaseCountVal)
    {
        WWWForm form = new WWWForm();


        form.AddField("playerID", playerID);
        form.AddField("method_1", method1);
        form.AddField("method_2", method2);
        form.AddField("method_3", method3);
        form.AddField("method_4", method4);
        form.AddField("method_5", method5);
        form.AddField("method_6", method6);
        form.AddField("method_7", method7);
        form.AddField("method_8", method8);
        form.AddField("method_9", method9);
        form.AddField("PhaseCount", PhaseCountVal);

        using (
            UnityWebRequest www =
                UnityWebRequest.Post(GlobalVariables.connection_url + "finalPhaseValuesToDB.php", form)
        )

        {
            yield return www.SendWebRequest();
            // isNetworkError always comes true, else is to check for logs

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var phpoutput = www.downloadHandler.text;
                //strpos($phpoutput,"Player exists");
                UnityEngine.Debug.Log(phpoutput);
            }

            //for removing memory leak errors
            www.Dispose();
        }
    }


    public void setTotalInvestmentValues()
    {
        roomName = PhotonNetwork.CurrentRoom.Name;
        string uri = GlobalVariables.connection_url + "fetchInvestmentValuesforAllPlayers.php" + "?roomName=" + roomName + "&method_1=" + method1 + "&method_2=" + method2 + "&method_3=" + method3;
        StartCoroutine(fetchInvestmentValuesforAllPlayers(uri));
    }
    IEnumerator fetchInvestmentValuesforAllPlayers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    // string[] words = webRequest.downloadHandler.text.Split(';');
                    string words = webRequest.downloadHandler.text;

                    //process the output data from DB into json and then, update the values in the unity screen
                    s = CreateFromJSON(words);
                    InvestmentManager.instance.UpdateTotalInvestmentValuesInUnityScreen(s);


                    // Debug.Log(CreateFromJSON(words[1]));

                    // JSON json_player_data = JSON.ParseString(words[1]);
                    // Debug.Log(json_player_data.ToString());

                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }

    //to fetch threshold values from DB.
    public void fetchThresholdQualityValues()
    {
        string uri = GlobalVariables.connection_url + "fetchThresholdQualityValues.php";
        StartCoroutine(fetchThresholdQualityValuesFromDB(uri));
    }
    IEnumerator fetchThresholdQualityValuesFromDB(string uri)
    {
        // Debug.Log("Starting coroutine fetchThresholdQualityValuesFromDB");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    threshold_values = CreateFromJSON_threshold_quality_values(webRequest.downloadHandler.text);
                    // Debug.Log("Threshold Quality Values: " + threshold_values.data[0].Role);

                    // string[] words = webRequest.downloadHandler.text.Split(';');

                    //process the output data from DB into json and then, update the values in the unity screen
                    break;

            }
            webRequest.Dispose();
        }

    }


    //to fetch threshold values from DB.
    public void fetchPlayerResourcesPerWeek()
    {
        string uri = GlobalVariables.connection_url + "fetchPlayerAvailableResourcesPerWeek.php";
        StartCoroutine(fetchPlayerResourcesPerWeekFromDB(uri));
    }
    IEnumerator fetchPlayerResourcesPerWeekFromDB(string uri)
    {
        // Debug.Log("Starting coroutine fetchThresholdQualityValuesFromDB");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    player_resource_data = CreateFromJSON_PlayerResources(webRequest.downloadHandler.text);
                    Debug.Log("Resource data: " + webRequest.downloadHandler.text);

                    // string[] words = webRequest.downloadHandler.text.Split(';');

                    //process the output data from DB into json and then, update the values in the unity screen
                    break;

            }
            webRequest.Dispose();
        }

    }


    public static investment_values CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<investment_values>(jsonString);
    }

    // public static player_values CreateFromJSON_player(string jsonString)
    // {
    //     return JsonUtility.FromJson<player_values>(jsonString);
    // }

    public static threshold_quality_values CreateFromJSON_threshold_quality_values(string jsonString)
    {
        return JsonUtility.FromJson<threshold_quality_values>(jsonString);
    }

    public static PlayerResources CreateFromJSON_PlayerResources(string jsonString)
    {
        return JsonUtility.FromJson<PlayerResources>(jsonString);
    }

    public static InvestmentData CreateFromJSON_InvestmentData(string jsonString)
    {
        return JsonUtility.FromJson<InvestmentData>(jsonString);
    }

    public static previous_total_investment_values CreateFromJSON_PreviousInvestmentData(string jsonString)
    {
        return JsonUtility.FromJson<previous_total_investment_values>(jsonString);
    }


}



[System.Serializable]
public class investment_values
{
    public List<method_values> data;

    [System.Serializable]
    public class method_values
    {
        public string method_1;
        public string method_2;
        public string method_3;
        public string method_4;
        public string method_5;
        public string method_6;
        public string method_7;
        public string method_8;
        public string method_9;
        public string roomName;
        public string playerRole;
        public string playername;
        public string playerID;
    }
}



// [System.Serializable]
// public class player_values
// {
//     public List<player_info> data_players;
// }

// [System.Serializable]
// public class player_info
// {
//     public string playerID;
//     public string playerRole;
// }


[System.Serializable]
public class threshold_quality_values
{
    public List<threshold_quality> data;

    [System.Serializable]
    public class threshold_quality
    {
        public string Role;
        public string StakeholderAnalyses_High;
        public string StakeholderAnalyses_Medium;
        public string StakeholderAnalyses_Low;
        public string EnvironmentModel_High;
        public string EnvironmentModel_Medium;
        public string EnvironmentModel_Low;
        public string ApplicationsScenario_High;
        public string ApplicationsScenario_Medium;
        public string ApplicationsScenario_Low;
        public string FunctionsHierarchy_High;
        public string FunctionsHierarchy_Medium;
        public string FunctionsHierarchy_Low;
        public string ActivityDiagram_High;
        public string ActivityDiagram_Medium;
        public string ActivityDiagram_Low;
        public string MorphologicalBox_High;
        public string MorphologicalBox_Medium;
        public string MorphologicalBox_Low;
        public string UtilityAnalysis_High;
        public string UtilityAnalysis_Medium;
        public string UtilityAnalysis_Low;
        public string LogicalArchitecture_High;
        public string LogicalArchitecture_Medium;
        public string LogicalArchitecture_Low;
        public string FMEA_High;
        public string FMEA_Medium;
        public string FMEA_Low;
    }
}


[System.Serializable]
public class PlayerResources
{
    public List<player_resources_values> data;

    [System.Serializable]
    public class player_resources_values
    {
        public string playerRole;
        public string resources;
    }
}


[System.Serializable]
public class InvestmentData
{
    public List<InvestmentValues> data;

    [System.Serializable]
    public class InvestmentValues
    {
        public string method_1;
        public string method_2;
        public string method_3;
        public string method_4;
        public string method_5;
        public string method_6;
        public string method_7;
        public string method_8;
        public string method_9;
        public string room_id;
        public string player_id;
    }
}


[System.Serializable]
public class previous_total_investment_values
{
    public List<previous_method_values> data;

    [System.Serializable]
    public class previous_method_values
    {
        public string method_1;
        public string method_2;
        public string method_3;
        public string method_4;
        public string method_5;
        public string method_6;
        public string method_7;
        public string method_8;
        public string method_9;
    }
}


