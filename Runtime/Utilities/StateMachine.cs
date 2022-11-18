using UnityEngine;

namespace Extendo.Utilities
{
	/// <summary>
	/// A generic state machine that can be used to define state-based logic.
	/// </summary>
	/// <typeparam name="TContext">The class that provides context to this state machine's states.</typeparam>
	/// <seealso cref="State{TContext}"/>
	public class StateMachine<TContext>
	{
		public    TContext          Context      { get; private set; }
		public    State<TContext>   CurrentState { get; private set; }
		public    State<TContext>   LastState    { get; private set; }
		protected State<TContext>[] states;

		public StateMachine(TContext context, params State<TContext>[] states)
		{
			this.Context = context;
			this.states  = states;
		}

		/// <summary>
		/// Sets the current state to the reference provided.
		/// </summary>
		public void TransitionTo(State<TContext> state)
		{
			if (CurrentState != null)
			{
				LastState = CurrentState;
				LastState.OnExit();
			}

			CurrentState = state;
			CurrentState.OnEnter();
		}

		/// <summary>
		/// Sets the current state to the state type provided. If the state does not exist, a warning will be logged.
		/// </summary>
		public void TransitionTo<TState>() where TState : State<TContext>
		{
			foreach (var state in states)
			{
				if (state is not TState)
					continue;

				TransitionTo(state);
				return;
			}

			Debug.LogWarning($"{typeof(TState)} was not found in {typeof(StateMachine<TContext>)}!");
		}

		public void UpdateActiveState()
		{
			CurrentState?.OnUpdate();
		}
	}
}