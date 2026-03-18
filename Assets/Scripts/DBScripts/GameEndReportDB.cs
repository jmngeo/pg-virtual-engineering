using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;


public class GameEndReportDB : MonoBehaviourPun
{
    public FinalReportPhaseData objFinalReportPhaseData;
    public FinalReportPhaseEndData objFinalReportPhaseEndData;
    public OptimumQualityData objOptimumQualityData;
    public FinalReportData objFinalReportData;

    public static GameEndReportDB instance;

    // Start is called before the first frame update
    void Start()
    {
        fnFinalReportPhaseData();
        fnFinalReportPhaseEndData();
        fnOptimumQualityData();

    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fnFinalReportPhaseData()
    {
        string uri = GlobalVariables.connection_url + "fetchFinalReportPhaseData.php" + "?room_id=" + PhotonNetwork.CurrentRoom.Name;
        StartCoroutine(GetFinalreportPhaseData(uri));
    }
    IEnumerator GetFinalreportPhaseData(string uri)
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
                    Debug.Log("GetFinalreportPhaseData:" + webRequest.downloadHandler.text);
                    objFinalReportPhaseData = CreateFromJSON_FinalReportPhaseData(webRequest.downloadHandler.text);


                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }
    public static FinalReportPhaseData CreateFromJSON_FinalReportPhaseData(string jsonString)
    {
        return JsonUtility.FromJson<FinalReportPhaseData>(jsonString);
    }

    // -------------------

    public void fnFinalReportPhaseEndData()
    {

        string uri = GlobalVariables.connection_url + "fetchFinalReportPhaseEndData.php" + "?room_id=" + PhotonNetwork.CurrentRoom.Name;

        StartCoroutine(GetFinalReportPhaseEndData(uri));
    }
    IEnumerator GetFinalReportPhaseEndData(string uri)
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
                    Debug.Log("GetFinalReportPhaseEndData:" + webRequest.downloadHandler.text);
                    objFinalReportPhaseEndData = CreateFromJSON_FinalReportPhaseEndData(webRequest.downloadHandler.text);


                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }
    public static FinalReportPhaseEndData CreateFromJSON_FinalReportPhaseEndData(string jsonString)
    {
        return JsonUtility.FromJson<FinalReportPhaseEndData>(jsonString);
    }

    // -------------------

    public void fnOptimumQualityData()
    {

        string uri = GlobalVariables.connection_url + "fetchOptimumQualityData.php";

        StartCoroutine(GetOptimumQualityData(uri));
    }
    IEnumerator GetOptimumQualityData(string uri)
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
                    Debug.Log("GetOptimumQualityData:" + webRequest.downloadHandler.text);
                    objOptimumQualityData = CreateFromJSON_OptimumQualityData(webRequest.downloadHandler.text);


                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }
    public static OptimumQualityData CreateFromJSON_OptimumQualityData(string jsonString)
    {
        return JsonUtility.FromJson<OptimumQualityData>(jsonString);
    }
    // -------------------


    public void fnFinalReportData(string quality)
    {
        string uri = GlobalVariables.connection_url + "fetchFinalReport.php" + "?quality=" + quality;
        StartCoroutine(GetFinalReportData(uri));
    }
    IEnumerator GetFinalReportData(string uri)
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
                    Debug.Log("GetFinalReportData:" + webRequest.downloadHandler.text);
                    objFinalReportData = CreateFromJSON_FinalReportData(webRequest.downloadHandler.text);

                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }
    public static FinalReportData CreateFromJSON_FinalReportData(string jsonString)
    {
        return JsonUtility.FromJson<FinalReportData>(jsonString);
    }

}

[System.Serializable]
public class FinalReportPhaseData
{
    public List<FinalData> data;

    [System.Serializable]
    public class FinalData
    {
        public string total_sum;
        public string sum_phase_1;
        public string sum_phase_2;
        public string sum_phase_3;
        public string sum_phase_4;

    }
}


[System.Serializable]
public class FinalReportPhaseEndData
{
    public List<FinalData> data;

    [System.Serializable]
    public class FinalData
    {
        public string score;
        public string phase_id;

    }
}


[System.Serializable]
public class OptimumQualityData
{
    public List<FinalData> data;

    [System.Serializable]
    public class FinalData
    {
        public string total_optimum_sum;
        public string sum_phase_1;
        public string sum_phase_2;
        public string sum_phase_3;
        public string sum_phase_4;

    }
}


[System.Serializable]
public class FinalReportData
{
    public List<FinalData> data;

    [System.Serializable]
    public class FinalData
    {
        public string game_report;
    }
}

