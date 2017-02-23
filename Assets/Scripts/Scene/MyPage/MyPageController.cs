using UnityEngine;
using System.Collections;

public class MyPageController : BaseController 
{
	protected override void Init () 
	{
		Presenters(E.Presenters.MyPageMain).SetTransition(
			new Transition(E.Scenes.Battle)
		);

		ChangePresenter(E.Presenters.MyPageMain);
	}
}
