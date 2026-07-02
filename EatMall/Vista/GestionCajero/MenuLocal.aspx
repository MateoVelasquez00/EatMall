<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="MenuLocal.aspx.cs" Inherits="EatMall.Vista.Usuario.MenuLocal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Menú del Local</h3>
    </div>

   <asp:GridView ID="gvProductos" runat="server"
    CssClass="table table-hover table-bordered align-middle"
    AutoGenerateColumns="false"
    DataKeyNames="Id" 
    OnRowCommand="gvProductos_RowCommand" 
    EmptyDataText="No hay productos registrados.">
    <Columns>

        <asp:TemplateField HeaderText="Imagen">
            <ItemTemplate>
                <asp:Image ID="imgProducto" runat="server" 
                    ImageUrl='<%# Eval("Imagen") %>' 
                    AlternateText="Producto"
                    CssClass="rounded shadow-sm" 
                    style="width: 50px; height: 50px; object-fit: cover;" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Id"          HeaderText="Id"          Visible="false" />
        <asp:BoundField DataField="Nombre"      HeaderText="Producto"    />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
        <asp:BoundField DataField="Precio"      HeaderText="Precio"      DataFormatString="{0:C}" />
        <asp:BoundField DataField="IdCategoria" HeaderText="Categoría"   />
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