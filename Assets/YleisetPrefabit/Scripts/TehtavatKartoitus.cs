using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TehtavatKartoitus : MonoBehaviour
{

    public List<TehtavaLuokka> tehtavat = new List<TehtavaLuokka>();
    public event Action KartoitusValmis;
    public void Kartoita(List<LoruNet.Exercise> setit)
    {
        KartoitusValmis += RyhmanHighScoret.instance.OnKartoitusValmis;  // Subscribe to the event
        foreach (var setti in setit)
        {
            List<TehtavaLuokka> tehtävä = new List<TehtavaLuokka>();
            try
            {
                foreach (var kysymys in setti.questions)
                {
                    var taitoData = setti.skillData.Find(s => s.id == kysymys.key);

                    tehtävä.Add(new TehtavaLuokka
                    {
                        settiID = setti.id,
                        skillDataId = setti.skillId,
                        tehtäväId = kysymys.key,
                        tehtavaKysymys = kysymys.question,
                        oVastaus = kysymys.answer,
                        vVastaus1 = kysymys.wrongAnswer1,
                        vVastaus2 = kysymys.wrongAnswer2,
                        vVastaus3 = kysymys.wrongAnswer3,
                        osaamisTaso = taitoData?.skill ?? 0,
                        vastaamisKerrat = taitoData?.count ?? 0,
                    }); ; ;
                }
            }
            catch (System.Exception e)
            {
                throw new System.Exception("Tehtäväkartoitus epäonnistui! -", e);
                //GameManager.print("Failed to extract player data, probably due to change in skill info format. Resetting skill data. error msg:\n" + e);
                //tehtäväKokoelma.contentSkillData = "";
                //tehtävä = tehtäväKokoelma.ExtractPlayerData();
            }
            tehtavat.AddRange(tehtävä);
        }
        Tehtavat.tehtavatTassaSessiossa = tehtavat;
        DataManager dat = gameObject.GetComponent<DataManager>();
        RyhmanHighScoret.email = dat.Email;
        RyhmanHighScoret.ryhmaId = dat.GroupId;
        KartoitusValmis?.Invoke();


    }


    //public void Kartoita (List<ExerciseData> tehtäväKokoelmat) {
    //    foreach (var tehtKokoelma in tehtäväKokoelmat) {
    //        List<TehtavaLuokka> teht;
    //        try {
    //            teht = tehtKokoelma.ExtractPlayerData ();
    //        } catch (System.Exception e) {
    //            DataManager.print ("Failed to extract player data, probably due to change in skill info format. Resetting skill data. error msg:\n"+e);
    //            tehtKokoelma.contentSkillData = "";
    //            teht = tehtKokoelma.ExtractPlayerData ();
    //        }
    //        tehtavat.AddRange (teht);
    //    }
    //    Tehtavat.tehtavatTassaSessiossa = tehtavat;
    //}
}