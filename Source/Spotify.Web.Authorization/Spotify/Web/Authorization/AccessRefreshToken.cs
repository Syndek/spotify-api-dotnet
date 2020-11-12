﻿using System;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents data about a Spotify <see cref="Authorization.AccessToken"/>
    /// and an optional refresh token that can be used to refresh it.
    /// </summary>
    public readonly struct AccessRefreshToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRefreshToken"/> structure with the specified values.
        /// </summary>
        /// <param name="value">A <see cref="String"/> representing the value of the access token.</param>
        /// <param name="scope">The <see cref="AuthorizationScopes"/> the access token is valid for.</param>
        /// <param name="expiresIn">The number of seconds after issue at which the access token will expire.</param>
        /// <param name="refreshToken">A <see cref="String"/> representing a token that can be used to refresh the access token.</param>
        public AccessRefreshToken(String value, AuthorizationScopes scope, Int32 expiresIn, String? refreshToken)
        {
            this.AccessToken = new(value, scope, expiresIn);
            this.RefreshToken = refreshToken;
        }

        /// <summary>
        /// Gets the underlying <see cref="Authorization.AccessToken"/> of the <see cref="AccessRefreshToken"/>.
        /// </summary>
        /// <returns>The underlying <see cref="Authorization.AccessToken"/> of the <see cref="AccessRefreshToken"/>.</returns>
        public AccessToken AccessToken { get; }
        /// <summary>
        /// Gets a token that can be used to refresh the <see cref="AccessToken"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing a token that can be used to refresh the <see cref="AccessToken"/>,
        /// or <see langword="null"/> if none was provided.
        /// </returns>
        public String? RefreshToken { get; }
    }
}