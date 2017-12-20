using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

	public GameObject m_pCoolTimeObject;
	public int m_pRecastTime;

	GameObject m_CoolTimeObject;

	protected int m_NowRecast;

	//=====================================
	//	ゲッター
	//=====================================

	//このアイテム(アシストマット)が発動しても大丈夫かどうか。 trueでOK
	protected bool GetisPlayItem()
	{
		if (m_NowRecast <= 0)return true;
		return false;
	}

	//=====================================
	//	セッター
	//=====================================

	protected void SetRecast(){ m_NowRecast = m_pRecastTime; }


	//
	void Start ()
	{
		m_CoolTimeObject = (GameObject)Instantiate(m_pCoolTimeObject,transform.position,Quaternion.identity);
		if(m_CoolTimeObject == null)
		{
			Debug.Log ("クールタイムオブジェクトNULL");
		}
		m_NowRecast = 0;
	}


	public void Recast()
	{
		float alpha = (float)m_NowRecast / (float)m_pRecastTime; 
		SpriteManager.GetInstance.SetSpriteAlpha (m_CoolTimeObject,alpha);
		if(m_NowRecast > 0)
		{
			m_NowRecast--;
		}
	}
}
