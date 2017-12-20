using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {

	Vector3 m_MoveVec;
	float m_MoveSpeed;


	void Start () {
		m_MoveVec = Vector3.down;
		m_MoveSpeed = 10.0f;
	}
	

	void Update () {
		transform.Translate(m_MoveVec * Time.deltaTime * m_MoveSpeed);
		if (transform.position.y < -10) 
		{
			transform.position = new Vector3 (0,10,0);
		}
	}
}
