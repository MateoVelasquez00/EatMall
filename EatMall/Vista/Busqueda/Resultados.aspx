<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resultados.aspx.cs" Inherits="EatMall.Vista.Busqueda.Resultados" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Resultados de búsqueda</title>
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f5f5f5;
            color: #333;
            padding: 30px;
        }

        h2 {
            font-size: 22px;
            color: #444;
            margin-bottom: 20px;
            border-left: 4px solid #e84040;
            padding-left: 12px;
        }

        .seccion {
            margin-bottom: 40px;
        }

        .grid {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .card {
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            width: 220px;
            overflow: hidden;
            transition: transform 0.2s, box-shadow 0.2s;
        }

            .card:hover {
                transform: translateY(-4px);
                box-shadow: 0 6px 20px rgba(0,0,0,0.12);
            }

            .card img {
                width: 100%;
                height: 150px;
                object-fit: cover;
            }

        .card-body {
            padding: 14px;
        }

            .card-body strong {
                font-size: 16px;
                display: block;
                margin-bottom: 6px;
                color: #222;
            }

            .card-body span {
                font-size: 13px;
                color: #666;
                display: block;
                margin-bottom: 8px;
            }

        .card-meta {
            font-size: 12px;
            color: #999;
            margin-top: 6px;
        }

        .badge {
            display: inline-block;
            background-color: #e8f5e9;
            color: #2e7d32;
            font-size: 11px;
            padding: 3px 8px;
            border-radius: 20px;
            margin-top: 6px;
        }

        .precio {
            font-size: 15px;
            font-weight: bold;
            color: #e84040;
            margin-top: 8px;
        }

        .nombreCC {
            font-size: 12px;
            color: #999;
            margin-top: 6px;
        }

        .sin-resultados {
            color: #aaa;
            font-style: italic;
            font-size: 14px;
        }

        .header {
            display: flex;
            align-items: center;
            gap: 15px;
            margin-bottom: 25px;
        }

        /* Botón */
        .btn-volver {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 42px;
            width: 42px;
            border-radius: 50%;
            background: #fff;
            border: 1px solid #ddd;
            cursor: pointer;
            color: #333;
            transition: all 0.2s ease;
        }

            .btn-volver:hover {
                background: #f5f5f5;
                transform: scale(1.05);
            }

            .btn-volver:active {
                transform: scale(0.95);
            }

        /* Título alineado */
        .header h1 {
            font-size: 26px;
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        

        <div class="header">
            <button type="button" class="btn-volver" onclick="history.back()">
                <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
                    <path d="M19 12H5M5 12L11 18M5 12L11 6"
                        stroke="currentColor"
                        stroke-width="2"
                        stroke-linecap="round"
                        stroke-linejoin="round" />
                </svg>
            </button>
            <h1>Resultados de búsqueda</h1>

        </div>
        <asp:Label ID="lblMensaje" runat="server"
            Visible="false"
            EnableViewState="false"
            CssClass="sin-resultados"> </asp:Label>
        <div class="seccion">
            <div class="grid">
                <asp:Repeater ID="rptLocales" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <a href='<%# "/Vista/Local/Tienda.aspx?idLocal=" + Eval("Id") + "&idPlazoleta=" + Eval("IdPlazoleta") + "&idCC=" + Eval("IdCC") %>'style="text-decoration: none; color: inherit;">
                                <img src='<%# Eval("Imagen") %>' alt="Local" onerror="this.src='https://via.placeholder.com/220x150?text=Sin+imagen'" />
                                <div class="card-body">
                                    <strong><%# Eval("Nombre") %></strong> <span><%# Eval("Descripcion") %></span>
                                    <div class="card-meta">🏷️ <%# Eval("Plazoleta.Nombre") %> </div>
                                    <div class="nombreCC">🏢<%# Eval("CentroComercial.Nombre") %></div>
                                    <span class="badge"><%# Eval("Estado") %></span>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="seccion">
            <div class="grid">
                <asp:Repeater ID="rptProductos" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <a href='<%# "/Vista/Local/Tienda.aspx?idLocal=" + Eval("Local.Id") + "&idPlazoleta=" + Eval("IdPlazoleta") + "&idCC=" + Eval("IdCC") %>'style="text-decoration: none; color: inherit;">
                                <img src='<%# Eval("Imagen") %>' alt="Producto" onerror="this.src='https://via.placeholder.com/220x150?text=Sin+imagen'" />
                                <div class="card-body">
                                    <strong><%# Eval("Nombre") %></strong> <span><%# Eval("Descripcion") %></span>
                                    <div class="precio">$<%# Eval("Precio", "{0:N2}") %></div>
                                    <div class="card-meta">♨️ <%# Eval("Local.Nombre") %></div>
                                    <div class="nombreCC">🏢 <%# Eval("CentroComercial.Nombre") %></div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="seccion">
            <div class="grid">
                <asp:Repeater ID="rptCiudad" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <a href='<%# "../Plazoleta/Plazoleta.aspx?id=" + Eval("Id") %>' style="text-decoration: none; color: inherit;">
                                <img src='<%# Eval("Imagen") %>' alt="Centro Comercial" onerror="this.src='https://via.placeholder.com/220x150?text=Sin+imagen'" />
                                <div class="card-body">
                                    <strong><%# Eval("Nombre") %></strong> <span><%# Eval("Descripcion") %></span>
                                    <div class="card-meta">📍 <%# Eval("Ubicacion") %></div>
                                    <div class="card-meta">🏙️ <%# Eval("Ciudad.NombreCiudad") %></div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="seccion">
            <div class="grid">
                <asp:Repeater ID="rptCentros" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <a href='<%# "../Plazoleta/Plazoleta.aspx?id=" + Eval("Id") %>' style="text-decoration: none; color: inherit;">
                                <img src='<%# Eval("Imagen") %>' alt="Centro Comercial" onerror="this.src='https://via.placeholder.com/220x150?text=Sin+imagen'" />
                                <div class="card-body">
                                    <strong><%# Eval("Nombre") %></strong> <span><%# Eval("Descripcion") %></span>
                                    <div class="card-meta">📍 <%# Eval("Ubicacion") %></div>
                                    <div class="card-meta">🏙️ <%# Eval("Ciudad.NombreCiudad") %></div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
