namespace graph1
{
	/// <summary>
	/// Класс флагов для доступа к разным функциям.
	/// Нужен в основно для общения между потоками.
	/// </summary>
	public static class SP_Flags
	{
		public static bool buttons_enable_flag = false;
		public static bool mesure_status_flag = false;
		public static bool external_message_flag = false;
	}
}
