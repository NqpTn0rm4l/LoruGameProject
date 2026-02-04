using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.UnityAssets.SocketIO;
using System;
using System.Linq;
using Newtonsoft.Json;



public class MultiplayerClient : MonoBehaviour
{

    public bool localTest;
    //loruuniserver.herokuapp.com
    public Transform localPlayerPos;
    SocketIOCommunicator sioCom;
    public string serverUrl;
    public bool ssl;

    [Serializable]
    public class Players
    {
        public List<Player> players;
    }
    public class UpdateData
    {
        public List<Player> players { get; set; }
    }

    [Serializable]
    public class DictPlayers
    {
        public Dictionary<string, Player> players;
    }

    [Serializable]
    public class Player
    {
        public string name;
        public int score;
        public int character;
        public int id;
        public Vector3 position;
    }

    [Serializable]
    struct GameData
    {
        public string gameName;
        public int peliNumero;
        public int useUpdate;
    }

    [Serializable]
    public struct PlayerDataIn
    {
        public int playerId;
    }
    [Serializable]
    struct NetworkAction
    {
        public string type;
        public string status;
        public int playerId;
        public int targetNumber;
        public int amount;
    }

    public Players networkPlayers;
    public Dictionary<string, Player> playerDict;
    public Player localPlayer;
    int lastPlayerCount;


    public static event Action<int> OnGameOver;
    public static event Action OnStartGame;
    public static event Action<int> OnSpawnTrack;
    public static event Action<int> OnStartTimer;

    public static event Action<string> OnConnect;
    public static event Action<string> OnJoin;
    public static event Action OnUpdate;


    void Emit(string event_, object obj)
    {
        // print("Versio1");
        string json = JsonUtility.ToJson(obj);
        //string json = JsonConvert.SerializeObject(obj);
        //string json = MiniJSON.Json.Serialize(obj);
        sioCom.Instance.Emit(event_, json, false);
        // print("sending event. " + event_ + " with data: " + json);

    }

    void BeginNetwork()
    {
        sioCom = gameObject.AddComponent<SocketIOCommunicator>();
        sioCom.secureConnection = ssl;
        sioCom.socketIOAddress = serverUrl;
        if (!sioCom.Instance.IsConnected())
        {
            sioCom.Instance.On("connect", (string data) =>
            {
                OnConnect?.Invoke(data);
                string gameName = RyhmanHighScoret.ryhmaId + Pelikohtaiset.pelinNimi;
                print("pelinimi = " + Pelikohtaiset.pelinNimi);
                print("Rymaid = " + RyhmanHighScoret.ryhmaId);
                GameData dat = new GameData()
                {
                    gameName = gameName,
                    useUpdate = 10 //10 framee / s
                };

                Debug.Log("Connected to multiplayes server, finding game...");
                Emit("find game", dat);
            });

            sioCom.Instance.On("on join", (string data) =>
            {
                OnJoin?.Invoke(data);
                print("Joined game");
                print(data);
                PlayerDataIn dat = JsonUtility.FromJson<PlayerDataIn>(data);
                localPlayer.id = dat.playerId;
                localPlayer.name = RyhmanHighScoret.pelaajanNimimerkki;
            });

            sioCom.Instance.On("update", (string payload) =>
            {
                print(payload);
                OnUpdate?.Invoke();
                // Deserialize only the players part of the JSON data
                var updateData = JsonConvert.DeserializeObject<UpdateData>(payload);
                playerDict = updateData.players.ToDictionary(player => player.id.ToString(), player => player);

                networkPlayers.players = playerDict.Values.ToList();
                localPlayer.position = localPlayerPos.position;
                Emit("player data", localPlayer);
            });

            sioCom.Instance.On("action", (string payload) =>
            {
#if UNITY_EDITOR
                payload = payload.Replace(@"\", "");
#endif

                NetworkAction receivedAct = JsonConvert.DeserializeObject<NetworkAction>(payload);

                switch (receivedAct.type)
                {
                    case "Start Game":
                        OnStartGame?.Invoke();
                        string gameName = RyhmanHighScoret.ryhmaId + Pelikohtaiset.pelinNimi;
                        print("Rymaid = " + RyhmanHighScoret.ryhmaId);
                        GameData dat = new GameData()
                        {
                            gameName = gameName,
                            useUpdate = 10 //10 framee / s
                        };
                        Emit("close room", dat);
                        break;

                    case "Game Over":
                        OnGameOver?.Invoke(receivedAct.playerId);

                        break;

                    case "Spawn Track":
                        OnSpawnTrack?.Invoke(receivedAct.amount);  // Invoke SpawnTrack with amount
                        break;

                    case "Start Timer":
                        OnStartTimer?.Invoke(receivedAct.amount); // Invoke StartTimer action with amount
                        break;

                }
            });
            sioCom.Instance.Connect();
        }
    }

    private void Start()
    {
        localPlayer = new Player()
        {
            name = RyhmanHighScoret.pelaajanNimimerkki,

        };
    }
    private void OnEnable()
    {

        if (localTest)
        {
            serverUrl = "localhost:7000";
            ssl = false;
        }
        BeginNetwork();
    }

    public static MultiplayerClient instance;

    void Awake()
    {
        MakeSingleton();

        void MakeSingleton()
        {
            if (instance == null)
            {
                instance = this;
                // DontDestroyOnLoad(gameObject);
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
        }
    }


    public void DoNetworkAction(string action, string status = null, int actorNumber = 0, int targetNumber = 0, int amount = 0)
    {
        NetworkAction toSend = new NetworkAction
        {
            type = action,
            status = status,
            playerId = actorNumber,
            targetNumber = targetNumber,
            amount = amount
        };
        // print("sending action" + toSend.type);
        // print(JsonUtility.ToJson(toSend));
        Emit("action", toSend);
    }
}
