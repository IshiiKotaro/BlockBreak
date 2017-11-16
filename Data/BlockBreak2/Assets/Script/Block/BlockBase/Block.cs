using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase {

	public GameObject[] m_BlockPrefab;  //ブロックのプレファブ
	GameObject m_Enemy;
    
	public override void Init(int _Hp)
	{
		m_Hp = _Hp;
		for(int Cnt = 0;Cnt < m_Hp; Cnt++)
		{
			m_BlockPrefab[Cnt].SetActive(true);
		}
	}


	void OnCollisionEnter2D(Collision2D _Other)
	{
		Debug.Log("Blockクラスのあたり判定");

		if(_Other.gameObject.tag == "Ball")
		{
			BallBase ballCS = _Other.gameObject.GetComponent<BallBase>();//
			if (ballCS == null){Debug.Log("ボールヌル");return;}

			//今の色のブロックを非表示
			if (m_Hp > 0) {
				m_BlockPrefab[m_Hp - 1].SetActive(false);
			}
			m_Hp -= ballCS.GetAtk();

			//範囲外アクセス防止 & ブロックが死んだとき
			if (m_Hp <= 0)
			{
				if(m_IsEnemy == true)
				{
					//50%の確率でアイテムドロップ
					if(Random.Range(0, 100) <= 50)
					{
						ItemManager.GetInstance.Create(Random.Range(0,6), transform.position);
					}
				}


				//エフェクト生成
				EffectManager.GetInstance.CreateEffect (0, transform.position);


				return;
			}
			//HP減少後に対応したブロックを表示
			m_BlockPrefab[m_Hp - 1].SetActive(true);
		}

		//

	}




	
}
