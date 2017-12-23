using System.Collections;
using System.Collections.Generic;
using UnityEngine;



enum BlockMassIndex:int
{
	WARNING = 12,
	DANGER = 14,
	GAMEOVER = 16,
}

public class BlockMass : MonoBehaviour 
{


    public GameObject m_BlockData;      //通常ブロック
	public GameObject m_ItemBlock;		//アイテムブロック





    int LEFT_MAX = -3;  //左におけるブロックの数。真ん中を0とする
    int RIGHT_MAX = 3;  //右におけるブロックの数。真ん中を0とする
    int BLOCKMASS_MAX = 7;//1列当たりのブロック最大数
	float MOVE_DIS = 0.45f;






	int m_CreateBlockCnt = 0;//作成したブロック数
	float m_BlockScal;

    bool m_IsDead;      //死亡フラグ　列のブロックが全て消えたら建てる
    int m_DestroyCnt;   //ブロック削除数
	int m_Index = 0;	//このブロックの進み
	float m_MoveDisSum = 0.0f;//合計進んだ距離





    List<GameObject> m_BlockList; //1列当たりのブロックデータ
	bool[] m_isCreate;

    //======================================
    //  ゲッター
    //======================================

    public bool GetIsDeadFlg() { return m_IsDead; }
    
	public int GetIndex(){ return m_Index; } 

    
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


	public void Create(int _MinBC,int _MaxBC,int _MinBH,int _MaxBH,int _BonusCnt)
    {

		if (PlayerPrefs.GetInt ("GameMode") == 2) 
		{
			LEFT_MAX = -6;
			RIGHT_MAX = 6;
			BLOCKMASS_MAX = 13;
			m_BlockScal = 0.5f;	//ブロック全体のスケール
		}
		else 
		{
			LEFT_MAX = -3;
			RIGHT_MAX = 3;
			BLOCKMASS_MAX = 7;
			m_BlockScal = 1.0f;	//ブロック全体のスケール
		}

		//ブロック作成フラグを作成
		m_isCreate = new bool[BLOCKMASS_MAX];
		for (int cnt = 0; cnt < BLOCKMASS_MAX; cnt++) 
		{
			m_isCreate [cnt] = false;
		}

		//ブロックの上限下限チェック
		if (_MaxBC > BLOCKMASS_MAX) _MaxBC = BLOCKMASS_MAX;
		if (_MinBC > BLOCKMASS_MAX) _MinBC = BLOCKMASS_MAX;


        //リストの作成（なんかStart()で作成するとNullとか言われて怒られる。）
        m_BlockList = new List<GameObject>();



		//優先して作るブロック
		InterruptBlockCreate(ScoreManager.GetInstance.GetWave());

		//全消しボーナス時に出すブロック
		BonusModeCreate(_BonusCnt);





		//ブロック配置(1列分)
        for(int Cnt = LEFT_MAX; Cnt <= RIGHT_MAX; Cnt++)
        {
            if (m_CreateBlockCnt >= _MaxBC) break;

            //ブロック１つを作成するかランダムで決定
            if (Random.Range(0, 10) >= 7) continue;

			//ブロック作成　Cntは座標を計算するのに使用
			NormalBlockCreate (Cnt,_MinBH,_MaxBH);
			m_CreateBlockCnt++;
        }


		m_CreateBlockCnt = 0;
    }


	void NormalBlockCreate(int _Cnt,int _MinBH,int _MaxBH)
	{
		if (m_isCreate [_Cnt + RIGHT_MAX] == true)return;


		//=====================================
		//ブロック生成
		//=====================================
		GameObject block = (GameObject)Instantiate(
			m_BlockData, 
			new Vector3(transform.position.x + (float)(_Cnt * 0.65 * m_BlockScal), transform.position.y, 0.0f),
			Quaternion.identity
		);
		//スケール調整
		block.transform.localScale = new Vector3(m_BlockScal * 0.5f,m_BlockScal * 0.35f,1.0f);


		//HP初期化
		BlockBase blockCS = block.GetComponent<BlockBase>();
		blockCS.Init(Random.Range(_MinBH, _MaxBH));

		//親子関係の設定
		block.transform.parent = this.transform;

		//ブロックリストに入れる
		m_BlockList.Add(block);

		//作成フラグを建てる
		m_isCreate[_Cnt + RIGHT_MAX] = true;


		//==================================
		//アシストアイコン設定(ランダムで)
		//==================================
		if(ScoreManager.GetInstance.GetWave() <= 7)return;
		int createPer;	//制作確立
		if (PlayerPrefs.GetInt("GameMode") == 2) 
		{
			createPer = 1;
		}
		else
		{
			createPer = 7;
		}
		if (Random.Range(0, 200) >= createPer) return;
		blockCS.Init(1);
		int assistIconId = Random.Range(0,(int)AssistType.HYPER_LASER_T - 1);
		blockCS.SetIsAssistIcon (true);
		blockCS.SetAssistId (assistIconId);
		AssistIconManager.GetInstance.Create(assistIconId,block.transform.position,block);
	}


