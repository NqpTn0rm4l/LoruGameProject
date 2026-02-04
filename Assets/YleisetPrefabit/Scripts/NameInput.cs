using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    public static string nickName;
    public TextMeshProUGUI inputText;
    // Start is called before the first frame update
    public void InitText()
    {
        nickName = inputText.text;
        RyhmanHighScoret.pelaajanNimimerkki = nickName;
        RyhmanHighScoret.LahetaPisteet();
    }

    // Update is called once per frame
}
