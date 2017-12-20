using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum YesNo:int
{
	YES = 0,
	NO,
	MAX,
}


enum ShopNo
{
	THUNDER,
	CROSSLASER,
	METEOR,
	VECTORUP,
	SHOT4WAY,
	TMP,
	MAX,
}



public class Shop : MonoBehaviour{

	public Text m_pMoney;
	public Image[] m_pText;
	public Button[] m_pButton;

	Vector2 m_Pos;
	int m_NowSelect = 0;

	void Start()
	{
		m_NowSelect = 0;
		PosReset ();
	}


	void Update()
	{
		int MyMoney = PlayerPrefs.GetInt ("Money");
		m_pMoney.text = "Money:" + MyMoney;




	}


	public void OnItemClicked(int _Id)
	{
		//テキストとYESNOボタンを出す
		m_pText [_Id].transform.position = new Vector3 (m_Pos.x,m_Pos.y,0.0f);
		m_pButton [0].transform.position = new Vector3 (m_Pos.x -60.0f,m_Pos.y -70.0f,1.0f);
		m_pButton [1].transform.position = new Vector3 (m_Pos.x + 60.0f,m_Pos.y -70.0f,1.0f);

		m_NowSelect = _Id;
	}


	public void YesClicked()
	{
		int cost = new int();
		switch(m_NowSelect)
		{
		case (int)ShopNo.THUNDER:
			cost = 300;
			break;
		case (int)ShopNo.CROSSLASER:
			cost = 700;
			break;
		case (int)ShopNo.METEOR:
			cost = 1000;
			break;
		case (int)ShopNo.VECTORUP:
			cost = 50;
			break;
		case (int)ShopNo.SHOT4WAY:
			cost = 300;
			break;
		case (int)ShopNo.TMP:
			cost = 300;
			break;
		}
		int MyMoney = PlayerPrefs.GetInt ("Money");
		if (MyMoney < cost) 
		{
			Debug.Log ("お金が足りません");
			PosReset ();
			return;
		}

		switch(m_NowSelect)
		{
		case (int)ShopNo.THUNDER:
			PlayerPrefs.SetInt ("Thunder",1);
			break;
		case (int)ShopNo.CROSSLASER:
			PlayerPrefs.SetInt ("CrossLaser",1);
			break;
		case (int)ShopNo.METEOR:
			PlayerPrefs.SetInt ("Meteor",1);
			break;
		case (int)ShopNo.VECTORUP:
			PlayerPrefs.SetInt ("VectorUp",1);
			break;
		case (int)ShopNo.SHOT4WAY:
			PlayerPrefs.SetInt ("Shot4Way",1);
			break;
		case (int)ShopNo.TMP:
			PlayerPrefs.SetInt ("Tmp",1);
			break;
		}

		MyMoney -= cost;
		PlayerPrefs.SetInt("Money",MyMoney);

		PosReset ();
	}


	public void NoClicked()
	{
		PosReset ();
	}


	void PosReset()
	{
		m_Pos.x = 164.0f;
		m_Pos.y = 246.5f;
		foreach (Image Idx in m_pText)
		{
			Idx.transform.position = new Vector3(-100,-100,0);
		}
		foreach (Button Idx in m_pButton)
		{
			Idx.transform.position = new Vector3(-100,-100,1);
		}
	}


	public void OnBackModeClicked()
	{
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("ModeSelect");
	}

}
