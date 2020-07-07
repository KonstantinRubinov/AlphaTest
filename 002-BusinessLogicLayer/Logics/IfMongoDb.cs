using System.Collections.Generic;
using System.Linq;

namespace AlphaTestSystem
{
	public class IfMongoDb
	{
		private List<GroupModel> gm = new List<GroupModel>();
		private List<UserModel> um = new List<UserModel>();

		private ITesterRepository testerRepository = new MongoTesterManager();

		public void AddMongoData()
		{
			gm.Add(testerRepository.AddGroup(1, "Group1"));
			gm.Add(testerRepository.AddGroup(2, "Group2"));
			gm.Add(testerRepository.AddGroup(3, "Group3"));
			gm.Add(testerRepository.AddGroup(4, "Group4"));
			gm.Add(testerRepository.AddGroup(5, "Group5"));

			um.Add(testerRepository.AddUser(1, "user01", 1, 0, "user01"));
			um.Add(testerRepository.AddUser(2, "user02", 2, 0, "user02"));
			um.Add(testerRepository.AddUser(3, "user03", 3, 0, "user03"));
			um.Add(testerRepository.AddUser(4, "user04", 4, 0, "user04"));
			um.Add(testerRepository.AddUser(5, "user05", 5, 0, "user05"));
			um.Add(testerRepository.AddUser(6, "user06", 1, 0, "user06"));
			um.Add(testerRepository.AddUser(7, "user07", 2, 0, "user07"));
			um.Add(testerRepository.AddUser(8, "user08", 3, 0, "user08"));
			um.Add(testerRepository.AddUser(9, "user09", 4, 0, "user09"));
			um.Add(testerRepository.AddUser(10, "user10", 5, 0, "user10"));
		}
	}
}
