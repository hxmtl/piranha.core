/*
 * Copyright (c) 2017 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

public static class SimpleSecurityExtensions
{
    /// <summary>
    /// Uses the piranha simple security implementation.
    /// </summary>
    /// <param name="builder">The current application builder</param>
    /// <returns>The builder</returns>    
    public static IApplicationBuilder UsePiranhaSimpleSecurity(this IApplicationBuilder builder) {
        return builder
            .UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationScheme = "Piranha.SimpleSecurity",
                LoginPath = "/Account/Login",
                AccessDeniedPath = "/Home/Forbidden",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });            
    }
}