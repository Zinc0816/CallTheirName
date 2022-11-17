
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using DG.Tweening;

public class MyMonsterCommand : MonoBehaviour
{
    private string[]  m_myname= new string[1];//="こたろう";//仮称
    //private string[]  _debug= new string[1];
    //private const string CONST_NAME;
    //音声コマンドのキーワード
    private string[] m_Keywords = { "たいあたり", "えんぶ", "ぼうぎょ" };

    //private KeywordRecognizer m_Recognizer;
    [SerializeField] private MyKeywordRecognizer mKeywordRecognizer;

    //右手の位置ターゲット
    [SerializeField] private GameObject _MyMonster;
    private Status _MyMonsterStatus;

    //左手の位置ターゲット
    [SerializeField] private GameObject _Enemy;
    private Status _EnemyStatus;

    /// <summary> マテリアルの色パラメータのID </summary>
    private static readonly int PROPERTY_COLOR = Shader.PropertyToID("_Color");

    /// <summary> モデルのRenderer </summary>
    [SerializeField]
    private Renderer _renderer;

    /// <summary> モデルのマテリアルの複製 </summary>
    private Material _material;
    private bool _called=false;

    [SerializeField] private GameObject _CallIcon;
    [SerializeField] private GameObject _MicIcon;
    //[SerializeField] private Commander _commander;

    [SerializeField] private AudioSource AudioS;
    [SerializeField] private AudioClip _tackle;
    [SerializeField] private AudioClip _dance;
    [SerializeField] private AudioClip _gard;

    void Start()
    {
        _material = _renderer.material;
        _MyMonsterStatus=_MyMonster.GetComponent<Status>();
        _EnemyStatus=_Enemy.GetComponent<Status>();
        //_commander.OnBeginTurn+=StartCommand();
        //mKeywordRecognizer = new MyKeywordRecognizer();
    }
 
    private void StartCommand(){
        m_myname[0]=MyMonsterStatus._myName;
        //_debug[0]="デバッグ";
        mKeywordRecognizer = new MyKeywordRecognizer();
        //Debug.Log(m_myname[0]);
        mKeywordRecognizer.Add(m_myname[0], CalledJudge);
        mKeywordRecognizer.Add(m_Keywords[0], Taiatari);
        mKeywordRecognizer.Add(m_Keywords[1], Enbu);
        mKeywordRecognizer.Add(m_Keywords[2], Bougyo);

        mKeywordRecognizer.Start();

    }
    
    private void StopCommand()
    {
        mKeywordRecognizer.Dispose();
    }    

    IEnumerator MyCommand(){
        StartCommand();
        _MicIcon.SetActive(true);
        Debug.Log("MyCommandStart");
        yield return new WaitForSeconds(7.0f);
        StopCommand();
        Debug.Log("MyCommandStop");
        _CallIcon.SetActive(false);
        _MicIcon.SetActive(false);        
        yield return new WaitForSeconds(1.0f);
        _EnemyStatus.HPSlider.value=_EnemyStatus._HP;

    }

    public void MyCommandCoroutine(){
        StartCoroutine(MyCommand());
    }

    private void CalledJudge(string text){
        _called=true;
        _CallIcon.SetActive(true);
        Debug.Log("called");
    }

    private void Taiatari(string text)
    {
        if(_called==true){
            Debug.Log("Taiatari");
            _MyMonster.transform.DOLocalMove(new Vector3(0f,0f,3f), 0.5f)
                            .SetRelative()
                            .SetEase(Ease.InOutElastic)
                            .SetLoops(2,LoopType.Yoyo);
            

            _Enemy.transform.DOLocalMove(new Vector3(0f,0f,1f), 0.1f)
                            .SetRelative()
                            .SetEase(Ease.InOutElastic)
                            .SetDelay(1.5f)
                            .SetLoops(2,LoopType.Yoyo);
            AudioS.PlayOneShot(_tackle);
                    
            _called=false;
            _EnemyStatus.GetDamage();

            _CallIcon.SetActive(false);
            _MicIcon.SetActive(false);
            //StopCommand();
        }

    }
    private void Enbu(string text)
    {
        if(_called==true){
            _MyMonster.transform.DOJump(new Vector3(0f,0f,0f), 2.0f,3,2f)
                            .SetRelative()
                            .SetDelay(0.2f);
            AudioS.PlayOneShot(_dance);
            _called=false; 
            _MyMonsterStatus.DanceFlag=true;
            _CallIcon.SetActive(false);
            _MicIcon.SetActive(false);
            //StopCommand();
        }           
    }

    private void Bougyo(string text)
    {
        if(_called==true){
            var seq=DOTween.Sequence();
            seq.Append(_MyMonster.transform.DOScale(new Vector3(0.9f,0.9f,0.9f), 1.0f)
                            .SetDelay(0.2f)
            );
            seq.Append(_MyMonster.transform.DOScale(new Vector3(0.5f,0.5f,0.5f), 1.0f)
            );
            AudioS.PlayOneShot(_gard);
            var color=Color.red;
            seq.Append(_material.DOColor(Color.red,0.4f));//赤に明滅
            seq.Append(_material.DOColor(Color.white,0.4f));
            _called=false;
            _MyMonsterStatus.GardFlag=true;

            _CallIcon.SetActive(false);
            _MicIcon.SetActive(false);
            //StopCommand();
        }

    }


}
