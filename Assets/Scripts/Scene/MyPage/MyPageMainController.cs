using UnityEngine;
using System.Collections;

public class MyPageMainController : BaseController 
{
	[SerializeField] UIButton btnBattle;

	protected override void OnEnter ()
	{

	}

	protected override void OnExit ()
	{

	}

	protected override void OnButtonClick (UIButton btn)
	{
		if (btn == btnBattle) 
		{
			DoTransition(Controllers.BattleStart);
		}	
	}
}
