﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using System;
using WareHouse.API.Application.Authentication;

namespace WareHouse.API.Application.Behaviors
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUserSevice _userSevice;

        public AuthorizationBehaviour(IUserSevice userSevice)
        {
            _userSevice = userSevice;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
            var user = await _userSevice.GetUser();
            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            //var isInRole = await _identityService.IsInRoleAsync(_currentUserService.UserId, role.Trim());
                            //if (isInRole)
                            //{
                            //    authorized = true;
                            //    break;
                            //}
                        }
                    }

                    // Must be a member of at least one role in roles
                    //if (!authorized)
                    //{
                    //    throw new ForbiddenAccessException();
                    //}
                }

                // Policy-based authorization
                var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                    {
                        //var authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

                        //if (!authorized)
                        //{
                        //    throw new ForbiddenAccessException();
                        //}
                    }
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}