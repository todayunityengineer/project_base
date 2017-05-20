using UnityEngine;
using System;
using System.Collections;

public class BattleStartController : BaseController 
{
	protected override void OnEnter ()
	{
		DoTransition(Controllers.BattleMain);
	}

	protected override void OnExit ()
	{

	}

	protected override void OnButtonClick (UIButton btn)
	{
		
	}
}
