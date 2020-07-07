using System.Collections.Generic;

namespace AlphaTestSystem
{
	public interface IUserRepository
	{
		//List<UserModel> GetAllUsers();
		bool IsFollowedByMe(int followedID, int followerID);
		UserModel GetUserByLogin(string name, string password);
		UserForTableModel AddFollower(int followedID, int followerID);
		UserForTableModel DeleteFollower(int followedID, int followerID);
		List<UserForTableModel> GetUsersForTable(int myId);
		UserForTableModel GetOneUserForTableById(int followerID, int followedID);
	}
}
