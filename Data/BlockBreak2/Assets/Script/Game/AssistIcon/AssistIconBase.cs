using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistIconBase : MonoBehaviour {

	GameObject m_OwnerBlock;    //持ち主のブロック
	protected int m_AssistIconId = -1;

	//============================
	// ゲッター
	//============================

	public GameObject GetOwner() { return m_OwnerBlock; }

	public int GetAssistId(){ return m_AssistIconId; }

	//============================
	// セッター
	//============================

	public void SetIdAndOwner(int _Id,GameObject _Owner) {
		m_AssistIconId = _Id; 
		m_OwnerBlock = _Owner;
	}


	public virtual void Init()
	{

	}


	public virtual void Update()
	{

	}
}
