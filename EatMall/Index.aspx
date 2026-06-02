<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EatMall.Index" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero-image {
            height: 60vh;
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            position: relative;
            width: 100%;
            border-radius: 12px;
        }

        .hero-text {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            color: white;
            text-align: center;
            width: 100%;
        }

        .card {
            border-radius: 12px;
            overflow: hidden;
            width: 100%;
        }

            .card img {
                border-radius: 10px;
            }

        .card-body h5 {
            font-size: 16px;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">

        <!-- CAROUSEL -->
        <div id="carouselEatMall"
            class="carousel slide mb-5 shadow-lg"
            data-bs-ride="carousel"
            style="border-radius: 15px; overflow: hidden;">

            <div class="carousel-inner">
                <asp:Repeater ID="rptCarousel" runat="server">
                    <ItemTemplate>
                        <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">

                            <div class="hero-image"
                                style="background-image: linear-gradient(rgba(0,0,0,0.6), rgba(0,0,0,0.6)), url('<%# Eval("Imagen") %>');">

                                <div class="hero-text">
                                    <h1 class="display-3 fw-bold">
                                        <%# Eval("Nombre") %>
                                    </h1>

                                    <p class="fs-4">
                                        <%# Eval("Ciudad.NombreCiudad") %> -
                                        <%# Eval("Ubicacion") %>
                                    </p>

                                    <a href='Vista/Plazoleta/Plazoleta.aspx?id=<%# Eval("Id") %>'
                                        class="btn btn-lg px-5 py-3 rounded-pill fw-bold"
                                        style="background-color: #F27F0D; color: white; border: none;">Explorar
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
        <h4 class="mb-3 mt-2"
            style="font-weight: 700; color: #1a1a1a;">Centros Comerciales
        </h4>

        <!-- LISTADO -->
        <div class="row">
            <asp:Repeater ID="rptCentrosComerciales" runat="server">
                <ItemTemplate>

                    <div class="col-lg-3 col-md-3 mb-4">

                        <div class="card shadow-sm p-3 h-100 position-relative">

                            <img src='<%# Eval("Imagen") %>'
                                class="card-img-top"
                                style="height: 150px; object-fit: cover;"
                                onerror="this.src='Vista/Assets/Img/CCDefault.png'" />

                            <div class="card-body text-start">

                                <h5 class="fw-bold">
                                    <%# Eval("Nombre") %>
                                </h5>

                                <p class="text-muted mb-1">
                                    <i class="bi bi-geo-alt-fill"></i>
                                    <%# Eval("Ciudad.NombreCiudad") %> -
                                    <%# Eval("Ciudad.Departamento.Nombre") %>
                                </p>

                                <p class="small text-secondary">
                                    <%# Eval("Ubicacion") %>
                                </p>

                                <a href='Vista/Plazoleta/Plazoleta.aspx?id=<%# Eval("Id") %>'
                                    class="btn w-100 mt-2 fw-bold"
                                    style="background-color: #F27F0D; color: white; border: none;">Ver Detalles
                                </a>

                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
        </div>

    </div>

</asp:Content>
