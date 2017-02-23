using UnityEngine;
using System;
using System.Collections;

public class BattleStartPresenter : BasePresenter 
{
	BattleStartView battleStartView {
		get{
			return view as BattleStartView;
		}
	}

	protected override void OnEnter ()
	{
		StartCoroutine(battleStartView.CountDown(3, () => transition.ExecuteTransition(E.Presenters.BattleMain)));
	}

	protected override void OnExit ()
	{

	}

	protected override void OnButtonClick (UIButton btn)
	{
		
	}
}
