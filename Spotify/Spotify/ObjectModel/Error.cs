﻿using System;
using System.Net;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a (non-authentication related) error.
    /// </summary>
    internal record Error : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record with the
        /// specified <paramref name="statusCode"/> and <paramref name="message"/>.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> of the response that returned the error.</param>
        /// <param name="message">A <see cref="String"/> representing a short description of the cause of the error.</param>
        internal Error(HttpStatusCode statusCode, String message) : base()
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the <see cref="HttpStatusCode"/> of the response that returned the <see cref="Error"/>.
        /// </summary>
        /// <returns>The <see cref="HttpStatusCode"/> of the response that returned the <see cref="Error"/>.</returns>
        internal HttpStatusCode StatusCode { get; init; }
        /// <summary>
        /// Gets or sets a short description of the cause of the <see cref="Error"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing a short description of the cause of the <see cref="Error"/>.</returns>
        internal String Message { get; init; }
    }
}