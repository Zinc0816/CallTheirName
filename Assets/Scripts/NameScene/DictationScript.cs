//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


public class DictationScript : MonoBehaviour
{
    [SerializeField]
    private Text m_Hypotheses;

    [SerializeField]
    private Text m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;
    [SerializeField] public GameObject MyMonster;

    [SerializeField] public MyMonsterStatus myStats;
    private string _name;

    void Start()
    {
        
        //MyMonster=GameObject.Find("Dragon");
        //myStats=MyMonster.GetComponent<MyMonsterStatus>();

        /*
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions.text += text + "\n";
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses.text += text;
            //_name+=text.ToString();
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
        */

        //m_DictationRecognizer.Start();
 
    }

    void InitDictation(){
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        m_DictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        m_DictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        m_DictationRecognizer.DictationError += DictationRecognizer_DictationError;      
    }


    private void DisableDictation() {
        m_DictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
        m_DictationRecognizer.DictationComplete -= DictationRecognizer_DictationComplete;
        m_DictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
        m_DictationRecognizer.DictationError -= DictationRecognizer_DictationError;
        m_DictationRecognizer.Dispose();
    }

    /*
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            m_DictationRecognizer.Start();
            



        } 
        else if(Input.GetKeyUp(KeyCode.Space)){
            m_DictationRecognizer.Stop();
            _name=m_Recognitions.text.ToString();
            myStats.NamingToMonster(m_Recognitions.text.ToString()); 
            Debug.Log("name:"+_name);
                     
        }  
    }
    */

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence) {
        Debug.Log("DictationResult: " + text);
        m_Recognitions.text = text ;
    }
 
    private void DictationRecognizer_DictationHypothesis(string text) {
        Debug.Log("DictationHypothesis: " + text);
    }
 
    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause) {
        Debug.Log("DictationComplete: " + cause);
    }
 
    private void DictationRecognizer_DictationError(string error, int hresult) {
        Debug.Log("DictationError: " + error);
    }

    public void Naming(){
        StartCoroutine("NamingCoroutine");
    }


    IEnumerator NamingCoroutine(){
        this.InitDictation();
        m_DictationRecognizer.Start();

        yield return new WaitForSeconds(5);
        m_DictationRecognizer.Stop();
        _name=m_Recognitions.text.ToString();//_nameに認識結果が入る
        //_name=m_DictationRecognizer.DictationResult.text;
        myStats.NamingToMonster(m_Recognitions.text.ToString()); 
        //m_DictationRecognizer.Stop();
        m_DictationRecognizer.Dispose();
        //Debug.Log("Dipose");
    }
    
}