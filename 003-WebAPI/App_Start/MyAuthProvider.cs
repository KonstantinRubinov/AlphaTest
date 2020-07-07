using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlphaTestSystem
{
	public class MyAuthProvider : OAuthAuthorizationServerProvider
	{
		UserModel userdata;

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var identity = new ClaimsIdentity(context.Options.AuthenticationType);

			if (GlobalVariable.logicType == 0)
				userdata = new EntityUserManager().GetUserByLogin(context.UserName, context.Password);
			else if (GlobalVariable.logicType == 1)
				userdata = new SqlUserManager().GetUserByLogin(context.UserName, context.Password);
			else
				userdata = new MySqlUserManager().GetUserByLogin(context.UserName, context.Password);


			if (userdata != null)
			{
				identity.AddClaim(new Claim(ClaimTypes.Name, userdata.userID.ToString()));
				context.Validated(identity);
			}
			else
			{
				context.SetError("invalid_grant", "Provided username and password is incorrect");
				context.Rejected();
			}
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
				if (property.Key.Equals(".issued"))
				{
					context.AdditionalResponseParameters.Add(".issuedTimeStamp", GetTimestamp(DateTime.Parse(property.Value)));
				}
				if (property.Key.Equals(".expires"))
				{
					context.AdditionalResponseParameters.Add(".expiresTimeStamp", GetTimestamp(DateTime.Parse(property.Value)));
				}
			}

			context.AdditionalResponseParameters.Add("myId", userdata.userID);
			return Task.FromResult<object>(null);
		}

		private string GetTimestamp(DateTime value)
		{
			return value.ToString("yyyyMMddHHmmssffff");
		}

	}
}