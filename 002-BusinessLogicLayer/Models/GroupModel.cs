using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace AlphaTestSystem
{
	[DataContract]
	public class GroupModel
	{
		private int _groupID;
		private string _groupName;

		public GroupModel() { }

		public GroupModel(int tmpGroupID, string tmpGroupName) {
			groupID = tmpGroupID;
			groupName = tmpGroupName;
		}



		[BsonId]
		[DataMember]
		public int groupID
		{
			get { return _groupID; }
			set { _groupID = value; }
		}

		[DataMember]
		public string groupName
		{
			get { return _groupName; }
			set { _groupName = value; }
		}

		public override string ToString()
		{
			return
				groupID + " " +
				groupName;
		}

		public static GroupModel ToObject(DataRow reader)
		{
			GroupModel groupModel = new GroupModel();
			groupModel.groupID = int.Parse(reader[0].ToString());
			groupModel.groupName = reader[1].ToString();

			Debug.WriteLine("groupModel:" + groupModel.ToString());
			return groupModel;
		}
	}
}
