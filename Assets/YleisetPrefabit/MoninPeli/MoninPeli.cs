using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.UnityAssets.SocketIO;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.Events;


public class MoninPeli : MonoBehaviour
{
    public UnityEvent moninpeliUpdate;
    public UnityEvent gameStart;
    public UnityEvent gameOver;
    public MyIntEvent playerLeft;
    [Serializable]
    public class MyIntEvent : UnityEvent<int> { }

    public int maxPlayers = 10;
    public bool localTest;
    bool ssl = true;
    //loruuniserver.herokuapp.com
    // public Transform localPlayerPos;
    public string url;
    public SocketIOCommunicator sioComPrefab;
    SocketIOCommunicator sioCom;

    [Serializable]
    public class Pelaajat
    {
        public List<Pelaaja> pelaajat;
    }


    public class UpdateData
    {
        public List<Pelaaja> players { get; set; }
    }
    [Serializable]
    public class DictPlayers
    {
        public Dictionary<string, Pelaaja> players;
    }

    [Serializable]
    public class Pelaaja
    {
        public string nimi;
        public int pisteet;
        public int hahmo;
        public int id;
        public Vector3 position;
    }
    [Serializable]
    struct GameData
    {
        public string gameName;
        public int peliNumero;
        public int useUpdate;
        public int playerCount;
    }
    [Serializable]
    public struct PlayerDataIn
    {
        public int playerId;
        public bool gameStarted;
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

    public int moniPelipisteet;
    public PelaajaLista pList;

    public Pelaajat pelaajatNyt;
    public List<Pelaaja> testList;
    public DictPlayers testNestedDict;
    public Dictionary<string, Pelaaja> testDict;
    public Pelaaja tamaPelaaja;
    public Pelaaja[] testArray;

    public TextMeshProUGUI lahtoLaskTeksti;
    public TextMeshProUGUI tulevaData;
    public GameObject monipeliVoitto;
    public static int peliAika;
    public GameObject moninPeliNappi;
    int gameIndex;

    public static bool moninPeliKaynnissa = false;

    void Emit(string event_, object obj)
    {
        string json = JsonUtility.ToJson(obj);
        sioCom.Instance.Emit(event_, json, false);
    }

    private void Start()
    {
        moninPeliKaynnissa = false;
        if (url == null) url = "https://loruuniserver.herokuapp.com/";
    }

    private void OnEnable()
    {
        if (localTest)
        {
            url = "localhost:7000";
            ssl = false;
        }
    }

