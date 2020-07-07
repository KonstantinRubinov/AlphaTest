using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace AlphaTestSystem
{
	[DataContract]
	public class UserModel
	{
		private int _userID;
		private string _userName;
		private int _userGroupId;
		private int _userNumberOfFollowers;
		private string _userPassword;
		private bool _followedByMe;

		public UserModel() { }

		public UserModel(int tmpUserId, string tmpUserName, int tmpUserGroupId, int tmpUserNumberOfFollowers, string tmpUserPassword) {
			userID = tmpUserId;
			userName = tmpUserName;
			userGroupId = tmpUserGroupId;
			userNumberOfFollowers = tmpUserNumberOfFollowers;
			userPassword = tmpUserPassword;
		}

		[BsonId]
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
		public int userGroupId
		{
			get { return _userGroupId; }
			set { _userGroupId = value; }
		}

		[DataMember]
		public int userNumberOfFollowers
		{
			get { return _userNumberOfFollowers; }
			set { _userNumberOfFollowers = value; }
		}

		[DataMember]
		public string userPassword
		{
			get { return _userPassword; }
			set { _userPassword = value; }
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
				userGroupId + " " +
				userNumberOfFollowers + " " +
				userPassword + " " +
				followedByMe;
		}

		public static UserModel ToObject(DataRow reader)
		{
			UserModel userModel = new UserModel();
			userModel.userID = int.Parse(reader[0].ToString());
			userModel.userName = reader[1].ToString();
			userModel.userGroupId = int.Parse(reader[2].ToString());
			userModel.userNumberOfFollowers = int.Parse(reader[3].ToString());
			userModel.userPassword = reader[4].ToString();

			Debug.WriteLine("userModel:" + userModel.ToString());
			return userModel;
		}
	}
}
