namespace AlphaTestSystem
{
	public interface ITesterRepository
	{
		GroupModel AddGroup(int groupID, string groupName);
		UserModel AddUser(int tmpUserId, string tmpUserName, int tmpUserGroupId, int tmpUserNumberOfFollowers, string tmpUserPassword);
	}
}
