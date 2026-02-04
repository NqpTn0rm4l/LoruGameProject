
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tehtavat : MonoBehaviour
{
    // tämä scripti luo säiliön johon ladataan tehtävät serveriltä
    public static List<TehtavaLuokka> tehtavatTassaSessiossa = new List<TehtavaLuokka>();

    //tehdään säiliö käyttäjätietojavarten
    public static Kayttajatiedot kayttajanTiedotTassaSessiossa = new Kayttajatiedot();

    // tässä tehdään Jevgenin luomasta DataManager scriptistä dontdestroy objekti, joka siirtää datan serveriltä ja serverille.
    public void Start(){
        //Debug.Log("Tapahtuma ? nro1");
        //GameObject klooni = Instantiate(new GameObject());
        //klooni.AddComponent<DataManager>();
       
    }
    //public void Awake()
    //{
    //    var gi = DataManager.Instance.gameObject;
    //}
    private void OnEnable()
    {
       
        var gi = DataManager.Instance.gameObject;
    }
    

}
