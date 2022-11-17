// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [SerializeField]
    public int _HP=100;
    public int _MaxHP=100;

    [SerializeField]
    public int _Attack=20;

    [SerializeField]
    public Slider HPSlider;

    //public int _DanceAttack=_Attack*2;
    //public int _DanceAttack;

    [SerializeField]
    public int _Body=7;

    [SerializeField]
    public int _Speed=6;

    public bool DanceFlag=false;
    public bool GardFlag=false;

    public bool isDead=false;
    
    [SerializeField]public Status other;

    [SerializeField] private Animator _anim;
    [SerializeField] private Text _HPText;

    [SerializeField] private GameObject _AttStatus;
    [SerializeField] private GameObject _GardStatus;
    // Start is called before the first frame update

    public void Start(){
        //_DanceAttack=_Attack*2;
        HPSlider.maxValue=_MaxHP;
        HPSlider.value=_HP;
        _HPText.GetComponent<Text>().text=_MaxHP.ToString();
    }

    public bool ReturnDeadFlag(){
        return isDead;
    }

    public void Update(){
        if(DanceFlag==true){
            _AttStatus.SetActive(true);
        }
        else if(DanceFlag==false){
            _AttStatus.SetActive(false);
        }

        if(GardFlag==true){
            _GardStatus.SetActive(true);
        }
        else if(GardFlag==false){
            _GardStatus.SetActive(false);
        }

    }



    public void GetDamage(){
        //_HP-=other_attack-my_body+Random.Range(-5,6);//+-で整数の乱数分差異を作る
        int damage=0;
        if(isDead==false){
            if(GardFlag==true&&other.DanceFlag==true){
                damage=(other._Attack*2-_Body)+Random.Range(-1,4);
                if(damage>=0){
                    _HP-=damage;
                }

            }
            else if(GardFlag==true&&other.DanceFlag==false){
                damage=(other._Attack-_Body)+Random.Range(-1,4);
                if(damage>=0){
                    _HP-=damage;
                }
            }
            else if(GardFlag==false&&other.DanceFlag==true){
                damage=(other._Attack*2)+Random.Range(-1,4);
                if(damage>=0){
                    _HP-=damage;
                }
            }
            else if(GardFlag==false&&other.DanceFlag==false){
                damage=(other._Attack)+Random.Range(-1,4);
                if(damage>=0){
                    _HP-=damage;
                }
            }
            if(_HP<=0){
                _HP=0;
                isDead=true;
                //_anim.SetBool("Dead",true);
                StartCoroutine(DeadAnim());
            }
            GardFlag=false;
            other.DanceFlag=false;
            Debug.Log(_HP);
            _HPText.GetComponent<Text>().text=_HP.ToString();
            HPSlider.value=_HP;
        }
    }

    IEnumerator DeadAnim(){
        yield return new WaitForSeconds(2.0f);
        _anim.SetBool("Dead",true);

    }


}
