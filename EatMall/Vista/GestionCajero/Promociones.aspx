<%@ Page Title="Promociones" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="Promociones.aspx.cs" Inherits="EatMall.Vista.GestionCajero.Promociones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Promociones del Local</h3>
    </div>

    <asp:GridView ID="gvPromociones" runat="server"
        CssClass="table table-hover table-bordered align-middle bg-white"
        AutoGenerateColumns="false"
        DataKeyNames="Id" 
        OnRowCommand="gvPromociones_RowCommand" 
        EmptyDataText="No hay promociones registradas para este local.">
        <Columns>

            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <asp:Image ID="imgPromocion" runat="server" 
                        ImageUrl='<%# Eval("Imagen") %>' 
                        AlternateText="Promoción"
                        CssClass="rounded shadow-sm" 
                        style="width: 50px; height: 50px; object-fit: cover;" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Id"     HeaderText="Id" Visible="false" />
            <asp:BoundField DataField="Nombre" HeaderText="Promoción" />
            <asp:BoundField DataField="Total"  HeaderText="Precio Combo" DataFormatString="{0:C}" />

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:LinkButton ID="btnCambiarEstado" runat="server"
                        CommandName="CambiarEstado"
                        CommandArgument='<%# Eval("Id") %>'
                        Style="text-decoration: none;"
                        CssClass='<%# Convert.ToBoolean(Eval("Estado")) ? "badge bg-success border-0" : "badge bg-danger border-0" %>'>
                        <%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</asp:Content>