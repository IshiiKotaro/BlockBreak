using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistBase : MonoBehaviour {

	public GameObject m_pAssistObject;
	protected GameObject m_AssistObject;



	protected int m_AssistId = 0;

	protected Vector3 m_AObjectPos;
	float m_AddY = 0.05f;
	float m_PosY = 0.0f;

	//===============================
	//	セッター
	//===============================

	public void SetId(int _id){ m_AssistId = _id; }



	protected void AssistObjectUpdate()
	{
		if (m_AssistObject == null)return;
		//徐々にα値と座標を↑にあげる。
		m_AssistObject.transform.position = new Vector3(
			m_AObjectPos.x,
			m_AObjectPos.y + m_PosY,
			m_AObjectPos.z);

		m_PosY += m_AddY;
		m_AddY *= 0.9f;

	}
}
