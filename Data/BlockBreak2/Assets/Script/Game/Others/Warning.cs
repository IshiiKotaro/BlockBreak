using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Warning : MonoBehaviour {

	const float cWarningInterval = 3.0f;	//警告時のアラート間隔
	const float cDangerInterval = 5.0f;		//危険時のアラート間隔

	bool m_isWarning = false;	//警告を出すべきか
	bool m_isPlaySE = false;	//SEを鳴らすべきか		
	float m_Alpha = 0.0f;
	float m_Interval = cWarningInterval;	//警告を出す間隔　デフォルト3.0f 値を大きくするほど間隔が短くなる


	SpriteRenderer m_Sprite;


	void Start()
	{
		m_Sprite = GetComponent<SpriteRenderer> ();
		ZeroAlpha ();
	}


	void Update () 
	{
		WarningCheck();

		if (m_isWarning == false) {ZeroAlpha ();return;}

		Alert ();



	}


	void ZeroAlpha()
	{
		if (m_Sprite == null)return;
		Color col = m_Sprite.color;
		col.a = 0.0f;
		m_Sprite.color = col;
	}


	void WarningCheck()
	{
		//一番進んでるブロック列を見て、危険信号を出すか決める
		BlockMass[] massObjects = GameObject.FindObjectsOfType<BlockMass>();
		int maxIndex = 0;
		foreach (BlockMass Idx in massObjects)
		{
			if (Idx.GetIndex () > maxIndex) 
			{
				maxIndex = Idx.GetIndex();
			}
		}

		int scal = 1;
		if (PlayerPrefs.GetInt ("GameMode") == 2) 
		{
			scal = 2;
		}
		if (maxIndex >= (int)BlockMassIndex.WARNING * scal) 
		{
			m_isWarning = true;
			m_Interval = cWarningInterval;
			if (maxIndex >= (int)BlockMassIndex.DANGER * scal) 
			{
				m_Interval = cDangerInterval;
			}
		}
		else
		{
			m_isWarning = false;
		}
	}


	void Alert()
	{
		m_Alpha = 0.5f * Mathf.Sin(Time.time * m_Interval);
		if (m_Alpha < 0.0f) 
		{
			m_Alpha = 0.0f;
			m_isPlaySE = false;
		}

		if (m_Alpha > 0.0f && m_isPlaySE == false) 
		{
			m_isPlaySE = true;
			SoundManager.GetInstance.PlaySE ((int)SEType.WARNING,1.0f,1.0f);
		}
		if (m_Sprite == null)return;
		Color col = m_Sprite.color;
		col.a = m_Alpha;
		m_Sprite.color = col;

	}

}
