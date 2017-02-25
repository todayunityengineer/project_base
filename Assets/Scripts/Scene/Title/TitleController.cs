using UnityEngine;
using System.Collections;

public class TitleController : BaseController 
{
	protected override Presenters defaultPresenter { get { return Presenters.TitleMain; } }

	protected override void Init ()
	{
		SetTransition(Presenters.TitleMain, Presenters.MyPageMain);
	}
}
