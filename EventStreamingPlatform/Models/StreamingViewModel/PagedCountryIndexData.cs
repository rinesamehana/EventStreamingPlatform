
using System.Collections;

namespace EventStreamingPlatform.Models.StreamingViewModel
{
    public class PagedCountryIndexData<Country> : IEnumerable<Country>
    {
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int? Countryid { get; private set; }

        public PagedCountryIndexData(IEnumerable<Country> items, int count, int pageIndex, int pageSize, int? id)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Countries = items;
            Countryid = id;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1 && Countryid == null);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages && Countryid == null);
            }
        }

        public static PagedCountryIndexData<Country> Create(IEnumerable<Country> source, int pageIndex, int pageSize, int? id)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedCountryIndexData<Country>(items, count, pageIndex, pageSize, id);
        }

        public IEnumerator<Country> GetEnumerator()
        {
            return Countries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Countries).GetEnumerator();
        }
    }
}