using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
	//
	public GameObject[] m_pItemPrefab;
	GameObject[] m_ItemList;
	bool m_isMove = false;

	Vector3 m_DownPos;

	//
	public void SetIsMove(bool _isMove){ m_isMove = _isMove; }


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


	void Start()
	{
		

	}

	public void ListCreate()
	{

	}


	public void Init()
	{
		m_isMove = false;

		if (m_ItemList == null) 
		{
			m_ItemList = new GameObject[m_pItemPrefab.Length];
			for (int cnt = 0; cnt < m_ItemList.Length; cnt++) 
			{
				m_ItemList[cnt] = (GameObject)Instantiate(
					m_pItemPrefab[cnt],
					transform.position,
					Quaternion.identity
				);
			}


			m_DownPos = new Vector3 ();

			float x = 0.0f;
			float y = 0.0f;

			if (PlayerPrefs.GetInt ("VectorUp") == 1) 
			{
				x = PlayerPrefs.GetFloat("Item1X");
				y = PlayerPrefs.GetFloat("Item1Y");
			} 
			else
			{
				x = -100.0f;
				y = -100.0f;
			}

			m_ItemList [0].transform.position = new Vector3 (x,y,0.0f);

			if (PlayerPrefs.GetInt ("Shot4Way") == 1)
			{
				x = PlayerPrefs.GetFloat("Item2X");
				y = PlayerPrefs.GetFloat("Item2Y");
			}
			else
			{
				x = -100.0f;
				y = -100.0f;
			}
			m_ItemList [1].transform.position = new Vector3 (x, y, 0.0f);

		}




	}


	public void AssistMove()
	{

	}


	public void AssistReset()
	{

	}


}
