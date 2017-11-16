using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BlockManager : SingletonMonoBehaviour<BlockManager> {




    public int m_MaxBlockCntUpInterval;     //1列当たりのブロックの最大数を上げるインターバル
    public int m_MinBlockCntUpInterval;
    public int m_MaxBlockHpUpInterval;
    public int m_MinBlockHpUpInterval;

    public GameObject m_BlockMassPrefab;

	List<GameObject> m_BlockMassList;


    int m_Wave;   //現在のWave数

	private BlockManager(){
		m_Wave = 1;
		m_BlockMassList = new List<GameObject> ();
	}


	public void Awake(){
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}





    /*ブロック(配列)データの作成
     _CreateCnt:一度に作るブロック列の数*/
    public void Create(int _CreateCnt = 1)
    {
        //現在のレベルに応じて、どれだけブロック等を出すか決める。あと、敵の数と耐久値も大雑把に決める
        //int maxBCnt = m_Wave / m_MaxBlockCntUpInterval;
        //int minBCnt = m_Wave / m_MinBlockCntUpInterval;
        //int maxBHp = m_Wave / m_MaxBlockHpUpInterval;
        //int minBHp = m_Wave / m_MinBlockHpUpInterval;

		int maxBCnt = 5;
		int minBCnt = m_Wave / m_MinBlockCntUpInterval;
		int maxBHp = m_Wave / 10 + 1;
		int minBHp = 1;

		if(maxBHp >= 7){maxBHp = 7;}
		if(minBHp >= 7){minBHp = 7;}

		for(int Cnt = 0;Cnt < _CreateCnt;Cnt++)
		{

			//既存のブロックを下に下げる
			BlockMass[] MassObjects = GameObject.FindObjectsOfType<BlockMass>();
			foreach (BlockMass Idx in MassObjects)
			{
				Idx.MoveDown();
			}



			//ブロック1列生成
			GameObject blockMass = (GameObject)Instantiate(m_BlockMassPrefab,transform.position,Quaternion.identity);
			BlockMass blockCS = blockMass.GetComponent<BlockMass>();
			blockCS.Create(m_Wave,minBCnt,maxBCnt,minBHp,maxBHp);

			//親子関係の設定
			blockMass.transform.parent = transform.parent;


			//リストに入れる
			m_BlockMassList.Add(blockMass);



			//Wave数更新
			m_Wave++;
		}
    }


	public void CheckDelete()
	{
		for(int cnt = 0;cnt < m_BlockMassList.Count;cnt++)
		{





		}






	}


	void CheckCreate()
	{
		
	}



}
