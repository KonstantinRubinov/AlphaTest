using MySql.Data.MySqlClient;

namespace AlphaTestSystem
{
	static public class DataQueryStringsMySql
	{
		static private string queryUsersByLogin = "SELECT * from Users WHERE Users.userName=@userNickName and Users.userPassword=@userPassword;";
		static private string queryIsFollowedByMeString = "SELECT COUNT(1) FROM FOLLOWS WHERE FOLLOWS.followedID = @followedID and FOLLOWS.followerID=@followerID;";
		static private string queryAddFollower = "Set @IncrementValue=1; " +
												 "INSERT INTO FOLLOWS(followedID, followerID) VALUES(@followedID, @followerID); " +
												 "UPDATE USERS SET USERS.userNumberOfFollowers = userNumberOfFollowers + @IncrementValue WHERE USERS.userID = @followedID; " +
												 "SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select COUNT(1) FROM FOLLOWS " +
												 "WHERE FOLLOWS.followedID = @followedID and FOLLOWS.followerID=@followerID) followedByMe " +
												 "from USERS " +
												 "LEFT JOIN alphatest.GROUPS ON USERS.userGroupId=alphatest.GROUPS.groupID " +
												 "WHERE USERS.userID = @followedID;";
		static private string queryDeleteFollower = "Set @IncrementValue=1; " +
													"DELETE FROM alphatest.FOLLOWS WHERE alphatest.FOLLOWS.followedID = @followedID and alphatest.FOLLOWS.followerID=@followerID; " +
													"UPDATE USERS SET USERS.userNumberOfFollowers = userNumberOfFollowers - @IncrementValue WHERE USERS.userID = @followedID; " +
													"SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select COUNT(1) FROM FOLLOWS " +
													"WHERE FOLLOWS.followedID = @followedID and FOLLOWS.followerID= @followerID) followedByMe " +
													"from USERS " +
													"LEFT JOIN alphatest.GROUPS ON USERS.userGroupId= alphatest.GROUPS.groupID " +
													"WHERE USERS.userID = @followedID;";
		static private string queryUsersWithFollowString = "SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, " +
														   "(select COUNT(1) FROM FOLLOWS " +
														   "WHERE FOLLOWS.followedID = USERS.userID and FOLLOWS.followerID=@myId) followedByMe " +
														   "from alphatest.USERS " +
														   "LEFT JOIN alphatest.GROUPS ON alphatest.USERS.userGroupId= alphatest.GROUPS.groupID;";
		static private string queryGetOneUserForTableByIdString = "SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select COUNT(*) FROM alphatest.FOLLOWS WHERE alphatest.FOLLOWS.followedID = @followedID and FOLLOWS.followerID= @followerID) followedByMe " +
																  "from alphatest.USERS " +
																  "LEFT JOIN alphatest.GROUPS ON alphatest.USERS.userGroupId= alphatest.GROUPS.groupID " +
																  "WHERE alphatest.USERS.userID = @followedID;";








		static private string procedureUsersByLogin = "CALL `AlphaTest`.`ReturnUserByNamePassword`(@userNickName, @userPassword);";
		static private string procedureIsFollowedByMeString = "CALL `AlphaTest`.`IsFollowed`(@followedID, @followerID);";
		static private string procedureAddFollower = "CALL `AlphaTest`.`AddFollower`(@followedID, @followerID);";
		static private string procedureDeleteFollower = "CALL `AlphaTest`.`DeleteFollower`(@followedID, @followerID);";
		static private string procedureUsersWithFollowString = "CALL `AlphaTest`.`GetUsersForTable`(@myId);";
		static private string procedureGetOneUserForTableByIdString = "CALL `AlphaTest`.`GetOneUserForTableById`(@followedID, @followerID);";

		static public MySqlCommand ReturnUserByNamePassword(string userNickName, string userPassword)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userNickName, userPassword, queryUsersByLogin);
			else
				return CreateSqlCommand(userNickName, userPassword, procedureUsersByLogin);
		}

		static public MySqlCommand IsFollowedByMe(int followedID, int followerID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followedID, followerID, queryIsFollowedByMeString);
			else
				return CreateSqlCommand(followedID, followerID, procedureIsFollowedByMeString);
		}

		static public MySqlCommand AddFollower(int followedID, int followerID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followedID, followerID, queryAddFollower);
			else
				return CreateSqlCommand(followedID, followerID, procedureAddFollower);
		}

		static public MySqlCommand DeleteFollower(int followedID, int followerID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followedID, followerID, queryDeleteFollower);
			else
				return CreateSqlCommand(followedID, followerID, procedureDeleteFollower);
		}

		static public MySqlCommand GetUsersForTable(int myId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(myId, queryUsersWithFollowString);
			else
				return CreateSqlCommand(myId, procedureUsersWithFollowString);
		}

		static public MySqlCommand GetOneUserForTableById(int followerID, int followedID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followerID, followedID, queryGetOneUserForTableByIdString);
			else
				return CreateSqlCommand(followerID, followedID, procedureGetOneUserForTableByIdString);
		}

		static private MySqlCommand CreateSqlCommand(string userNickName, string userPassword, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userNickName", userNickName);
			command.Parameters.AddWithValue("@userPassword", userPassword);
			return command;
		}

		static private MySqlCommand CreateSqlCommand(int followedID, int followerID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@followedID", followedID);
			command.Parameters.AddWithValue("@followerID", followerID);
			return command;
		}

		static private MySqlCommand CreateSqlCommand(int myId, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@myId", myId);
			return command;
		}

		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}
	}
}
