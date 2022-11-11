namespace Extendo.Utilities
{
	public abstract class State<TContext>
	{
		protected TContext Context { get; private set; }

		protected State(TContext context)
		{
			Context = context;
		}

		public abstract void OnEnter();
		public abstract void OnUpdate();
		public abstract void OnExit();
	}
}