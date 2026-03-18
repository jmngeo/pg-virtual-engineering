using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    [Header("Screens")]
    public GameObject LoadingGameText;
    public GameObject mainScreen;
    public GameObject createRoomScreen;
    public GameObject lobbyScreen;
    public GameObject lobbyBrowserScreen;

    [Header("Main Screen")]
    public Button createRoomButton;
    public Button findRoomButton;

    [Header("Lobby")]
    public TextMeshProUGUI playerListText;
    public TextMeshProUGUI roomInfoText;
    public Button startGameButton;

    [Header("Lobby Browser")]
    public RectTransform roomListContainer;
    public GameObject roomButtonPrefabs;

    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomList = new List<RoomInfo>();

    // Start is called before the first frame update
    void Start()
    {
        //disable the menu buttons at the start of the game
        createRoomButton.interactable = false;
        findRoomButton.interactable = false;
        //enable the cursor
        Cursor.lockState = CursorLockMode.None;

        //if we are in game or not??
        if (PhotonNetwork.InRoom)
        {
            //make the room visible again
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }

    //swap the current Screen
    public void SetScreen(GameObject screen)
    {
        //disable all screens first
        LoadingGameText.SetActive(false);
        mainScreen.SetActive(false);
        lobbyBrowserScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        //activate the requested Screen
        screen.SetActive(true);

        if (screen == lobbyBrowserScreen)
            UpdateLobbyBrowserUI();
    }

    public void OnBackToMainScreen()
    {
        SetScreen(mainScreen);
    }

    public void OnPlayerNameChanged(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text;
    }

    public override void OnConnectedToMaster()
    {
        SetScreen(mainScreen);

        //enable the menu buttons when we connected to master
        createRoomButton.interactable = true;
        findRoomButton.interactable = true;
    }

    public void OnCreateRoomButton()
    {
        SetScreen(createRoomScreen);
    }

    public void onFindRoomButton()
    {
        SetScreen(lobbyBrowserScreen);
    }

    public void onExitGameButton()
    {
        Application.Quit();
    }

    public void OnCreateButton(TMP_InputField roomNameInput)
    {
        string uniqueRoomName = roomNameInput.text + System.Guid.NewGuid().ToString(); //Adding a GUID to the room name to make it unique
        NetworkManager.instance.CreateRoom(uniqueRoomName);
    }

    public override void OnJoinedRoom()
    {
        SetScreen(lobbyScreen);
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();
    }

    //work here for the lobby
    [PunRPC]
    void UpdateLobbyUI()
    {
        //enable start button just for the player who created the room
        startGameButton.interactable = PhotonNetwork.IsMasterClient;

        //display all of the players
        playerListText.text = "";

        //loop through all the players
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient)
                playerListText.text += player.NickName + " [Master]" + "\n";
            else
                playerListText.text += player.NickName + "\n";

            // Debug.Log("Player " + player.NickName + "'s User ID: " + player.UserId + "\nRoom Name : " + PhotonNetwork.CurrentRoom.Name);
        }

        //set room info text
        //roomInfoText.text = "<b>Room Name </b> \n" + PhotonNetwork.CurrentRoom.Name;
        // roomInfoText.text = PhotonNetwork.CurrentRoom.Name;
        roomInfoText.text = PhotonNetwork.CurrentRoom.Name.Substring(0, PhotonNetwork.CurrentRoom.Name.Length - 36); // to hide the GUID from the room name

        //Debug.Log ( PhotonNetwork.playerName + " : " + PhotonNetwork.player.name + " : " + PhotonNetwork.player.ID + " : " + PhotonNetwork.player.isLocal );
        // Debug.Log("Player List: " + PhotonNetwork.PlayerList.ToString());
        // Debug.Log("Actor number: " + PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public void OnStartGameButton()
    {
        //invisible the room which client master going to start it
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        //Tell everyone to load to the Game scene (important: The scene name is GameIntro1
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "GameIntroduction");
    }

    public void OnLeaveLobbyButton()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen);
    }


    GameObject CreateRoomButton()
    {
        GameObject buttonObject = Instantiate(roomButtonPrefabs, roomListContainer.transform);
        roomButtons.Add(buttonObject);
        return buttonObject;
    }

    void UpdateLobbyBrowserUI()
    {
        //disable all rooms Buttons
        foreach (GameObject button in roomButtons)
        {
            button.SetActive(false);
        }

        //Display all current rooms in the master Client
        for (int x = 0; x < roomList.Count; x++)
        {
            //Get or create the button object
            GameObject button = x >= roomButtons.Count ? CreateRoomButton() : roomButtons[x];

            button.SetActive(true);
            //set the room name and player count text
            //button.transform.Find("Room name Text").GetComponent<TextMeshProUGUI>().text = roomList[x].Name;
            button.transform.Find("Room name Text").GetComponent<TextMeshProUGUI>().text = roomList[x].Name.Substring(0, roomList[x].Name.Length - 36); // to hide the GUID from the room name
            button.transform.Find("Player Counter Text").GetComponent<TextMeshProUGUI>().text = roomList[x].PlayerCount + " / " + roomList[x].MaxPlayers;



            //set the button when we click on them
            //Button buttoncomp = button.GetComponent<Button>(); OR, we edit this to add Join Room button
            Button buttoncomp = button.transform.Find("Join Room Button").GetComponent<Button>();
            string roomName = roomList[x].Name;

            //adding a condition for Join Room button interactibility on reaching max players in room
            if (roomList[x].PlayerCount == roomList[x].MaxPlayers)
            {
                buttoncomp.interactable = false;
                GameObject.Find("Join Room Button").GetComponentInChildren<TextMeshProUGUI>().text = "Room Full";
            }

            buttoncomp.onClick.RemoveAllListeners();
            buttoncomp.onClick.AddListener(() => { OnJoinRoomButton(roomName); });
        }
    }

    public void OnRefreshButton()
    {
        UpdateLobbyBrowserUI();
    }

    public void OnJoinRoomButton(string roomName)
    {
        NetworkManager.instance.JoinRoom(roomName);
    }

    public override void OnRoomListUpdate(List<RoomInfo> allRooms)
    {
        roomList = allRooms;
    }
}
