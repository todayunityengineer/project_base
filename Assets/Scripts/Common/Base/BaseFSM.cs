using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum Scenes 
{
	Blank = 0,
	Title = 1,
	MyPage = 2,
	Battle = 3,
}

public enum Controllers
{
	None = 0,
	TitleMain = 10,
	MyPageMain = 20,
	BattleStart = 30,
	BattleMain = 31,
	BattlePause = 32,
	BattleResult = 33,
}
	
public abstract class BaseFSM : MonoBehaviour
{ 	
	// Static //
	public static BaseFSM Instance { get; private set; }
	public static Scenes activeScene;
	protected static Controllers firstController;

	static Scenes GetSceneOfController(Controllers controller)
	{
		return (Scenes)((int)controller/10);
	}

	protected static Action GetTransitionAction (Controllers transition) 
	{
		if (GetSceneOfController(transition) != activeScene){
			return () => {
				firstController = transition;
				Instance.LoadScene(GetSceneOfController(transition));
			};
		}else{
			return () => Instance.ChangeController(transition);
		}
	}

	public static void ProjectLoaded ()
	{
		Instance = GameObject.FindObjectOfType<BaseFSM>();
		activeScene = (Scenes)Enum.Parse(typeof(Scenes), SceneManager.GetActiveScene().name);

		if (activeScene != Scenes.Blank)
		{
			Fade.Instance.Close(Instance.Initialized);
		}

		Instance.Init();
	}
		
	// Instance //

	protected abstract Controllers defaultController { get; }
	protected abstract void Init ();
	protected virtual void FadeClose (){}
	protected virtual void EndScene (){}

	bool isLoaded = false;
	Controllers presentState = Controllers.None;

	Dictionary<Controllers, BaseController> _controller;
	protected Dictionary<Controllers, BaseController> controller {
		get{
			if (_controller == null){
				_controller = new Dictionary<Controllers, BaseController>();
				BaseController[] controllers = FindObjectsOfType<BaseController>();
				foreach(BaseController c in controllers)
				{
					_controller.Add((Controllers)Enum.Parse(typeof(Controllers), c.GetType().Name.Replace("Controller","")), c);
				}
			}
			return _controller;
		}
	}

	void Initialized()
	{
		FadeClose();

		if (firstController != Controllers.None) ChangeController(firstController);
		else ChangeController(defaultController);

		isLoaded = true;
	}

	protected void SetTransition(Controllers controllerFrom, params Controllers[] controllerTo)
	{
		for (int i = 0; i < controllerTo.Length; i++) {
			controller[controllerFrom].SetTransition(controllerTo[i], GetTransitionAction(controllerTo[i]));
		}
	}

	void ChangeController (Controllers nextController)
	{
		if (presentState != nextController)
		{
			if (presentState != Controllers.None) 
			{
				controller[Instance.presentState].Exit();
			}
			presentState = nextController;	
			controller[nextController].Enter();
		}
	}

	void LoadScene (Scenes nextScene)
	{
		if (activeScene == Scenes.Blank)
		{
			SceneManager.LoadScene(nextScene.ToString());
		}
		else 
		{
			Fade.Instance.Open(() => SceneManager.LoadScene(Scenes.Blank.ToString()));
			Instance.EndScene();
			Instance.isLoaded = false;
		}
	}
}