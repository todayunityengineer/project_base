using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class StateTransition 
{
	Transition[] transitions;

	public StateTransition(Transition[] transitions) 
	{
		this.transitions = transitions;
	}

	public void ExecuteTransition(Enum transition)
	{
		Transition selectTransition = transitions.Where(t => t.transitionName == transition.ToString()).FirstOrDefault();
		selectTransition.changePresentAction.Invoke();
	}
}

public class Transition
{
	public string transitionName;
	public Action changePresentAction;

	public Transition(Enum transitionName, Action changePresentAction = null) 
	{
		this.transitionName = transitionName.ToString();
		if (changePresentAction != null) {
			this.changePresentAction = changePresentAction;
		}else {
			this.changePresentAction = BaseController.GetDefaultTransitionAction(transitionName);
		}
	}
}