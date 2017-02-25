using UnityEngine;
using System.Collections;

public class MyPageController : BaseController 
{
	protected override Presenters defaultPresenter { get { return Presenters.MyPageMain; } }

	protected override void Init () 
	{
		SetTransition(Presenters.MyPageMain, Presenters.BattleStart);
	}
}
