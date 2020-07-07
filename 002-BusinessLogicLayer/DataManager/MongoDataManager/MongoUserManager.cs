using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTestSystem
{
	public class MongoUserManager : IUserRepository
	{
		private readonly IMongoCollection<FollowsModel> _follows;
		private readonly IMongoCollection<GroupModel> _group;
		private readonly IMongoCollection<UserModel> _users;

		public MongoUserManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);


			_follows = database.GetCollection<FollowsModel>(ConnectionStrings.FollowsCollectionName);
			_group = database.GetCollection<GroupModel>(ConnectionStrings.GroupCollectionName);
			_users = database.GetCollection<UserModel>(ConnectionStrings.UserCollectionName);
		}

		public bool IsFollowedByMe(int followedID, int followerID)
		{
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();

			return _follows.Find<FollowsModel>(f => f.followedID==followedID && f.followerID==followerID).Any();
			
		}

		public UserModel GetUserByLogin(string name, string password)
		{
			if (name.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (password.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _users.Find<UserModel>(user => user.userName.Equals(name) && user.userPassword.Equals(password)).Project(u => new UserModel
			{
				userID = u.userID,
				userName = u.userName,
				userGroupId = u.userGroupId,
				userNumberOfFollowers = u.userNumberOfFollowers,
				userPassword = u.userPassword
			}).SingleOrDefault();
		}


		public UserForTableModel AddFollower(int mfollowedID, int mfollowerID)
		{
			if (mfollowedID < 0)
				throw new ArgumentOutOfRangeException();
			if (mfollowerID < 0)
				throw new ArgumentOutOfRangeException();

			FollowsModel followsModel = new FollowsModel();
			followsModel.followedID = mfollowedID;
			followsModel.followerID = mfollowerID;

			_follows.InsertOne(followsModel);
			UserModel userModel= _users.Find<UserModel>(user => user.userID==mfollowedID).Project(u => new UserModel
			{
				userID = u.userID,
				userName = u.userName,
				userGroupId = u.userGroupId,
				userNumberOfFollowers = u.userNumberOfFollowers,
				userPassword = u.userPassword
			}).SingleOrDefault();
			if (userModel == null)
				return null;

			userModel.userNumberOfFollowers = userModel.userNumberOfFollowers + 1;
			_users.ReplaceOne(u => u.userID == mfollowedID, userModel);
			return GetOneUserForTableById(mfollowerID, userModel.userID);
		}


		public UserForTableModel DeleteFollower(int mfollowedID, int mfollowerID)
		{
			if (mfollowedID < 0)
				throw new ArgumentOutOfRangeException();
			if (mfollowerID < 0)
				throw new ArgumentOutOfRangeException();

			_follows.DeleteOne(followsModel => followsModel.followedID == mfollowedID && followsModel.followerID == mfollowerID);
			UserModel userModel = _users.Find<UserModel>(user => user.userID == mfollowedID).Project(u => new UserModel
			{
				userID = u.userID,
				userName = u.userName,
				userGroupId = u.userGroupId,
				userNumberOfFollowers = u.userNumberOfFollowers,
				userPassword = u.userPassword
			}).SingleOrDefault();
			if (userModel == null)
				return null;

			userModel.userNumberOfFollowers = userModel.userNumberOfFollowers - 1;
			_users.ReplaceOne(u => u.userID == mfollowedID, userModel);
			return GetOneUserForTableById(mfollowerID, userModel.userID);
		}


		public List<UserForTableModel> GetUsersForTable(int myId)
		{
			var resultQuary = from users in _users.AsQueryable()
							  join groups in _group.AsQueryable() on users.userGroupId equals groups.groupID into joinedGroups
							  select new UserForTableModel
							  {
								  userID = users.userID,
								  userName = users.userName,
								  groupName = joinedGroups.First().groupName,
								  userNumberOfFollowers = users.userNumberOfFollowers
							  };

			List<UserForTableModel> usersForTable = new List<UserForTableModel>();

			foreach (UserForTableModel userForTable in resultQuary)
			{
				userForTable.followedByMe = IsFollowedByMe(userForTable.userID, myId);
				usersForTable.Add(userForTable);
			}

			return usersForTable;
		}


		public UserForTableModel GetOneUserForTableById(int followerID, int followedID)
		{
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();

			var resultQuary = from users in _users.AsQueryable()
							  join groups in _group.AsQueryable() on users.userGroupId equals groups.groupID into joinedGroups
							  where (users.userID == followedID)
							  select new UserForTableModel
							  {
								  userID = users.userID,
								  userName = users.userName,
								  groupName = joinedGroups.First().groupName,
								  userNumberOfFollowers = users.userNumberOfFollowers
							  };
			
			UserForTableModel userForTableModel = resultQuary.SingleOrDefault();
			userForTableModel.followedByMe = IsFollowedByMe(followedID, followerID);
			return userForTableModel;
		}

		

		private string FindGroupNameById(int id)
		{
			GroupModel groupModel = _group.Find<GroupModel>(group => group.groupID == id).Project(g => new GroupModel
			{
				groupName = g.groupName
			}).SingleOrDefault();

			return groupModel.groupName;
		}
	}
}
