using System;

[Serializable]
public class TehtavaLuokka 
{ 
    //nämä haetaan serveriltä mutta tietoa ei palauteta
    public string settiID = "";
    public int tehtäväId = 0; //järjestys nro
    public int luokkaAste;
    public string oppiAine;
    public string osaAlue;

    public string tehtavaKysymys = "";
    public string oVastaus = "";
    public string vVastaus1 = "";
    public string vVastaus2 = "";
    public string vVastaus3 = "";

    //nämä ovat käyttäjäkohtaista tietoa joka pitää palauttaa serverille
    public string skillDataId;
    public int osaamisTaso = 50;
    public int vastaamisKerrat = 0;

    //ideoita, 
    public int osaamisNopeusTaso = 0; //Esim. kuinka nopeasti kysymykseen vastataan(ei toimi kaikissa peleissä)
    public int varmuusTaso = 0; // kuinka paljon osaamistaso on muuttunut tai seilaako se jatkuvasti edestakaisin.

    public TehtavaLuokka()
    {
        tehtavaKysymys = "";
        oVastaus = "";
        vVastaus1 = "";
        vVastaus2 = "";
        vVastaus3 = "";
        osaamisTaso = 50;
        vastaamisKerrat = 0;

        osaamisNopeusTaso = 0;
        varmuusTaso = 0;
    }
}