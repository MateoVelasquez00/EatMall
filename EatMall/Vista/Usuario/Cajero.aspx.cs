using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario
{
    public partial class Cajero : System.Web.UI.Page<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="Cajero.aspx.cs" Inherits="EatMall.Vista.Usuario.Cajero" %>
<asp:Content ID = "Content1" ContentPlaceHolderID="head2" runat="server">
</asp:Content>
<asp:Content ID = "Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Pedidos de Hoy</h3>
        <span class="badge bg-info fs-6">
            <%= DateTime.Now.ToString("dd/MM/yyyy") %>
        </span>
    </div>

    <asp:GridView ID = "gvPedidos" runat="server" 
        CssClass="table table-hover table-bordered"
        AutoGenerateColumns="false"
        EmptyDataText="No hay pedidos para hoy."
        OnSelectedIndexChanged="gvPedidos_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField = "CodigoPedido"   HeaderText="Código"   />
            <asp:BoundField DataField = "NombreCliente"  HeaderText="Cliente"  />
            <asp:BoundField DataField = "TelefonoCliente" HeaderText="Teléfono" />
            <asp:BoundField DataField = "HoraEntrega"    HeaderText="Hora"     DataFormatString="{0:hh\\:mm}" />
            <asp:BoundField DataField = "TipoEntrega"    HeaderText="Tipo"     />
            <asp:BoundField DataField = "Total"          HeaderText="Total"    DataFormatString="{0:C}" />
            <asp:BoundField DataField = "Estado"         HeaderText="Estado"   />
            <asp:ButtonField Text = "Ver detalle" CommandName="Select" 
                ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-info" />
        </Columns>
    </asp:GridView>

    <!-- Panel detalle del pedido -->
    <asp:Panel ID = "pnlDetalle" runat="server" Visible="false" CssClass="mt-4">
        <h5>Detalle del pedido: <asp:Label ID = "lblCodigoPedido" runat="server" /></h5>
        <asp:GridView ID = "gvDetalle" runat="server"
            CssClass="table table-sm table-bordered"
            AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField = "NombreProducto" HeaderText="Producto"  />
                <asp:BoundField DataField = "Cantidad"       HeaderText="Cantidad"  />
                <asp:BoundField DataField = "PrecioUnitario" HeaderText="Precio"    DataFormatString="{0:C}" />
                <asp:BoundField DataField = "Subtotal"       HeaderText="Subtotal"  DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <!-- Cambiar estado -->
        <div class="d-flex gap-2 mt-3">
            <asp:HiddenField ID = "hfIdPedido" runat="server" />
            <asp:Button ID = "btnEnPreparacion" runat="server" Text="En Preparación" 
                CssClass="btn btn-warning" OnClick="btnEnPreparacion_Click" />
            <asp:Button ID = "btnListo" runat="server" Text="Listo" 
                CssClass="btn btn-success" OnClick="btnListo_Click" />
        </div>
    </asp:Panel>

</asp:Content>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}