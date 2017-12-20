using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNum : MonoBehaviour {



	public GameObject[] m_pDigit;	//桁

	void Start()
	{

	}



	void Update()
	{
		int score = ScoreManager.GetInstance.GetScore();

		for(int Cnt = 0;Cnt < m_pDigit.Length;Cnt++)
		{
			NumSingle numCS = m_pDigit [Cnt].GetComponent<NumSingle> ();
			if(numCS == null)Debug.Log ("数値ヌル");return;
			numCS.DrawNumSingle (1);

		}







	}


}
