namespace Application.Util
{
    public class Pagination
    {
        public Pagination(int? limit, int? offset)
        {
            Limit = limit;
            Offset = offset;
        }
        public Pagination()
        {

        }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}