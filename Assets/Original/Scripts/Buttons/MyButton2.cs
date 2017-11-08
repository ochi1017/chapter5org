using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton2 : MonoBehaviour {

	public void OnClick()
    {
        Debug.Log("Skill2 click!");
        gameObject.SetActive(false);
        MyCanvas.SetActive("Skill", true);
    }
}
