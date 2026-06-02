<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ConfirmacionPago.aspx.cs"
    Inherits="EatMall.Vista.Pago.ConfirmacionPago" %>

<!DOCTYPE html>
<html>
<head>
    <title>Confirmación de Pago</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=DM+Serif+Display:ital@0;1&family=DM+Sans:wght@300;400;500&display=swap" rel="stylesheet" />
    <style>
        body {
            background: #f5f4f0;
            font-family: 'DM Sans', sans-serif;
            min-height: 100vh;
        }

        .pago-wrap {
            background: #ffffff;
            border-radius: 20px;
            padding: 2.25rem 2rem;
            max-width: 520px;
            margin: 3rem auto;
            border: 0.5px solid rgba(0,0,0,0.08);
        }

        .pago-step {
            display: inline-block;
            font-size: 11px;
            font-weight: 500;
            letter-spacing: 0.08em;
            text-transform: uppercase;
            color: #888780;
            background: #f5f4f0;
            border: 0.5px solid rgba(0,0,0,0.12);
            border-radius: 20px;
            padding: 3px 10px;
            margin-bottom: 1.25rem;
        }

        .pago-title {
            font-family: 'DM Serif Display', serif;
            font-size: 28px;
            color: #fb640c;
            font-weight: 400;
            margin: 0 0 1.75rem;
            line-height: 1.2;
        }

            .pago-title em {
                font-style: italic;
                color: #fb640c;
            }

        .metodo-card {
            background: #f9f9f7;
            border: 0.5px solid rgba(0,0,0,0.08);
            border-radius: 12px;
            padding: 1rem 1.25rem;
            display: flex;
            align-items: center;
            gap: 14px;
            margin-bottom: 1rem;
        }

        .metodo-icon-box {
            width: 46px;
            height: 46px;
            border-radius: 10px;
            background: #E1F5EE;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
        }

            .metodo-icon-box img {
                width: 26px;
                height: auto;
            }

        .metodo-info {
            flex: 1;
        }

        .metodo-label {
            font-size: 11px;
            color: #888780;
            margin: 0 0 2px;
            letter-spacing: 0.04em;
            text-transform: uppercase;
        }

        .metodo-value {
            font-size: 15px;
            font-weight: 500;
            color: #1a1a18;
            margin: 0;
        }

        .metodo-badge {
            font-size: 11px;
            font-weight: 500;
            color: #0F6E56;
            background: #E1F5EE;
            border-radius: 20px;
            padding: 3px 10px;
            white-space: nowrap;
        }

        .desglose {
            background: #f9f9f7;
            border: 0.5px solid rgba(0,0,0,0.08);
            border-radius: 12px;
            padding: 1rem 1.25rem;
            margin-bottom: 1.25rem;
        }

        .desglose-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            font-size: 13px;
            color: #5F5E5A;
            padding: 4px 0;
        }

            .desglose-row.total-row {
                border-top: 0.5px solid rgba(0,0,0,0.1);
                margin-top: 8px;
                padding-top: 12px;
                font-size: 14px;
                color: #1a1a18;
                font-weight: 500;
            }

        .total-amount {
            font-family: 'DM Serif Display', serif;
            font-size: 22px;
            color: #0F6E56;
            font-weight: 400;
        }

        .btn-confirmar {
            display: block;
            width: 100%;
            padding: 14px;
            background: #1D9E75;
            color: #ffffff;
            border: none;
            border-radius: 10px;
            font-family: 'DM Sans', sans-serif;
            font-size: 15px;
            font-weight: 500;
            cursor: pointer;
            letter-spacing: 0.01em;
            transition: background 0.15s, transform 0.1s;
            margin-bottom: 10px;
            text-align: center;
        }

            .btn-confirmar:hover {
                background: #0F6E56;
                color: #fff;
            }

            .btn-confirmar:active {
                transform: scale(0.99);
            }

        .btn-cancelar {
            display: block;
            width: 100%;
            padding: 12px;
            background: transparent;
            color: #5F5E5A;
            border: 0.5px solid rgba(0,0,0,0.18);
            border-radius: 10px;
            font-family: 'DM Sans', sans-serif;
            font-size: 14px;
            cursor: pointer;
            transition: background 0.15s;
            text-align: center;
        }

            .btn-cancelar:hover {
                background: #f5f4f0;
                color: #1a1a18;
            }

        .seguro-note {
            display: flex;
            align-items: center;
            gap: 6px;
            justify-content: center;
            font-size: 12px;
            color: #888780;
            margin-top: 14px;
        }

        .seguro-dot {
            width: 6px;
            height: 6px;
            border-radius: 50%;
            background: #1D9E75;
            flex-shrink: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pago-wrap">

            <span class="pago-step">Paso 3 de 3</span>

            <h1 class="pago-title">Confirma tu<br>
                <em>pedido</em></h1>

            <%-- Método de pago --%>
            <div class="metodo-card">
                <div class="metodo-icon-box">
                    <asp:Image ID="imgMetodo" runat="server" Width="26px" />
                </div>
                <div class="metodo-info">
                    <p class="metodo-label">Método de pago</p>
                    <asp:Label ID="lblMetodo" runat="server" CssClass="metodo-value" />
                </div>
                <span class="metodo-badge">Seleccionado</span>
            </div>

            <%-- Desglose de precios --%>
            <%-- Desglose de precios (asegúrate de incluir la apertura del div) --%>
            <div class="desglose">

                <div class="desglose-row">
                    <span>Hora de entrega</span>
                    <asp:Label ID="lblHoraEntrega" runat="server" CssClass="text-muted" Text="No especificada" />
                </div>

                <div class="desglose-row">
                    <span>Subtotal</span>
                    <asp:Label ID="lblSubtotal" runat="server" />
                </div>

                <div class="desglose-row">
                    <span>Domicilio</span>
                    <asp:Label ID="lblDomicilio" runat="server" Text="Gratis" />
                </div>

                <div class="desglose-row">
                    <span>Descuento</span>
                    <asp:Label ID="lblDescuento" runat="server" Text="$0.00" Style="color: #1D9E75;" />
                </div>

                <div class="desglose-row total-row">
                    <span>Total a pagar</span>
                    <asp:Label ID="lblTotal" runat="server" CssClass="total-amount" />
                </div>

            </div>

        <%-- Botones --%>
        <asp:Button ID="btnConfirmar" runat="server"
            Text="Confirmar pago"
            CssClass="btn-confirmar"
            OnClick="btnConfirmar_Click" />

        <asp:Button ID="btnCancelar" runat="server"
            Text="Cancelar"
            CssClass="btn-cancelar"
            OnClick="btnCancelar_Click" />

        <div class="seguro-note">
            <div class="seguro-dot"></div>
            Pago seguro y encriptado
   
        </div>

        </div>
    </form>
</body>
</html>
