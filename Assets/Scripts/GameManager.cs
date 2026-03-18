using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviourPun
{
    [Header("Players")]
    public string playerPrefabPath;
    public PlayerController[] players;//for looking up all the players 
    public Transform[] spawnPoint;
    public float respawnTime;
    private int playersInGame;

    [Header("Role Selection Intro")]   //nidhiadded
    public GameObject RoleSelectionIntro;  //nidhiadded

    [Header("Role Selection Screen")]
    public GameObject RoleSelectionScreen;
    public List<GameObject> RoleButtons = new List<GameObject>();

    [Header("Role Description Screen")]
    public List<GameObject> RoleDescriptions = new List<GameObject>();
    public GameObject RoleDescription;

    [Header("Role Confirmation Screen")]
    public GameObject RoleConfirmationScreen;


    public string roleName = "";

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.setBackgroundColorForCanvas();
        // GlobalVariables.setBackgroundColorForCanvasChild("rolesBackground");

        // players = new PlayerController[PhotonNetwork.PlayerList.Length];
        // photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void ImInGame()
    {
        //add one to playersInGame variable for every player who joins the game
        playersInGame++;

        //spawn player depend on the list of player that joined the lobby room
        if (playersInGame == PhotonNetwork.PlayerList.Length)
            SpawnPlayer();
    }

    void SpawnPlayer()
    {
        //spawn player randomly in spawnPoint list position
        GameObject playerObject = PhotonNetwork.Instantiate(playerPrefabPath, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);

        //initialize players
        playerObject.GetComponent<PhotonView>().RPC("Initialized", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    public void onRoleButtonPress()
    {
        //get the name of the role button that was pressed 
        roleName = EventSystem.current.currentSelectedGameObject.name;
        roleName = roleName.Remove(0, 3);
        //GameObject RoleButtonClicked = RoleDescriptions.Find(x=>x.name==roleName);
        //SetScreen(RoleButtonClicked);

        SetScreen(RoleDescriptions.Find(x => x.name == roleName));
        RoleDescription.SetActive(true);
    }

    //swap the current Screen
    public void SetScreen(GameObject screen) //TODO:: iterate with GameObject<>
    {
        //for every RoleDescriptions, set it to false
        foreach (GameObject roleDescription in RoleDescriptions)
        {
            roleDescription.SetActive(false);
        }
        RoleSelectionIntro.SetActive(false);  //nidhiadded
        RoleSelectionScreen.SetActive(false);
        RoleDescription.SetActive(false);
        RoleConfirmationScreen.SetActive(false);
        //activate the requested Screen
        screen.SetActive(true);
    }

    //nidhiadded
    public void onRoleSelectionIntroButtonPress()
    {
        SetScreen(RoleSelectionScreen);
    }
    public void onMethodDescriptionNextButtonPress()
    {
        SetScreen(RoleConfirmationScreen);
    }
    public void onBacktoRoleSelectionScreenButtonPress()
    {
        SetScreen(RoleSelectionScreen);
    }

    public void onConfirmRoleButtonPress()
    {
        //update Role Selection screen buttons when someone has confirmed a role 
        Button button = RoleButtons.Find(x => x.name == ("Btn" + roleName)).GetComponent<Button>();
        // Debug.Log("The button name is " + button.name);

        photonView.RPC("UpdateRoleSelectionScreen", RpcTarget.All, button.name);

        SceneManager.LoadScene("phase" + GlobalVariables.PhaseCounter + "-" + "Introduction");
    }

    [PunRPC]
    void UpdateRoleSelectionScreen(string buttonName)
    {
        RoleButtons.Find(x => x.name == (buttonName)).GetComponent<Button>().interactable = false;
    }

}
