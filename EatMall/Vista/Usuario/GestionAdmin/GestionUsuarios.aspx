<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs"
    Inherits="EatMall.Vista.Usuario.GestionAdmin.GestionUsuarios"
    MasterPageFile="~/Vista/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <%-- Tu contenido aquí --%>
    <asp:Panel ID="pnlBuscador"
        runat="server"
        DefaultButton="btnBuscar"
        CssClass="flex-grow-1"
        Style="max-width: 500px;">

        <div class="input-group">
            <span class="input-group-text"
                style="border-radius: 50px 0 0 50px; background-color: #f1f3f4; border: none; padding-left: 20px;">
                <i class="bi bi-search" style="color: #aaa;"></i>
            </span>

            <asp:TextBox ID="txtBusqueda"
                runat="server"
                CssClass="form-control"
                placeholder="Buscar Usuario"
                Style="background-color: #f1f3f4; border: none; height: 45px; box-shadow: none;" />

            <asp:LinkButton ID="btnBuscar"
                runat="server"
                CssClass="btn btn-warning d-flex align-items-center"
                Style="border-radius: 0 50px 50px 0; padding-right: 20px; padding-left: 15px; background-color: #FFA94D; color: white;"
                OnClick="btnBuscar_Click">

            <span class="d-none d-md-inline me-1">Buscar</span>
            <i class="bi bi-arrow-right-short"
                style="font-size: 1.2rem;"></i>
            </asp:LinkButton>
        </div>
    </asp:Panel>
    <br />
    <!-- TABLA -->
    <asp:GridView
        ID="gvUsuarios"
        runat="server"
        CssClass="table table-bordered table-striped"
        AutoGenerateColumns="false">

        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Documento" HeaderText="Documento" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Rol.Nombre" HeaderText="Rol" />
        </Columns>
    </asp:GridView>

    <!-- PAGINACIÓN -->
    <div class="d-flex justify-content-between mt-3">
        <asp:Button
            ID="btnAnterior"
            runat="server"
            Text="Anterior"
            CssClass="btn btn-outline-dark"
            OnClick="btnAnterior_Click" />

        <asp:Button
            ID="btnSiguiente"
            runat="server"
            Text="Siguiente"
            CssClass="btn btn-outline-dark"
            OnClick="btnSiguiente_Click" />
    </div>


</asp:Content>

