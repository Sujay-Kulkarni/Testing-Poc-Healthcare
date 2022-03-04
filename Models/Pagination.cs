using Testing_Poc_Healthcare.Interface;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientSearchPagination : IPagination
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public PatientSearchPagination()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PatientSearchPagination(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
