namespace Extendo.Utilities
{
	/// <summary>
	/// A generic state machine that can be used to define state-based logic.
	/// </summary>
	/// <typeparam name="TContext">The class that provides context to this state machine's states.</typeparam>
	/// <seealso cref="State"/>
	public class StateMachine<TContext>
	{
		public TContext Context       { get; private set; }
		public State    CurrentState  { get; private set; }
		public State    PreviousState { get; private set; }

		public StateMachine(TContext context)
		{
			Context = context;
		}

		/// <summary>
		/// Sets the current state to the reference provided.
		/// </summary>
		public void TransitionTo(State state)
		{
			if (CurrentState != null)
			{
				PreviousState = CurrentState;
				PreviousState.OnExit();
			}

			CurrentState = state;
			CurrentState.OnEnter();
		}

		public void TransitionToPreviousState()
		{
			TransitionTo(PreviousState);
		}

		public void Update()
		{
			CurrentState?.OnUpdate();
		}

		public abstract class State
		{
			public TContext Context { get; private set; }

			protected State(TContext context)
			{
				Context = context;
			}

			public abstract void OnEnter();
			public abstract void OnExit();
			public abstract void OnUpdate();
		}
	}
}