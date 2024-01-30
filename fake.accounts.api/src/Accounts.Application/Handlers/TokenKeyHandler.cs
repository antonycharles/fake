using System.Security.Cryptography;
using System.Text.Json;
using Accounts.Core.Handlers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Application.Handlers
{
    public class TokenKeyHandler : ITokenKeyHandler
    {
        private readonly IDistributedCache _cache;  
        private readonly string KEY_CACHE = "accountsapi:tokenkey";

        public  TokenKeyHandler(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        }

        public ECDsaSecurityKey GetKey()
        {
            var jsonWebKey = GetJsonWebKeyCache();

            var key = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                D = Base64UrlEncoder.DecodeBytes(jsonWebKey.D),
                Q = new ECPoint
                {
                    X = Base64UrlEncoder.DecodeBytes(jsonWebKey.X),
                    Y = Base64UrlEncoder.DecodeBytes(jsonWebKey.Y)
                }
            }); 

            return new ECDsaSecurityKey(key)
            {
                KeyId = jsonWebKey.KeyId,
            };
        }

        public IList<JsonWebKey> GetPublicKey()
        {
            List<JsonWebKey> publicKeys = new List<JsonWebKey>();

            var key = GetKey();

            var parameters = key.ECDsa.ExportParameters(true);
            var jwk = new JsonWebKey()
            {
                Kty = JsonWebAlgorithmsKeyTypes.EllipticCurve,
                Use = "sig",
                Kid = key.KeyId,
                KeyId = key.KeyId,
                X = Base64UrlEncoder.Encode(parameters.Q.X),
                Y = Base64UrlEncoder.Encode(parameters.Q.Y),
                //D = Base64UrlEncoder.Encode(parameters.D),
                Crv = JsonWebKeyECTypes.P256,
                Alg = "ES256"
            };

            publicKeys.Add(jwk);

            return publicKeys;
        }

        private JsonWebKey GetJsonWebKeyCache()
        {
            var json = _cache.GetString(KEY_CACHE);
            if(json != null)
            {
                var key = JsonSerializer.Deserialize<JsonWebKey>(json);
                return key;
            }
            else 
            {
                
                var jwk = CreateJsonWebKey();
                json = JsonSerializer.Serialize<JsonWebKey>(jwk);

                _cache.SetStringAsync(KEY_CACHE, json, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });

                return jwk;
            }
        }

        private JsonWebKey CreateJsonWebKey()
        {
            var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var parameters = key.ExportParameters(true);
            var keyId = Guid.NewGuid().ToString();

            var jwk = new JsonWebKey()
            {
                Kty = JsonWebAlgorithmsKeyTypes.EllipticCurve,
                Use = "sig",
                Kid = keyId,
                KeyId = keyId,
                X = Base64UrlEncoder.Encode(parameters.Q.X),
                Y = Base64UrlEncoder.Encode(parameters.Q.Y),
                D = Base64UrlEncoder.Encode(parameters.D),
                Crv = JsonWebKeyECTypes.P256,
                Alg = "ES256"
            };

            return jwk;
        }
    }
}