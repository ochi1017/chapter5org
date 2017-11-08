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
    }

    void Update()
    {
        //キャラクターの遅延時間
        if (timer > 0)
        {
            if (second >= 0.0f)
            {
                second -= Time.deltaTime;
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
            Debug.Log(msecond);
            _mslider.value = msecond;
        }
        else if (msecond <= 0.0f)
        {
            stonefrog.Attack();
            unitychan.Damage();
            msecond = 3.0f;
            hp -= 30;
            hplabel.text = "HP:" + hp;
        }
    }

	public void attack()
    {
        if (timer== 0)
            timer = 1;
        second = 3f;
        mp -= 10;
        mplabel.text = "MP:" + mp;
        ehp -= eehp / 10;
        ehplabel.text = "HP:" + ehp;
    }

    public void skill()
    {
        if (timer == 0)
            timer = 1;
        second = 3f;
        mp -= 20;
        mplabel.text = "MP:" + mp;
        ehp -= eehp / 5;
        ehplabel.text = "HP:" + ehp;
    }

    public void special()
    {
        if (timer == 0)
            timer = 1;
        second = 3f;
        mp -= 40;
        mplabel.text = "MP:" + mp;
        ehp -= eehp / 3;
        ehplabel.text = "HP:" + ehp;
    }

    public void dodge()
    {
        mp -= 10;
        mplabel.text = "MP:" + mp;
    }

    public int bosseehp()
    {
        return eehp;
    }
}
