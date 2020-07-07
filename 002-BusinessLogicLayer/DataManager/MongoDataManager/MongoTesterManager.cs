using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTestSystem
{
	public class MongoTesterManager: ITesterRepository
	{
		private readonly IMongoCollection<GroupModel> _groups;
		private readonly IMongoCollection<UserModel> _users;

		public MongoTesterManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_groups = database.GetCollection<GroupModel>(ConnectionStrings.GroupCollectionName);
			_users = database.GetCollection<UserModel>(ConnectionStrings.UserCollectionName);
		}

		public GroupModel AddGroup(int groupID, string groupName)
		{
			if (!_groups.Find<GroupModel>(gr => gr.groupID.Equals(groupID)).Any())
				_groups.InsertOne(new GroupModel(groupID, groupName));
			GroupModel tmpGroupModel = GetGroupById(groupID);
			return tmpGroupModel;
		}

		private GroupModel GetGroupById(int groupID)
		{
			return _groups.Find(Builders<GroupModel>.Filter.Eq(gr => gr.groupID, groupID)).Project(group => new GroupModel
			{
				groupID = group.groupID,
				groupName = group.groupName
			}).SingleOrDefault();
		}

		public UserModel AddUser(int tmpUserId, string tmpUserName, int tmpUserGroupId, int tmpUserNumberOfFollowers, string tmpUserPassword)
		{
			if (!_users.Find<UserModel>(u => u.userID.Equals(tmpUserId)).Any())
				_users.InsertOne(new UserModel(tmpUserId, tmpUserName, tmpUserGroupId, tmpUserNumberOfFollowers, tmpUserPassword));
			UserModel tmpUserModel = GetUserById(tmpUserId);
			return tmpUserModel;
		}

		private UserModel GetUserById(int userId)
		{
			return _users.Find(Builders<UserModel>.Filter.Eq(u => u.userID, userId)).Project(user => new UserModel
			{
				userID = user.userID,
				userName = user.userName,
				userGroupId = user.userGroupId,
				userNumberOfFollowers = user.userNumberOfFollowers,
				userPassword = user.userPassword
			}).SingleOrDefault();
		}
	}
}
