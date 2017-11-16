using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour {
  




    protected bool m_IsEnemy = false; //敵を保有してるか
	protected int m_Hp;               //ブロックのHP

    //===================
    //ゲッター
    //===================

    public int GetHp() { return m_Hp;  }

    public bool GetIsEnemy() { return m_IsEnemy;  }


    //===================
    // セッター
    //===================

    public void SetIsEnemy(bool _Enemy) { m_IsEnemy = _Enemy; }


    void Start()
    {

    }



    public virtual void Init(int _Hp)
    {
		m_Hp = _Hp;
    }






}
