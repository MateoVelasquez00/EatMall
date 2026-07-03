<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="Cajero.aspx.cs" Inherits="EatMall.Vista.Usuario.Cajero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <asp:Panel ID="pnlPedidosLista" runat="server" Visible="true">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h3>Pedidos de Hoy</h3>
            <span class="badge bg-info fs-6">
                <%= DateTime.Now.ToString("dd/MM/yyyy") %>
            </span>
        </div>

        <asp:GridView ID="gvPedidos" runat="server"
            CssClass="table table-hover table-bordered"
            AutoGenerateColumns="false"
            DataKeyNames="Id"
            EmptyDataText="No hay pedidos para hoy."
            OnSelectedIndexChanged="gvPedidos_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="CodigoPedido" HeaderText="Código" />
                <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
                <asp:BoundField DataField="TelefonoCliente" HeaderText="Teléfono" />
                <asp:BoundField DataField="HoraEntrega" HeaderText="Hora" DataFormatString="{0:g}" />
                <asp:BoundField DataField="TipoEntrega" HeaderText="Tipo" />
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:ButtonField Text="Ver detalle" CommandName="Select"
                    ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-info" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlDetalle" runat="server" Visible="false" CssClass="container-fluid p-0">

        <div class="d-flex align-items-center gap-3 mb-4">
            <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn btn-outline-secondary d-flex align-items-center gap-1" OnClick="btnVolver_Click">
                ← Volver
            </asp:LinkButton>
            <h3 class="m-0">Resumen del pedido:
                <asp:Label ID="lblCodigoPedido" runat="server" class="text-info" /></h3>
        </div>

        <div class="row">
            <div class="col-md-8 card shadow-sm p-4 bg-white">
                <div class="table-responsive">
                    <table class="table align-middle">
                        <thead class="table-light text-muted">
                            <tr>
                                <th>Nombre del producto</th>
                                <th class="text-center">Estado Producto</th>
                                <th class="text-center">Cantidad</th>
                                <th class="text-end">Precio Unidad</th>
                                <th class="text-end">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDetalle" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <div class="d-flex align-items-center gap-3" style="min-height: 55px;">
                                                <img src='<%# Eval("Imagen") %>'
                                                    alt='<%# Eval("NombreProducto") %>'
                                                    class="rounded shadow-sm"
                                                    referrerpolicy="no-referrer"
                                                    style="width: 50px; height: 50px; object-fit: cover; min-width: 50px; flex-shrink: 0;" />

                                                <span class="fw-semibold text-secondary text-wrap m-0" style="max-width: 250px; line-height: 1.2;">
                                                    <%# Eval("NombreProducto") %>
                                                </span>
                                            </div>
                                        </td>

                                       <td class="text-center">
    <span class='<%# Eval("EstadoProducto").ToString().Trim().ToUpper() == "LISTO" ? "badge bg-success" : "badge bg-warning text-dark" %>'>
        <%# Eval("EstadoProducto") %>
    </span>
</td>

                                        <td class="text-center text-secondary"><%# Eval("Cantidad") %></td>      
                                        <td class="text-end text-secondary"><%# String.Format("{0:C}", Eval("PrecioProducto")) %></td>
                                        <td class="text-end fw-bold"><%# String.Format("{0:C}", Eval("Subtotal")) %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card shadow-sm p-4 bg-white h-100 d-flex flex-column justify-content-between">
                    <div>
                        <h5 class="text-center fw-bold mb-4">Detalles de pago</h5>

                        <asp:Repeater ID="rptResumenPrecios" runat="server">
                            <ItemTemplate>
                                <div class="d-flex justify-content-between align-items-start mb-2 text-muted" style="font-size: 0.9rem;">
                                    <span class="text-wrap pe-2" style="max-width: 70%; text-align: left;"><%# Eval("NombreProducto") %></span>
                                    <span class="fw-semibold text-dark text-end" style="white-space: nowrap; min-width: 30%;">
                                        <%# String.Format("{0:C}", Eval("Subtotal")) %>
                                    </span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <hr class="text-muted" />

                        <div class="d-flex justify-content-between align-items-center my-3">
                            <span class="fw-bold fs-5">Monto total</span>
                            <span class="fw-bold fs-4 text-warning">
                                <asp:Label ID="lblMontoTotal" runat="server" />
                            </span>
                        </div>
                    </div>

                    <div>
                        <div class="card bg-light border-0 shadow-sm rounded-3 mb-4">
                            <div class="card-body p-3">

                                <div class="d-flex justify-content-between align-items-center">
                                    <span class="text-secondary">Medio de Pago</span>
                                    <span class="fw-semibold text-dark text-end text-uppercase">
                                        <asp:Label ID="lblMedioPago" runat="server" Text="Online" />
                                    </span>
                                </div>

                                <div class="d-flex justify-content-between align-items-center border-top pt-2">
                                    <span class="text-secondary">Referencia de Pago</span>
                                    <span class="fw-semibold text-dark text-end font-monospace" style="max-width: 60%; word-break: break-all;">
                                        <asp:Label ID="lblPayuRef" runat="server" Text="N/A" />
                                    </span>
                                </div>

                                <div class="d-flex justify-content-between align-items-center border-top pt-2">
                                    <span class="text-secondary">Fecha </span>
                                    <span class="text-dark text-end" style="white-space: nowrap;">
                                        <asp:Label ID="lblFechaTrans" runat="server" Text="N/A" />
                                    </span>
                                </div>

                                <div class="d-flex justify-content-between align-items-center border-top pt-2">
                                    <span class="text-secondary">Estado del Pago</span>
                                    <span>
                                        <asp:Label ID="lblEstadoPago" runat="server" CssClass="badge px-2.5 py-1.5 fw-bold" Text="Desconocido" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="d-grid gap-2">
    <asp:HiddenField ID="hfIdPedido" runat="server" />
    
    <asp:Button ID="btnEnPreparacion" runat="server" Text="En Preparación"
        CssClass="btn btn-warning fw-bold text-white" OnClick="btnEnPreparacion_Click" />
    
   
    <asp:Button ID="btnEntregado" runat="server" Text="Entregado"
        CssClass="btn btn-success fw-bold" OnClick="btnEntregado_Click" />
</div>

                </div>
            </div>
        </div>

    </asp:Panel>
</asp:Content>
