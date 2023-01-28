using NuGet.Packaging;

namespace Agency.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> list, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPageCount = (int)Math.Ceiling((double)count / pageSize);
            this.AddRange(list);
        }
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious { get => CurrentPage > 1; }
        public bool HasNext { get => CurrentPage < TotalPageCount; }
        public static PaginatedList<T> Create(IQueryable<T> query, int pageNumber, int pageSize)
        {
            int count = query.Count();
            List<T> list = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(list, count, pageNumber, pageSize);
        }
    }
}
