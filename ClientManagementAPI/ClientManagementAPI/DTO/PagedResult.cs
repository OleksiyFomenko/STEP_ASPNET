namespace ClientManagementAPI.DTO
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();

        public int Page { get; set; }

        public int Size { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public static PagedResult<T> Create(
        List<T> items,
        int page,
        int size,
        int totalCount)
        {
            int totalPages = (int)Math.Ceiling(
                (double)totalCount / size);

            return new PagedResult<T>
            {
                Items = items,
                Page = page,
                Size = size,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = page > 1,
                HasNextPage = page < totalPages
            };
        }

    }
}
