using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBonus : MonoBehaviour {

	public GameObject m_pChainBonusMessage;

	const float cDefMessageX = 10.0f;		//全消しメッセージの初期座標

	bool m_isChainBonus = false;
	GameObject m_Block;
	GameObject m_Effect;
	GameObject m_ChainBonusMessage;

	int m_AssistId = 0;
	float m_MessageX = cDefMessageX;

	//セッター
	public void SetChainBonus()
	{
		int chain = ScoreManager.GetInstance.GetNowCombo ();
		if (chain < 30)return;
		if(chain >= 30)m_AssistId = (int)AssistType.THUNDER;
		if(chain >= 40)m_AssistId = (int)AssistType.CROSS_LASER;
		if(chain >= 50)m_AssistId = (int)AssistType.METEOR;

		m_isChainBonus = true; 
	}



	// Use this for initialization
	void Start () 
	{
		m_isChainBonus = false;

	}


	public void Init()
	{
		m_isChainBonus = false;
		m_MessageX = cDefMessageX;
		m_ChainBonusMessage = (GameObject)Instantiate(m_pChainBonusMessage,transform.position,Quaternion.identity);
		SpriteManager.GetInstance.SetSpriteAlpha (m_ChainBonusMessage,0.0f);
	}


	//チェインボーナスを付与するブロックを決める。
	void BonusBlockDecision()
	{
		Block[] blockObjects = GameObject.FindObjectsOfType<Block>();
		bool isDecision = false;
		foreach (Block Idx in blockObjects)
		{
			if (isDecision == true)break;
			if (Random.Range (0, 100) > 5)continue;
			m_Block = Idx.gameObject;
			//エフェクト生成
			m_Effect = EffectManager.GetInstance.CreateEffect((int)EffectType.AURA_LOOP,transform.position,0.0f);
			m_MessageX = cDefMessageX;
			SpriteManager.GetInstance.SetSpriteAlpha (m_ChainBonusMessage,1.0f);
			isDecision = true;
			return;
		}
		//もし↑でブロックが決まらなかったら。
		if(isDecision == true)return;
		foreach (Block Idx in blockObjects)
		{
			m_Block = Idx.gameObject;
			//エフェクト生成
			m_Effect = EffectManager.GetInstance.CreateEffect((int)EffectType.AURA_LOOP,transform.position,0.0f);
			m_MessageX = cDefMessageX;
			SpriteManager.GetInstance.SetSpriteAlpha (m_ChainBonusMessage,1.0f);
			isDecision = true;
			return;
		}
		Destroy (m_Effect);
		return;
	}


	void MessagePosUpdate()
	{
		EventManager.GetInstance.SetIsEvent (true);
		if (m_MessageX >= 0.05f) 
		{
			m_MessageX *= 0.9f;
		}
		else
		{
			if (m_MessageX >= 0.0f)m_MessageX *= -1.0f;
			m_MessageX *= 1.1f;
		}
		m_ChainBonusMessage.transform.position = new Vector3 (m_MessageX, transform.position.y, 0.0f);
	}


	public void ChainBonusUpdate ()
	{
		if (m_isChainBonus == false)return;

		MessagePosUpdate ();



		//もし本来ボーナスブロックを付与するはずのブロックがすでに壊されていた場合
		if (m_Block == null) 
		{
			BonusBlockDecision ();
		}
		else 
		{
			if (m_Effect == null) 
			{
				Debug.Log ("EffectNULL");
				return;
			}
				
			Debug.Log ("ChainBonus!");
			m_Effect.transform.parent = m_Block.transform;

			Block blockCS = m_Block.GetComponent<Block>();

			//ブロックの座標を取得して、そこに向かってエフェクトを追尾させる。
			blockCS.GetPos();
			Vector3 blockVec = new Vector3(); 
			blockVec.x =  m_Block.transform.position.x - m_Effect.transform.position.x;
			blockVec.y =  m_Block.transform.position.y - m_Effect.transform.position.y;
			blockVec *= 0.95f;

			m_Effect.transform.position = new Vector3 (blockCS.GetPos().x - blockVec.x,blockCS.GetPos().y - blockVec.y,0.0f);

			//ブロックにエフェクトがヒットしたら、アシストブロックに変換させる。
			if (blockVec.magnitude < 0.01f) 
			{
				blockCS.Init(1);
				int assistIconId = m_AssistId;
				blockCS.SetIsAssistIcon (true);
				blockCS.SetAssistId (assistIconId);
				AssistIconManager.GetInstance.Create(assistIconId,m_Block.transform.position,m_Block);
				Destroy (m_Effect);
				m_isChainBonus = false;
				SpriteManager.GetInstance.SetSpriteAlpha (m_ChainBonusMessage,0.0f);
				EventManager.GetInstance.SetIsEvent (false);
			}
		}
	}
}
