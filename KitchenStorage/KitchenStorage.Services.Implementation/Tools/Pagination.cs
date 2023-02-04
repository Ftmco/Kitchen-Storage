namespace KitchenStorage.Services.Implementation.Tools
{
    public static class Pagination
    {
        public static int PageCount(this int modelCount, int pageSize)
                => pageSize == 0 ? modelCount : modelCount / pageSize;

        public static PaginationResult<T> GetPaginationResult<T>(this T tmodels, int pageCount)
                      => new(PageCount: pageCount, Result: tmodels);
    }
}
