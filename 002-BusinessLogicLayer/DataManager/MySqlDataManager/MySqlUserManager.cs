using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AlphaTestSystem
{
	public class MySqlUserManager : MySqlDataBase, IUserRepository
	{
		public bool IsFollowedByMe(int followedID, int followerID)
		{
			if (followedID < 0)
				throw new ArgumentOutOfRangeException();
			if (followerID < 0)
				throw new ArgumentOutOfRangeException();


			DataTable dt = new DataTable();
			int followed = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsMySql.IsFollowedByMe(followedID, followerID));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsMySql.ReturnUserByNamePassword(name, password));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsMySql.AddFollower(followedID, followerID));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsMySql.DeleteFollower(followedID, followerID));
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

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsMySql.GetUsersForTable(myId));
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
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(DataQueryStringsMySql.GetOneUserForTableById(followerID, followedID));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserForTableModel.ToObject(ms);
			}

			return userModel;
		}
	}
}
