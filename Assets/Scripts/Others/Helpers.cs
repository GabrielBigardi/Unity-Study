public enum DayOfWeek
{
	Monday,
	Tuesday,
	Wednesday,
	Thursday,
	Friday,
	Saturday,
	Sunday,
}

public static class Helpers
{
	public static float Map(float value, float inputStart, float inputStop, float outputStart, float outputStop)
	{
		return outputStart + (outputStop - outputStart) * ((value - inputStart) / (inputStop - inputStart));
	}

	public static DayOfWeek GetDayOfWeek(int day)
	{
		return (DayOfWeek)((day - 1) % 7);
	}
}