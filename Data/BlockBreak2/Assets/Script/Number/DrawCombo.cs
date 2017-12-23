using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCombo : MonoBehaviour {

	public GameObject m_pNumPrefab;			//1桁分のデータ

	GameObject[] m_Digit;	//表示する桁数


	int m_DrawNum = 0;
	int m_DrawTime = 80;

	float m_AddPosY = 0.05f;


	void Start () {
		//3桁分データ生成
		m_Digit = new GameObject[3];
		for (int Cnt = 0; Cnt < m_Digit.Length; Cnt++)
		{
			m_Digit[Cnt] = (GameObject)Instantiate(m_pNumPrefab,transform.position,Quaternion.identity);
			m_Digit [Cnt].transform.parent = this.transform;
			m_Digit [Cnt].transform.position = new Vector3 (this.transform.position.x + (float)((Cnt-1) * 0.3),this.transform.position.y,this.transform.position.z);
			NumSingle digitCS = m_Digit[Cnt].GetComponent<NumSingle>();
			digitCS.Init();
		}




		//描画する数値設定
		m_DrawNum = ScoreManager.GetInstance.GetNowCombo();
	}
	

	void Update () {
		//座標更新

		//描画
		int digit = 0;
		bool isDraw = false;
		int saveNum = m_DrawNum;
		for (int cnt = 100; digit < m_Digit.Length; cnt /= 10,digit++)
		{
			if (m_DrawNum >= 1000)continue;
			int nowDigitNum = saveNum / cnt;
			if (nowDigitNum != 0) { isDraw = true; }

			if (isDraw == false)continue;
			//スクリプト取得
			NumSingle digitCS = m_Digit[digit].GetComponent<NumSingle>();
			if (digitCS != null) {
				digitCS.DrawNumSingle(nowDigitNum);
			}
			//今の桁を消す
			saveNum -= (nowDigitNum * cnt);
		}

		//削除チェック
		m_DrawTime--;
		if (m_DrawTime < 0) 
		{
			//子から削除する
			//Destroy(m_Digit[0]);
			//Destroy(m_Digit[1]);
			//Destroy(m_Digit[2]);

			//自身を削除
			//Destroy(gameObject);
		}


	}
}
