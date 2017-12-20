using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour {

	public GameObject[] m_pBreakBlockPrefab;
	int m_NowTime = 0;
	Vector3 m_MoveVec;
	int m_AddAng;				//1フレームの回転角度　Randomを使用するためint型　なのでキャスト必須
	int m_Ang = 0;


	// Use this for initialization
	void Start () {
		for(int Cnt = 0;Cnt < m_pBreakBlockPrefab.Length;Cnt++)
		{
			m_pBreakBlockPrefab [Cnt].SetActive (false);
		}
		m_pBreakBlockPrefab [0].SetActive (true);
	}

	//瓦礫初期化　Hp:死亡直前のブロックHP
	public void Init(int _Hp)
	{
		_Hp--;
		if (_Hp <= 0)_Hp = 0;


		m_pBreakBlockPrefab [_Hp].SetActive (true);

		//移動ベクトルはランダムで決定
		m_MoveVec = Quaternion.Euler(0.0f,0.0f,(float)Random.Range(-30,30)) * (Vector3.up * 3);

		//回転角度もある程度ランダムで決定
		m_AddAng = Random.Range(10,30);
	}

	
	// Update is called once per frame
	void Update () {
		
		//回転
		//transform.rotation = Quaternion.Euler(0,0,(float)m_Ang);
		//transform.localRotation = Quaternion.Euler(0,0,(float)m_Ang);
		//m_Ang += m_AddAng;

		//移動
		transform.Translate(m_MoveVec * Time.deltaTime);
		m_MoveVec.y -= 0.1f;


		m_NowTime++;
		if (m_NowTime >= 60) 
		{
			Destroy (gameObject);
		}
	}
}
