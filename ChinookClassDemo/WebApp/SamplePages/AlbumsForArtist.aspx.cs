﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

# region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.SamplePages
{
    public partial class AlbumsForArtist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void FetchAlbums_Click(object sender, EventArgs e)
        {
            if (ArtistList.SelectedIndex ==0)
            {
                Message.Text = "No Artist has been selected";
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
            AlbumController sysmgr = new AlbumController();
            List<AlbumItem> info = sysmgr.Albums_GetByArtist(int.Parse(ArtistList.SelectedValue));

            AlbumsofArtistList.DataSource = info;
            AlbumsofArtistList.DataBind();
        }
    }
}