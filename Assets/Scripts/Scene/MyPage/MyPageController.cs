﻿using UnityEngine;
using System.Collections;

public class MyPageController : BaseController 
{
	enum State 
	{
		Main = 0,	
	}

	protected override void Init () 
	{
		ChangePresenter((int)State.Main);
	}

	protected override void OnButtonClick (GameObject go){}
}