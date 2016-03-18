using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncStateMachine
{
    public class Transition
    {
        private IState CurrentState { get; set; }

        /// <summary>
        /// Checks whether this Transistion is currently pending
        /// </summary>
        /// <returns>Returns true if this transition is pending, otherwise false</returns>
        public bool IsPending => CurrentState.GetType() == typeof (PendingState);

        /// <summary>
        /// Checks whether this Transistion is currently running
        /// </summary>
        /// <returns>Returns true if this transition is running, otherwise false</returns>
        public bool IsRunning => CurrentState.GetType() == typeof (RunningState);

        /// <summary>
        /// Checks whether this Transistion is interrupted
        /// </summary>
        /// <returns>Returns true if this transition is interrupted, otherwise false</returns>
        public bool IsInterrupted => CurrentState.GetType() == typeof(InterruptedState);

        /// <summary>
        /// Checks whether this Transistion is completed
        /// </summary>
        /// <returns>Returns true if this transition is completed, otherwise false</returns>
        public bool IsCompleted => CurrentState.GetType() == typeof(CompletedState);

        public Transition()
        {
            CurrentState = new PendingState();
        }

        public void Execute()
        {
            CurrentState.Execute(this);
        }

        public void Interrupt()
        {
            CurrentState.Interrupt(this);
        }

        public void Complete()
        {
            CurrentState.Complete(this);
        }

        #region States

        public interface IState
        {
            void Execute(Transition transition);
            void Interrupt(Transition transition);
            void Complete(Transition transition);
        }

        public class PendingState : IState
        {
            public void Execute(Transition transition)
            {
                transition.CurrentState = new RunningState();
            }

            public void Interrupt(Transition transition)
            {
                transition.CurrentState = new InterruptedState();
            }

            public void Complete(Transition transition)
            {
                // Unable to complete a pending state
            }
        }

        public class RunningState : IState
        {
            public void Execute(Transition transition)
            {
                // Unable to execute an already running transition
            }

            public void Interrupt(Transition transition)
            {
                transition.CurrentState = new InterruptedState();
            }

            public void Complete(Transition transition)
            {
                transition.CurrentState = new CompletedState();
            }
        }

        public class InterruptedState : IState
        {
            public void Execute(Transition transition)
            {
                // Unable to execute an interrupted transition
            }

            public void Interrupt(Transition transition)
            {
                // Unable to interrupt an already interrupted transition
            }

            public void Complete(Transition transition)
            {
                // Unable to complete an interrupted transition
            }
        }

        public class CompletedState : IState
        {
            public void Execute(Transition transition)
            {
                // Unable to execute a completed transition
            }

            public void Interrupt(Transition transition)
            {
                // Unable to interrupt a completed transition
            }

            public void Complete(Transition transition)
            {
                // Unable to complete an already completed transition
            }
        }

        #endregion
    }   
}
