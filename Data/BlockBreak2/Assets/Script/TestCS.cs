using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCS : MonoBehaviour {

	public GameObject m_pTestPrefab;
	GameObject m_TestData;
	GameClear m_GClearCS;


	// Use this for initialization
	void Start () {

		m_TestData = (GameObject)Instantiate(
			m_pTestPrefab, 
			transform.position,
			Quaternion.identity
		);
		m_TestData.GetComponent<GameClear> ().Init();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
