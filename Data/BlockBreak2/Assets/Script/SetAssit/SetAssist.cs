using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAssist : MonoBehaviour {

	Vector3 m_DownPos;
	public GameObject[] m_pMatPrefab;
	GameObject[] m_MatList;

	void Start()
	{
		int m_Cnt = 0;
		if (PlayerPrefs.GetInt ("VectorUp") == 1) 
		{
			m_Cnt++;
		}
		if (PlayerPrefs.GetInt ("Shot4Way") == 1) 
		{
			m_Cnt++;
		}
		//
		m_MatList = new GameObject[m_Cnt];
		for (int cnt = 0; cnt < m_MatList.Length; cnt++) 
		{
			m_MatList[cnt] = (GameObject)Instantiate(
				m_pMatPrefab[cnt],
				transform.position,
				Quaternion.identity
			);
		}

		OnResetClicked ();




	}

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Fire1") == false)return;



		//アシストマットを移動
		m_DownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//マットに触っているか。
		for (int cnt = 0; cnt < m_MatList.Length; cnt++) 
		{
			if (m_DownPos.x > m_MatList [cnt].transform.position.x - 0.5f && m_DownPos.x < m_MatList[cnt].transform.position.x + 0.5f &&
				m_DownPos.y > m_MatList [cnt].transform.position.y - 0.5f && m_DownPos.y < m_MatList [cnt].transform.position.y + 0.5f)
			{
				m_MatList[cnt].transform.position = new Vector3 (m_DownPos.x,m_DownPos.y,0.0f);
			}
		}
		if (m_MatList.Length >= 1)
		{
			PlayerPrefs.SetFloat("Item1X",m_MatList [0].transform.position.x);
			PlayerPrefs.SetFloat("Item1Y",m_MatList [0].transform.position.y);
		}

		if (m_MatList.Length >= 2) 
		{
			PlayerPrefs.SetFloat ("Item2X", m_MatList [1].transform.position.x);
			PlayerPrefs.SetFloat ("Item2Y", m_MatList [1].transform.position.y);
		}
	}


	public void OnBackToModeSelectClicked()
	{
		//
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("ModeSelect");
	}


	public void OnResetClicked()
	{
		PlayerPrefs.SetFloat("Item1X",-2.0f);
		PlayerPrefs.SetFloat("Item1Y",-2.0f);

		PlayerPrefs.SetFloat("Item2X", 2.0f);
		PlayerPrefs.SetFloat("Item2Y",-2.0f);




		float x = 0.0f;
		float y = 0.0f;
		x = PlayerPrefs.GetFloat("Item1X");
		y = PlayerPrefs.GetFloat("Item1Y");
		if (m_MatList.Length >= 1) 
		{
			m_MatList [0].transform.position = new Vector3 (x, y, 0.0f);
		}

		x = PlayerPrefs.GetFloat("Item2X");
		y = PlayerPrefs.GetFloat("Item2Y");
		if (m_MatList.Length >= 2) 
		{
			m_MatList [1].transform.position = new Vector3 (x, y, 0.0f);
		}
	}

}
