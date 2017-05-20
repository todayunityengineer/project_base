using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlankFSM : BaseFSM 
{
	protected override Controllers defaultController { get { return Controllers.None; } }

	protected override void Init ()
	{
		if (firstController == Controllers.None) 
		{
			GetTransitionAction(Controllers.TitleMain).Invoke();
		}
		else 
		{
			GetTransitionAction(firstController).Invoke();
		}
	}
}
