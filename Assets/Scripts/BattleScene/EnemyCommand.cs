using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyCommand : MonoBehaviour
{
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
    [SerializeField] private GameObject _RecBottun;

    [SerializeField] private GameObject _MyTurn; 
    [SerializeField] private GameObject _EnemyTurn; 

    [SerializeField] private AudioSource AudioS;
    [SerializeField] private AudioClip _tackle;
    [SerializeField] private AudioClip _dance;
    [SerializeField] private AudioClip _gard;

    // Start is called before the first frame update
    void Start()
    {
        _material = _renderer.material;
        _MyMonsterStatus=_MyMonster.GetComponent<Status>();
        _EnemyStatus=_Enemy.GetComponent<Status>();
        
    }


    private void Taiatari()
    {
        
        Debug.Log("Taiatari");
        _Enemy.transform.DOLocalMove(new Vector3(0f,0f,-3f), 0.5f)
                        .SetRelative()
                        .SetEase(Ease.InOutElastic)
                        .SetLoops(2,LoopType.Yoyo);

        _MyMonster.transform.DOLocalMove(new Vector3(0f,0f,-1f), 0.1f)
                        .SetRelative()
                        .SetEase(Ease.InOutElastic)
                        .SetDelay(1.5f)
                        .SetLoops(2,LoopType.Yoyo);
        AudioS.PlayOneShot(_tackle);
        _MyMonsterStatus.GetDamage();
        
        

    }
    private void Enbu()
    {
        
        _Enemy.transform.DOJump(new Vector3(0f,0f,0f), 2.0f,3,2f)
                        .SetRelative()
                        .SetDelay(0.2f);
        AudioS.PlayOneShot(_dance);
        _EnemyStatus.DanceFlag=true;
        
            
                
    }

    private void Bougyo()
    {
        
        var seq=DOTween.Sequence();
        seq.Append(_Enemy.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 1.0f)
                        .SetDelay(0.2f)
        );
        seq.Append(_Enemy.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 1.0f)
        );
        AudioS.PlayOneShot(_gard);
        var color=Color.red;
        seq.Append(_material.DOColor(Color.red,0.4f));//赤に明滅
        seq.Append(_material.DOColor(Color.white,0.4f));
        _EnemyStatus.GardFlag=true;
        
    

    }

    IEnumerator EnemyCommandStart(){
        //StartCommand();
        yield return new WaitForSeconds(0.5f);
        int x=Random.Range(0,100);
        if(x<50){
            Taiatari();
        }
        else if(x>=50 && x<75){
            Enbu();
        }
        else {
            Bougyo();
        }
        yield return new WaitForSeconds(1.0f);
        _MyMonsterStatus.HPSlider.value=_MyMonsterStatus._HP;
        yield return new WaitForSeconds(1.0f);
        _RecBottun.SetActive(true);
        _EnemyTurn.SetActive(false);
        _MyTurn.SetActive(true);

        //StopCommand();
    }

    public void EnemyCommandCoroutine(){
        StartCoroutine(EnemyCommandStart());
    }


}
