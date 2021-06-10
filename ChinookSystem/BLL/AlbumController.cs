using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Name Spaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using System.ComponentModel; // Becuase we are using the ODS (Object Data Source) Wizard
#endregion

namespace ChinookSystem.BLL
{
    [DataObject] // For ODS
    public class AlbumController // notice public
    {

        #region Queries
        public List<ArtistAlbumsByTitleandYear> Albums_ArtistAlbumsByTitleandYear()
        {
            using (var context = new ChinookSystemContext())
            {
               IEnumerable<ArtistAlbumsByTitleandYear> results = context.Albums //adding context is the one change from  grabbing from linqpad
               .OrderBy(x => x.Artist.Name)
               .ThenBy(x => x.Title)
               .ThenByDescending(x => x.ReleaseYear)
               .Where(x => x.ReleaseYear > 1979 && x.ReleaseYear < 1998)
               .Select(x => new ArtistAlbumsByTitleandYear
               {
                   Artist = x.Artist.Name,
                   Title = x.Title,
                   Year = x.ReleaseYear,
                   Label = x.ReleaseLabel == null ? "Unkown" : x.ReleaseLabel //Replace null with "Unkown"
               });

                return results.ToList();
             }

        }

        public List<AlbumItem> Albums_GetByArtist(int artistid)
        {
            using (var context = new ChinookSystemContext())
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

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<AlbumItem> Albums_List()
        {
            using (var context = new ChinookSystemContext())
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

        #region Add, Update and Delete CRUD
        [DataObjectMethod(DataObjectMethodType.Insert,false)] // Insert for ADD methods. (We used to use select)
        
        public int Albums_Add(AlbumItem album)  //bring in a class instance
        {
            using (var context = new ChinookSystemContext())
            {
                // Due to the fact that e have seperated the handling of our entities
                // fromt he data transfer between web app and class library
                // using the ViewModel classes, we MUST create an instance 
                // of the entity and MOVE the data from the ViewModel class instance
                // to the entity instance.
                Album addAlbum = new Album()
                {
                    // no PK becuase it is an identity PK (auto incremented)
                    Title = album.Title,
                    ArtistId = album.ArtistId,
                    ReleaseYear = album.ReleaseYear,
                    ReleaseLabel = album.ReleaseLabel
                }; //The above is the new step we do this class year

                //staging 

                //setup in local memory
                //at this point you will NOT have sent anything to the database
                // therefore, you will NOT have your new pkey as yet.
                context.Albums.Add(addAlbum);

                //commit to database
                //ont his command you:
                // a) execute any entity validation annotation (in the entity documents)
                // b) send your local memory staging to the database for execution
                // after a successful execution your entity instance will have the new 
                // PK value
                context.SaveChanges();

                //at this point, if succesful, you entity instance has the new 
                // pkey value.
                return addAlbum.AlbumId;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update,false)]
        public void Albums_Update(AlbumItem album)  //bring in a class instance
        {
            using (var context = new ChinookSystemContext())
            {
                
                Album updateAlbum = new Album()
                {
                    //  PK needed to find
                    // you MUST identify the record that is to be updated
                    // this is done using the pkey
                    AlbumId = album.AlbumId,
                    Title = album.Title,
                    ArtistId = album.ArtistId,
                    ReleaseYear = album.ReleaseYear,
                    ReleaseLabel = album.ReleaseLabel
                }; 

                //staging 
                // all fields on record are changed
                context.Entry(updateAlbum).State = System.Data.Entity.EntityState.Modified;

                //commit to database
                //ont his command you:
                // a) execute any entity validation annotation (in the entity documents)
                // b) send your local memory staging to the database for execution
                // after a successful execution your entity instance will have the new 
                // PK value
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete,false)]
        public void Albums_Delete(AlbumItem album)
        {
            Albums_Delete(album.AlbumId);
        }

        //Overload & does not need ODS (Dataobject method)
        public void Albums_Delete(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                //example of a physical delete
                // this is were the record is physically removed from the database
                // thus, you will do a .Remove()
                var exists = context.Albums.Find(albumid);
                // if the result is either the record or a null
                if (exists == null)
                {
                    throw new Exception($"No albumid by the id of {albumid} exists on file.");
                }
                    
                context.Albums.Remove(exists);
                context.SaveChanges();
                

                //example of a logical delete 
                //this is where you will set a attribute on the database record
                // which logicaly indicates not to use the record
                // this type of delete is actually an Update to the record.
                // aka FLAG the record as non-active
            }

        }
        #endregion 

    }
}
