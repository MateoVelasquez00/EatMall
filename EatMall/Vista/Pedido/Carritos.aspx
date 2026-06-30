<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carritos.aspx.cs" Inherits="EatMall.Vista.Pedido.Carritos" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .carrito-card { border-radius: 20px; box-shadow: 0 0 20px rgba(0,0,0,0.08); background: white; border: none; }
        .producto-row { border-radius: 12px; border: 1px solid #f0f0f0; transition: all 0.2s; background: #fff; margin-bottom: 15px; }
        .producto-row:hover { box-shadow: 0 4px 15px rgba(0,0,0,0.08); }
        .total-card { border-radius: 20px; background-color: #ffffff; border: none; box-shadow: 0 0 20px rgba(0,0,0,0.08); }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:HiddenField ID="CarritoData" runat="server" />

    <div class="container mt-5 mb-5">
        <h3 class="fw-bold mb-4"><i class="bi bi-cart3 me-2 text-primary"></i>Mi Carrito</h3>

        <div class="nav-item mb-4">
            <a href="/Index.aspx" class="nav-link d-flex align-items-center text-secondary">
                <i class="bi bi-arrow-left-short fs-4"></i><span class="fw-semibold ms-1">Volver al inicio</span>
            </a>
        </div>

        <div class="row">
            <div class="col-md-8">
                <div class="card carrito-card p-4">
                    
                    <div class="mb-4">
                        <label class="fw-bold mb-2">Seleccione la hora de entrega:</label>
                        <asp:DropDownList ID="ddlHoraEntrega" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div id="contenedorCarrito"></div>

                  
                    <div id="panelVacio" class="text-center py-5" style="display: none;">
                        <i class="bi bi-cart-x text-secondary" style="font-size: 4rem;"></i>
                        <h5 class="text-muted mt-3">Tu carrito está vacío</h5>
                        <a href="/Index.aspx" class="btn btn-primary mt-3">Ir a Tiendas</a>
                    </div>

                </div>
            </div>

            <div class="col-md-4">
                <div class="card total-card p-4">
                    <h5 class="fw-bold mb-4">Resumen del pedido</h5>
                    <div class="d-flex justify-content-between mb-2">
                        <span class="text-muted">Subtotal</span>

                        <span class="fw-bold">$<span id="lblSubtotalText">0.00</span></span>
                    </div>
                    <hr />
                    <div class="d-flex justify-content-between mb-4">
                        <span class="fw-bold fs-5">Total</span>
                        <span class="fw-bold fs-5 text-primary">$<span id="lblTotalText">0.00</span></span>
                    </div>
                    
                   
                    <asp:Button ID="btnConfirmar" runat="server" 
                        Text="Confirmar pedido →" 
                        CssClass="btn btn-success w-100 fw-bold" 
                        OnClientClick="MtPrepararEnvio();" 
                        OnClick="btnConfirmar_Click" />
                    
                    <a href="/Index.aspx" class="btn btn-outline-secondary w-100 mt-2">Seguir comprando</a>
                </div>
            </div>
        </div>
    </div>


    <script src='<%= ResolveUrl("~/Js/Carrito.js") %>'></script>
</asp:Content>