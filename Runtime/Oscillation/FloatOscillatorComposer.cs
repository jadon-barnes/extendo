namespace Extendo.Oscillation
{
	public class FloatOscillatorComposer : OscillatorComposer<FloatOscillator, float>
	{
		public override float GetSumOfModulations()
		{
			float sum = 0f;

			foreach (var modulation in modulations)
			{
				sum += modulation.Evaluate(time);
			}

			return sum * strength;
		}
	}
}