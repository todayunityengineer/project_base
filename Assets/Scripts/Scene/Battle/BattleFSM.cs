using UnityEngine;
using System.Collections;

public class BattleFSM : BaseFSM 
{
	protected override Presenters defaultPresenter { get { return Presenters.BattleStart; } }

	protected override void Init ()
	{
		SetTransition(Presenters.BattleStart, Presenters.BattleMain);

		SetTransition(Presenters.BattleMain, Presenters.BattlePause, Presenters.BattleResult);

		SetTransition(Presenters.BattlePause, Presenters.BattleMain);

		SetTransition(Presenters.BattleResult, Presenters.MyPageMain);
	}
}
