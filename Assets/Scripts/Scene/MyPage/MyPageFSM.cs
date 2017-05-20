using UnityEngine;
using System.Collections;

public class MyPageFSM : BaseFSM 
{
	protected override Controllers defaultController { get { return Controllers.MyPageMain; } }

	protected override void Init () 
	{
		SetTransition(Controllers.MyPageMain, Controllers.BattleStart);
	}
}
