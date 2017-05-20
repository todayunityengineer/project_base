﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlankFSM : BaseFSM 
{
	protected override Presenters defaultPresenter { get { return Presenters.None; } }

	protected override void Init ()
	{
		if (firstPresenter == Presenters.None) 
		{
			GetTransitionAction(Presenters.TitleMain).Invoke();
		}
		else 
		{
			GetTransitionAction(firstPresenter).Invoke();
		}
	}
}