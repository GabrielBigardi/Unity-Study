public static class Helpers
{
	public static float Map(float value, float inputStart, float inputStop, float outputStart, float outputStop)
	{
		return outputStart + (outputStop - outputStart) * ((value - inputStart) / (inputStop - inputStart));
	}
}