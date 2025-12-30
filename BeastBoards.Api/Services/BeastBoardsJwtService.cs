using System.Buffers.Text;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BeastBoards.Api.Extensions;
using BeastBoards.Api.Stubs;
using BeastBoards.Api.Stubs.Steam;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;

namespace BeastBoards.Api.Services
{
    public class BeastBoardsJwtService
    {

        private readonly BeastBoardsConfig _config;
        private readonly IJwtEncoder _jwtEncoder;
        private readonly IJwtDecoder _jwtDecoder;


        public BeastBoardsJwtService(BeastBoardsConfig config)
        {
            _config = config;

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IDateTimeProvider dateTimeProvider = new UtcDateTimeProvider();
            IJwtValidator jwtValidator = new JwtValidator(serializer, dateTimeProvider, new ValidationParameters()
            {
                ValidateSignature = true,
                ValidateExpirationTime = true,
                ValidateIssuedTime = true
            });

            _jwtEncoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            _jwtDecoder = new JwtDecoder(serializer, jwtValidator, urlEncoder, algorithm);
        }


        public string Create(AuthenticateUserTicketResponse ticket)
        {

            var payload = new SteamUserStub()
            {
                UserId = ulong.Parse(ticket.Response.Params.SteamId),
                Exp = DateTime.UtcNow.AddMinutes(600).ConvertToUnixTimestamp(),
                Iat = DateTime.UtcNow.ConvertToUnixTimestamp()
            };

            var token = _jwtEncoder.Encode(payload, _config.JwtSigningKey);

            return token;
        }

        public SteamUserStub Parse(string token)
        {           
            var payload = _jwtDecoder.DecodeToObject<SteamUserStub>(token, _config.JwtSigningKey);
            return payload;        
        }
    }
}
