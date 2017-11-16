using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    
    bool m_IsDelete;//消滅フラグ

    //========================
    //ゲッター
    //========================

    public bool GetIsDelete() { return m_IsDelete; }
    
   
    
    //
	void Start () {
        m_IsDelete = false;
	}
	
	// Update is called once per frame
	void Update (){
		
	}


    void OnCollisionEnter2D(Collision2D _Other)
    {
		if(_Other.gameObject.tag == "Ball")
		{
			m_IsDelete = true;
			//Debug.Log("アイテムとボールがヒット！");
		}
    }
}
