using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager :  SingletonMonoBehaviour<SpriteManager>{

	public void Awake(){
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	/*GameObjectの中にあるスプライトのα値を弄る
	_Obj:　引数オブジェクト。この中にあるスプライトデータを弄る*/
	public void SetSpriteAlpha(GameObject _Obj,float _Alpha)
	{
		if (_Obj == null)return;
		SpriteRenderer goSprite = _Obj.GetComponent<SpriteRenderer>();
		Color col = goSprite.color;
		col.a = _Alpha;
		goSprite.color = col;
	}

	/*引数で受け取ったスプライトのα値を弄る
	_Sprite 引数スプライト*/
	public void SetSpriteAlpha(SpriteRenderer _Sprite,float _Alpha)
	{
		if (_Sprite == null)return;

		Color col = _Sprite.color;
		col.a = _Alpha;
		_Sprite.color = col;
	}



	public void SetSpriteCol(GameObject _Obj,float _R,float _G,float _B,float _Alpha)
	{
		SpriteRenderer goSprite = _Obj.GetComponent<SpriteRenderer>();
		Color col = goSprite.color;

		col.r = _R;
		col.g = _G;
		col.b = _B;
		col.a = _Alpha;
		goSprite.color = col;
	}


}
