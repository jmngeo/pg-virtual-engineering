using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public int maxPlayers; //maximum number of players

    //singleton
    public static NetworkManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            gameObject.SetActive(false);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //connecting to the master server at beginning of the game
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom(string roomName)
    {
        //creating different rooms
        RoomOptions options = new RoomOptions();
        //max number of players can join that room
        options.MaxPlayers = (byte)maxPlayers;
        //to publish User ID
        options.PublishUserId = true;
        //create room
        PhotonNetwork.CreateRoom(roomName, options);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    [PunRPC]
    public void checkTargetAndChangeScene(string MastersceneName, string OthersceneName)
    {

        //setting the value of the global variable for the phase end event to true

        GlobalVariables.isInPhaseEndEvent = true;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(MastersceneName);
        }

        else
        {
            PhotonNetwork.LoadLevel(OthersceneName);
        }
    }

    [PunRPC]
    void SetFinalScoreScreenforAll()
    {
        if (!PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("phase" + GlobalVariables.PhaseCounter + "-" + "PhaseEndEvent");
    }

}
