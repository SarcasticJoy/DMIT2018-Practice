<%@ Page Title="Albums for Artist" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumsForArtist.aspx.cs" Inherits="WebApp.SamplePages.AlbumsForArtist" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Albums for Artist</h1>
    <asp:Label ID="Message" runat="server"></asp:Label>
    <br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <br />
    <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" 
                                                     DataTextField="DisplayField" 
                                                     DataValueField="ValueField"
                                                     AppendDataBoundItems="true">
    <asp:ListItem Value="0">select an artist...</asp:ListItem>
        
    </asp:DropDownList> &nbsp; &nbsp;
    <asp:LinkButton ID="FetchAlbums" runat="server" OnClick="FetchAlbums_Click">Fetch Albums</asp:LinkButton>
    <asp:ObjectDataSource ID="ArtistListODS" OnSelected="SelectCheckForException" runat="server" OldValuesParameterFormatString="original_{0}" 
                                                            SelectMethod="Artists_List" 
                                                            TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
    <br />
    <br />
    <asp:GridView ID="AlbumsofArtistList" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="AlbumsofArtistList_PageIndexChanging" PageSize="5">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("AlbumId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Label">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("ReleaseLabel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            Artist has no Albums on file.
        </EmptyDataTemplate>
        <PagerSettings Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="3" PreviousPageText="Back" />
    </asp:GridView>
</asp:Content>
