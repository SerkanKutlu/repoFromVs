using IdentityServer4.Models;

namespace Identity.Server.Files;

public class Config
{
   public static IEnumerable<ApiResource> ApiResources =>
      new List<ApiResource>
      {
         new ApiResource
         {
            Name = "api",
            DisplayName = "Api #1",
            ApiSecrets = {new Secret("api.secret".Sha256())},
            Scopes = {"api.read", "api.upsert", "api.delete"}

         }
      };

   public static IEnumerable<ApiScope> ApiScopes =>
      new List<ApiScope>
      {
         new ApiScope("api.read", "Read Permission"),
         new ApiScope("api.upsert", "Write Permission"),
         new ApiScope("api.delete", "Delete Permission")
      };

   public static IEnumerable<Client> ApiClients =>
      new List<Client>
      {
         new Client
         {
            ClientId = "app",
            ClientSecrets = {new("app.secret".Sha256())},
            ClientName = "Mobile APP",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = {"api.read"}
         },
         new Client
         {
            ClientId = "postman",
            ClientSecrets = {new("postman.secret".Sha256())},
            ClientName = "POSTMAN",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = {"api.read","api.write"}
         }
      };
}