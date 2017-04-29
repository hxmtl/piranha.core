/*
 * Copyright (c) 2017 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Piranha.AspNetCore
{
    public class SimpleSecurity : ISecurity
    {
        /// <summary>
        /// Gets/sets the available users.
        /// </summary>
        private List<SimpleUser> Users { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SimpleSecurity() {
            Users = new List<SimpleUser>();
        }

        /// <summary>
        /// Creates a new security object for the given users.
        /// </summary>
        /// <param name="users">The users</param>
        public SimpleSecurity(params SimpleUser[] users) : base() {
            Users.AddRange(users);
        }

        /// <summary>
        /// Gets if the current user is authenticated.
        /// </summary>
        public bool IsAuthenticated { 
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Authenticates the given credentials without
        /// signing in the user.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>If the given credentials were correct</returns>
        public bool Authenticate(string username, string password) {
            return Users.Count(u => u.UserName == username && u.Password == password) == 1;
        }

        /// <summary>
        /// Authenticates and signs in the user with the
        /// given credentials.
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>If the user was signed in</returns>
        public async Task<bool> SignIn(HttpContext context, string username, string password) {
            if (Authenticate(username, password)) {
                var user = Users.Single(u => u.UserName == username && u.Password == password);

                var claims = new List<Claim>();
                foreach (var claim in user.Claims) {
                    claims.Add(new Claim(claim, "true"));
                }
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.Sid, user.Id));

                var identity = new ClaimsIdentity(claims, user.Password);
                var principle = new ClaimsPrincipal(identity);

                await context.Authentication.SignInAsync("Piranha.SimpleSecurity", principle);

                return true;                
            }
            return false;
        }

        /// <summary>
        /// Signs out the current user.
        /// </summary>
        /// <param name="context">The current http context</param>
        public Task SignOut(HttpContext context) {
            return context.Authentication.SignOutAsync("Piranha.SimpleSecurity");
        }

        /// <summary>
        /// Checks if the current user has the given role.
        /// </summary>
        /// <param name="role">The role</param>
        /// <returns>If the user has the role</returns>
        public bool IsInRole(string role) {
            throw new NotImplementedException();
        }
    }
}