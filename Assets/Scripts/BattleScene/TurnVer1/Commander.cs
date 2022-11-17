using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Commander : MonoBehaviour {

	[SerializeField]
	int m_Priority;

	public int Priority { get => m_Priority; set => m_Priority = value; }

	public bool IsTurn { get; private set; }

	public event Action<Commander> OnBeginTurn;

	//public event Action<Commander> OnEndTurn;

	void OnEnable () {
		TurnManager.Instance.AddCommander(this);
	}

	void OnDisable () {
		TurnManager.Instance.RemoveCommander(this);
	}

	// ターンを開始する関数の最低限のコード。
	// ゲームによっては「HPが0ならスキップ」みたいな処理を入れる。
	public bool BeginTurn () {
		if (IsTurn) {
			return false;
		}
		IsTurn = true;

		OnBeginTurn?.Invoke(this);//OnBeginTurnがnullでないとき、OnBeginTurnに登録されたイベント(各関数)にこのクラスを代入

		return true;
	}

	public void EndTurn () {
		//OnEndTurn?.Invoke(this);
		IsTurn = false;
	}

}