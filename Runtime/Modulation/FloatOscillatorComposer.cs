namespace Extendo.Modulation
{
	public class FloatOscillatorComposer : OscillatorComposer<FloatModulator, float>
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