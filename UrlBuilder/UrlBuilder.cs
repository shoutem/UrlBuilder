namespace Se.Url
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Util;

    /// <summary>
    /// A UrlBuilder that can be used to build URLs using a fluent API.
    /// </summary>
    public class UrlBuilder
    {
        private static readonly Dictionary<string, int> DefaultPorts = new Dictionary<string, int>
        {   
            { "ftp", 21 },
            { "http", 80 },
            { "https", 443 }
        };

        private UriBuilder uriBuilder;
        private IList<string> segments;
        private QueryParamCollection queryParams;
        private bool hasTrailingSlash;

        /// <summary>
        /// Gets the scheme name of the UrlBuilder.
        /// </summary>
        public string GetScheme()
        {
            return uriBuilder.Scheme;
        }

        /// <summary>
        /// Sets the scheme name of the UrlBuilder.
        /// </summary>
        public UrlBuilder SetScheme(string scheme)
        {
            if (scheme == null)
            {
                throw new ArgumentNullException("scheme", "Scheme cannot be null.");
            }

            if (scheme == string.Empty)
            {
                throw new ArgumentException("scheme", "Scheme cannot be empty.");
            }

            if (HasDefaultPort() && DefaultPorts.ContainsKey(scheme))
            {
                SetPort(DefaultPorts[scheme]);
            }

            uriBuilder.Scheme = scheme;
            return this;
        }

        /// <summary>
        /// Gets the port number of the UrlBuilder.
        /// </summary>
        public int GetPort()
        {
            return uriBuilder.Port;
        }

        /// <summary>
        /// Sets the port number of the UrlBuilder.
        /// </summary>
        public UrlBuilder SetPort(int port)
        {
            uriBuilder.Port = port;
            return this;
        }

        /// <summary>
        /// Gets the Domain Name System (DNS) host name or IP address of a server.
        /// </summary>
        public string GetHost()
        {
            return uriBuilder.Host;
        }

        /// <summary>
        /// Sets the Domain Name System (DNS) host name or IP address of a server.
        /// </summary>
        public UrlBuilder SetHost(string host)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host", "Host cannot be null.");
            }

            if (host == string.Empty)
            {
                throw new ArgumentException("host", "Host cannot be empty.");
            }

            uriBuilder.Host = host;
            return this;
        }

        /// <summary>
        /// Creates path from list of segments
        /// </summary>
        public string GetPath()
        {
            var path = string.Join("/", segments);

            if (segments.Count > 0 && hasTrailingSlash)
            {
                path += "/";
            }

            return path;
        }

        /// <summary>
        /// Sets the path of the UrlBuilder
        /// </summary>
        public UrlBuilder SetPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path", "Path cannot be null.");
            }

            segments.Clear();

            var segmentStrings = path.Split('/').ToList();
            foreach (var segment in segmentStrings)
            {
                AddPathSegment(segment);
            }

            hasTrailingSlash = path.EndsWith("/");
            return this;
        }

        /// <summary>
        /// Constructs a UrlBuilder object
        /// </summary>
        public UrlBuilder()
        {
            uriBuilder = new UriBuilder();
            segments = new List<string>();
            queryParams = new QueryParamCollection();
        }

        /// <summary>
        /// Constructs a UrlBuilder object from a string.
        /// </summary>
        /// <param name="baseUrl">The URL to use as a starting point (required)</param>
        public UrlBuilder(string baseUrl) 
        {
            if (baseUrl == null)
            {
                throw new ArgumentNullException("baseUrl");
            }

            Initialize(baseUrl);
        }

        /// <summary>
        /// Constructs a UrlBuilder object from a urlBuilder.
        /// </summary>
        /// <param name="urlBuilder">The urlBuilder to use as a starting point (required)</param>
        public UrlBuilder(UrlBuilder urlBuilder)
        {
            if (urlBuilder == null)
            {
                throw new ArgumentNullException("urlBuilder");
            }

            Initialize(urlBuilder.ToString());
        }

        /// <summary>
        /// Constructs a UrlBuilder object from a Uri.
        /// </summary>
        /// <param name="uri">The Uri object to use as a starting point</param>
        public UrlBuilder(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            Initialize(uri.ToString());
        }

        /// <summary>
        /// Initialize UrlBuilder fields from url string
        /// </summary>
        /// <param name="baseUrl">Url as string</param>
        private void Initialize(string baseUrl)
        {
            uriBuilder = new UriBuilder(baseUrl);
            segments = new List<string>();
            hasTrailingSlash = false;
            SetPath(uriBuilder.Path);

            var query = uriBuilder.Query.TrimStart('?');
            queryParams = QueryParamCollection.Parse(query.Length > 1 ? query : string.Empty);
        }

        /// <summary>
        /// Returns true if default port for scheme is set
        /// </summary>
        /// <returns></returns>
        private bool HasDefaultPort()
        {
            return DefaultPorts.ContainsKey(uriBuilder.Scheme) &&
                DefaultPorts[uriBuilder.Scheme] == uriBuilder.Port;
        }

        /// <summary>
        /// Encodes characters that are illegal in a URL path, including '?'. Does not encode reserved characters, i.e. '/', '+', etc.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        private static string CleanSegment(string segment) {
            var unescaped = Uri.UnescapeDataString(segment);
            return Uri.EscapeUriString(unescaped).Replace("?", "%3F").Trim().TrimStart('/').TrimEnd('/');
        }

        /// <summary>
        /// Decodes a URL-encoded query string value.
        /// </summary>
        /// <param name="value">The encoded query string value.</param>
        /// <returns></returns>
        public static string DecodeQueryParamValue(string value) {
            return Uri.UnescapeDataString((value ?? "").Replace("+", " "));
        }

        /// <summary>
        /// URL-encodes a query string value.
        /// </summary>
        /// <param name="value">The query string value to encode.</param>
        /// <param name="encodeSpaceAsPlus">If true, spaces will be encoded as + signs. Otherwise, they'll be encoded as %20.</param>
        /// <returns></returns>
        public static string EncodeQueryParamValue(object value, bool encodeSpaceAsPlus) {
            var result = Uri.EscapeDataString((value ?? "").ToInvariantString());
            return encodeSpaceAsPlus ? result.Replace("%20", "+") : result;
        }

        /// <summary>
        /// Adds a segment to list of segments
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public UrlBuilder AddPathSegment(string segment)
        {
            if (segment == null)
                throw new ArgumentNullException("segment");

            var cleanSegment = CleanSegment(segment);
            if (!string.IsNullOrEmpty(cleanSegment))
            {
                segments.Add(cleanSegment);
            }

            hasTrailingSlash = segment.EndsWith("/");
            return this;
        }

        /// <summary>
        /// Removes the first occurrence of a segment from list of segments
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public UrlBuilder RemovePathSegment(string segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException("segment");
            }
                
            segments.Remove(CleanSegment(segment));
            return this;
        }

        /// <summary>
        /// Adds a parameter to the query string, overwriting the value if name exists.
        /// </summary>
        /// <param name="name">name of query string parameter</param>
        /// <param name="value">value of query string parameter</param>
        /// <returns>The UrlBuilder object with the query string parameter added</returns>
        public UrlBuilder SetQueryParam(string name, object value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name", "Query parameter name cannot be null.");
            }
                
            queryParams[name] = value;
            return this;
        }

        /// <summary>
        /// Parses values (usually an anonymous object or dictionary) into name/value pairs and adds them to the query string, overwriting any that already exist.
        /// </summary>
        /// <param name="values">Typically an anonymous object, ie: new { x = 1, y = 2 }</param>
        /// <returns>The UrlBuilder object with the query string parameters added</returns>
        public UrlBuilder SetQueryParams(object values)
        {
            if (values == null)
            {
                return this;
            }
                
            foreach (var kv in values.ToKeyValuePairs())
            {
                SetQueryParam(kv.Key, kv.Value);
            }
                
            return this;
        }

        /// <summary>
        /// Adds a parameter to the query string, appends the value if param already exists.
        /// </summary>
        /// <param name="name">name of query string parameter</param>
        /// <param name="value">value of query string parameter</param>
        /// <returns>The UrlBuilder object with the query string parameter appended</returns>
        public UrlBuilder AppendQueryParam(string name, object value)
        {
            if (ContainsQueryParam(name))
            {
                var paramList = new List<object>();
                var val = GetQueryParam(name);

                if (val is string || !(val is IEnumerable))
                {
                    paramList.Add(val);
                }
                else
                {
                    foreach (var subval in val as IEnumerable)
                    {
                        paramList.Add(subval);
                    }
                }

                paramList.Add(value);
                SetQueryParam(name, paramList);
            }
            else
            {
                SetQueryParam(name, value);
            }

            return this;
        }

        /// <summary>
        /// Contains specific name(key) in queryParams
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns bool value</returns>
        public bool ContainsQueryParam(string name)
        {
            return queryParams.ContainsKey(name);
        }

        /// <summary>
        /// Gets query param by name
        /// </summary>
        /// <param name="name">Name of query param.</param>
        /// <returns>Returns query param</returns>
        public object GetQueryParam(string name)
        {
            return queryParams[name];
        }

        /// <summary>
        /// Gets query param keys
        /// </summary>
        /// <returns>Returns list of query param keys</returns>
        public List<string> GetParamNames()
        {
            return queryParams.Keys.ToList();
        } 

        /// <summary>
        /// Removes a name/value pair from the query string by name.
        /// </summary>
        /// <param name="name">Query string parameter name to remove</param>
        /// <returns>The UrlBuilder object with the query string parameter removed</returns>
        public UrlBuilder RemoveQueryParam(string name)
        {
            queryParams.Remove(name);
            return this;
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query string by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The UrlBuilder object with the query string parameters removed</returns>
        public UrlBuilder RemoveQueryParams(params string[] names)
        {
            foreach (var name in names)
            {
                queryParams.Remove(name);
            }
                
            return this;
        }

        /// <summary>
        /// Removes multiple name/value pairs from the query string by name.
        /// </summary>
        /// <param name="names">Query string parameter names to remove</param>
        /// <returns>The UrlBuilder object with the query string parameters removed</returns>
        public UrlBuilder RemoveQueryParams(IEnumerable<string> names)
        {
            foreach (var name in names)
            {
                queryParams.Remove(name);
            }
                
            return this;
        }

        /// <summary>
        /// Gets Uri from UrlBuilder
        /// </summary>
        /// <returns>Returns Uri object</returns>
        public Uri ToUri()
        {
            uriBuilder.Path = GetPath();
            uriBuilder.Query = queryParams.ToString(false);
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Gets relative Uri from UrlBuilder
        /// </summary>
        /// <returns>Returns Uri object</returns>
        public Uri ToRelativeUri()
        {
            var baseUriBuilder = new UriBuilder
            {
                Scheme = uriBuilder.Scheme,
                Host = uriBuilder.Host,
                Port = uriBuilder.Port
            };

            var uri = ToUri();
            return baseUriBuilder.Uri.MakeRelativeUri(uri);
        }

        /// <summary>
        /// Converts this UrlBuilder object to its string representation.
        /// </summary>
        /// <returns>Returns full url</returns>
        public override string ToString()
        {
            var uri = ToUri();
            var components = UriComponents.AbsoluteUri;
            if (HasDefaultPort())
            {
                components &= ~ UriComponents.Port;
            }

            return uri.GetComponents(components, UriFormat.UriEscaped);
        }

        /// <summary>
        /// Gets the relative URL from the UrlBuilder.
        /// </summary>
        /// <returns>Returns the relative URL</returns>
        public string ToRelativeString()
        {
            var uri = ToRelativeUri();
            return uri.ToString();
        }

        /// <summary>
        /// Implicit conversion from UrlBuilder to String.
        /// </summary>
        /// <param name="url">the UrlBuilder object</param>
        /// <returns>The string</returns>
        public static implicit operator string(UrlBuilder url)
        {
            return url.ToString();
        }

        /// <summary>
        /// Implicit conversion from String to UrlBuilder.
        /// </summary>
        /// <param name="url">the String representation of the UrlBuilder</param>
        /// <returns>The UrlBuilder</returns>
        public static implicit operator UrlBuilder(string url)
        {
            return new UrlBuilder(url);
        }

        /// <summary>
        /// Implicit conversion from UrlBuilder to Uri.
        /// </summary>
        /// <param name="url">the UrlBuilder object</param>
        /// <returns>The Uri</returns>
        public static implicit operator Uri(UrlBuilder url)
        {
            return url.ToUri();
        }

        /// <summary>
        /// Implicit conversion from Uri to UrlBuilder.
        /// </summary>
        /// <param name="uri">the String representation of the UrlBuilder</param>
        /// <returns>The UrlBuilder</returns>
        public static implicit operator UrlBuilder(Uri uri)
        {
            return new UrlBuilder(uri);
        }
    }
}
