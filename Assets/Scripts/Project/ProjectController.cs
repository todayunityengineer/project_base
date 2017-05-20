using UnityEngine;
using System.Collections;

public sealed class ProjectController : MonoBehaviour 
{
	static ProjectController Instance;

	public static bool isLoaded {get; private set;}

	void Awake ()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			StartCoroutine (Init ());
		}
		else 
		{
			ProjectLoaded();
			Destroy(gameObject);
		}
	}

	IEnumerator Init () 
	{
		yield return StartCoroutine(Master.Load());

		GameObject.Instantiate(Resources.Load(R.FadePrefab) as GameObject);
		isLoaded = true;

		ProjectLoaded();
	}

	void ProjectLoaded () 
	{
		BaseFSM.ProjectLoaded();
	}
}
