<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="EatMall.Vista.GestionCajero.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3 class="fw-bold">Historial de Pedidos Entregados </h3>
    </div>

    <asp:GridView ID="gvHistorialPedidos" runat="server"
        CssClass="table table-hover table-bordered align-middle bg-white"
        AutoGenerateColumns="false"
        DataKeyNames="Id" 
        EmptyDataText="No hay pedidos registrados como entregados para este local.">
        <Columns>
            
            <%-- COLUMNAS DE DATOS --%>
            <asp:BoundField DataField="CodigoPedido"  HeaderText="Código" />
            <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
            <asp:TemplateField HeaderText="Fecha / Hora">
                <ItemTemplate>
                    <%# Convert.ToDateTime(Eval("FechaPedido")).ToString("dd/MM/yyyy") %> - <%# Eval("HoraEntrega") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TipoEntrega"   HeaderText="Tipo" />
            <asp:BoundField DataField="Total"         HeaderText="Total" DataFormatString="{0:C}" />
            
            <%-- COLUMNA ESTADO (FIJO EN ENTREGADO) --%>
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <span class="badge bg-success">Entregado</span>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</asp:Content>