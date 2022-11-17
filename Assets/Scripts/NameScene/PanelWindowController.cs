using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PanelWindowController : MonoBehaviour
{
    [SerializeField] GameObject Panel_Welcome;
    [SerializeField] GameObject Panel_Naming;
    [SerializeField] GameObject Panel_Check;
    [SerializeField] GameObject Panel_ToBattleScene;

    public GameObject MyMonster;

    public MyMonsterStatus myStats;

    [SerializeField] private AudioClip command;
    [SerializeField] private AudioSource AudioS;

    // Start is called before the first frame update
    void Start()
    {
        Panel_Welcome.SetActive(true);
        Panel_Naming.SetActive(false);
        Panel_Check.SetActive(false);
        Panel_ToBattleScene.SetActive(false);

        MyMonster=GameObject.Find("Dragon");
        myStats=MyMonster.GetComponent<MyMonsterStatus>();

    }

    public void WelcomeToNaming(){
        AudioS.PlayOneShot(command);
        Panel_Welcome.SetActive(false);
        Panel_Naming.SetActive(true);

    }

    public void NamingToCheckPanel(){
        AudioS.PlayOneShot(command);
        Panel_Naming.SetActive(false);
        myStats.NameFunc();
        StartCoroutine(DelayCoroutine(5.5f, () =>
        {
            // 3秒後にここの処理が実行される
            Panel_Check.SetActive(true);
        }));
        
    }

    public void CheckToBattle(){
        AudioS.PlayOneShot(command);
        Panel_ToBattleScene.SetActive(true);
        Panel_Check.SetActive(false);
    }

    public void CheckToNaming(){
        AudioS.PlayOneShot(command);
        Panel_Check.SetActive(false);
        Panel_Naming.SetActive(true);
    }

    public void BattleSceneActivate(){
        AudioS.PlayOneShot(command);
        Panel_ToBattleScene.SetActive(false);
        FadeManager.Instance.LoadScene ("BattleScene", 1.0f);
        //シーン遷移関数
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

}
