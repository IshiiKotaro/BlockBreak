using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : BlockBase {




	void OnCollisionEnter2D(Collision2D _Other)
	{
		//Debug.Log("ItemBlockクラスのあたり判定");
		m_Hp = 0;
		if(_Other.gameObject.tag == "Ball")
		{
			ItemManager.GetInstance.Create(Random.Range(0,6), transform.position);//0~5までの乱数
		}
	}

}
