using UnityEngine;
using System.Collections;

public class TitleMainController : BaseController 
{
	[SerializeField]UIButton btnStart;

	protected override void OnEnter ()
	{
	}

	protected override void OnExit ()
	{
	}

	protected override void OnButtonClick (UIButton btn)
	{
		if (btnStart == btn)
		{
			DoTransition(Controllers.MyPageMain);
 		}
	}
}
