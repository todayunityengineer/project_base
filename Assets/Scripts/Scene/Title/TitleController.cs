using UnityEngine;
using System.Collections;

public class TitleController : BaseController 
{
	protected override void Init ()
	{
		Presenters(E.Presenters.TitleMain).SetTransition(
			new Transition(E.Scenes.MyPage)
		);
	
		ChangePresenter(E.Presenters.TitleMain);
	}
}
