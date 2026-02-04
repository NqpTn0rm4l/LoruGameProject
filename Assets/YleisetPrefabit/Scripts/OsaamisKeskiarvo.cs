using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class OsaamisKeskiarvo : MonoBehaviour
{
    public TextMeshProUGUI keskiarvo;
    // Start is called before the first frame update
    private void OnEnable()
    {
        keskiarvo.text = Tehtavat.tehtavatTassaSessiossa.Average(t => t.osaamisTaso). ToString("0.0");
    }
}
