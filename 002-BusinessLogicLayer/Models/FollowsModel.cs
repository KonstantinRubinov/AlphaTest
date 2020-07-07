using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace AlphaTestSystem
{
	[DataContract]
	public class FollowsModel
	{
		private string _followId;
		private int _followCounter;
		private int _followedID;
		private int _followerID;

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[DataMember]
		public string followId
		{
			get { return _followId; }
			set { _followId = value; }
		}

		[DataMember]
		public int followCounter
		{
			get { return _followCounter; }
			set { _followCounter = value; }
		}

		[DataMember]
		public int followedID
		{
			get { return _followedID; }
			set { _followedID = value; }
		}

		[DataMember]
		public int followerID
		{
			get { return _followerID; }
			set { _followerID = value; }
		}

		public override string ToString()
		{
			return
				followCounter + " " +
				followedID + " " +
				followerID;
		}

		public static FollowsModel ToObject(DataRow reader)
		{
			FollowsModel followsModel = new FollowsModel();
			followsModel.followCounter = int.Parse(reader[0].ToString());
			followsModel.followedID = int.Parse(reader[1].ToString());
			followsModel.followerID = int.Parse(reader[2].ToString());

			Debug.WriteLine("followsModel:" + followsModel.ToString());
			return followsModel;
		}
	}
}
