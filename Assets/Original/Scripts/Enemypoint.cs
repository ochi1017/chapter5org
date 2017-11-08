using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypoint : MonoBehaviour {
    public GameObject prefab;
	// Use this for initialization
	void Start () {
        GameObject go = (GameObject)Instantiate(
            prefab,
            Vector3.zero,
            Quaternion.identity);
        go.transform.SetParent(transform, false);
	}
	
	void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + offset, 0.5f);
        //プレファブ名のアイコン表示
        if (prefab != null)
            Gizmos.DrawIcon(transform.position + offset, prefab.name, true);
    }
}
