using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlphaTestSystem
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class UserApiController : ApiController
    {
		private IUserRepository userRepository;
		public UserApiController(IUserRepository _userRepository)
		{
			userRepository = _userRepository;
		}

		[HttpGet]
		[Route("users/follower/{followedID}")]
		public HttpResponseMessage IsFollowedByMe(int followedID)
		{
			try
			{
				int followerID = int.Parse(base.ControllerContext.RequestContext.Principal.Identity.Name);
				bool follow = userRepository.IsFollowedByMe(followedID, followerID);
				return Request.CreateResponse(HttpStatusCode.OK, follow);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpGet]
		[Route("users/follow/{followedID}")]
		public HttpResponseMessage AddFollower(int followedID)
		{
			try
			{
				int followerID = int.Parse(base.ControllerContext.RequestContext.Principal.Identity.Name);
				UserForTableModel user = userRepository.AddFollower(followedID, followerID);
				return Request.CreateResponse(HttpStatusCode.OK, user);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("users/unfollow/{followedID}")]
		public HttpResponseMessage DeleteFollower(int followedID)
		{
			try
			{
				int followerID = int.Parse(base.ControllerContext.RequestContext.Principal.Identity.Name);
				UserForTableModel user = userRepository.DeleteFollower(followedID, followerID);
				return Request.CreateResponse(HttpStatusCode.OK, user);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpGet]
		[Route("users/fortable")]
		public HttpResponseMessage GetUsersForTable()
		{
			try
			{
				int myId = int.Parse(base.ControllerContext.RequestContext.Principal.Identity.Name);
				List<UserForTableModel> usersForTable = userRepository.GetUsersForTable(myId);
				return Request.CreateResponse(HttpStatusCode.OK, usersForTable);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("users/fortable/{followedID}")]
		public HttpResponseMessage GetOneUserForTableById(int followedID)
		{
			try
			{
				int followerID = int.Parse(base.ControllerContext.RequestContext.Principal.Identity.Name);
				UserForTableModel user = userRepository.GetOneUserForTableById(followerID, followedID);
				return Request.CreateResponse(HttpStatusCode.OK, user);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}
