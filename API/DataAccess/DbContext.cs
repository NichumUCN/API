using Microsoft.Data.SqlClient;

namespace API.DataAccess {
	public class DbContext {
		private static DbContext _instance = null;

		public SqlConnection Connection = null;

		private DbContext() {
		}

		public static DbContext? Instance(string connectionString) {

			if (_instance == null) {
				_instance = new DbContext();
			}
			_instance.Connection = new SqlConnection(connectionString);
			return _instance;
		}
	}
}
