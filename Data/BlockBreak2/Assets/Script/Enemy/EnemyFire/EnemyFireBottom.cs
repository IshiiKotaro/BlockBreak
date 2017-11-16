using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBottom : EnemyBase {

	public int m_pFireInterval;	//炎吐き出す間隔
	const int m_FireShotTime = 20;//炎吐き出す時間

	int m_NowTime = 0;
	int m_NowFireTime = 0;

	void Update () {
		m_NowTime++;

		//火炎放射
		if(m_NowTime >= m_pFireInterval)
		{
			EnemyAtkManager.GetInstance.Create(1, transform.position,transform.gameObject);
			m_NowFireTime++;
			if(m_NowTime >= m_pFireInterval + m_FireShotTime)
			{
				m_NowTime = 0;
			}

		}



	}
}
