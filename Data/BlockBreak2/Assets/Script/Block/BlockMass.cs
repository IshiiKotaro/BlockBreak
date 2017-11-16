using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BlockMass : MonoBehaviour {


    public GameObject m_BlockData;      //通常ブロック
	public GameObject m_ItemBlock;		//アイテムブロック





    const int LEFT_MAX = -3;  //左におけるブロックの数。真ん中を0とする
    const int RIGHT_MAX = 3;  //右におけるブロックの数。真ん中を0とする
    const int BLOCKMASS_MAX = 7;//1列当たりのブロック最大数

    bool m_IsDead;      //死亡フラグ　列のブロックが全て消えたら建てる
    int m_DestroyCnt;   //ブロック削除数

    List<GameObject> m_BlockList; //1列当たりのブロックデータ


    //======================================
    //  ゲッター
    //======================================

    bool GetIsDeadFlg() { return m_IsDead;  }
    


    
    //

	
	void Start () {
        //
       
        m_IsDead = false;
        m_DestroyCnt = 0;

	}


    void Update()
    {
        DeleteCheck();
    }


	public void Create(int _Wave,int _MinBC,int _MaxBC,int _MinBH,int _MaxBH)
    {
        //ブロックの上限下限チェック
        if (_MaxBC > BLOCKMASS_MAX) _MaxBC = BLOCKMASS_MAX;
        if (_MinBC > BLOCKMASS_MAX) _MinBC = BLOCKMASS_MAX;



        //リストの作成（なんかStart()で作成するとNullとか言われて怒られる。）
        m_BlockList = new List<GameObject>();


        int CreateBlockCnt = 0;//作成したブロック数
		int CreateItemBCnt = 0;//作成したアイテムブロック数

		//ブロック配置(1列分)
        for(int Cnt = LEFT_MAX; Cnt <= RIGHT_MAX; Cnt++)
        {
            if (CreateBlockCnt >= _MaxBC) break;




            //ブロック１つを作成するかランダムで決定
            if (Random.Range(0, 10) >= 7) continue;

			//
			if (_Wave % 5 == 0 && CreateItemBCnt == 0) {
				ItemBlockCreate(Cnt);
				CreateItemBCnt++;
			}
			else 
			{
				//ブロック作成　Cntは座標を計算するのに使用
				NormalBlockCreate (Cnt,_MinBH,_MaxBH);
			}









			CreateBlockCnt++;
        }
    }


	void NormalBlockCreate(int _Cnt,int _MinBH,int _MaxBH)
	{
		//=====================================
		//ブロック生成
		//=====================================
		GameObject block = (GameObject)Instantiate(
			m_BlockData, 
			new Vector3(transform.position.x + (float)(_Cnt * 0.83), transform.position.y, 0.0f), 
			Quaternion.identity
		);

		//HP初期化
		BlockBase blockCS = block.GetComponent<BlockBase>();//
		blockCS.Init(Random.Range(_MinBH, _MaxBH));

		//親子関係の設定
		block.transform.parent = this.transform;

		//ブロックリストに入れる
		m_BlockList.Add(block);


		//===================================
		//  エネミー生成
		//===================================
		if (Random.Range(0, 2) == 1) return;


		//生成座標とID設定
		Vector3 enemyPos = block.transform.position;
		//enemyPos.y += 0.3f;
		int enemyId = Random.Range(0,EnemyManager.GetInstance.GetPrefabListSize());
		blockCS.SetIsEnemy (true);

		EnemyManager.GetInstance.Create(enemyId,enemyPos,block);

	}


	void ItemBlockCreate (int _Cnt)
	{
		//=====================================
		//ブロック生成
		//=====================================
		GameObject block = (GameObject)Instantiate(
			m_ItemBlock, 
			new Vector3(transform.position.x + (float)(_Cnt * 0.83), transform.position.y, 0.0f),
			Quaternion.identity
		);

		//HP初期化
		BlockBase blockCS = block.GetComponent<BlockBase>();//
		blockCS.Init(1);

		//親子関係の設定
		block.transform.parent = this.transform;

		//ブロックリストに入れる
		m_BlockList.Add(block);
	}







    //ブロックを下に下げる処理
    public void MoveDown()
    {
        Vector3 Move;
        Move.x = 0.0f;
        Move.y = -0.6f;
        Move.z = 0.0f;


        transform.Translate(Move);
    }


    //ブロック単体の削除チェック(列データの削除はマネージャに丸投げ)
    void DeleteCheck()
    {
        //ブロック削除チェック
        foreach (GameObject data in m_BlockList)
        {
            //nullチェック
            if (data == null) continue;



            //ブロックデータのスクリプト取得
            BlockBase block_CS = data.GetComponent<BlockBase>();


            //ブロックの削除
            if (block_CS.GetHp() > 0) continue;
            


            Destroy(data);
            //Debug.Log("BlockDestroy");

            //削除数記憶
            m_DestroyCnt++;


        }

        if (m_DestroyCnt >= m_BlockList.Count)
        {
            m_IsDead = true;
        }
    }


}