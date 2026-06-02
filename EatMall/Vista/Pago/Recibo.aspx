<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="Recibo.aspx.cs" 
    Inherits="EatMall.Vista.Pago.Recibo" %>

<!DOCTYPE html>
<html>
<head>
    <title>Recibo de Pago</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=DM+Serif+Display:ital@0;1&family=DM+Sans:wght@300;400;500&display=swap" rel="stylesheet"/>
    <style>
        body {
            background: #f5f4f0;
            font-family: 'DM Sans', sans-serif;
            min-height: 100vh;
        }

        .recibo-wrap {
            max-width: 480px;
            margin: 3rem auto;
            padding: 0 1rem;
        }

        /* ── Encabezado éxito ── */
        .recibo-header {
            background: #ffffff;
            border-radius: 16px 16px 0 0;
            border: 0.5px solid rgba(0,0,0,0.08);
            border-bottom: none;
            padding: 2rem 2rem 1.5rem;
            text-align: center;
        }

        .check-circle {
            width: 64px;
            height: 64px;
            border-radius: 50%;
            background: #E1F5EE;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 1rem;
        }

        .check-circle svg {
            width: 30px;
            height: 30px;
        }

        .recibo-titulo {
            font-family: 'DM Serif Display', serif;
            font-size: 26px;
            font-weight: 400;
            color: #1a1a18;
            margin: 0 0 4px;
        }

        .recibo-titulo em {
            font-style: italic;
            color: #1D9E75;
        }

        .recibo-subtitulo {
            font-size: 13px;
            color: #888780;
            margin: 0;
        }

        /* ── Código del pedido ── */
        .codigo-pedido {
            background: #f0faf6;
            border-left: 3px solid #1D9E75;
            border-right: 0.5px solid rgba(0,0,0,0.08);
            border-top: 0.5px solid rgba(29,158,117,0.2);
            border-bottom: 0.5px solid rgba(29,158,117,0.2);
            padding: 1rem 2rem;
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .codigo-label {
            font-size: 11px;
            letter-spacing: 0.08em;
            text-transform: uppercase;
            color: #0F6E56;
            margin: 0 0 2px;
        }

        .codigo-valor {
            font-family: 'DM Serif Display', serif;
            font-size: 22px;
            color: #1a1a18;
            font-weight: 400;
            margin: 0;
        }

        .codigo-badge {
            font-size: 11px;
            font-weight: 500;
            color: #0F6E56;
            background: #E1F5EE;
            border-radius: 20px;
            padding: 4px 12px;
        }

        /* ── Cuerpo del recibo ── */
        .recibo-body {
            background: #ffffff;
            border: 0.5px solid rgba(0,0,0,0.08);
            border-top: none;
            border-bottom: none;
            padding: 1.5rem 2rem;
        }

        .recibo-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 8px 0;
            font-size: 13px;
        }

        .recibo-row-label {
            color: #888780;
        }

        .recibo-row-value {
            font-weight: 500;
            color: #1a1a18;
            text-align: right;
        }

        .estado-pill {
            font-size: 11px;
            font-weight: 500;
            color: #0F6E56;
            background: #E1F5EE;
            border-radius: 20px;
            padding: 3px 10px;
        }

        .separador {
            border: none;
            border-top: 1.5px dashed rgba(0,0,0,0.1);
            margin: 12px 0;
        }

        /* ── Total ── */
        .recibo-total {
            background: #f9f9f7;
            border: 0.5px solid rgba(0,0,0,0.08);
            border-top: none;
            border-bottom: none;
            padding: 1rem 2rem;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .total-label {
            font-size: 14px;
            font-weight: 500;
            color: #1a1a18;
        }

        .total-monto {
            font-family: 'DM Serif Display', serif;
            font-size: 26px;
            color: #0F6E56;
            font-weight: 400;
        }

        /* ── Footer / botón ── */
        .recibo-footer {
            background: #ffffff;
            border-radius: 0 0 16px 16px;
            border: 0.5px solid rgba(0,0,0,0.08);
            border-top: none;
            padding: 1.25rem 2rem 1.75rem;
        }

        .btn-inicio {
            display: block;
            width: 100%;
            padding: 13px;
            background: #1D9E75;
            color: #ffffff;
            border: none;
            border-radius: 10px;
            font-family: 'DM Sans', sans-serif;
            font-size: 15px;
            font-weight: 500;
            cursor: pointer;
            transition: background 0.15s, transform 0.1s;
            text-align: center;
        }

        .btn-inicio:hover  { background: #0F6E56; color: #fff; }
        .btn-inicio:active { transform: scale(0.99); }

        .seguro-note {
            display: flex;
            align-items: center;
            gap: 6px;
            justify-content: center;
            font-size: 12px;
            color: #888780;
            margin-top: 12px;
        }

        .seguro-dot {
            width: 6px; height: 6px;
            border-radius: 50%;
            background: #1D9E75;
            flex-shrink: 0;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div class="recibo-wrap">

    <%-- Encabezado --%>
    <div class="recibo-header">
        <div class="check-circle">
            <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M5 12.5L9.5 17L19 7" stroke="#1D9E75" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
        </div>
        <h1 class="recibo-titulo">¡Pago <em>exitoso!</em></h1>
        <p class="recibo-subtitulo">Tu pedido ha sido confirmado y está en camino</p>
    </div>

    <%-- Código del pedido --%>
    <div class="codigo-pedido">
        <div>
            <p class="codigo-label">Código del pedido</p>
            <asp:Label ID="lblCodigoPedido" runat="server" CssClass="codigo-valor"/>
        </div>
        <span class="codigo-badge">Activo</span>
    </div>

    <%-- Detalle --%>
    <div class="recibo-body">

        <div class="recibo-row">
            <span class="recibo-row-label">N° Transacción</span>
            <asp:Label ID="lblIdTransaccion" runat="server" CssClass="recibo-row-value"/>
        </div>

        <div class="recibo-row">
            <span class="recibo-row-label">Método de pago</span>
            <asp:Label ID="lblMetodo" runat="server" CssClass="recibo-row-value"/>
        </div>

        <div class="recibo-row">
            <span class="recibo-row-label">Fecha</span>
            <asp:Label ID="lblFecha" runat="server" CssClass="recibo-row-value"/>
        </div>

        <div class="recibo-row">
            <span class="recibo-row-label">Estado</span>
            <asp:Label ID="lblEstado" runat="server" CssClass="estado-pill"/>
        </div>

        <hr class="separador"/>

        <div class="recibo-row">
            <span class="recibo-row-label">Subtotal</span>
            <asp:Label ID="lblSubtotal" runat="server" CssClass="recibo-row-value"/>
        </div>

        <div class="recibo-row">
            <span class="recibo-row-label">Domicilio</span>
            <asp:Label ID="lblDomicilio" runat="server" CssClass="recibo-row-value"/>
        </div>

    </div>

    <%-- Total --%>
    <div class="recibo-total">
        <span class="total-label">Total pagado</span>
        <asp:Label ID="lblTotal" runat="server" CssClass="total-monto"/>
    </div>

    <%-- Footer --%>
    <div class="recibo-footer">
        <asp:Button ID="btnInicio" runat="server"
            Text="Volver al inicio"
            CssClass="btn-inicio"
            OnClick="btnInicio_Click"/>
        <div class="seguro-note">
            <div class="seguro-dot"></div>
            Transacción segura y encriptada
        </div>
    </div>

</div>
</form>
</body>
</html>
