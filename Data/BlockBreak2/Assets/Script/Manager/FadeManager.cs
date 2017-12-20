using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager :SingletonMonoBehaviour<FadeManager>{


	public GameObject[] m_pBlockData;

	const float cWidthInterval = 0.95f;
	const float cHeightInterval = 0.77f;

	//この辺ひとまとめにする
	List<GameObject> m_BlockList;	//フェードに使用するブロックリスト 7*12列用意する
	List<float> m_AddYList;			//加算値
	List<int>   m_MoveStartTimeList;//移動開始時間
	List<bool>  m_isMoveList;
	List<Vector3> m_BlockPosList;

	bool m_isFadeOut;				//フェードアウトしてるか
	bool m_isFadeIn;				//フェードインしてるか
	int m_NowTime;
	bool m_isFadeEnd;


	//=====================================
	//	ゲッター
	//=====================================

	public bool GetIsFadeEnd(){ return m_isFadeEnd; }


	//=====================================
	//	セッター
	//=====================================

	public void SetFadeEndReset(){ m_isFadeEnd = false; }

	void SetBlockAlpha(float _Alpha)
	{
		for (int cnt = 0; cnt < m_AddYList.Count; cnt++) 
		{
			SpriteManager.GetInstance.SetSpriteAlpha (m_BlockList[cnt],_Alpha);
		}
	}


	//
	public void Awake()
	{
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	//初期化　画像の生成と初期座標の設定を行う。
	public void AllInit()
	{
		m_BlockList = new List<GameObject> ();
		m_AddYList = new List<float> ();
		m_MoveStartTimeList = new List<int> ();
		m_isMoveList = new List<bool> ();
		m_BlockPosList = new List<Vector3> ();

		for (int cnt = 0; cnt < 7 * 14; cnt++)
		{
			int rand = Random.Range (0,m_pBlockData.Length - 1);
			GameObject block = (GameObject)Instantiate(
				m_pBlockData[rand],
				Vector3.zero,
				Quaternion.identity
			);
			block.transform.parent = this.transform;


			float xPos = (float)(cnt % 7);
			xPos -= 3.0f;
			xPos *= cWidthInterval;
			float yPos = (float)(cnt / 7);
			yPos *= cHeightInterval;
			yPos -= 4.7f;
			block.transform.position = new Vector3(xPos,yPos,0.0f);
			//描画順番を設定
			SpriteRenderer sprite = block.GetComponent<SpriteRenderer>();
			sprite.sortingOrder = -cnt / 7;


			m_BlockList.Add (block);

			//座標のバックアップを記憶
			Vector3 pos = block.transform.position;
			m_BlockPosList.Add (pos);


			//初期加算値を設定
			float data = 0.0f;
			m_AddYList.Add (data);

			//移動開始時間を設定
			int time = 0;
			m_MoveStartTimeList.Add (time);

			bool isMove = false;
			m_isMoveList.Add (isMove);
		}
		m_isFadeIn = false;
		m_isFadeOut = false;
		m_NowTime = 0;
		m_isFadeEnd = false;

		//BlockPosUpdate();

		SetBlockAlpha (0.0f);
	}


	//フェードイン初期化　画面を段々見えるようにする。
	public void FadeInInit()
	{
		m_isFadeIn = true;
		m_isFadeOut = false;
		m_NowTime = 0;

		for (int cnt = 0; cnt < m_AddYList.Count; cnt++) 
		{
			m_AddYList [cnt] = 0.0f;
			m_MoveStartTimeList [cnt] = (cnt);
			m_isMoveList [cnt] = false;
		}
		BlockPosUpdate();

		SetBlockAlpha (1.0f);

	}

	//フェードアウト初期化　画面を段々見えなくする
	public void FadeOutInit()
	{
		m_isFadeIn = false;
		m_isFadeOut = true;
		m_NowTime = 0;

		for (int cnt = 0; cnt < m_AddYList.Count; cnt++) 
		{
			m_AddYList [cnt] = 10.0f;
			m_MoveStartTimeList [cnt] = (cnt);
			m_isMoveList [cnt] = false;
		}
		BlockPosUpdate();

		SetBlockAlpha (1.0f);

	}


	void Update()
	{
		FadeInUpdate ();

		FadeOutUpdate ();

		BlockPosUpdate ();
	}


	void FadeInUpdate()
	{
		if (m_isFadeIn == false)return;
		TimeUpdate ();

		for (int cnt = 0; cnt < m_BlockList.Count; cnt++)
		{
			if (m_isMoveList [cnt] == false)continue;
			m_AddYList [cnt] -= 0.02f;
			m_AddYList [cnt] *= 1.2f;
			if (m_AddYList [cnt] < -16.0f)m_AddYList [cnt] = -16.0f;
		}
		if (m_AddYList [m_AddYList.Count - 1] <= -15.0f) 
		{
			m_isFadeIn = false;
			m_isFadeEnd = true;
		}
	}


	void FadeOutUpdate()
	{
		if (m_isFadeOut == false)return;
		TimeUpdate ();
		for (int cnt = 0; cnt < m_BlockList.Count; cnt++)
		{
			if (m_isMoveList [cnt] == false)continue;
			m_AddYList [cnt] *= 0.8f;
		}
		if (m_AddYList [m_AddYList.Count - 1] <= 0.1f) 
		{
			m_isFadeOut = false;
			m_isFadeEnd = true;
		}
	}


	void TimeUpdate()
	{
		for (int cnt = 0; cnt < m_BlockList.Count; cnt++)
		{
			if (m_MoveStartTimeList [cnt] == m_NowTime) 
			{
				m_isMoveList [cnt] = true;
			}
		}
		m_NowTime++;
	}


	void BlockPosUpdate()
	{
		for (int cnt = 0; cnt < m_BlockList.Count; cnt++) 
		{
			m_BlockList [cnt].transform.position = new Vector3 (m_BlockPosList[cnt].x,m_BlockPosList[cnt].y + m_AddYList[cnt],m_BlockPosList[cnt].z);
		}
	}

}
