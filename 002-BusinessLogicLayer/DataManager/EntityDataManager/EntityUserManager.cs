using AlphaTestSystem.EntityDataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTestSystem
{
	public class EntityUserManager : EntityBaseManager, IUserRepository
	{
		public bool IsFollowedByMe(int followedID, int followerID)
		{
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();


			if (GlobalVariable.queryType == 0)
				return DB.FOLLOWS.Any(u => u.followedID == followedID && u.followerID == followerID);
			else
			{
				if (DB.IsFollowedByMe(followedID, followerID).Equals(1))
					return true;
				else
					return false;
			}
		}

		public UserModel GetUserByLogin(string name, string password)
		{
			if (name.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (password.Equals(""))
				throw new ArgumentOutOfRangeException();

			if (GlobalVariable.queryType == 0)
				return DB.USERS.Where(u => u.userName.Equals(name)).Where(u => u.userPassword.Equals(password)).Select(u => new UserModel
				{
					userID = u.userID,
					userName = u.userName,
					userGroupId = u.userGroupId,
					userNumberOfFollowers = u.userNumberOfFollowers,
					userPassword = u.userPassword
				}).SingleOrDefault();
			else
				return DB.ReturnUserByNamePassword(name, password).Select(u => new UserModel
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

			if (GlobalVariable.queryType == 0)
			{
				FOLLOW follow = new FOLLOW
				{
					followedID = mfollowedID,
					followerID = mfollowerID
				};
				DB.FOLLOWS.Add(follow);

				USER user = DB.USERS.Where(u => u.userID == mfollowedID).SingleOrDefault();
				if (user == null)
					return null;
				user.userNumberOfFollowers = user.userNumberOfFollowers + 1;
				int flag = DB.SaveChanges();
				return GetOneUserForTableById(mfollowerID, user.userID);
			}
			else
				return DB.AddFollower(mfollowedID, mfollowerID).Select(u => new UserForTableModel
				{
					userID = u.userID,
					userName = u.userName,
					groupName = u.groupName,
					userNumberOfFollowers = u.userNumberOfFollowers,
					followedByMe = u.followedByMe.Value
				}).SingleOrDefault();
		}


		public UserForTableModel DeleteFollower(int mfollowedID, int mfollowerID)
		{
			if (mfollowedID < 0)
				throw new ArgumentOutOfRangeException();
			if (mfollowerID < 0)
				throw new ArgumentOutOfRangeException();

			if (GlobalVariable.queryType == 0)
			{
				FOLLOW follow = DB.FOLLOWS.Where(f => f.followedID == mfollowedID && f.followerID == mfollowerID).SingleOrDefault();
				if (follow == null)
					return GetOneUserForTableById(mfollowerID, mfollowedID);
				DB.FOLLOWS.Remove(follow);

				USER user = DB.USERS.Where(u => u.userID == mfollowedID).SingleOrDefault();
				user.userNumberOfFollowers = user.userNumberOfFollowers - 1;

				int flag = DB.SaveChanges();
				return GetOneUserForTableById(mfollowerID, user.userID);
			}
			else
				return DB.DeleteFollower(mfollowedID, mfollowerID).Select(u => new UserForTableModel
				{
					userID = u.userID,
					userName = u.userName,
					groupName = u.groupName,
					userNumberOfFollowers = u.userNumberOfFollowers,
					followedByMe = u.followedByMe.Value
				}).SingleOrDefault();
		}


		public List<UserForTableModel> GetUsersForTable(int myId)
		{
			if (GlobalVariable.queryType == 0)
				return DB.USERS.Select(u => new UserForTableModel
				{
					userID = u.userID,
					userName = u.userName,
					groupName = u.GROUP.groupName,
					userNumberOfFollowers = u.userNumberOfFollowers,
					followedByMe = DB.FOLLOWS.Any(f => f.followerID == myId && f.followedID == u.userID)
				}).ToList();
			else
				return DB.GetUsersForTable(myId).Select(u => new UserForTableModel
				{
					userID = u.userID,
					userName = u.userName,
					groupName = u.groupName,
					userNumberOfFollowers = u.userNumberOfFollowers,
					followedByMe = u.followedByMe.Value
				}).ToList();
		}


		public UserForTableModel GetOneUserForTableById(int followerID, int followedID)
		{
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();

			if (GlobalVariable.queryType == 0)
				return DB.USERS.Where(u => u.userID == followedID).Select(u => new UserForTableModel
				{
					userID = u.userID,
					userName = u.userName,
					groupName = u.GROUP.groupName,
					userNumberOfFollowers = u.userNumberOfFollowers,
					followedByMe = DB.FOLLOWS.Any(f => f.followerID == followerID && f.followedID == followedID)
				}).SingleOrDefault();
			else
				return DB.GetOneUserForTableById(followerID, followedID).Select(u => new UserForTableModel
				{
					userID = u.userID,
					userName = u.userName,
					groupName = u.groupName,
					userNumberOfFollowers = u.userNumberOfFollowers,
					followedByMe = u.followedByMe.Value
				}).SingleOrDefault();
		}
	}
}
