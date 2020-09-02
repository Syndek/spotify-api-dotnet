﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents a <see cref="SpotifyAuthorizationFlow"/> for accessing endpoints that do not access user information.
    /// </summary>
    /// <remarks>
    /// To be able to access user information, a <see cref="AuthorizationCodeFlow"/> should be used instead.
    /// A <see cref="ClientCredentialsFlow"/> does not include user authorization and therefore cannot be used to access user information.
    /// However, it does allow for a higher rate limit than requests made without an access token.
    /// </remarks>
    /// <seealso cref="AuthorizationCodeFlow"/>
    public class ClientCredentialsFlow : SpotifyAuthorizationFlow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCredentialsFlow"/> class with the specified values.
        /// </summary>        
        /// <param name="httpClient">A <see cref="HttpClient"/> instance to use to make requests to the Spotify Accounts service.</param>
        /// <param name="clientId">A valid Spotify Web API client ID.</param>
        /// <param name="clientSecret">The secret key of the application with the specified client ID.</param>
        public ClientCredentialsFlow(HttpClient httpClient, String clientId, String clientSecret) : base(httpClient, clientId, clientSecret) { }

        /// <inheritdoc/>
        public override async ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            if (base.CurrentAccessToken?.HasExpired == false)
            {
                return base.CurrentAccessToken.Value;
            }

            using var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            using var message = new HttpRequestMessage(HttpMethod.Post, SpotifyAuthorizationFlow.TokenUri) { Content = content };
            message.Headers.Authorization = base.BasicAuthenticationHeader;

            base.CurrentAccessToken = await base.HttpClient.SendMessageAsync<AccessToken, AuthenticationError>(message, cancellationToken);

            return base.CurrentAccessToken!.Value;
        }
    }
}