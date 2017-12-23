using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase {

	public GameObject[] m_BlockPrefab;  //ブロックのプレファブ

	SpriteRenderer m_Shadow;
	float m_ShadowAlpha = 1.0f;
    
	public override void Init(int _Hp)
	{
		m_Hp = _Hp;
		for (int Cnt = 0; Cnt < m_BlockPrefab.Length; Cnt++) 
		{
			m_BlockPrefab[Cnt].SetActive(false);
		}


		for(int Cnt = 0;Cnt < m_Hp; Cnt++)
		{
			m_BlockPrefab[Cnt].SetActive(true);
		}
		m_Shadow = GetComponent<SpriteRenderer>();
	}


	void Update()
	{
		if (m_ShadowAlpha <= 0.0f)return;
		if (m_Shadow == null)return;
		var color = m_Shadow.color;
		color.a = m_ShadowAlpha;
		m_Shadow.color = color;

		m_ShadowAlpha -= 0.05f;
	}


	void OnCollisionEnter2D(Collision2D _Other)
	{
		if(_Other.gameObject.tag == "Ball")
		{
			BallBase ballCS = _Other.gameObject.GetComponent<BallBase>();//
			if (ballCS == null){Debug.Log("ボールヌル");return;}
			if (EventManager.GetInstance.GetIsEvent () == true)return;

			int Damage = ballCS.GetAtk();
			BlockDamage (Damage,false);
		}
	}


	public void BlockDamage(int _Damage,bool _isLowCost)
	{

		//スコア加算
		ScoreManager.GetInstance.AddScore(200 * (ScoreManager.GetInstance.GetNowCombo() + 1));

		if (_isLowCost == false) 
		{
			//破壊SE コンボ数に応じてサウンドのピッチを上げる
			float pitch = 1.0f + (float)(ScoreManager.GetInstance.GetNowCombo() * 0.005f);
			SoundManager.GetInstance.PlaySE(0,0.5f,pitch);
			//ヒットエフェクト生成
			EffectManager.GetInstance.CreateEffect((int)EffectType.HIT,transform.position,0.0f);
			//若干カメラを揺らす
			EventManager.GetInstance.CamQuake(0.05f,-0.05f);
		}




		//コンボ加算
		ScoreManager.GetInstance.AddCombo ();
		//今の色のブロックを非表示
		if (m_Hp > 0) {
			m_BlockPrefab[m_Hp - 1].SetActive(false);
		}

		int BeHp = m_Hp;
		m_Hp -= _Damage;

		//範囲外アクセス防止 & ブロックが死んだとき
		if (m_Hp <= 0)
		{
			//
			if ( m_isAssistIcon == true ) 
			{
				AssistManager.GetInstance.Create (m_AssistId,transform.position);
			}

			//瓦礫生成
			if (PlayerPrefs.GetInt("GameMode") != 2) 
			{
				BreakBlockManager.GetInstance.Create(BeHp,transform.position);
				//BreakBlockManager.GetInstance.Create(BeHp,transform.position);
			}


			return;
		}
		//HP減少後に対応したブロックを表示
		m_BlockPrefab[m_Hp - 1].SetActive(true);
	}

	
}