    public void AloitaMoninPeli()
    {
        pelaajatNyt.pelaajat.Clear();
        moninPeliKaynnissa = false;
        sioCom = gameObject.AddComponent<SocketIOCommunicator>();
        sioCom.secureConnection = ssl;
        sioCom.socketIOAddress = url;

        pList.gameObject.SetActive(true);
        pList.UusiMoninPeli();
        moninPeliNappi.gameObject.SetActive(false);
        moniPelipisteet = 0;

        if (GameObject.Find("PelaaNappi") != null)
        {
            GameObject.Find("PelaaNappi").SetActive(false);
        }

        if (!sioCom.Instance.IsConnected() && !moninPeliKaynnissa)
        {
            void FindGame()
            {

                string gameName = RyhmanHighScoret.ryhmaId + Pelikohtaiset.pelinNimi;
                GameData dat = new GameData()
                {
                    gameName = gameName,
                    useUpdate = 10 //10 framee / s
                };
                Debug.Log("YhdistettyMoninpeliin: " + dat.gameName);
                Emit("find game", dat);
            }

            sioCom.Instance.On("connect", (string data) =>
            {
                FindGame();
            });

            sioCom.Instance.On("on join", (string data) =>
            {
                print("Connected to game");
                print(data);
                PlayerDataIn dat = JsonUtility.FromJson<PlayerDataIn>(data);
                tamaPelaaja.nimi = RyhmanHighScoret.pelaajanNimimerkki;
                tamaPelaaja.id = dat.playerId;

                if (dat.playerId == maxPlayers)
                {
                    sioCom.Instance.Emit("close room");
                }
                if (dat.playerId > maxPlayers || dat.gameStarted)
                {
                    sioCom.Instance.Emit("disconnect");
                    FindGame();
                }

                Emit("player data", tamaPelaaja);

            });

            sioCom.Instance.On("update", (string payload) =>
            {
                var updateData = JsonConvert.DeserializeObject<UpdateData>(payload);
                testDict = updateData.players.ToDictionary(player => player.id.ToString(), player => player);

                pelaajatNyt.pelaajat = testDict.Values.ToList();
                //tamaPelaaja.position = localPlayerPos.position;
                // poista toisten hahmot ja randomisoi jäjellä olevista
                if (!moninPeliKaynnissa && tamaPelaaja.hahmo == 0)
                {
                    List<int> hahmoNumerot = Enumerable.Range(0, 10).ToList();
                    foreach (var item in pelaajatNyt.pelaajat)
                    {
                        if (hahmoNumerot.Contains(item.id))
                        {
                            hahmoNumerot.Remove(item.id);
                        }
                    }
                    tamaPelaaja.hahmo = hahmoNumerot[UnityEngine.Random.Range(0, hahmoNumerot.Count)];
                }
                Emit("player data", tamaPelaaja);
                moninpeliUpdate.Invoke();
            });

            //sioCom.Instance.On("counter", (string payload) =>
            //{
            //    int peliTik = int.Parse(payload);
            //    if (peliTik > 600)
            //    {
            //        if (pelaajatNyt.pelaajat.Count > 1)
            //        {
            //            int tmp = (peliTik - 600) / 5;
            //            lahtoLaskTeksti.text = tmp.ToString();
            //        }
            //    }
            //    else
            //    {
            //        if (pelaajatNyt.pelaajat.Count > 1)
            //        {
            //            peliAika = peliTik / 5;
            //            lahtoLaskTeksti.text = peliAika.ToString();
            //        } 
            //    }

            //    //JsonUtility.FromJson<ServerTechData>(payload);
            //    //tahan asiat jotka tapahtuu kun uuttta dataa saadaan

            //});



            sioCom.Instance.On("action", (string payload) =>
            {
                print("recieving act: " + payload);
#if UNITY_EDITOR
                payload = payload.Replace(@"\", "");
#endif

                NetworkAction receivedAct = JsonConvert.DeserializeObject<NetworkAction>(payload);
                switch (receivedAct.type)
                {
                    case "Start Game":
                        pList.gameObject.SetActive(false);
                        SceneManager.LoadScene(Pelikohtaiset.pelinNimi);
                        if (GameObject.Find("YleisetAlussa") != null)
                        {
                            GameObject.Find("YleisetAlussa").GetComponent<Musiikki>().HiljennaMusa();
                        }
                        moninPeliKaynnissa = true;
                        break;

                    case "Game Over":
                        if (moninPeliKaynnissa)
                        {
                            KatkaiseYhteys();
                            moninPeliKaynnissa = false;
                            monipeliVoitto.SetActive(true);
                        }

                        break;
                    case "player left":

                        pList.PoistaPelaaja(receivedAct.playerId);

                        break;

                }
            });

            sioCom.Instance.Connect();

            //InvokeRepeating("PaivitaMoninPeli", 0.2f, 0.2f);
        }

    }

    public void StartMultiplayer()
    {
        if (!moninPeliKaynnissa)
        {
            sioCom.Instance.Emit("start game");
            DoNetworkAction("Start Game");
        }
    }

    public void MoninPeliPisteet(int pisteet)
    {
        if (moninPeliKaynnissa)
        {
            tamaPelaaja.pisteet = pisteet;
            //todo korja voittoehto

        }

    }

    private void Update()
    {
        if (!moninPeliKaynnissa)
        {
            pList.PaivitaPelaajat();

        }
    }

    public void KatkaiseYhteys()
    {
        if (sioCom != null)
        {
            //sioCom.Instance.Emit("disconnect");
            sioCom.Instance.Close();
            Destroy(sioCom);
            moninPeliKaynnissa = false;
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
        print("sending action" + toSend.type);
        print(JsonUtility.ToJson(toSend));
        Emit("action", toSend);
    }

    public static MoninPeli instance;
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
}
