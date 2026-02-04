using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using static LoruNet;

public enum Commands
{
    Start,
    HandlePlayerData,
    LogPlayerAction,
    LogMsg,
    GetGroupId

}

public class DataManager : Singleton<DataManager>
{
    public string UserName = "guest";
    public string UserId = "5ed5f8a328611600079b2411";
    public string GroupId = "noGroup";
    public string Email = "anonymous@anom.com";
    public bool PrintOutboundingMsgs = false;
    public bool testBuild = false; //lokaalitestaus
    // public TasksHandler TasksHandler;
    public LoruNet LoruNet;
    public TehtavatKartoitus TehtavatKartoitus;


    public void SetDomainURL(string URL) => LoruNet.DomainURL = URL;

    public void SetUserId(string user) => UserId = user;
    public void SetGroupId(string groupId) => GroupId = groupId;
    public void SetUserEmail(string email) => Email = email;
    //public void SetUserEmail(string user) => Email = user;
    public void GameQuit() => Application.Quit();
    public void EnableLogging() => PrintOutboundingMsgs = true;
    public void DisableLogging() => PrintOutboundingMsgs = false;
    public void GetPlayerData() => Instance.LoruNet.TryGetData();

    public string gameVersion { get => "Build v: " + Application.version; }

    public static void LataaPelaajanData() => Instance.LoruNet.TryGetData();
    public static void TallennaPelaajanData_Vanhentunut()
    {

    }

    internal static void LähetäPelaajanVastausData(TehtavaLuokka tehtavaNyt) =>
        Instance.StartCoroutine(Instance.LoruNet.ESendPlayerActionSkillData(tehtavaNyt.skillDataId, tehtavaNyt.tehtäväId, tehtavaNyt.osaamisTaso, tehtavaNyt.vastaamisKerrat));

    void Start()
    {
        Invoke("Connect", 0.2f);
    }

    void Connect()
    {
#if UNITY_ANDROID

        string tmp = AndroidApp.androidEmail ;
        Debug.Log(tmp);
        AndroidDebugger.Log(tmp);
        Email = tmp;
        

    

#endif
#if !UNITY_ANDROID && UNITY_EDITOR
        //AndroidApp.androidEmail = "miika@lorugames.com";
        //string tmp = AndroidApp.androidEmail ;
        //Debug.Log(tmp);
        //Email = tmp;

        var KonenNimi = System.Environment.MachineName;

        switch (KonenNimi)
        {
            case "DESKTOP-E2PVEQ7":
                Email = "miika@lorugames.com";
                GroupId = "5ed637f1d5991f0007f03bab";
                break;
            case "LAPTOP-U9DCMB7T":
                Email = "arto@lorugames.com";
                break;
            case "DESKTOP-8J5IBMT":
                Email = "jouta.helm@gmail.com";
                break;
            default:
                UserId = "5ebe6870cafa3300087eb648";
                Email = "manundi@gmail.com";
                GroupId = "5ed637f1d5991f0007f03bab";
                break;
        }


#endif
        // TasksHandler = GetComponent<TasksHandler> ();
        LoruNet = gameObject.AddComponent<LoruNet>();
        TehtavatKartoitus = gameObject.AddComponent<TehtavatKartoitus>();

        Initialize();
    }


    private void Update()
    {
        // if (Input.GetKeyUp(KeyCode.Home)) Time.timeScale = Time.timeScale == 10f ? 1f : Time.timeScale == 3f ? 10f : 3f;
    }

    void Initialize()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
        LoruNet.BrowserDispatch(Commands.Start, gameVersion);
        LoruNet.BrowserDispatch(Commands.GetGroupId, gameVersion);
#else
        LataaPelaajanData();
#endif
    }



    public static void print(string msg) => Instance.LoruNet.BrowserDispatch(Commands.LogMsg, msg);
}