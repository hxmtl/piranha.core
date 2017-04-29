/*
 * Copyright (c) 2017 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

namespace Piranha
{
    public interface ISecurity
    {
        /// <summary>
        /// Gets if the current user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Authenticates the given credentials without
        /// signing in the user.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>If the given credentials were correct</returns>
        bool Authenticate(string username, string password);

        /// <summary>
        /// Checks if the current user has the given role.
        /// </summary>
        /// <param name="role">The role</param>
        /// <returns>If the user has the role</returns>
        bool IsInRole(string role);
    }
}