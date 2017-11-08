using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMaker : MonoBehaviour {
    public GameObject effectPrefab;
    public GameObject chara;

    public void MakeEffect()
    {
        Invoke("MakeEffect2", 3.0f);
    }

    public void MakeEffect2()
    {
        if (effectPrefab != null)
        {
            Instantiate(
                effectPrefab,
                chara.transform.position,
                Quaternion.identity
                );
        }
    }
}
