using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboBasic : MonoBehaviour {

    public float speedZ;
    public float speedX;

    // Update is called once per frame
    void Update() {
        this.transform.position += new Vector3(0, 0, speedZ);
        this.transform.position += new Vector3(speedX, 0, 0);

        if (this.transform.position.x > 2.5)
        {
            speedX = speedX * -1;
            this.transform.position += new Vector3(speedX, 0, 0);
        }
        else if (this.transform.position.x < -2.5)
        {
            speedX = speedX * -1;
            this.transform.position += new Vector3(speedX, 0, 0);
        }
     }
}
