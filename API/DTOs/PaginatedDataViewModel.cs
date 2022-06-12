namespace API.DTOs
{
    public class PaginatedDataViewModel
    {
        public int PageIndex { get; set; }

        private int TotalPages;
        private int PageSize = 5;

        private int Skip;

        public void GenerateSkipAndTotalPage(int totalItemsSize)
        {
            
            TotalPages = (int)Math.Ceiling(totalItemsSize / (double) PageSize);
            Skip = (PageIndex - 1) * PageSize;

            if (PageIndex <= 0 || TotalPages < PageIndex)
            {
                throw new Exception("Invalid page index!");
            }
        }

        public int GetTotalPages()
        {
            return TotalPages;
        }

        public int GetSkip()
        {
            return Skip;
        }

        public int GetPageSizes()
        {
            return PageSize;
        }
    }
}
