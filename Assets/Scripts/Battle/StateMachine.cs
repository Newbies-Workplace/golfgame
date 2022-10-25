using UnityEngine;

namespace Battle
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;
        private State _previousState;
        
        public void SetState(State state)
        {
            if (State != null)
                _previousState = State;
            
            State = state;
            StartCoroutine(State.Start());
        }

        public State GetPreviousState()
        {
            return _previousState;
        }
    }
}