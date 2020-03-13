namespace graph1
{
	/// <summary>
	/// Класс флагов для доступа к разным функциям.
	/// Нужен в основном для общения между потоками.
	/// </summary>
	static class SP_Flags
	{
		/// <summary>
		/// Флаг для перевода интерфейса в активное состояние
		/// </summary>
		public static bool get_ready_flag = false;
		/// <summary>
		/// Флаг для вывода сообщения из параллельного потока
		/// </summary>
		public static bool external_message_flag = false;
	}
}
