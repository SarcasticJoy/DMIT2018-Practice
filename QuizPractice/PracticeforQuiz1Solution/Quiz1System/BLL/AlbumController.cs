using Quiz1System.DAL;
using Quiz1System.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz1System.BLL
{
    [DataObject]
    public class AlbumController
    {
        #region Queries

        public List<AlbumItem> Albums_GetByArtist(int artistid)
        {
            using (var context = new QuizPracticeSystemContext())
            {
                //LINQ query
                IEnumerable<AlbumItem> results = from x in context.Albums
                                                 where x.ArtistId == artistid
                                                 select new AlbumItem
                                                 {
                                                     AlbumId = x.AlbumId,
                                                     Title = x.Title,
                                                     ArtistId = artistid,
                                                     ReleaseYear = x.ReleaseYear,
                                                     ReleaseLabel = x.ReleaseLabel
                                                 };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AlbumItem> Albums_List()
        {
            using (var context = new QuizPracticeSystemContext())
            {
                //LINQ query
                IEnumerable<AlbumItem> results = from x in context.Albums
                                                 select new AlbumItem
                                                 {
                                                     AlbumId = x.AlbumId,
                                                     Title = x.Title,
                                                     ArtistId = x.ArtistId,
                                                     ReleaseYear = x.ReleaseYear,
                                                     ReleaseLabel = x.ReleaseLabel
                                                 };
                return results.ToList();
            }
        }
        #endregion
    }
}
