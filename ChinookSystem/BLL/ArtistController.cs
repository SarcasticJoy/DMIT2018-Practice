using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject] // exposes the class
    public class ArtistController // Make sure to give the controller public access
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)] //expose the method
        public List<SelectionList> Artists_List() // Ne wclass selectionlist insteads of Artist
        {
            using(var context = new ChinookSystemContext())
            {
                //Old Class:
                // return context.Artists.ToList();

                //This example uses "method" syntax. It will look alot like an object
                //Find out what IEnumerable is when we do LINK query
                IEnumerable<SelectionList> results = context.Artists.Select( row => new SelectionList //you can name row whatever you want, its just a variable
                                                            {
                                                                ValueField = row.ArtistId,
                                                                DisplayField = row.Name
                                                            });
                return results.OrderBy(x => x.DisplayField).ToList();


            }
        }
    }
}
