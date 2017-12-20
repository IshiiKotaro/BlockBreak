using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartMessage : MonoBehaviour {

	public GameObject[] m_pReadySprite;	//5
	public GameObject[] m_pGoSprite;	//2

	GameObject[] m_ReadySprite;			//5
	GameObject[] m_GoSprite;			//2

	Vector2[] m_ReadyPos;
	Vector2[] m_ReadyBreakPos;
	Vector2[] m_ReadyBreakVec;
	float[]	m_ReadyAddY;
	bool[] m_isReadyDown;

	bool[] m_isGoMove;
	float m_GoScal;

	int m_Time = 0;
	int m_Index = 0;

	bool m_isStart = false;




	public bool GetIsStart(){ return m_isStart; }



	// Use this for initialization
	void Start ()
	{
		
	}


	public void Init()
	{
		m_ReadySprite = new GameObject[m_pReadySprite.Length];

		m_GoSprite = new GameObject[m_pGoSprite.Length];

		m_ReadyPos = new Vector2[m_pReadySprite.Length];
		m_ReadyBreakPos = new Vector2[m_pReadySprite.Length];
		m_ReadyBreakVec = new Vector2[m_pReadySprite.Length];
		m_ReadyAddY = new float[m_pReadySprite.Length];
		m_isReadyDown = new bool[m_pReadySprite.Length];

		m_isGoMove = new bool[m_pGoSprite.Length];

		//Ready初期化
		for (int cnt = 0; cnt < m_ReadyPos.Length; cnt++) 
		{
			//オブジェクトの生成
			m_ReadySprite[cnt] =  (GameObject)Instantiate(
				m_pReadySprite[cnt], 
				new Vector3(transform.position.x + (float)((cnt - 2)  * 1.3f), m_ReadyPos[cnt].y + m_ReadyBreakPos[cnt].y + 10.0f, 0.0f), 
				Quaternion.identity
			);
			m_ReadyPos [cnt].x = (float)cnt - 2.0f;
			m_ReadyPos [cnt].y = 0.0f;

			m_ReadyBreakPos [cnt].x = 0.0f;
			m_ReadyBreakPos [cnt].y = 0.0f;

			m_ReadyAddY[cnt] = 10.0f;
			m_isReadyDown [cnt] = false;
		}

		//Go初期化
		for (int cnt = 0; cnt < m_pGoSprite.Length; cnt++) 
		{
			m_isGoMove [cnt] = false;

			m_GoSprite [cnt] = (GameObject)Instantiate (
				m_pGoSprite[cnt], 
				transform.position,
				Quaternion.identity
			);
			SpriteManager.GetInstance.SetSpriteAlpha (m_GoSprite[cnt],0.0f);
		}
		m_GoScal = 5.0f;


		m_Time = 0;
		m_Index = 0;
	}

	
	public void StartMessage()
	{
		m_Time++;
		if (m_Time % 10 == 0 && m_Index < m_ReadySprite.Length) 
		{
			//SE
			SoundManager.GetInstance.PlaySE ((int)SEType.SWING,1.0f,1.0f);

			m_isReadyDown [m_Index] = true;
			m_Index++;
		}

		//Readyの移動
		for (int cnt = 0; cnt < m_ReadySprite.Length; cnt++)
		{
			if (m_isReadyDown [cnt] == false)continue;
			m_ReadySprite [cnt].transform.position = new Vector3 (m_ReadyPos[cnt].x + m_ReadyBreakPos[cnt].x,m_ReadyPos[cnt].y + m_ReadyBreakPos[cnt].y + m_ReadyAddY[cnt],0.0f);
			m_ReadyAddY[cnt] *= 0.85f;
		}

		//Go移動
		if (m_Time == 100) 
		{
			m_isGoMove[0] = true;
			SpriteManager.GetInstance.SetSpriteAlpha (m_GoSprite[0],1.0f);
		}
		if (m_isGoMove [0] == false)return;

		m_GoSprite [0].transform.localScale = new Vector3(1.0f + m_GoScal,1.0f + m_GoScal,1.0f);
		m_GoScal *= 0.9f;

		//GO!
		if (m_GoScal <= 0.1f) 
		{


			SpriteManager.GetInstance.SetSpriteAlpha (m_GoSprite[0],0.0f);
			SpriteManager.GetInstance.SetSpriteAlpha (m_GoSprite[1],1.0f);
			for(int cnt = 0; cnt < m_ReadySprite.Length; cnt++)
			{
				m_ReadyBreakVec[cnt].x = (float)(cnt - 2)*0.1f;
				m_ReadyBreakVec[cnt].y = 0.3f;
				m_ReadyBreakPos[cnt].x += m_ReadyBreakVec[cnt].x;
				m_ReadyBreakPos[cnt].y += m_ReadyBreakVec[cnt].y;
			}
		}

		if (m_Time >= 200) 
		{
			SpriteManager.GetInstance.SetSpriteAlpha (m_GoSprite[0],0.0f);
			SpriteManager.GetInstance.SetSpriteAlpha (m_GoSprite[1],0.0f);
			for (int cnt = 0; cnt < m_ReadySprite.Length; cnt++) 
			{
				SpriteManager.GetInstance.SetSpriteAlpha (m_ReadySprite[cnt],0.0f);
				m_ReadyBreakPos [cnt].x = 0.0f;
				m_ReadyBreakPos [cnt].y = 0.0f;

				m_ReadyBreakVec[cnt].x = 0.0f;
				m_ReadyBreakVec[cnt].y = 0.0f;
			}

			m_isStart = true;
		}



	}





}
