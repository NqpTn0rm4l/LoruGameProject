using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class LoruNet : MonoBehaviour
{
    public static bool connected = false;
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void Dispatch(string cmd, string payload); // notify the browser
    [DllImport("__Internal")]
    private static extern void Back(); // notify the browser

#else
    void Dispatch(string cmd, string payload)
    {

    }
    void Back()
    {

    }
#endif

    const string localhost = "localhost:8000";
#if UNITY_ANDROID
    
    const string googleHost = "https://loruplay.appspot.com";
#else
    const string googleHost = "https://loruplay.appspot.com";
#endif
    public string DomainURL = googleHost;
    public ExercisesCollection ExercisesColl = new ExercisesCollection();


    public void TryGetData()
    {
        
        try
        {
            StartCoroutine(EDownloadPlayerData((e) => DataManager.Instance.TehtavatKartoitus.Kartoita(e)));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed to provide data. " + e);
        }
    }

    IEnumerator EDownloadPlayerData(Action<List<Exercise>> onSuccess)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        var url = $"{DomainURL}/api/skills/actives?userId={DataManager.Instance.UserId}";
#else
        var url = $"{DomainURL}/api/skills/actives?email={DataManager.Instance.Email}";
#endif
        DataManager.print("Ladataan url: " + url);

        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.timeout = 10;
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            if (req.result != UnityWebRequest.Result.ConnectionError) 
            {
                connected = true;
                byte[] result = req.downloadHandler.data;
                string jsonObject = System.Text.Encoding.Default.GetString(result);
                // string jsonObject = System.Text.Encoding.Default.GetString(result);
                DataManager.print(jsonObject);
                var exercisesCollection = ExercisesCollectionFromJSON(jsonObject);
                //TODO Quick Hack for testing purposes
                DataManager.Instance.UserId = exercisesCollection.userId;
                onSuccess(exercisesCollection.exercises);
                
            }
            else Debug.LogWarning("Failed to download data: " + req.error);
        }
    }


    public IEnumerator ESendPlayerActionSkillData(string skillId, int id, int skillLevel, int tryCount)
    {
        var url = $"{DomainURL}/api/skills";
        DataManager.print($" - {skillLevel} - {tryCount} --- url: {url} id: {skillId}");
        var skillInfo = new OutboundingSkillData
        {
            skillId = skillId,
            id = id,
            skill = skillLevel,
            count = tryCount,
        };

        var skillInfoJson = JsonUtility.ToJson(skillInfo);
        DataManager.print(skillInfoJson);

        using (UnityWebRequest req = UnityWebRequest.PostWwwForm(url, ""))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(skillInfoJson);
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            // req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
            req.SetRequestHeader("Content-Type", "application/json");
            req.timeout = 10;
            yield return req.SendWebRequest();
            DataManager.print("Uploading player skill data.");

            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error While Sending: " + req.error);
            }
            else if (req.downloadHandler.text != "")
            {
                DataManager.print(nameof(ESendPlayerActionSkillData) + " Received: " + req.downloadHandler.text);
            }
        }
    }


    [Serializable]
    public class Author
    {
        public string username;
        public string avatar;
        public bool following;
    }

    [Serializable]
    public class Question
    {
        public int key;
        public string question;
        public string answer;
        public string wrongAnswer1;
        public string wrongAnswer2;
        public string wrongAnswer3;
    }

    [Serializable]
    public class Exercise
    {
        public string id;
        public string title;
        public List<string> tags;
        public List<Question> questions;
        public string skillId;
        public List<SkillData> skillData;
    }

    [Serializable]
    public class SkillData
    {
        public int id;
        public int skill;
        public int count;
    }

    [Serializable]
    public class OutboundingSkillData
    {
        public string skillId;
        public int id;
        public int skill;
        public int count;
    }

    [Serializable]
    public class ExercisesCollection
    {
        public List<Exercise> exercises = new List<Exercise>();
        public string userId;
    }


    public ExercisesCollection ExercisesCollectionFromJSON(string jsonObject)
    {
        print(jsonObject);
        ExercisesColl = JsonUtility.FromJson<ExercisesCollection>(jsonObject);
        return ExercisesColl;
    }

    public void BrowserDispatch(Commands command, string payload)
    {
        string cmd = Enum.GetName(typeof(Commands), command);

#if !UNITY_EDITOR && UNITY_WEBGL
        if(!DataManager.Instance.testBuild) Dispatch (cmd, payload);
#elif UNITY_EDITOR
        if (DataManager.Instance.PrintOutboundingMsgs)
            print(cmd + ": " + payload);
#endif
    }

    public void BrowserBack()
    {
       
#if !UNITY_EDITOR && UNITY_WEBGL
        if(!DataManager.Instance.testBuild) Back ();
#elif UNITY_EDITOR
     
#endif
    }

}