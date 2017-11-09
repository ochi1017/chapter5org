using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMaker : MonoBehaviour {
    public GameObject effectPrefab;
    public GameObject chara;
    public IdleChangerorg unity;
    public BossGameController bgc;

    public void MakeEffect()
    {
        if (bgc.unitymp() >= 40)
        {
            if (unity.GetAction() == 0)
            {
                Invoke("MakeEffect2", 6.0f);
                unity.SetAction(1);
            }
        }
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
            unity.SetAction(0);
        }
    }
}
