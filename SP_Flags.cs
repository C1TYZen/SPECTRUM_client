namespace graph1
{
	/// <summary>
	/// Класс флагов для доступа к разным функциям.
	/// Нужен в основном для общения между потоками.
	/// </summary>
	static class SP_Flags
	{
		public static bool get_ready_flag = false;
		public static bool mesure_status_flag = false;
		public static bool external_message_flag = false;
	}
}
