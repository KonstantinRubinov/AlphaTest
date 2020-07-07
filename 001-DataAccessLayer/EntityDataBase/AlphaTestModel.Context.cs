﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlphaTestSystem.EntityDataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    public partial class AlphaTestEntities : DbContext
    {
        public AlphaTestEntities() : base("name=AlphaTestEntities")
        {
        }

		public override int SaveChanges()
		{
			try
			{
				return base.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				// Retrieve the error messages as a list of strings.
				var errorMessages = ex.EntityValidationErrors
						.SelectMany(x => x.ValidationErrors)
						.Select(x => x.ErrorMessage);

				// Join the list to a single string.
				var fullErrorMessage = string.Join("; ", errorMessages);
				Debug.WriteLine("DbEntityValidationException: " + errorMessages);
				throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);
			}
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FOLLOW> FOLLOWS { get; set; }
        public virtual DbSet<GROUP> GROUPS { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
    
        public virtual ObjectResult<AddFollower_Result> AddFollower(Nullable<int> followedID, Nullable<int> followerID)
        {
            var followedIDParameter = followedID.HasValue ?
                new ObjectParameter("followedID", followedID) :
                new ObjectParameter("followedID", typeof(int));
    
            var followerIDParameter = followerID.HasValue ?
                new ObjectParameter("followerID", followerID) :
                new ObjectParameter("followerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AddFollower_Result>("AddFollower", followedIDParameter, followerIDParameter);
        }
    
        public virtual ObjectResult<DeleteFollower_Result> DeleteFollower(Nullable<int> followedID, Nullable<int> followerID)
        {
            var followedIDParameter = followedID.HasValue ?
                new ObjectParameter("followedID", followedID) :
                new ObjectParameter("followedID", typeof(int));
    
            var followerIDParameter = followerID.HasValue ?
                new ObjectParameter("followerID", followerID) :
                new ObjectParameter("followerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DeleteFollower_Result>("DeleteFollower", followedIDParameter, followerIDParameter);
        }
    
        public virtual ObjectResult<GetAllUsers_Result> GetAllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllUsers_Result>("GetAllUsers");
        }
    
        public virtual ObjectResult<GetOneUserById_Result> GetOneUserById(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOneUserById_Result>("GetOneUserById", userIDParameter);
        }
    
        public virtual ObjectResult<GetOneUserForTableById_Result> GetOneUserForTableById(Nullable<int> followedID, Nullable<int> followerID)
        {
            var followedIDParameter = followedID.HasValue ?
                new ObjectParameter("followedID", followedID) :
                new ObjectParameter("followedID", typeof(int));
    
            var followerIDParameter = followerID.HasValue ?
                new ObjectParameter("followerID", followerID) :
                new ObjectParameter("followerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOneUserForTableById_Result>("GetOneUserForTableById", followedIDParameter, followerIDParameter);
        }
    
        public virtual ObjectResult<GetUsersForTable_Result> GetUsersForTable(Nullable<int> myId)
        {
            var myIdParameter = myId.HasValue ?
                new ObjectParameter("myId", myId) :
                new ObjectParameter("myId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUsersForTable_Result>("GetUsersForTable", myIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> IsFollowedByMe(Nullable<int> followedID, Nullable<int> followerID)
        {
            var followedIDParameter = followedID.HasValue ?
                new ObjectParameter("followedID", followedID) :
                new ObjectParameter("followedID", typeof(int));
    
            var followerIDParameter = followerID.HasValue ?
                new ObjectParameter("followerID", followerID) :
                new ObjectParameter("followerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("IsFollowedByMe", followedIDParameter, followerIDParameter);
        }
    
        public virtual ObjectResult<ReturnUserByNamePassword_Result> ReturnUserByNamePassword(string userName, string userPassword)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var userPasswordParameter = userPassword != null ?
                new ObjectParameter("userPassword", userPassword) :
                new ObjectParameter("userPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReturnUserByNamePassword_Result>("ReturnUserByNamePassword", userNameParameter, userPasswordParameter);
        }
    }
}
