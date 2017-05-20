using UnityEngine;
using System.Collections;

public class TitleFSM : BaseFSM 
{
	protected override Presenters defaultPresenter { get { return Presenters.TitleMain; } }

	protected override void Init ()
	{
		SetTransition(Presenters.TitleMain, Presenters.MyPageMain);
	}
}
