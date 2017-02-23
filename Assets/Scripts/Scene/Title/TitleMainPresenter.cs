using UnityEngine;
using System.Collections;

public class TitleMainPresenter : BasePresenter 
{
	TitleMainView titleMainView {
		get{
			return view as TitleMainView;
		}
	}

	[SerializeField]UIButton btnStart;

	protected override void OnEnter ()
	{
		titleMainView.OnEnter();
	}

	protected override void OnExit ()
	{
		titleMainView.OnExit();
	}

	protected override void OnButtonClick (UIButton btn)
	{
		if (btnStart == btn)
		{
			transition.ExecuteTransition(E.Scenes.MyPage);
 		}
	}
}
