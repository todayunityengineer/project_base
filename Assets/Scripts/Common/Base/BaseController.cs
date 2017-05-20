using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseController : MonoBehaviour , IButtonListener
{
	#if UNITY_EDITOR
	public bool inState { get { return inThisState; } }
	#endif

	protected bool inThisState { get; private set; }
	bool _isFirstEnter = true;
	protected bool isFirstEnter { get{ return _isFirstEnter; }}
	Dictionary<Controllers, Action> transitions = new Dictionary<Controllers, Action>();

	public void SetTransition (Controllers controller, Action action)
	{
		transitions.Add(controller, action);
	}

	protected void DoTransition (Controllers controller)
	{
		if (transitions.ContainsKey(controller) & inThisState) transitions[controller].Invoke();
	}

	public void Enter () 
	{
		inThisState = true;
		OnEnter();	
	}

	protected abstract void OnEnter ();

	public void Exit () 
	{
		_isFirstEnter = false;
		inThisState = false;
		OnExit();
	}

	protected abstract void OnExit ();

	public void ButtonClick(UIButton btn)
	{
		if (inThisState) OnButtonClick(btn);
	}

	protected abstract void OnButtonClick(UIButton btn);

	void Reset()
	{
		gameObject.name = this.GetType().Name;
	}
}



