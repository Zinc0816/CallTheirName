using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : SingletonMonoBehaviour<TurnManager> {

	public bool startLoopOnStart;

	// 登録されたCommander
	readonly List<Commander> m_Commanders = new List<Commander>();

	// 保留中のCommander
	readonly HashSet<Commander> m_PendingCommanders = new HashSet<Commander>();

	void Start () {
		if (startLoopOnStart) {
			StartLoop();
		}
	}

	public void StartLoop () {
		StartCoroutine(Loop());
	}

	IEnumerator Loop () {
		while (true) {
			// 保留中のCommanderをループに追加する
			if (m_PendingCommanders.Count > 0) {
				foreach (Commander commander in m_PendingCommanders.ToArray()) {
					if (commander) {
						m_Commanders.Add(commander);
					}
				}
				m_PendingCommanders.Clear();
			}

			// ターンを回す
			foreach (Commander commander in OrderedCommanders().ToArray()) {
				if (commander == null) {
					m_Commanders.Remove(commander);
					continue;
				}

				if (commander.BeginTurn()) {
					while ((commander != null) && commander.IsTurn) {
						yield return null;
					}
				}
			}
			yield return null;
		}
	}

	// 登録されたCommanderを優先度順に並び替たシーケンスを返す
	IEnumerable<Commander> OrderedCommanders () {
		return m_Commanders
			.Where(c => c != null)
			.OrderByDescending(c => c.Priority);
	}

	// Commanderを仮登録する
	public bool AddCommander (Commander commander) {
		if (commander == null) {
			throw new ArgumentNullException(nameof(commander));
		}
		return m_PendingCommanders.Add(commander);
	}

	// Commanderをループから削除する
	public bool RemoveCommander (Commander commander) {
		m_Commanders.Remove(commander);
		return m_PendingCommanders.Remove(commander);
	}
}