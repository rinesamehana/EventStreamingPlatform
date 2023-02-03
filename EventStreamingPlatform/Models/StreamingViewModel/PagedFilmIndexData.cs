using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStreamingPlatform.Models.StreamingViewModel

{
    public class PagedFilmIndexData<Film> : IEnumerable<Film>
    {
        public IEnumerable<Film> Films { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int? FilmId { get; private set; }

        public PagedFilmIndexData(IEnumerable<Film> items, int count, int pageIndex, int pageSize, int? id)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Films = items;
            FilmId = id;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1 && FilmId == null);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages && FilmId == null);
            }
        }

        public static PagedFilmIndexData<Film> Create(IEnumerable<Film> source, int pageIndex, int pageSize, int? id)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedFilmIndexData<Film>(items, count, pageIndex, pageSize, id);
        }

        public IEnumerator<Film> GetEnumerator()
        {
            return Films.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Films).GetEnumerator();
        }
    }
}
