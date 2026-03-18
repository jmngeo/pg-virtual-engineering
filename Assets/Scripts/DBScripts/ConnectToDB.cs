using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectToDB : MonoBehaviourPun
{
    private string playerName = "";
    private string playerID = "";
    private string roomName = "";
    private string roleName = "";
    private bool isMaster = false;
    private string updatedBy = "User 1";
    private string createdBy = "User 2";

    //for methods
    private string method1 = "";
    private string method2 = "";
    private string method3 = "";
    private string method4 = "";
    private string method5 = "";
    private string method6 = "";
    private string method7 = "";
    private string method8 = "";
    private string method9 = "";


    public void SendDBValues()
    {
        InitializingValues();

        StartCoroutine(Register());
    }

    public void InitializingValues()
    {
        playerName = PhotonNetwork.NickName;
        playerID = PhotonNetwork.LocalPlayer.UserId;
        isMaster = PhotonNetwork.IsMasterClient;
        roomName = PhotonNetwork.CurrentRoom.Name;
        roleName = GameManager.instance.roleName;

        // Debug.Log("ConnectToDB Logs");
        // Debug.Log("Player Name: " + playerName + " Player ID: " + playerID);
        // Debug.Log("Testing The Role name is : " + GameManager.instance.roleName);
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();

        form.AddField("playerName", playerName);
        form.AddField("playerID", playerID);
        form.AddField("roomName", roomName);
        form.AddField("roleName", roleName);
        form.AddField("isMaster", isMaster.ToString());
        form.AddField("updatedBy", updatedBy);
        form.AddField("createdBy", createdBy);

        //for methods
        form.AddField("method1", method1);
        form.AddField("method2", method2);
        form.AddField("method3", method3);
        form.AddField("method4", method4);
        form.AddField("method5", method5);
        form.AddField("method6", method6);
        form.AddField("method7", method7);
        form.AddField("method8", method8);
        form.AddField("method9", method9);

        using (
            UnityWebRequest www =
                UnityWebRequest.Post(GlobalVariables.connection_url + "register.php", form)
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
}
