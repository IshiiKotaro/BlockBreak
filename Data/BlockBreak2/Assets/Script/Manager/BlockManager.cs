using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BlockManager : SingletonMonoBehaviour<BlockManager> {




    public int m_MaxBlockCntUpInterval;     //1列当たりのブロックの最大数を上げるインターバル
    public int m_MinBlockCntUpInterval;
    public int m_MaxBlockHpUpInterval;
    public int m_MinBlockHpUpInterval;

    public GameObject m_BlockMassPrefab;


	bool m_isNextWave = false;	//次のWAVEに進んでもいいか





	public bool GetisNextWave(){ return m_isNextWave; }//次のWAVEに進んでもいいか


	public bool GetisBlockAllDelete()
	{
		//ブロックが全部消滅したかチェック
		BlockMass[] massObjects = GameObject.FindObjectsOfType<BlockMass>();
		int cnt = 0;
		foreach (BlockMass Idx in massObjects)
		{
			cnt++;
		}
		if (cnt != 0)return false;

		return true;
	}

	//
	//
	//

	public void SetisNextWave(bool _isNext){ m_isNextWave = _isNext; }


	private BlockManager(){}


	public void Awake(){
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	public void Init()
	{
		m_isNextWave = false;
	}




    /*ブロック(配列)データの作成
     _isAddWave WAVEを増やすかどうか
     _BonusCnt 全消しボーナスで追加したブロック数 何もない時は-1を指定する */
	public void Create(bool _isAddWave,int _BonusCnt)
    {
		//SE
		SoundManager.GetInstance.PlaySE ((int)SEType.BLOCKCREATE,1.0f,1.0f);



		int maxBCnt = 7;//5
		int minBCnt = ScoreManager.GetInstance.GetWave() / m_MinBlockCntUpInterval - 2;
		int maxBHp = ScoreManager.GetInstance.GetWave() / 20 + 3;
		int minBHp = 1;

		if(maxBHp >= 7){maxBHp = 7;}
		if(minBHp >= 7){minBHp = 7;}
		if(minBHp <= 0){minBHp = 1;}
		if (PlayerPrefs.GetInt ("GameMode") == 2) 
		{
			maxBCnt = 14;

			if(minBCnt <= 0){minBCnt = 0;}

		} 
		else
		{
			maxBCnt = 7;

			if(minBCnt <= 0){minBCnt = 0;}
		}





		//for(int Cnt = 0;Cnt < _CreateCnt;Cnt++)
		{

			//ブロック1列生成
			GameObject blockMass = (GameObject)Instantiate(m_BlockMassPrefab,transform.position,Quaternion.identity);
			BlockMass blockCS = blockMass.GetComponent<BlockMass>();
			blockCS.Create(minBCnt,maxBCnt,minBHp,maxBHp,_BonusCnt);

			//親子関係の設定
			blockMass.transform.parent = transform.parent;

			m_isNextWave = false;

			if (_isAddWave == true) 
			{
				//Wave数更新
				ScoreManager.GetInstance.AddWave();
			}

		}
    }


	//次のブロック列を出す処理。　出し終わったらtrueを返す
	//_isAddBonus ボーナスカウントを増やすか
	//_isAddWave  WAVEを増やすかどうか
	//_isCreateBlock ブロックを生成するかどうか
	//_BonasCnt		全消しボーナスで追加したブロック数 何もない時は-1を指定する
	public bool NextWave(bool _isAddBonus,bool _isAddWave,bool _isCreateBlock,int _BonusCnt)
	{
		bool MoveEndFlg = true;
		//既存のブロックを下に下げる
		BlockMass[] MassObjects = GameObject.FindObjectsOfType<BlockMass>();
		foreach (BlockMass Idx in MassObjects)
		{
			MoveEndFlg = Idx.MoveDown();
		}
		//もしブロックの移動が終わったら
		if(MoveEndFlg == false)return false;
		foreach (BlockMass Idx in MassObjects)
		{
			Idx.AddIndex();
		}

		//ブロック列生成
		if (_isCreateBlock == true) 
		{
			Create (_isAddWave,_BonusCnt);
		}
		else
		{
			m_isNextWave = false;
		}
			
	

		if (_isAddBonus == true)
		{
			EventManager.GetInstance.AddBonusCnt (1);//
		}

		return true;
	}



}
