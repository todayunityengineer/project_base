using UnityEngine;
using System.Collections;

public class TitleFSM : BaseFSM 
{
	protected override Controllers defaultController { get { return Controllers.TitleMain; } }

	protected override void Init ()
	{
		SetTransition(Controllers.TitleMain, Controllers.MyPageMain);
	}
}
