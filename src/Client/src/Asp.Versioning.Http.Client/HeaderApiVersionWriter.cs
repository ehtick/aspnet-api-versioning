﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.

namespace Asp.Versioning.Http;

/// <summary>
/// Represents an API version writer that writes the value to a HTTP header.
/// </summary>
public sealed class HeaderApiVersionWriter : IApiVersionWriter
{
    private readonly string headerName;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderApiVersionWriter"/> class.
    /// </summary>
    /// <param name="headerName">The HTTP header name to write the API version to.</param>
    public HeaderApiVersionWriter( string headerName )
    {
        if ( string.IsNullOrEmpty( headerName ) )
        {
            throw new ArgumentNullException( headerName );
        }

        this.headerName = headerName;
    }

    /// <inheritdoc />
    public void Write( HttpRequestMessage request, ApiVersion apiVersion )
    {
        if ( request == null )
        {
            throw new ArgumentNullException( nameof( request ) );
        }

        if ( apiVersion == null )
        {
            throw new ArgumentNullException( nameof( apiVersion ) );
        }

        var headers = request.Headers;

        if ( !headers.Contains( headerName ) )
        {
            headers.TryAddWithoutValidation( headerName, apiVersion.ToString() );
        }
    }
}