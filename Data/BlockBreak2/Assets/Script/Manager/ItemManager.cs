using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>{

	public GameObject[] m_pItemPrefab;
	GameObject[] m_ItemList;



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
		m_ItemList = new GameObject[m_pItemPrefab.Length];
		for (int cnt = 0; cnt < m_ItemList.Length; cnt++) 
		{
			m_ItemList[cnt] = (GameObject)Instantiate(
				m_pItemPrefab[cnt],
				transform.position,
				Quaternion.identity
			);
		}
	}


	public void Init()
	{
		float x = 0.0f;
		float y = 0.0f;

		if (PlayerPrefs.GetInt ("VectorUp") == 1) 
		{
			x = 0.0f;
			y = 0.1f;
		} 
		else
		{
			x = -100.0f;
			y = -100.0f;
		}

		m_ItemList [0].transform.position = new Vector3 (x,y,0.0f);

		if (PlayerPrefs.GetInt ("Shot4Way") == 1)
		{
			x = 0.0f;
			y = -1.0f;
		}
		else
		{
			x = -100.0f;
			y = -100.0f;
		}
		m_ItemList [1].transform.position = new Vector3 (x, y, 0.0f);



	}





}
