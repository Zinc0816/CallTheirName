using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBattleSystem : MonoBehaviour
{
    [SerializeField] private MyMonsterCommand _MyMonster;
    [SerializeField] private EnemyCommand _Enemy;
    [SerializeField] private GameObject _RecBottun;
    [SerializeField] private Status _MyStatus;
    [SerializeField] private Status _EnemyStatus;

    private bool _IsPlayerTurn;
    private bool _IsEnemyTurn;
    private bool _GameOver;
    private bool _GameClear;
    private bool _IsRunningCoroutine;

    [SerializeField] private GameObject _MyTurn; 
    [SerializeField] private GameObject _EnemyTurn; 

    [SerializeField] private AudioSource AudioS;
    [SerializeField] private AudioClip _click;

    float second=0f;
    // Start is called before the first frame update
    void Start()
    {
        _IsPlayerTurn=true;
        _IsEnemyTurn=false;
        _GameOver=false;
        _GameClear=false;
        _IsRunningCoroutine=false;
    }

    // Update is called once per frame
    void Update()
    {
        _GameClear=_EnemyStatus.ReturnDeadFlag();
        _GameOver=_MyStatus.ReturnDeadFlag();
        if(!_GameOver&&!_GameClear){
            if(!_IsPlayerTurn&&_IsEnemyTurn){
                second+=Time.deltaTime;
                if(second>=10.0f){
                    second=0;
                    EnemyAction();
                    Debug.Log("EnemyCall");
                    _EnemyTurn.SetActive(true);
                    _MyTurn.SetActive(false);
                    //_RecBottun.SetActive(true);
                }
            }
        }
        else if(_GameOver){
            //Scene遷移コルーチン(Animationみせるため)
            StartCoroutine(ToGameOverScene());
            //StartCoroutine(ToGameOverScene());
            
        }
        else if(_GameClear){
            //Scene遷移コルーチン(Animationみせるため)
            StartCoroutine(ToGameClearScene());
            
            
        }       
    }

    IEnumerator ToGameOverScene(){
        if(_IsRunningCoroutine){yield break;}
        _IsRunningCoroutine=true;
        yield return new WaitForSeconds(4.0f);
        FadeManager.Instance.LoadScene ("GameOverScene", 1.0f);
        //_IsRunningCoroutine=true;
    }

    IEnumerator ToGameClearScene(){
        if(_IsRunningCoroutine){yield break;}
        _IsRunningCoroutine=true;
        yield return new WaitForSeconds(4.0f);
        FadeManager.Instance.LoadScene ("GameClearScene", 1.0f);
        //_IsRunningCoroutine=true;
    }

    private void MyMonsterAction(){
        _IsPlayerTurn=false;
        _MyMonster.MyCommandCoroutine();
        
        _IsEnemyTurn=true;
    }

    public void OnClick(){
        AudioS.PlayOneShot(_click);
        MyMonsterAction();
        _RecBottun.SetActive(false);
    }

    private void EnemyAction(){
        _IsPlayerTurn=true;
        _Enemy.EnemyCommandCoroutine();
        
        _IsEnemyTurn=false;
    }
}