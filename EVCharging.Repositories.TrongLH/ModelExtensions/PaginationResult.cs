namespace EVCharging.Repositories.TrongLH.ModelExtensions;

public class PaginationResult<T> where T : class
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public T? Items { get; set; }
}