//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class MyMonsterStatus : MonoBehaviour
{

    GameObject DictationManager;
    public static string _myName; 
    public static StringBuilder x;

    // Start is called before the first frame update
    void Awake()
    {
        if(_myName!=null){
            _myName=null;
            //名づけ関数
        }
        else{
            //名づけ関数
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NamingToMonster(string _name)
    {
        _myName=_name;
        //_myName.TrimEnd();
        //_myName=_myName.Replace("/n","");
        // x=new StringBuilder();
        // for(int i=0;i<_myName.Length;i++)
        // {
        //     if(i>0) x.AppendLine();
        //     x.Append(_myName[i]);
        // }
        // _myName=x.ToString();
        Debug.Log("My Name is "+_myName+"!");
        //Debug.Log(_myName.EndsWith("\n"));

    }

    public string ReturnName(){
        return _myName;
    }

    public void NameFunc(){
        //Debug.Log("NameFunc");
        if(_myName!=null){
            _myName=null;
            //名づけ関数
        }
        DictationManager=GameObject.Find("DictationManager");
        DictationManager.GetComponent<DictationScript>().Naming();
    }
    
}
