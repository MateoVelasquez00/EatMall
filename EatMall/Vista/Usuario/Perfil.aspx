<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="EatMall.Vista.Usuario.Perfil" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .perfil-card {
            border-radius: 20px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
            max-width: 600px;
            margin: auto;
        }

        .avatar {
            width: 90px;
            height: 90px;
            border-radius: 50%;
            background-color: #ffc107;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 2.5rem;
            color: white;
            margin: auto;
        }

        .pedido-card {
            border-radius: 12px;
            border: 1px solid #dee2e6;
            transition: all 0.2s;
        }

            .pedido-card:hover {
                box-shadow: 0 4px 15px rgba(0,0,0,0.1);
                transform: translateY(-2px);
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 mb-5">

        <!-- CARD PERFIL -->
        <div class="card perfil-card p-4 mb-5">

            <!-- Avatar -->
            <div class="text-center mb-4">
                <div class="avatar mb-3">
                    <i class="bi bi-person-fill"></i>
                </div>
                <h4 class="fw-bold">
                    <asp:Label ID="lblNombreCompleto" runat="server" />
                </h4>
                <p class="text-muted">
                    <asp:Label ID="lblEmail" runat="server" />
                </p>
            </div>

            <hr />

            <!-- Formulario -->
            <div class="mb-3">
                <label class="form-label fw-semibold">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label fw-semibold">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label fw-semibold">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label fw-semibold">Nueva Contraseña</label>
                <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" />
                <small class="text-muted">Deja en blanco si no deseas cambiarla</small>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-success fw-semibold" />

            <div class="d-grid mt-4">
                <asp:Button ID="btnGuardar" runat="server"
                    Text="Guardar Cambios"
                    CssClass="btn btn-warning fw-bold"
                    OnClick="btnGuardar_Click" />
            </div>

        </div>

        <!-- CARD PEDIDOS -->
        <div class="card perfil-card p-4">
            <h5 class="fw-bold mb-4">
                <i class="bi bi-bag-check me-2"></i>Mis Pedidos
            </h5>

            <asp:Repeater ID="rptPedidos" runat="server">
                <ItemTemplate>
                    <div class="pedido-card p-3 mb-3">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <h6 class="fw-bold mb-1">
                                    <i class="bi bi-receipt me-2"></i>
                                    <%# Eval("CodigoPedido") %>
                                </h6>
                                <p class="text-muted mb-1 small">
                                    <i class="bi bi-calendar me-1"></i>
                                    <%# Convert.ToDateTime(Eval("FechaPedido")).ToString("dd/MM/yyyy hh:mm tt") %>
                                </p>
                                <p class="text-muted mb-0 small">
                                    <i class="bi bi-truck me-1"></i>
                                    <%# Eval("TipoEntrega") %>
                                </p>
                            </div>
                            <div class="text-end">
                                <span class='badge <%# Eval("Estado").ToString() == "Completado" ? "bg-success" : Eval("Estado").ToString() == "Cancelado" ? "bg-danger" : "bg-warning text-dark" %>'>
                                    <%# Eval("Estado") %>
                                </span>
                                <p class="fw-bold mt-2 text-primary mb-0">
                                    $<%# string.Format("{0:N0}", Eval("Total")) %>
                                </p>
                            </div>
                        </div>
                        <div class="text-end mt-2">
                            <a href='<%# "/Vista/Usuario/DetallesPedidos.aspx?id=" + Eval("Id") %>'
                                class="btn btn-sm btn-outline-primary">
                                <i class="bi bi-eye me-1"></i>Ver detalle
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Label ID="lblSinPedidos" runat="server"
                Text="Aún no tienes pedidos realizados."
                CssClass="text-muted"
                Visible="false" />
        </div>

    </div>
</asp:Content>
