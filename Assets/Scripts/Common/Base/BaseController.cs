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

public enum Presenters
{
	None = 0,
	TitleMain = 10,
	MyPageMain = 20,
	BattleStart = 30,
	BattleMain = 31,
	BattlePause = 32,
	BattleResult = 33,
}
	
public abstract class BaseController : MonoBehaviour
{ 	
	// Static //
	public static BaseController Instance { get; private set; }

	public static Scenes activeScene;

	protected static Presenters firstPresenter;

	static Scenes GetSceneOfPresenter(Presenters presenter)
	{
		return (Scenes)((int)presenter/10);
	}

	protected static Action GetTransitionAction (Presenters transition) 
	{
		if (GetSceneOfPresenter(transition) != activeScene){
			return () => {
				firstPresenter = transition;
				Instance.LoadScene(GetSceneOfPresenter(transition));
			};
		}else{
			return () => Instance.ChangePresenter(transition);
		}
	}

	public static void ProjectLoaded ()
	{
		Instance = GameObject.FindObjectOfType<BaseController>();
		activeScene = (Scenes)Enum.Parse(typeof(Scenes), SceneManager.GetActiveScene().name);

		if (activeScene != Scenes.Blank)
		{
			Fade.Instance.Close(Instance.Initialized);
		}

		Instance.Init();
	}
		
	// Instance //

	protected abstract Presenters defaultPresenter { get; }
	protected abstract void Init ();
	protected virtual void FadeClose (){}
	protected virtual void EndScene (){}

	bool isLoaded = false;
	Presenters presentState = Presenters.None;

	Dictionary<Presenters, BasePresenter> _presenter;
	protected Dictionary<Presenters, BasePresenter> presenter {
		get{
			if (_presenter == null){
				_presenter = new Dictionary<Presenters, BasePresenter>();
				BasePresenter[] presenters = FindObjectsOfType<BasePresenter>();
				foreach(BasePresenter p in presenters)
				{
					_presenter.Add((Presenters)Enum.Parse(typeof(Presenters), p.GetType().Name.Replace("Presenter","")), p);
				}
			}
			return _presenter;
		}
	}

	void Initialized()
	{
		FadeClose();

		if (firstPresenter != Presenters.None) ChangePresenter(firstPresenter);
		else ChangePresenter(defaultPresenter);

		isLoaded = true;
	}

	protected void SetTransition(Presenters presenterFrom, params Presenters[] presenterTo)
	{
		for (int i = 0; i < presenterTo.Length; i++) {
			presenter[presenterFrom].SetTransition(presenterTo[i], GetTransitionAction(presenterTo[i]));
		}
	}

	void ChangePresenter (Presenters nextPresenter)
	{
		if (presentState != nextPresenter)
		{
			if (presentState != Presenters.None) 
			{
				presenter[Instance.presentState].Exit();
			}
			presentState = nextPresenter;	
			presenter[nextPresenter].Enter();
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