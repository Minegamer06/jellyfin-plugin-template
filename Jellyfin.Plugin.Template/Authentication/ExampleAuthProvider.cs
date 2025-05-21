using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jellyfin.Data.Entities;
using MediaBrowser.Controller.Authentication;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.Template.Authentication
{
    internal class ExampleAuthProvider : IAuthenticationProvider
    {
        private readonly ILogger<ExampleAuthProvider> _logger;
        private string _password;

        public ExampleAuthProvider(ILogger<ExampleAuthProvider> logger)
        {
            _logger = logger;
            _password = string.Empty;
        }

        /// <summary>
        /// Gets the Name of the AuthProvider.
        /// </summary>
        public string Name => "Example - AuthProvider";

        /// <summary>
        /// Gets a value indicating whether the Auth-Provider is Enabled.
        /// </summary>
        public bool IsEnabled => true;

        public async Task<ProviderAuthenticationResult> Authenticate(string username, string password)
        {
            _logger.LogTrace("Login - Start: {User}", username);
            try
            {
                await Task.Delay(1000).ConfigureAwait(false);
                if (username == "demo" && password == _password)
                {
                    return new ProviderAuthenticationResult() { Username = username };
                }
                else
                {
                    throw new AuthenticationException("Username or Password wrong");
                }
            }
            finally
            {
                _logger.LogTrace("Login - Complete: {User}", username);
            }
        }

        public async Task ChangePassword(User user, string newPassword)
        {
            _logger.LogTrace("Change PW - Start: {User}", user.Username);
            _password = newPassword;
            await Task.Delay(1000).ConfigureAwait(false);
            _logger.LogTrace("Change PW - Complete: {User}", user.Username);
        }

        public bool HasPassword(User user)
        {
            return string.IsNullOrEmpty(_password);
        }
    }
}
