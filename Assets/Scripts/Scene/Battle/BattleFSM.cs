using UnityEngine;
using System.Collections;

public class BattleFSM : BaseFSM 
{
	protected override Controllers defaultController { get { return Controllers.BattleStart; } }

	protected override void Init ()
	{
		SetTransition(Controllers.BattleStart, Controllers.BattleMain);

		SetTransition(Controllers.BattleMain, Controllers.BattlePause, Controllers.BattleResult);

		SetTransition(Controllers.BattlePause, Controllers.BattleMain);

		SetTransition(Controllers.BattleResult, Controllers.MyPageMain);
	}
}
