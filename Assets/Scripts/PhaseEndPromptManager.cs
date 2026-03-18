using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhaseEndPromptManager : MonoBehaviourPun
{
    [Header("Screens")]
    public GameObject NormalPromptScreen;
    public GameObject FinalRoundPromptScreen;

    [Header("Buttons")]
    public GameObject PromptScreenButtonsContainer;
    public GameObject FinalRoundPromptScreenButtonsContainer;


    void Start()
    {
        GlobalVariables.setBackgroundColorForCanvas();

        if (GlobalVariables.roundCounter == 8)
        {
            NormalPromptScreen.SetActive(false);
            FinalRoundPromptScreen.SetActive(true);
        }
        else
            FinalRoundPromptScreen.SetActive(false);

        //setting background color
        // GlobalVariables.setBackgroundColor(new List<GameObject>() { NormalPromptScreen, FinalRoundPromptScreen }, "NoBackground");

        //if isMaster, show buttons
        if (PhotonNetwork.IsMasterClient)
        {
            PromptScreenButtonsContainer.SetActive(true);
            FinalRoundPromptScreenButtonsContainer.SetActive(true);
        }
        else
        {
            PromptScreenButtonsContainer.SetActive(false);
            FinalRoundPromptScreenButtonsContainer.SetActive(false);
        }
    }

    public void onReinvestButtonClick()
    {
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "phase" + GlobalVariables.PhaseCounter + "-" + "Investments");
    }

    public void onPhaseEndEventButtonClick()
    {
        NetworkManager.instance.photonView.RPC("checkTargetAndChangeScene", RpcTarget.All, "phase" + GlobalVariables.PhaseCounter + "-" + "PhaseEndEvent", "phase" + GlobalVariables.PhaseCounter + "-" + "DevelopingMethods");
    }
}
