<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetallesPedidos.aspx.cs"
    Inherits="EatMall.Vista.Usuario.DetallesPedidos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Detalle del Pedido</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" />
    <style>
        .btn-naranja {
            background-color: #ffc107;
            color: black;
        }

            .btn-naranja:hover {
                background-color: #ffc107;
                color: black;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container py-4">
            <div class="card p-4">
                <h5 class="fw-bold mb-4">
                    <i class="bi bi-receipt me-2"></i>Detalle del Pedido
                </h5>

                <div class="accordion" id="accordionDetalles">
                    <asp:Repeater ID="rptDetalles" runat="server">
                        <ItemTemplate>
                            <%-- CABECERA --%>
                            <h2 class="accordion-header">
                                <button class="accordion-button collapsed py-3" type="button"
                                    data-bs-toggle="collapse"
                                    data-bs-target='<%# "#collapse-" + Eval("Id") %>'>
                                    <div class="d-flex align-items-center gap-3 w-100">
                                        <img src='<%# Eval("ImagenProducto") %>' width="80" height="80"
                                            class="rounded" style="object-fit: cover; flex-shrink: 0;"
                                            onerror="this.src='/img/placeholder.png'" />
                                        <div class="flex-grow-1">
                                            <p class="fw-bold mb-1"><%# Eval("NombreProducto") %></p>

                                            <p class="text-muted small mb-1">
                                                <i class="bi bi-award me-1"></i><%# Eval("Descripcion") %>
                                            </p>
                                            <p class="fw-bold text-primary mb-0">
                                                $<%# string.Format("{0:N0}", Eval("Subtotal")) %>
                                            </p>
                                        </div>
                                        <span class="badge bg-secondary me-3">x<%# Eval("Cantidad") %></span>
                                    </div>
                                </button>
                            </h2>

                            <%-- CONTENIDO EXPANDIBLE --%>
                            <div id='<%# "collapse-" + Eval("Id") %>'
                                class="accordion-collapse collapse"
                                data-bs-parent="#accordionDetalles">
                                <div class="accordion-body pt-0 ps-4">
                                    <hr class="mt-0" />
                                    <p class="text-muted small mb-1">
                                        <i class="bi bi-tag me-1"></i>Precio unitario: 
                                          <span class="fw-bold text-dark">$<%# string.Format("{0:N0}", Eval("PrecioProducto")) %></span>
                                    </p>
                                    <p class="text-muted small mb-1">
                                        <i class="bi bi-currency-dollar me-1"></i>Subtotal: 
                                          <span class="fw-bold text-dark">$<%# string.Format("{0:N0}", Eval("Subtotal")) %></span>
                                    </p>
                                    <p class="text-muted small mb-1">
                                        <i class="bi bi-hash me-1"></i>Cantidad: 
                                          <span class="fw-bold text-dark"><%# Eval("Cantidad") %></span>
                                    </p>
                                    <p class="text-muted small mb-1">
                                        <i class="bi bi-shop me-1"></i>Local:
                                           <span class="fw-bold text-dark"><%# Eval("NombreLocal") %></span>
                                    </p>
                                    <p class="text-muted small mb-0">
                                        <i class="bi bi-building me-1"></i>Centro Comercial: 
                                           <span class="fw-bold text-dark"><%# Eval("NombreCC") %></span>
                                    </p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <asp:Label ID="lblSinDetalles" runat="server"
                    Text="No se encontraron detalles para este pedido."
                    CssClass="text-muted" Visible="false" />

                <a href="Perfil.aspx" class="btn btn-naranja mt-3">
                    <i class="bi bi-arrow-left me-1"></i>Volver
                </a>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
