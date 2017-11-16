using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBallShield : MonoBehaviour {

	const int m_cMaxHp = 5;
	int m_NowHp = 0;

	//===================================
	//	ゲッター
	//===================================

	public int GetNowHp(){ return m_NowHp; }

	//===================================
	//	セッター
	//===================================

	public void SetShield(){ m_NowHp = m_cMaxHp; }

	//
	public void Damage(){
		if (m_NowHp <= 0)return;
		m_NowHp--;

	}

}
