using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFrogController : MonoBehaviour {

   // public string animateText;
    private Animator animate;
    int timer;
    float second = 3.0f;

    void Start()
    {
        // 各参照の初期化
        animate = GetComponent<Animator>();
        //		animator.SetBool("NowStanding",true); //NowStandingパラメータにtrueをセット
    }

    void Update()
    { 
            if (second >= 0.0f)
            {
                second -= Time.deltaTime;
                Debug.Log(second);
            }
            else if (second <= 0.0f)
            {
            Attack();
            second = 3.0f;
            }
    }

    public void Attack()
    {
        animate.SetTrigger("Attack");
    }

    public void Damage()
    {
        Invoke("Damege2", 3.0f);
    }

    public void Damege2()
    {
        animate.SetTrigger("Damage");
    }

    public void Death()
    {
        animate.SetTrigger("Death");
    }
}
