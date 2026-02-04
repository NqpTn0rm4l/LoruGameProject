using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VastauksenVahvistaja : MonoBehaviour
{
    ParticleSystem tahtiPartikkelit;
    public float nopeus = 1;

    public static VastauksenVahvistaja instance;

    void Awake()
    {
        MakeSingleton();

        void MakeSingleton()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        tahtiPartikkelit = GameObject.Find("VastausTahdet").GetComponent<ParticleSystem>();
    }
   
  
    public static void Paalle()
    {
        ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
        emitOverride.startLifetime = 10f;
        emitOverride.startLifetime = 2.5f;
        instance.tahtiPartikkelit.Emit(emitOverride, 10);
        instance.GetComponent<AudioSource>().Play();
        instance.GetComponent<Animator>().Play("Vahvistaja");
        instance.GetComponent<Animator>().speed = GameObject.Find("VastauksenVahvistaja").GetComponent<VastauksenVahvistaja>().nopeus;
    }
    public static void Pois()
    {

        instance.GetComponent<Animator>().Play("Piilossa");

    }



}
