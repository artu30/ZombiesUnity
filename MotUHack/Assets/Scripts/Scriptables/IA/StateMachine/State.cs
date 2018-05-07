using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Zombie Assets/AI/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(ZombieStateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    public void DoActions(ZombieStateController controller)
    {
        for(int i = 0; i < actions.Length; ++i)
        {
            actions[i].Act(controller);
        }
    }
    
    private void CheckTransitions(ZombieStateController controller)
    {
        for (int i = 0; i < transitions.Length; ++i)
        {
            bool decisionSucceded = transitions[i].decision.Decide(controller);
            if (decisionSucceded) controller.TransitionToState(transitions[i].trueState);
            else controller.TransitionToState(transitions[i].falseState);
        }
    }
}
