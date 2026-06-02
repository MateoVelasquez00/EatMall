<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Local.aspx.cs"
    Inherits="EatMall.Vista.Pago.Local"
    MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <style>
        .local-card {
            background: white;
            border-radius: 16px;
            padding: 24px 16px;
            text-align: center;
            box-shadow: 0 2px 8px rgba(0,0,0,0.07);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
            height: 100%;
            position: relative;
        }

        .local-card:hover {
            transform: translateY(-4px);
            box-shadow: 0 6px 18px rgba(0,0,0,0.12);
        }

        .local-icono {
            width: 90px;
            height: 90px;
            border-radius: 50%;
            object-fit: cover;
            border: 3px solid #e0e0e0;
        }

        .local-nombre {
            font-size: 1rem;
            font-weight: 700;
            color: #1a1a1a;
            margin-top: 12px;
            margin-bottom: 8px;
        }

        .btn-ver-menu {
            background-color: #F27F0D;
            color: white;
            border: none;
            border-radius: 8px;
            width: 100%;
            padding: 10px;
            font-weight: 600;
            margin-top: 16px;
            transition: background 0.2s;
            display: block;
            text-decoration: none;
            text-align: center;
        }

        .btn-ver-menu:hover {
            background-color: #F27F0D;
            color: white;
        }

        .btn-cerrado {
            background-color: #e5e7eb;
            color: #9ca3af;
            border: none;
            border-radius: 8px;
            width: 100%;
            padding: 10px;
            font-weight: 600;
            margin-top: 16px;
            cursor: not-allowed;
        }

        .local-wrapper {
            position: relative;
            width: 90px;
            margin: 0 auto;
        }

        .estado-local {
            width: 16px;
            height: 16px;
            border-radius: 50%;
            position: absolute;
            bottom: 4px;
            right: 4px;
            border: 2px solid white;
        }

        .abierto {
            background-color: #28a745;
        }

        .cerrado {
            background-color: #dc3545;
        }

        .calificacion {
            position: absolute;
            top: 10px;
            left: 12px;
            color: #f59e0b;
            font-size: 0.85rem;
            font-weight: 600;
        }
    </style>

    <div class="container py-5">

        <!-- Botón volver -->
        <div class="mb-2">
            <asp:HyperLink ID="btnVolverPlazoleta"
                runat="server"
                Text="← Volver"
                CssClass="btn btn-outline-dark btn-sm" />
        </div>

        <!-- CAROUSEL -->
        <div id="carouselEatMall"
            class="carousel slide mb-5 shadow-lg"
            data-bs-ride="carousel"
            style="border-radius: 15px; overflow: hidden;">

            <div class="carousel-inner">
                <asp:Repeater ID="rptCarousel" runat="server">
                    <ItemTemplate>
                        <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                            <div style="height: 320px;
                                background-image: linear-gradient(rgba(0,0,0,0.45), rgba(0,0,0,0.45)), url('<%# Eval("Imagen") %>');
                                background-size: cover;
                                background-position: center;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                text-align: center;
                                color: white;">

                                <div>
                                    <span style="background-color: #c0392b;
                                        color: white;
                                        padding: 4px 14px;
                                        border-radius: 20px;
                                        font-size: 0.8rem;
                                        font-weight: 600;">
                                        Promoción
                                    </span>

                                    <h2 class="fw-bold mt-2 mb-1">
                                        <%# Eval("Nombre") %>
                                    </h2>

                                    <p style="font-size: 1.3rem;
                                        font-weight: 600;
                                        color: #fbbf24;">
                                        $<%# Eval("Total", "{0:N0}") %>
                                    </p>

                                   <a href='<%# "/Vista/Local/Tienda.aspx?idLocal=" + Eval("IdLocal") + "&idPlazoleta=" + Eval("IdPlazoleta") + "&idCC=" + Request.QueryString["idCC"] + "&idCategoria=99" %>'
                                    style="display: inline-block;
                                    margin-top: 12px;
                                    background-color: #F27F0D;
                                    color: white;
                                    padding: 10px 28px;
                                    border-radius: 25px;
                                    font-weight: 600;
                                    text-decoration: none;">
                                    Ver Promoción
                                    </a>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <button class="carousel-control-prev"
                type="button"
                data-bs-target="#carouselEatMall"
                data-bs-slide="prev">
                <span class="carousel-control-prev-icon"></span>
            </button>

            <button class="carousel-control-next"
                type="button"
                data-bs-target="#carouselEatMall"
                data-bs-slide="next">
                <span class="carousel-control-next-icon"></span>
            </button>
        </div>

        <!-- TITULO -->
        <h4 class="mb-3 mt-2 fw-bold">Locales</h4>

        <!-- LOCALES -->
        <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-4">
            <asp:Repeater ID="rptLocales" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="local-card">

                            <div class="calificacion">
                                ★ <%# string.Format("{0:0.0}", Convert.ToDouble(Eval("Promedio"))) %>
                            </div>

                            <div class="local-wrapper">
                                <asp:Image ID="imgLocal"
                                    runat="server"
                                    ImageUrl='<%# string.IsNullOrEmpty(Eval("Imagen").ToString()) ? "~/img/default-local.png" : Eval("Imagen").ToString() %>'
                                    CssClass="local-icono"
                                    AlternateText='<%# Eval("Nombre") %>' />
                                <%--muestra el estado punto rojo o verde--%>
                                <span class='estado-local <%# Eval("Estado").ToString().Trim() == "Abierto" ? "abierto" : "cerrado" %>'>
                                </span>
                            </div>

                            <p class="local-nombre">
                                <%# Eval("Nombre") %>
                            </p>
                            <%--si esta abierto local boton naranja si no deshabilita el boton--%>
                            <%# Eval("Estado").ToString().Trim() == "Abierto"
                                ? "<a href='/Vista/Local/Tienda.aspx?idLocal=" + Eval("Id") +
                                  "&idPlazoleta=" + Request.QueryString["idPlazoleta"] +
                                  "&idCC=" + Request.QueryString["idCC"] +
                                  "' class='btn-ver-menu'>Ver Menú</a>"
                                : "<button class='btn-cerrado' disabled>Cerrado</button>" %>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <asp:Label ID="lblMensaje"
            runat="server"
            CssClass="mt-3 d-block alert alert-warning"
            Visible="false" />

    </div>

</asp:Content>