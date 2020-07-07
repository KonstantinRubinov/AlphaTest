namespace AlphaTestSystem
{
	static public class ConnectionStrings
	{
		static private string sqlConnectionString = "Data Source =.; Initial Catalog = AlphaTest; Integrated Security = True";
		static private string mySqlConnectionString = "server=localhost; user id = root; persistsecurityinfo=True; password=Rk14101981; database=AlphaTest; UseAffectedRows=True; Allow User Variables=True";

		static public string ConnectionString = "mongodb://localhost:27017";
		static public string DatabaseName = "AlphaTest";
		static public string FollowsCollectionName = "Follows";
		static public string GroupCollectionName = "Group";
		static public string UserCollectionName = "User";

		static public string GetSqlConnection()
		{
			return sqlConnectionString;
		}

		static public string GetMySqlConnection()
		{
			return mySqlConnectionString;
		}
	}
}
