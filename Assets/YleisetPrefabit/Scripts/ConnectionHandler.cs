using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectionHandler : MonoBehaviour
{
    
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void Dispatch(string cmd, string payload); // notify the browser
#else
    void Dispatch(string cmd, string payload)
    {

    }
#endif

    const string localhost = "localhost:8000";
#if UNITY_ANDROID
    const string azureHost = "";
#else
    const string azureHost = "https://lorupoc.azurewebsites.net";
#endif
    public string DomainURL = azureHost;
    public void SetDomainURL(string URL) => this.DomainURL = URL;
    public ExerciseCollection ExerciseCollection;

    public void SetUserName(string userName) => DataManager.Instance.UserName = userName;
    public void GameQuit() => Application.Quit();
    public void EnableLogging() => DataManager.Instance.PrintOutboundingMsgs = true;
    public void DisableLogging() => DataManager.Instance.PrintOutboundingMsgs = false;
    public void SavePlayerData() => HandleSaveData(DataManager.Instance.TehtavatKartoitus.tehtavat);

    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        SavePlayerData();
#endif
    }

    public void TryGetData()
    {
        try
        {
//#if UNITY_ANDROID
//            GetLocalPlayerData ((e) => DataManager.Instance.TehtavatKartoitus.Kartoita (e));
//#else
//            DownloadPlayerData((e) => DataManager.Instance.TehtavatKartoitus.Kartoita(e));
//#endif

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed to provide data. " + e);
        }
    }

    public void DownloadPlayerData(Action<List<ExerciseData>> onSuccess)
    {
        StartCoroutine(EDownloadPlayerData(onSuccess));
    }

    public void GetLocalPlayerData(Action<List<ExerciseData>> onSuccess)
    {
        try
        {
            string jsonObject = "{\"exercises\" :" + Resources.Load<TextAsset>("data").text + "}";
            var exerciseData = ExerciseDataListFromJSON(jsonObject);
            exerciseData.ForEach(e => e.contentSkillData = PlayerPrefs.GetString("id" + e.exerciseID.ToString()));
            onSuccess(exerciseData);
        }
        catch (Exception e)
        {
            DataManager.print("Failed to get local data: " + e);
        }
    }

    public void HandleSaveData(List<TehtavaLuokka> tehtavat)
    {
        if (tehtavat.Count == 0) return;

        tehtavat.Sort((a, b) => a.tehtäväId.CompareTo(b.tehtäväId));
        for (int i = 0; i < ExerciseCollection.exercises.Count; i++)
        {
            int id = ExerciseCollection.exercises[i].exerciseID;
            var tehtByID = tehtavat.Where(t => t.settiID == id.ToString()).ToArray();
            string skillData = "";
            for (int j = 0; j < tehtByID.Length; j++)
            {
                if (j != 0)
                    skillData += "\r\n";
                skillData += tehtByID[j].osaamisTaso + "\t" + tehtByID[j].vastaamisKerrat;
            }
#if UNITY_ANDROID
            PlayerPrefs.SetString ("id"+id.ToString (), skillData);
            PlayerPrefs.Save();
#else
            UploadPlayerSkillData(id, skillData);
#endif
        }
    }

    IEnumerator EDownloadPlayerData(Action<List<ExerciseData>> onSuccess)
    {
        var url = $"{DomainURL}/api/exercises/personal?userName={DataManager.Instance.UserName}";
        DataManager.print(url);
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.timeout = 10;
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            if (!req.isNetworkError)
            {
                byte[] result = req.downloadHandler.data;
                string jsonObject = "{\"exercises\" :" + System.Text.Encoding.Default.GetString(result) + "}";
                // string jsonObject = System.Text.Encoding.Default.GetString(result);
                DataManager.print(jsonObject);
                onSuccess(ExerciseDataListFromJSON(jsonObject));
            }
            else Debug.LogWarning("Failed to download data: " + req.error);
        }
    }

    void UploadPlayerSkillData(int exerciseID, string skillData)
    {
        // var url = $"{DomainURL}/api/exercises/skill";
        // GameManager.print (url);
        // var skillInfo = new SkillData {
        //     exerciseID = exerciseID,
        //     userName = GameManager.Instance.UserName,
        //     contentSkillData = skillData
        // };

        // var skillInfoJson = JsonUtility.ToJson (skillInfo);
        // GameManager.print (skillInfoJson);
        // using (UnityWebRequest req = UnityWebRequest.Post (url, "")) {
        //     byte[] jsonToSend = new System.Text.UTF8Encoding ().GetBytes (skillInfoJson);
        //     req.uploadHandler = (UploadHandler) new UploadHandlerRaw (jsonToSend);
        //     // req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        //     req.SetRequestHeader ("Content-Type", "application/json");
        //     req.timeout = 10;
        //     req.SendWebRequest ();
        // }
        StartCoroutine(EUploadPlayerSkillData(exerciseID, skillData));
    }

    IEnumerator EUploadPlayerSkillData(int exerciseID, string skillData)
    {
        var url = $"{DomainURL}/api/exercises/skill";
        DataManager.print(url);
        var skillInfo = new SkillData
        {
            exerciseID = exerciseID,
            userName = DataManager.Instance.UserName,
            contentSkillData = skillData
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
                Debug.Log("Error While Sending: " + req.error);
            }
            else if (req.downloadHandler.text != "")
            {
                Debug.Log(nameof(EUploadPlayerSkillData) + " Received: " + req.downloadHandler.text);
            }
        }
    }

    public List<ExerciseData> ExerciseDataListFromJSON(string jsonObject)
    {
        ExerciseCollection = JsonUtility.FromJson<ExerciseCollection>(jsonObject);
        return ExerciseCollection.exercises;
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

    //string GetLocalJsonData() =>
    //    Resources.Load<TextAsset>("data").text;
    ////.Replace("$1", PlayerPrefs.GetString("1"))
    ////.Replace("$2", PlayerPrefs.GetString("2"))
    ////.Replace("$3", PlayerPrefs.GetString("3"))
    ////.Replace("$4", PlayerPrefs.GetString("4"))
    ////.Replace("\'", "\"");

}

