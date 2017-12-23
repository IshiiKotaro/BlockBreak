using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour {
  




    protected bool m_IsEnemy = false; //敵を保有してるか
	protected bool m_isAssistIcon = false;
	protected int m_AssistId = -1;

	protected int m_Hp;               //ブロックのHP




    //===================
    //ゲッター
    //===================

    public int GetHp() { return m_Hp;  }

    public bool GetIsEnemy() { return m_IsEnemy;  }

	public bool GetIsAssistIcon(){ return m_isAssistIcon; }
	public int GetAssistId(){ return m_AssistId; }

	public Vector3 GetPos(){return transform.position;} 

    //===================
    // セッター
    //===================

    public void SetIsEnemy(bool _Enemy) { m_IsEnemy = _Enemy; }

	public void SetIsAssistIcon(bool _Assist){ m_isAssistIcon = _Assist; }
	public void SetAssistId(int _Assist){ m_AssistId = _Assist; }

    void Start()
    {

    }



    public virtual void Init(int _Hp)
    {
		m_Hp = _Hp;
    }






}
