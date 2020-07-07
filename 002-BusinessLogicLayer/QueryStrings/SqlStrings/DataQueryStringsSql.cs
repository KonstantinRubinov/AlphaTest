using System.Data.SqlClient;

namespace AlphaTestSystem
{
	static public class DataQueryStringsSql
	{
		static private string queryUsersByLogin = "SELECT * from Users WHERE userName=@userNickName and userPassword=@userPassword;";
		static private string queryIsFollowedByMeString = "SELECT COUNT(1) FROM FOLLOWS WHERE followedID = @followedID and followerID=@followerID;";
		static private string queryAddFollower = "INSERT INTO FOLLOWS (followedID, followerID) VALUES (@followedID, @followerID); DECLARE @IncrementValue int SET @IncrementValue = 1 UPDATE USERS SET userNumberOfFollowers = userNumberOfFollowers + @IncrementValue WHERE userID = @followedID; SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(1) AS BIT) FROM[FOLLOWS] WHERE(followedID = USERS.userID and followerID = @followerID)) as followedByMe from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID WHERE USERS.userID = @followedID;";
		static private string queryDeleteFollower = "DELETE FROM FOLLOWS WHERE followedID = @followedID and followerID=@followerID; DECLARE @IncrementValue int SET @IncrementValue = 1 UPDATE USERS SET userNumberOfFollowers = userNumberOfFollowers - @IncrementValue WHERE userID = @followedID; SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(1) AS BIT) FROM[FOLLOWS] WHERE(followedID = USERS.userID and followerID = @followerID)) as followedByMe from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID WHERE USERS.userID = @followedID;";
		static private string queryUsersWithFollowString = "SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(1) AS BIT) FROM [FOLLOWS] WHERE (followedID = USERS.userID and followerID=@myId)) as followedByMe " +
														   "from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID;";
		static private string queryGetOneUserForTableByIdString = "SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(1) AS BIT) FROM [FOLLOWS] WHERE (followedID = @followedID and followerID=@followerID)) as followedByMe " +
																  "from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID " +
																  "WHERE USERS.userID = @followedID;";




		static private string procedureUsersByLogin = "EXEC ReturnUserByNamePassword @userNickName, @userPassword;";
		static private string procedureIsFollowedByMeString = "EXEC IsFollowed @followedID, @followerID;";
		static private string procedureAddFollower = "EXEC AddFollower @followedID, @followerID;";
		static private string procedureDeleteFollower = "EXEC DeleteFollower @followedID, @followerID;";
		static private string procedureUsersWithFollowString = "EXEC GetUsersForTable @myId;";
		static private string procedureGetOneUserForTableByIdString = "EXEC GetOneUserForTableById @followedID, @followerID;";

		static public SqlCommand ReturnUserByNamePassword(string userNickName, string userPassword)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userNickName, userPassword, queryUsersByLogin);
			else
				return CreateSqlCommand(userNickName, userPassword, procedureUsersByLogin);
		}

		static public SqlCommand IsFollowedByMe(int followedID, int followerID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followedID, followerID, queryIsFollowedByMeString);
			else
				return CreateSqlCommand(followedID, followerID, procedureIsFollowedByMeString);
		}

		static public SqlCommand AddFollower(int followedID, int followerID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followedID, followerID, queryAddFollower);
			else
				return CreateSqlCommand(followedID, followerID, procedureAddFollower);
		}

		static public SqlCommand DeleteFollower(int followedID, int followerID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followedID, followerID, queryDeleteFollower);
			else
				return CreateSqlCommand(followedID, followerID, procedureDeleteFollower);
		}

		static public SqlCommand GetUsersForTable(int myId)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(myId, queryUsersWithFollowString);
			else
				return CreateSqlCommand(myId, procedureUsersWithFollowString);
		}

		static public SqlCommand GetOneUserForTableById(int followerID, int followedID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(followerID, followedID, queryGetOneUserForTableByIdString);
			else
				return CreateSqlCommand(followerID, followedID, procedureGetOneUserForTableByIdString);
		}

		static private SqlCommand CreateSqlCommand(string userNickName, string userPassword, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);
			command.Parameters.AddWithValue("@userNickName", userNickName);
			command.Parameters.AddWithValue("@userPassword", userPassword);
			return command;
		}

		static private SqlCommand CreateSqlCommand(int followedID, int followerID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);
			command.Parameters.AddWithValue("@followedID", followedID);
			command.Parameters.AddWithValue("@followerID", followerID);
			return command;
		}

		static private SqlCommand CreateSqlCommand(int myId, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);
			command.Parameters.AddWithValue("@myId", myId);
			return command;
		}

		static private SqlCommand CreateSqlCommand(string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			return command;
		}
	}
}
