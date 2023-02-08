using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStreamingPlatform.Models.StreamingViewModel
{
    public class SerieIndexData<Serie> : IEnumerable<Serie>
    {
        public IEnumerable<Serie> Seriess { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
        public IEnumerable<Episode> Episodes { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int? SerieId { get; private set; }

        public SerieIndexData(IEnumerable<Serie> items, int count, int pageIndex, int pageSize, int? id)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Seriess = items;
            SerieId = id;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1 && SerieId == null);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages && SerieId == null);
            }
        }

        public static SerieIndexData<Serie> Create(IEnumerable<Serie> source, int pageIndex, int pageSize, int? id)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new SerieIndexData<Serie>(items, count, pageIndex, pageSize, id);
        }

        public IEnumerator<Serie> GetEnumerator()
        {
            return Seriess.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Seriess).GetEnumerator();
        }
    }
}
