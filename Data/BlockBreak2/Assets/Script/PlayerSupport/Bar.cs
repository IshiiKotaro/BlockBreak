using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//自陣手前のバー
public class Bar : MonoBehaviour {



	//
	int m_NowHp = 0;
	bool m_isActive = false;

	//====================================
	//	ゲッター
	//====================================

	public bool GetIsActive(){ return m_isActive; }

	//====================================
	//	セッター
	//====================================

	public void SetActiveBar()
	{
		m_isActive = true;
		m_NowHp = 5;

	}


	//
	void Start()
	{


	}



	void Update ()
	{
		//アクティブの時、シールドを表示してやる。




	}

	//
	void OnCollisionEnter2D(Collision2D _Other)
	{
		//ボール以外とぶつかった時はスルーする
		if(_Other.gameObject.tag != "Ball")return;

		//
		m_NowHp--;




	}
}
