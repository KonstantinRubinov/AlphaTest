using AlphaTestSystem.EntityDataBase;
using System;

namespace AlphaTestSystem
{
	public class EntityBaseManager //: IDisposable
	{
		protected AlphaTestEntities DB = new AlphaTestEntities();

		public void Dispose()
		{
			DB.Dispose();
		}
	}
}