	public void AssistBlockCreate(int _AssistId,int _Cnt)
	{
		_Cnt -= RIGHT_MAX;
		if (m_isCreate [_Cnt + RIGHT_MAX] == true)return;

		//=====================================
		//ブロック生成
		//=====================================
		GameObject block = (GameObject)Instantiate(
			m_BlockData,
			new Vector3(transform.position.x + (float)(_Cnt * 0.65), transform.position.y, 0.0f), 
			Quaternion.identity
		);

		//スケール調整
		block.transform.localScale = new Vector3(m_BlockScal * 0.5f,m_BlockScal * 0.35f,1.0f);

		//HP初期化
		BlockBase blockCS = block.GetComponent<BlockBase>();//
		blockCS.Init(1);

		//親子関係の設定
		block.transform.parent = this.transform;

		//ブロックリストに入れる
		m_BlockList.Add(block);

		//==================================
		//アシストアイコン設定
		//==================================
		blockCS.SetIsAssistIcon (true);
		blockCS.SetAssistId (_AssistId);
		AssistIconManager.GetInstance.Create(_AssistId,block.transform.position,block);

		//作成フラグを建てる
		m_isCreate[_Cnt + RIGHT_MAX] = true;
	}



	void InterruptBlockCreate(int _Wave)
	{
		switch (_Wave)
		{
		//_AssistId,_Cnt(どこに作るか),HPの順番
		//Case 1:AssistBlockCreate ((int)AssistType.METEOR, 3);break;
		case 10:AssistBlockCreate ((int)AssistType.BOMB, 0);break;
		case 15:AssistBlockCreate ((int)AssistType.SHOT_GUN, 5);break;
		case 20:AssistBlockCreate ((int)AssistType.HYPER_LASER_R, 0);AssistBlockCreate ((int)AssistType.HYPER_LASER_T,6);break;
		case 30:AssistBlockCreate (2, 4);break;
		case 40:AssistBlockCreate (2, 5);break;
		case 50:AssistBlockCreate ((int)AssistType.METEOR, 5);break;
		//case 16:AssistBlockCreate (2, 6);break;
		}
	}
	

	//全消しボーナスの時にだけ生成するブロック
	void BonusModeCreate(int _BonusCnt)
	{
		int isCreate = Random.Range (0, 100);
		if (isCreate > 20)return;

		int CreateId = Random.Range (0, 10);

		if (_BonusCnt >= 0 && _BonusCnt < 2) 
		{
			if (CreateId < 5) 
			{
				AssistBlockCreate ((int)AssistType.SHOT_4WAY,3);
			}
			else
			{
				AssistBlockCreate ((int)AssistType.HYPER_LASER_L,-LEFT_MAX + RIGHT_MAX);
				AssistBlockCreate ((int)AssistType.HYPER_LASER_T,0);
			}
		}
		else if (_BonusCnt < 4) 
		{
			if (CreateId < 8) 
			{
				AssistBlockCreate ((int)AssistType.SHOT_GUN,6);
			}
			else
			{
				AssistBlockCreate ((int)AssistType.THUNDER,4);
			}
		} 
		else if (_BonusCnt < 6)
		{
			if (CreateId < 5) 
			{
				AssistBlockCreate ((int)AssistType.THUNDER,3);
			}
			else
			{
				AssistBlockCreate ((int)AssistType.HYPER_LASER_R,0);
			}
		}
		else if (_BonusCnt < 8) 
		{
			AssistBlockCreate ((int)AssistType.CROSS_LASER,Random.Range(0,-LEFT_MAX + RIGHT_MAX));
		}
		else if (_BonusCnt < 12) 
		{
			AssistBlockCreate ((int)AssistType.METEOR,Random.Range(0,RIGHT_MAX + 3));
		}
		else
		{
			AssistBlockCreate ((int)AssistType.METEOR,Random.Range(0,RIGHT_MAX + 3));
		}




	}





    //ブロックを下に下げる処理
    public bool MoveDown()
    {


        Vector3 Move;
        Move.x = 0.0f;
        Move.y = -0.03f;
        Move.z = 0.0f;
        transform.Translate(Move);

		m_MoveDisSum -= Move.y;
		if (m_MoveDisSum >= MOVE_DIS * m_BlockScal) 
		{
			return true;
		}
		return false;
    }


	public void AddIndex()
	{
		if (m_IsDead == true)return;
		m_Index++;
		m_MoveDisSum = 0.0f;
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
			Destroy (gameObject);
        }
    }


}