using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*敵キャラ管理　基底クラス
 基本的にブロックの上に配置される
 ブロックが消えると、一緒に消える。
 Blockインスタンスがエネミー生成を行う。*/


public class EnemyBase : MonoBehaviour {

    GameObject m_OwnerBlock;    //持ち主のブロック





    //============================
    // ゲッター
    //============================
    
    public GameObject GetOwner() { return m_OwnerBlock; }
    
    //============================
    // セッター
    //============================

    public void SetOwner(GameObject _Owner) {
        m_OwnerBlock = _Owner;
    }



    public virtual void Init()
    {
		
    }


    public virtual void Update()
    {
		  
    }



}
