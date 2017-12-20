using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumSingle : MonoBehaviour{

	public GameObject[] m_pNumPrefab;	//0~9までのプレファブ
	public float m_pScal;

	List<GameObject> m_NumSingleList;	//0~9までのデータ

	//




	public void Init()
	{
		m_NumSingleList = new List<GameObject>();
		for(int Cnt = 0;Cnt < m_pNumPrefab.Length;Cnt++)
		{

			GameObject numObject = (GameObject)Instantiate( m_pNumPrefab[Cnt],transform.position,Quaternion.identity);
			numObject.transform.parent = this.transform;
			numObject.transform.localScale = new Vector3(m_pScal,m_pScal,1.0f);

			m_NumSingleList.Add (numObject);
		}
		ResetNumAlpha ();
	}





	public void DrawNumSingle(int _Num)
	{
		//Debug.Log ("リストサイズ:"+ m_NumSingleList.Count);


		if (_Num <= 0)_Num = 0;
		if (_Num >= 9)_Num = 9;
		//return;
		//一旦全部アルファ値を0にする
		ResetNumAlpha();


		SpriteRenderer NumSprite = m_NumSingleList[_Num].GetComponent<SpriteRenderer> ();
		if (NumSprite == null)return;

		Color Col = NumSprite.color;
		Col.a = 1;
		NumSprite.color = Col;

	}


	void ResetNumAlpha()
	{
		for (int cnt = 0; cnt < m_NumSingleList.Count; cnt++)
		{
			if (m_NumSingleList[cnt] == null)continue;
			SpriteRenderer NumSprite = m_NumSingleList[cnt].GetComponent<SpriteRenderer> ();
			if (NumSprite == null)continue;

			Color Col = NumSprite.color;
			Col.a = 0;
			NumSprite.color = Col;
		}
	}
}
