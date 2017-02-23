using UnityEngine;
using System.Collections;

public abstract class BasePresenter : MonoBehaviour , IButtonListener
{
	#if UNITY_EDITOR
	public bool inState {
		get{
			return inThisState;
		}
	}
	#endif

	[SerializeField] protected BaseView view;

	protected bool inThisState { get; private set; }
	bool _isFirstEnter = true;
	protected bool isFirstEnter { get{ return _isFirstEnter; }}

	protected StateTransition transition { get; private set; }

	public void SetTransition (params Transition[] transitions)
	{
		this.transition = new StateTransition(transitions);
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