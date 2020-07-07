using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace AlphaTestSystem
{
	[DataContract]
	public class UserForTableModel
	{
		private int _userID;
		private string _userName;
		private string _groupName;
		private int _userNumberOfFollowers;
		private bool _followedByMe;

		[DataMember]
		public int userID
		{
			get { return _userID; }
			set { _userID = value; }
		}

		[DataMember]
		public string userName
		{
			get { return _userName; }
			set { _userName = value; }
		}

		[DataMember]
		public string groupName
		{
			get { return _groupName; }
			set { _groupName = value; }
		}

		[DataMember]
		public int userNumberOfFollowers
		{
			get { return _userNumberOfFollowers; }
			set { _userNumberOfFollowers = value; }
		}

		[DataMember]
		public bool followedByMe
		{
			get { return _followedByMe; }
			set { _followedByMe = value; }
		}

		public override string ToString()
		{
			return
				userID + " " +
				userName + " " +
				groupName + " " +
				userNumberOfFollowers + " " +
				followedByMe;
		}

		public static UserForTableModel ToObject(DataRow reader)
		{
			UserForTableModel userForTableModel = new UserForTableModel();
			userForTableModel.userID = int.Parse(reader[0].ToString());
			userForTableModel.userName = reader[1].ToString();
			userForTableModel.groupName = reader[2].ToString();
			userForTableModel.userNumberOfFollowers = int.Parse(reader[3].ToString());

			try
			{
				userForTableModel.followedByMe = int.Parse(reader[4].ToString()) > 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mysql followedByMe: " + ex.Message);
			}

			try
			{
				userForTableModel.followedByMe = bool.Parse(reader[4].ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mssql followedByMe: " + ex.Message);
			}

			Debug.WriteLine("userForTableModel:" + userForTableModel.ToString());
			return userForTableModel;
		}
	}
}
