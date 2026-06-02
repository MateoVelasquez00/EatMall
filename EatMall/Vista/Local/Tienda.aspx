<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tienda.aspx.cs"
    Inherits="EatMall.Vista.Local.Tienda" MasterPageFile="~/Site.Master"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        body {
            padding-top: 0;
            background-color: #f5f5f5;
        }

        /* BOTÓN TOGGLE FIJO */
        .btn-toggle-sidebar {
            position: fixed;
            top: 76px;
            left: 0;
            z-index: 200;
            background: white;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            width: 36px;
            height: 36px;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

            .btn-toggle-sidebar:hover {
                background: #f8f9fa;
            }

        /* SIDEBAR */
        .sidebar-local {
            position: fixed;
            top: 72px;
            left: 0;
            width: 320px;
            height: calc(100vh - 72px);
            background: white;
            padding: 20px;
            overflow-y: auto;
            box-shadow: 2px 0 10px rgba(0,0,0,0.1);
            z-index: 100;
        }

        .local-img {
            width: 100%;
            height: 220px;
            object-fit: cover;
            border-radius: 12px;
        }

        /* PRODUCTOS - con sidebar abierto */
        .productos-container {
            margin-left: 340px;
            padding: 20px;
            padding-right: 90px; /* espacio para el carrito flotante */
            padding-bottom: 100px;
            transition: margin-left 0.3s ease;
        }

            /* PRODUCTOS - con sidebar cerrado (4 columnas) */
            .productos-container.sidebar-cerrado {
                margin-left: 20px;
            }

        .producto-card {
            background: white;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
            overflow: hidden;
            height: 100%;
        }

        .producto-img {
            width: 100%;
            height: 180px;
            object-fit: cover;
        }

        .producto-body {
            padding: 15px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- BOTÓN TOGGLE FIJO -->
    <button type="button" class="btn-toggle-sidebar" id="btnToggle"
        onclick="toggleSidebar()" title="Mostrar/ocultar info del local">
        <i id="iconoSidebar" class="bi bi-layout-sidebar"></i>
    </button>

    <!-- SIDEBAR -->
    <div id="sidebarLocal" class="sidebar-local">

        <asp:Image ID="imgLocalInfo"
            runat="server"
            CssClass="local-img mb-3" />

        <h4 class="fw-bold">
            <asp:Label ID="lblNombreLocal" runat="server" />
        </h4>

        <p class="text-muted">
            <asp:Label ID="lblDescripcionLocal" runat="server" />
        </p>

        <hr />

        <p>
            ⭐ <strong>Calificación:</strong>
            <asp:Label ID="lblCalificacion" runat="server" />
        </p>

        <p>
            📞 <strong>Teléfono:</strong><br />
            <asp:Label ID="lblTelefono" runat="server" />
        </p>

        <p>
            ✉ <strong>Email:</strong><br />
            <asp:Label ID="lblEmail" runat="server" />
        </p>

        <p>
            🕒 <strong>Horario:</strong><br />
            <asp:Label ID="lblHorario" runat="server" />
        </p>

        <p>
            📌 <strong>Estado:</strong>
            <asp:Label ID="lblEstado" runat="server" />
        </p>

    </div>

    <!-- CATEGORÍAS -->
<div class="d-flex gap-2 flex-wrap mb-3" id="barraCategorias">
    <a href='?idLocal=<%= Request.QueryString["idLocal"] %>&idPlazoleta=<%= Request.QueryString["idPlazoleta"] %>&idCC=<%= Request.QueryString["idCC"] %>'
        class='<%= string.IsNullOrEmpty(Request.QueryString["idCategoria"]) ? "btn btn-dark rounded-pill px-4" : "btn btn-outline-dark rounded-pill px-4" %>'>
        Todos
    </a>
    <asp:Repeater ID="rptCategorias" runat="server">
        <ItemTemplate>
            <a href='<%# "?idLocal=" + Request.QueryString["idLocal"] + "&idPlazoleta=" + Request.QueryString["idPlazoleta"] + "&idCC=" + Request.QueryString["idCC"] + "&idCategoria=" + Eval("Id") %>'
                class='<%# Request.QueryString["idCategoria"] == Eval("Id").ToString() ? "btn btn-dark rounded-pill px-4" : "btn btn-outline-dark rounded-pill px-4" %>'>
                <%# Eval("Nombre") %>
            </a>
        </ItemTemplate>
    </asp:Repeater>
    <a href='?idLocal=<%= Request.QueryString["idLocal"] %>&idPlazoleta=<%= Request.QueryString["idPlazoleta"] %>&idCC=<%= Request.QueryString["idCC"] %>&idCategoria=99'
        class='<%= Request.QueryString["idCategoria"] == "99" ? "btn btn-dark rounded-pill px-4" : "btn btn-outline-dark rounded-pill px-4" %>'>
        Promociones
    </a>
</div>

    <!-- PRODUCTOS -->
    <div id="productosContainer" class="productos-container">

        <div class="d-flex align-items-center gap-2 mb-4">
            <asp:HyperLink ID="btnVolverLocal"
                runat="server"
                Text="← Volver"
                CssClass="btn btn-outline-dark btn-sm" />
            <h3 class="fw-bold mb-0">Productos</h3>
        </div>

        <div class="row" id="rowProductos">
            <asp:Repeater ID="rptProductos" runat="server"
                OnItemCommand="rptProductos_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-3 col-producto mb-4">
                        <div class="producto-card">
                            <img src='<%# Eval("Imagen").ToString().StartsWith("http") 
                                ? Eval("Imagen").ToString() 
                                : ResolveUrl("~/" + Eval("Imagen").ToString()) %>'
                                alt='<%# Eval("Nombre") %>'
                                class="producto-img"
                                onerror="this.style.display='none'" />
                            <div class="producto-body">
                                <h6 class="fw-bold"><%# Eval("Nombre") %></h6>
                                <small class="text-muted d-block mb-2"><%# Eval("Descripcion") %></small>
                                <strong class="text-success">$<%# Eval("Precio", "{0:N2}") %></strong>
                                <div class="d-flex align-items-center mt-3 gap-2">
                                    <asp:TextBox ID="txtCantidad" runat="server" Text="1"
                                        CssClass="form-control form-control-sm"
                                        Style="width: 60px" TextMode="Number" min="1" />
                                    <asp:Button runat="server" Text="Agregar"
                                        CssClass="btn btn-primary btn-sm"
                                        Style="background-color: #FFA94D; color: white; border: none;"
                                        CommandName="AgregarCarrito"
                                        CommandArgument='<%# Eval("Id") %>' />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </div>

    <script>
        var sidebarVisible = true;
        /*recordar el estado del sidebar visible o no */
        window.onload = function () {
            var estado = sessionStorage.getItem('sidebarVisible');

            if (estado === 'false') {
                document.getElementById('sidebarLocal').style.display = 'none';
                document.getElementById('productosContainer').classList.add('sidebar-cerrado');
                document.getElementById('barraCategorias').style.marginLeft = '20px';
                document.getElementById('iconoSidebar').className = 'bi bi-layout-sidebar-inset';
                document.querySelectorAll('.col-producto').forEach(function (col) {
                    col.className = 'col-md-3 col-producto mb-4';
                });
                sidebarVisible = false;
            } else {
                sessionStorage.setItem('sidebarVisible', 'true');
                document.getElementById('barraCategorias').style.marginLeft = '340px';
                document.querySelectorAll('.col-producto').forEach(function (col) {
                    col.className = 'col-md-4 col-producto mb-4';

                });
                sidebarVisible = true;
            }
        };
        /*visibilidad del sidebar, cuando el sidebar este oculto caben mas productos en la fila*/
        function toggleSidebar() {
            var sidebar = document.getElementById('sidebarLocal');
            var container = document.getElementById('productosContainer');
            var barra = document.getElementById('barraCategorias');
            var icono = document.getElementById('iconoSidebar');
            var cols = document.querySelectorAll('.col-producto');

            if (sidebarVisible) {
                sidebar.style.display = 'none';
                container.classList.add('sidebar-cerrado');
                barra.style.marginLeft = '20px';
                icono.className = 'bi bi-layout-sidebar-inset';
                cols.forEach(function (col) {
                    col.className = 'col-md-3 col-producto mb-4';
                });
                sessionStorage.setItem('sidebarVisible', 'false');
            } else {
                sidebar.style.display = 'block';
                container.classList.remove('sidebar-cerrado');
                barra.style.marginLeft = '340px';
                icono.className = 'bi bi-layout-sidebar';
                cols.forEach(function (col) {
                    col.className = 'col-md-4 col-producto mb-4';
                });
                sessionStorage.setItem('sidebarVisible', 'true');
            }
            sidebarVisible = !sidebarVisible;
        }
    </script>

</asp:Content>
