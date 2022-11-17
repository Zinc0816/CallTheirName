using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine;

public class MyKeywordRecognizer
{
    private Dictionary<string, Action<string>> mKeyAndActionDic;
    private KeywordRecognizer mKeywordRecognizer;

    public MyKeywordRecognizer()
    {
        mKeyAndActionDic = new Dictionary<string, Action<string>>();
    }

    ~MyKeywordRecognizer()
    {
        Debug.Log("Dispose~");
        Dispose();
    }

    /// <summary>
    /// キーワードの聞き取りを開始するタイミングで呼ぶ
    /// </summary>
    public void Start()
    {
        mKeywordRecognizer = new KeywordRecognizer(mKeyAndActionDic.Select(value => value.Key).ToArray());
        mKeywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        mKeywordRecognizer.Start();
    }

    /// <summary>
    /// キーと対応する処理を登録する
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="action">処理</param>
    public void Add(string key, Action<string> action)
    {
        if (mKeyAndActionDic.ContainsKey(key)) throw new ArgumentException("キーが重複しています");
        mKeyAndActionDic.Add(key, action);
        //Debug.Log(key);
    }

    /// <summary>
    /// 終了時に呼び出す
    /// </summary>
    public void Dispose()
    {
        Debug.Log("Dispose");
        if (mKeywordRecognizer.IsRunning) mKeywordRecognizer.Stop();
        mKeywordRecognizer.Dispose();
    }

    /// <summary>
    /// 一時停止時に呼び出す
    /// </summary>
    public void Stop()
    {
        Debug.Log("Stop");
        if (mKeywordRecognizer.IsRunning) mKeywordRecognizer.Stop();
        
    }

    /// <summary>
    /// 受け取ったキーに対応する処理を呼び出す
    /// </summary>
    /// <param name="args"></param>
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (!mKeyAndActionDic.ContainsKey(args.text)) return;
        mKeyAndActionDic[args.text](args.text);
    }
}