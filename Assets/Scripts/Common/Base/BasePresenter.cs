using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BasePresenter : MonoBehaviour , IButtonListener
{
	#if UNITY_EDITOR
	public bool inState { get { return inThisState; } }
	#endif

	[SerializeField] protected BaseView view;

	protected bool inThisState { get; private set; }
	bool _isFirstEnter = true;
	protected bool isFirstEnter { get{ return _isFirstEnter; }}
	Dictionary<Presenters, Action> transitions = new Dictionary<Presenters, Action>();

	public void SetTransition (Presenters presenter, Action action)
	{
		transitions.Add(presenter, action);
	}

	protected void DoTransition (Presenters presenter)
	{
		if (transitions.ContainsKey(presenter)) transitions[presenter].Invoke();
	}

	public void Enter () 
	{
		OnEnter();	
		inThisState = true;
		_isFirstEnter = false;
	}

	protected abstract void OnEnter ();

	public void Exit () 
	{
		inThisState = false;
		OnExit();
	}

	protected abstract void OnExit ();

	public void ButtonClick(UIButton btn)
	{
		if (inThisState) OnButtonClick(btn);
	}

	protected abstract void OnButtonClick(UIButton btn);
}



