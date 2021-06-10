using Quiz1System.BLL;
using Quiz1System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticeforQuiz1
{
    public partial class ViewAlbumPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FetchAlbums_Click(object sender, EventArgs e)
        {
            if (ArtistList.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Artist Selection", "No Artist has been selected");
            }
            else
            {
                RefreshList();
            }
        }

        protected void AlbumsofArtistList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Needed for changing pages in gridview
            AlbumsofArtistList.PageIndex = e.NewPageIndex;
            RefreshList();
        }

        protected void RefreshList()
        {
            //error handling for the class library calls
            MessageUserControl.TryRun(() => { // The () means use this one
                AlbumController sysmgr = new AlbumController();
                List<AlbumItem> info = sysmgr.Albums_GetByArtist(int.Parse(ArtistList.SelectedValue));

                AlbumsofArtistList.DataSource = info;
                AlbumsofArtistList.DataBind();
            }, "Artist Albums", "View artist albums"); // first title, then message. You notice that we are passing the code and two strings as parameters
            // If you don't want a message you have to remove botht he title and the message string
        }

        protected void SelectCheckForException(object sender,
                                                ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e); // Implementing Dans Nuget.  Dont forget to tell the asp webpage side to use this method
                                                            // I mean this: <asp:ObjectDataSource ID="ArtistListODS" OnSelected="SelectCheckForException"
        }
    }
}