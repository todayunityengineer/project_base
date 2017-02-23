using UnityEngine;
using System.Collections;

public class BattleController : BaseController 
{
	protected override void Init ()
	{
		Presenters(E.Presenters.BattleStart).SetTransition(
			new Transition(E.Presenters.BattleMain)
		);

		Presenters(E.Presenters.BattleMain).SetTransition(
			new Transition(E.Presenters.BattlePause),
			new Transition(E.Presenters.BattleResult)
		);

		Presenters(E.Presenters.BattlePause).SetTransition(
			new Transition(E.Presenters.BattleMain)
		);

		Presenters(E.Presenters.BattleResult).SetTransition(
			new Transition(E.Scenes.MyPage)
		);
	}

	protected override void FadeClose ()
	{
		ChangePresenter(E.Presenters.BattleStart);
	}
}
