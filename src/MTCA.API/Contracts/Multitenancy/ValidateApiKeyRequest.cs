namespace MTCA.API.Contracts.Tenant;

/// <summary>
/// 
/// </summary>
/// <param name="ApiKey"></param>
public record ValidateApiKeyRequest(
    Guid ApiKey);