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
				PreviousState.OnExit(Context);
			}

			CurrentState = state;
			CurrentState.OnEnter(Context);
		}

		public void Update()
		{
			CurrentState?.OnUpdate(Context);
		}

		public abstract class State
		{
			public abstract void OnEnter(TContext context);
			public abstract void OnUpdate(TContext context);
			public abstract void OnExit(TContext context);
		}
	}
}