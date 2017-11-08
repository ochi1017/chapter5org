using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {
    public void Stage1Button(){
        Application.LoadLevel("Main");
    }
    public void Stage2Button()
    {
        Application.LoadLevel("Main2");
    }
}