[Serializable]
public class ExerciseCollection
{
    public List<ExerciseData> exercises;
}

[Serializable]
public class ExerciseData
{
    public string title;
    public string subject;
    public string content;
    public int exerciseID;
    public string contentSkillData;

    int[][] skillData = null;

    public List<TehtavaLuokka> ExtractPlayerData()
    {
        if (string.IsNullOrEmpty(content)) return null;

        var tehtävät = new List<TehtavaLuokka>();
        Debug.Log(content);
        var rows = content.Replace("\r", "").Split('\n');
        // skillData ??= StringToSkillData(contentSkillData);
        if (skillData == null) CreateSkillData();
        if (skillData?.Length != rows.Length) skillData = null;

        for (int i = 0; i < rows.Length; i++)
        {
            string[] vals = rows[i].Split('\t');
            tehtävät.Add(
                new TehtavaLuokka
                {
                    settiID = exerciseID.ToString(),
                    tehtäväId = i,
                    oppiAine = subject,
                    osaAlue = title,
                    tehtavaKysymys = vals[0],
                    oVastaus = vals[1],
                    vVastaus1 = vals[2],
                    vVastaus2 = vals[3],
                    vVastaus3 = vals[4],
                    osaamisTaso = skillData?[i]?[0] ?? 50,
                    vastaamisKerrat = skillData?[i]?[1] ?? 0,
                }
            );
        }
        return tehtävät;
    }

    public int[][] CreateSkillData()
    {
        string data = contentSkillData;
        if (string.IsNullOrEmpty(data)) return null;
        data = data.Replace("\r", "");
        var rows = data.Split('\n');
        skillData = new int[rows.Length][];
        for (int i = 0; i < skillData.Length; i++)
        {
            string[] vals = rows[i].Split('\t');
            skillData[i] = new int[2];
            skillData[i][0] = int.Parse(vals[0]);
            skillData[i][1] = int.Parse(vals[1]);
        }
        return skillData;
    }

    // public string SkillDataToString () {
    //     if (skillData == null) StringToSkillData (contentSkillData);;

    //     string skillDataString = "";
    //     for (int i = 0; i < this.skillData.Length; i++) {
    //         skillDataString += this.skillData[i][0] + "\t" + this.skillData[i][1] + "\r\n";
    //     }
    //     return skillDataString;
    // }

}

public class SkillData
{
    public int exerciseID;
    public string userName;
    public string contentSkillData;
}

// [Serializable]
// public class SkillDataCollection {
//     public int[][] SkillData;

//     public SkillDataCollection(string data) {
//         data = data.Replace ("\r", "");
//         var rows = data.Split ('\n');
//         SkillData = new int[rows.Length][];
//         for (int i = 0; i < SkillData.Length; i++)
//         {
//             string[] vals = rows[i].Split('\t');
//             SkillData[i][0] = int.Parse(vals[0]);
//             SkillData[i][1] = int.Parse(vals[1]);
//         }
//     }

//     public override string ToString () {
//         string skillData = "";
//         for (int i = 0; i < SkillData.Length; i++) {
//             skillData += SkillData[i][0] + "\t" + SkillData[i][1] + "\r\n";
//         }
//         return skillData;
//     }
// }

// public class SkillInfo{
//     public int id;
//     public int skill;
//     public int tries;
// }

// [Serializable]
// public class Exercise
// {
//     public int id;
//     public string title;
//     public string subject;
//     public string area;
//     public string description;
//     public string content;
// }

// public List<Exercise> ExercisesFromJSON(string jsonObject)
// {
//     return JsonUtility.FromJson<ExerciseCollection>(jsonObject).Exercises;
// }

// public List<Exercise> ExerciseListFromJSON(string jsonObject)
// {
//     return JsonUtility.FromJson<List<Exercise>>(jsonObject);
// }