using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGameController : MonoBehaviour {

    int ehp = 300;
    int eehp = 300;
    int hp = 300;
    int mp = 100;
    int timer;
    int skillscale = 40;
    int specialscale = 4;
    int tien = 1;
    int dodgeact;

    private float second;
    private float msecond = 3.0f;

    public Text hplabel;
    public Text mplabel;
    public Text ehplabel;
    public StoneFrogController stonefrog;
    public IdleChangerorg unitychan;

    Slider _slider;
    Slider _mslider;

    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        _mslider = GameObject.Find("mSlider").GetComponent<Slider>();
        //
    }

    void Update()
    {
        //キャラクターの遅延時間
        if (timer > 0)
        {
            if (second >= 0.0f)
            {
                second -= Time.deltaTime / tien;
                //Debug.Log(second);
                _slider.value = second;
            }
            else if(second <= 0.0f)
            {
                _slider.value = _slider.maxValue;
                timer = 0;
            }
        }
        //モブの遅延時間
        if (msecond >= 0.0f)
        {
            msecond -= Time.deltaTime;
            //Debug.Log(msecond);
            _mslider.value = msecond;
        }
        else if (msecond <= 0.0f)
        {
         
            if (hp >= 0)
            {
                stonefrog.Attack();
                unitychan.Damage();
                msecond = 3.0f;
                hp -= 30;
                hplabel.text = "HP:" + hp;
            }
        }

        if (hp <= 0)
        {
            unitychan.Lose();
            Invoke("ReturnTotitle", 2.5f);
        }
        if (ehp <= 0)
        {
            Invoke("MoveToStageSerect", 2.5f);
        }
    }

	public void attack()
    {
        tien = 1;
        if (second <= 0)
        {
            if (timer == 0)
                timer = 1;
            second = 3f;
            if (mp >= 10)
            {
                mp -= 10;
                mplabel.text = "MP:" + mp;
            }
        }
    }

    public void skill()
    {
        tien = 1;
        if (second <= 0)
        {
            if (timer == 0)
                timer = 1;
            second = 3f;
            if (mp >= 20)
            {
                mp -= 20;
                mplabel.text = "MP:" + mp;
            }
        }
    }

    public void special()
    {
        if (second <= 0)
        {
            tien = 2;
            if (timer == 0)
                timer = 1;
            second = 3f;
            if (mp >= 40)
            {
                mp -= 40;
                mplabel.text = "MP:" + mp;
                skillscale *= specialscale;
            }
        }
    }

    public void dodge()
    {
        dodgeact = 1;
        if (mp >= 10)
        {
            mp -= 10;
            mplabel.text = "MP:" + mp;
        }
    }

    public int unitymp()
    {
        return mp;
    }

    public int getenemyhp()
    {
        return ehp;
    }

    public void setenemyhp(int sethp)
    {
        ehp = sethp;
    }

    public int getdodgeact()
    {
        return dodgeact;
    }

    public void setdodgeact(int act)
    {
        dodgeact = act;
    }

    public int getskillscale()
    {
        return skillscale;
    }

    void ReturnTotitle()
    {
        Application.LoadLevel("Title");
    }

    void MoveToStageSerect()
    {
        Application.LoadLevel("StageSerect");
    }
}
