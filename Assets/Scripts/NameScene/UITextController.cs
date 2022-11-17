using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{
    [SerializeField] private string _str1;
    [SerializeField] private string _str2;
    [SerializeField] private GameObject _MassageText;
    [SerializeField] public GameObject _MyMonster;
    public MyMonsterStatus myStats;

    public void OnEnable() {
        _MyMonster=GameObject.Find("Dragon");
        myStats=_MyMonster.GetComponent<MyMonsterStatus>();
        string name=myStats.ReturnName();
        _MassageText.GetComponent<Text>().text=_str1+"\n"+name+"\n"+_str2;
    
    }
}
