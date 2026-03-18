using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;


public class PhaseEndEventDB : MonoBehaviourPun
{
    private string playerName = "";
    private string playerID = "";
    // private string roomName = "";
    // private string roleName = "";
    // private bool isMaster = false;

    // //for methods
    // private string method1 = "";
    // private string method2 = "";
    // private string method3 = "";
    // private string method4 = "";
    // private string method5 = "";
    // private string method6 = "";
    // private string method7 = "";
    // private string method8 = "";
    // private string method9 = "";

    public static PhaseEndEventDB instance;


    void Start()
    {
        FetchQuestionnaire();
        FetchPhaseEndReports();
    }

    private void Awake()
    {
        instance = this;
    }

    public void SendPhaseEndScoreValues()
    {
        StartCoroutine(SendPhaseEndScore(PhaseEndEventManager.instance.score, GlobalVariables.PhaseCounter, PhotonNetwork.CurrentRoom.Name));
    }

    public void FetchQuestionnaire()
    {
        playerID = PhotonNetwork.LocalPlayer.UserId;
        playerName = PhotonNetwork.NickName;
        string uri = GlobalVariables.connection_url + "questionnaire.php" + "?PhaseCount=" + GlobalVariables.PhaseCounter;

        StartCoroutine(FetchQuestionnaireFromDB(uri));
    }
    IEnumerator FetchQuestionnaireFromDB(string uri)
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

                    string words = webRequest.downloadHandler.text;
                    Debug.Log(words);

                    Questionnaire questions = CreateFromJSON_Questionnaire(words);
                    PhaseEndEventManager.q = questions;

                    for (int i = 0; i < questions.data.Count; i++)
                    {
                        Debug.Log("question: " + questions.data[i].question + " | answer: " + questions.data[i].answer);
                    }

                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }

    IEnumerator SendPhaseEndScore(int score, int phase_id, string room_id)
    {
        WWWForm form = new WWWForm();

        form.AddField("score", score);
        form.AddField("phase_id", phase_id);
        form.AddField("room_id", room_id);

        using (
            UnityWebRequest www =
                UnityWebRequest.Post(GlobalVariables.connection_url + "phase_end_event_score.php", form)
        )

        {
            yield return www.SendWebRequest();


            // isNetworkError always comes true, else is to check for logs
            UnityEngine.Debug.Log("1");
            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                UnityEngine.Debug.Log("2");
                Debug.Log(www.error);
            }
            else if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                UnityEngine.Debug.Log("3");
                Debug.Log(www.error);
            }
            else
            {
                UnityEngine.Debug.Log("4");
                var phpoutput = www.downloadHandler.text;

                //strpos($phpoutput,"Player exists");
                UnityEngine.Debug.Log(phpoutput);

                // if (phpoutput.IndexOf("Player exists") == -1)
                // {
                //     UnityEngine.Debug.Log("User created succesfully!");
                //     UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                // }
                //UnityEngine.Debug.Log(www.downloadHandler.text);
            }

            //for removing memory leak errors
            www.Dispose();
        }


    }

    public static Questionnaire CreateFromJSON_Questionnaire(string jsonString)
    {
        return JsonUtility.FromJson<Questionnaire>(jsonString);
    }



    public void FetchPhaseEndReports()
    {

        string m1 = "";
        string m2 = "";
        string m3 = "";
        int q1 = 0;
        int q2 = 0;
        int q3 = 0;


        if (GlobalVariables.PhaseCounter == 1)
        {
            m1 = GlobalVariables.MethodNames[0];
            m2 = GlobalVariables.MethodNames[1];
            m3 = GlobalVariables.MethodNames[2];

            q1 = InvestmentManager.Method1Quality;
            q2 = InvestmentManager.Method2Quality;
            q3 = InvestmentManager.Method3Quality;
        }
        else if (GlobalVariables.PhaseCounter == 2)
        {
            m1 = GlobalVariables.MethodNames[3];
            m2 = GlobalVariables.MethodNames[4];

            q1 = InvestmentManager.Method4Quality;
            q2 = InvestmentManager.Method5Quality;
        }
        else if (GlobalVariables.PhaseCounter == 3)
        {
            m1 = GlobalVariables.MethodNames[5];
            m2 = GlobalVariables.MethodNames[6];

            q1 = InvestmentManager.Method6Quality;
            q2 = InvestmentManager.Method7Quality;
        }
        else if (GlobalVariables.PhaseCounter == 4)
        {
            m1 = GlobalVariables.MethodNames[7];
            m2 = GlobalVariables.MethodNames[8];

            q1 = InvestmentManager.Method8Quality;
            q2 = InvestmentManager.Method9Quality;

        }

        string uri = "";
        if (GlobalVariables.PhaseCounter == 1)
        {
            uri = GlobalVariables.connection_url + "fetchPhaseEndReport.php?method1=" + m1 + "&method2=" + m2 + "&method3=" + m3 + "&method1Quality=" + q1 + "&method2Quality=" + q2 + "&method3Quality=" + q3 + "&PhaseCount=" + GlobalVariables.PhaseCounter;
        }
        else
        {
            uri = GlobalVariables.connection_url + "fetchPhaseEndReport.php?method1=" + m1 + "&method2=" + m2 + "&method1Quality=" + q1 + "&method2Quality=" + q2 + "&PhaseCount=" + GlobalVariables.PhaseCounter;
        }


        StartCoroutine(FetchPhaseEndReportsFromDB(uri));
    }
    IEnumerator FetchPhaseEndReportsFromDB(string uri)
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

                    string words = webRequest.downloadHandler.text;
                    Debug.Log(words);

                    PhaseEndReports report = CreateFromJSON_PhaseEndReports(words);
                    PhaseEndEventManager.r = report;

                    Debug.Log("q1: " + report.data[0].quality);
                    Debug.Log("q2: " + report.data[1].quality);
                    break;
            }

            //for removing memory leak errors
            webRequest.Dispose();
        }
    }

    public static PhaseEndReports CreateFromJSON_PhaseEndReports(string jsonString)
    {
        return JsonUtility.FromJson<PhaseEndReports>(jsonString);
    }
}


[System.Serializable]
public class Questionnaire
{
    public List<Questions> data;

    [System.Serializable]
    public class Questions
    {
        public string id;
        public string question;
        public string option_1;
        public string option_2;
        public string option_3;
        public string option_4;
        public string isBinary;
        public string quality;
        public string answer;
        public string method;
        public string phase;
    }
}

[System.Serializable]
public class PhaseEndReports
{
    public List<ReportValues> data;

    [System.Serializable]
    public class ReportValues
    {
        public string id;
        public string phase_name;
        public string method_name;
        public string quality;
        public string method_report;
    }
}

