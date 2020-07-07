using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AlphaTestSystem
{
	public class SqlUserManager : SqlDataBase, IUserRepository
	{
		public bool IsFollowedByMe(int followedID, int followerID)
		{
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();


			DataTable dt = new DataTable();
			int followed = 0;
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsSql.IsFollowedByMe(followedID, followerID));
			}
			foreach (DataRow ms in dt.Rows)
			{
				followed = int.Parse(ms[0].ToString());
			}

			if (followed == 0) return false;
			else return true;
		}

		public UserModel GetUserByLogin(string name, string password)
		{
			if (name.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (password.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			UserModel userModel = new UserModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsSql.ReturnUserByNamePassword(name, password));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObject(ms);
			}

			return userModel;
		}

		public UserForTableModel AddFollower(int followedID, int followerID)
		{
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			UserForTableModel userModel = new UserForTableModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsSql.AddFollower(followedID, followerID));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserForTableModel.ToObject(ms);
			}

			return userModel;
		}

		public UserForTableModel DeleteFollower(int followedID, int followerID)
		{
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			UserForTableModel userModel = new UserForTableModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsSql.DeleteFollower(followedID, followerID));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserForTableModel.ToObject(ms);
			}

			return userModel;
		}

		public List<UserForTableModel> GetUsersForTable(int myId)
		{
			DataTable dt = new DataTable();
			List<UserForTableModel> arrUsers = new List<UserForTableModel>();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsSql.GetUsersForTable(myId));
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrUsers.Add(UserForTableModel.ToObject(ms));
			}

			return arrUsers;
		}

		public UserForTableModel GetOneUserForTableById(int followerID, int followedID)
		{
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			UserForTableModel userModel = new UserForTableModel();
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsSql.GetOneUserForTableById(followerID, followedID));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserForTableModel.ToObject(ms);
			}

			return userModel;
		}
	}
}
