//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] GameObject _panelWindow;
    [SerializeField] GameObject _panelWindowController;
    private PanelWindowController _pWC;
    [SerializeField] int CheckID;

    // [SerializeField] private AudioClip command;
    // [SerializeField] private AudioSource AudioS;

    /*
    void Start(){
        _pWC=_panelWindowController.GetComponent<PanelWindowController>();
    }
    */

    public void OnClick()
    {
        
        _pWC=_panelWindowController.GetComponent<PanelWindowController>();
        //Debug.Log("押された!");  // ログを出力
        if(_panelWindow.name=="Panel_Welcome"){
            _pWC.WelcomeToNaming();
        }
        else if(_panelWindow.name=="Panel_Naming"){
            Debug.Log("Naming");
            _pWC.NamingToCheckPanel();
        }
        else if(_panelWindow.name=="Panel_Check" && CheckID==0){
            _pWC.CheckToBattle();
        }
        else if(_panelWindow.name=="Panel_Check" && CheckID==1){
            _pWC.CheckToNaming();
        }
        else if(_panelWindow.name=="Panel_ToBattleScene"){
            _pWC.BattleSceneActivate();
        }
        //AudioS.PlayOneShot(command);
    }
}
