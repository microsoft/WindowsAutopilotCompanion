using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace CompanionApp.Services
{
    public enum AuthenticationResultCode
    {
        Succesfull,
        Canceled,
        Denied,
        Unknown,
        None
    }


    public class ADALAuthentication
    {
        private string error = "";
        private AuthenticationResultCode resultCode = AuthenticationResultCode.None;
        private AuthenticationResult authResult = null;

        public IPlatformParameters platformParameters
        {
            get;
            set;
        }

        public AuthenticationResultCode ResultCode
        {
            get
            {
                return resultCode;
            }
        }

        public AuthenticationResult AuthResult
        {
            get
            {
                return authResult;
            }
        }

        public string Error
        {
            get
            {
                return error;
            }
        }

        private ADALAuthentication()
        {

        }

        private static ADALAuthentication instance = null;

        public static ADALAuthentication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADALAuthentication();
                }
                return instance;
            }
        }

        public async Task<AuthenticationResultCode> Authenticate(string resource, string clientId, string returnUrl)
        {
            try
            {
                AuthenticationContext ac = new AuthenticationContext("https://login.microsoftonline.com/common");
                authResult = await ac.AcquireTokenAsync(resource, clientId, new Uri(returnUrl), platformParameters);
                resultCode = AuthenticationResultCode.Succesfull;
            }
            catch (AdalException adalEx)
            {
                switch (adalEx.ErrorCode)
                {
                    case "authentication_canceled":
                        resultCode = AuthenticationResultCode.Canceled;
                        break;

                    case "access_denied":
                        resultCode = AuthenticationResultCode.Denied;
                        break;

                    default:
                        resultCode = AuthenticationResultCode.Unknown;
                        break;
                }
            }
            catch (Exception ex)
            {
                resultCode = AuthenticationResultCode.Unknown;
                error = ex.Message + " " + ex.StackTrace;
            }

            return resultCode;
        }

    }
}