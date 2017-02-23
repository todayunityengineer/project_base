using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

public abstract class BaseController : MonoBehaviour
{ 	
	// Static //

	public static BaseController Instance { get; private set; }

	public static void ProjectLoaded ()
	{
		Instance = GameObject.FindObjectOfType<BaseController>();
		Instance.Init();

		string activeSceneName = SceneManager.GetActiveScene().name;
		if (activeSceneName != E.Scenes.Blank.ToString())
		{
			Fade.Instance.Close(Instance.Initialized);
		}
	}

	protected static void ChangePresenter (Enum a)
	{
		if (Instance.presentState != a.ToString())
		{
			if (Instance.presentState != "") 
			{
				Instance.Presenters(Instance.presentState).Exit();
			}
			Instance.presentState = a.ToString();	
			Instance.Presenters(a).Enter();
		}
	}

	protected static void LoadScene (E.Scenes nextScene)
	{
		string activeSceneName = SceneManager.GetActiveScene().name;
		if (activeSceneName == E.Scenes.Blank.ToString())
		{
			SceneManager.LoadScene(nextScene.ToString());
		}
		else 
		{
			BlankController.nextScene = nextScene;
			Fade.Instance.Open(() => SceneManager.LoadScene(E.Scenes.Blank.ToString()));
			Instance.EndScene();
			Instance.isLoaded = false;
		}
	}

	public static Action GetDefaultTransitionAction (Enum transition) 
	{
		if (Enum.IsDefined(typeof(E.Scenes), transition.ToString())) return () => LoadScene((E.Scenes)Enum.Parse(typeof(E.Scenes), transition.ToString()));
		else if (Enum.IsDefined(typeof(E.Presenters), transition.ToString())) return () => ChangePresenter((E.Presenters)Enum.Parse(typeof(E.Presenters), transition.ToString()));
		else return null;
	}
		
	// Instance //

	protected abstract void Init ();
	protected virtual void FadeClose (){}
	protected virtual void EndScene (){}

	BasePresenter[] _presenters;
	BasePresenter[] presenters { 
		get {
			if (_presenters == null) _presenters = FindObjectsOfType<BasePresenter>(); 
			return _presenters;
		}
	}
	string presentState = "";
	bool isLoaded = false;

	protected BasePresenter Presenters (Enum presenterName)
	{
		return Presenters(presenterName.ToString());
	}

	BasePresenter Presenters (string presenterName)
	{
		return presenters.Where(p => p.GetType().Name.Contains(presenterName)).FirstOrDefault();
	}

	void Initialized()
	{
		FadeClose();
		isLoaded = true;
	}
}