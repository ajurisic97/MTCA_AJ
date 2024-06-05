namespace MTCA.API.Contracts;
/// <summary>
/// BaseContract
/// </summary>
/// <param name="Page"></param>
/// <param name="PageSize"></param>
public abstract record BaseContract(
    int Page = 1,
    int PageSize = 10);
