using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaeTehtavat : MonoBehaviour
{
    /* tämä scipti hakee pelin alussa pelaajan henkilökohtaiset tehtävät serveriltä ja tallentaa ne sessiokohtaisesti Tehtavat luokkaan.
     * kannattaisko tämän hakea myös javascriptiltä käyttäjän tieto
     * 
     * Kannattaako dataa hakea vain pelin alussa? 
     * Ehkä aina kun kenttä vaihtuu tai kuolee tms haetaan uusi data ja päivitetään tasot serverille
     * 
     * 
     * Päivitä taso serverille()
     * for each tehtavaluokka in tehtavaTassaSessiossa
     *  serverille lähetä:
     *      käyttäjäpankintehtavat[tehtavaTassasessiossa.Id] = tehtavaTassasessiossa[id]
     *      
     *      Tallennetaan siis sesiokohtaisista tehtavaluokista ID:n mukaan severille kaikki osaamiseen liittyvä tieto
     * 
     * 
     */
}
