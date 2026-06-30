<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="PerfilAdmin.aspx.cs" Inherits="EatMall.Vista.Usuario.PerfilAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <style>
        .page-titulo {
            font-size: 26px;
            font-weight: 700;
            color: var(--color-rol);
            margin-bottom: 28px;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .seccion-titulo {
            font-size: 0.72rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1.2px;
            color: var(--color-rol);
            margin: 0 0 14px;
        }

        .perfil-card {
            background: white;
            border: 1px solid var(--color-rol-light);
            border-radius: 16px;
            padding: 32px;
            width: 100%;
        }

        .avatar-perfil {
            width: 72px;
            height: 72px;
            border-radius: 50%;
            background: var(--color-rol);
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-size: 1.8rem;
            flex-shrink: 0;
        }

        .form-control {
            border: 1.5px solid #dee2e6;
            border-radius: 8px;
            padding: 10px 14px;
            transition: border-color 0.2s ease, box-shadow 0.2s ease;
        }

            .form-control:focus {
                border-color: var(--color-rol);
                box-shadow: none;
                outline: none;
            }

        .form-label {
            font-weight: 600;
            color: #444;
            font-size: 0.875rem;
            margin-bottom: 4px;
        }

        .divider {
            border: none;
            border-top: 1px solid #bccac0;
            margin: 24px 0;
        }

        .btn-guardar {
            background: var(--color-rol) !important;
            color: white !important;
            border: none !important;
            border-radius: 8px;
            padding: 11px 32px;
            font-weight: 600;
            font-size: 1rem;
            transition: opacity 0.2s ease, transform 0.1s ease;
            cursor: pointer;
        }

            .btn-guardar:hover {
                background: var(--color-rol) !important;
                color: white !important;
                opacity: .9;
                transform: translateY(-1px);
            }
    </style>

    <h4 class="page-titulo">
        <span class="material-symbols-outlined" style="font-size: 28px;">person</span>
        Mi Perfil
    </h4>

    <div class="perfil-card">

        <%-- Avatar + nombre + email --%>
        <div class="d-flex align-items-center gap-3 mb-4">
            <div class="avatar-perfil">
                <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">person</span>
            </div>
            <div>
                <h5 class="fw-bold mb-0" style="font-size: 16px;">
                    <asp:Label ID="lblNombreCompleto" runat="server" />
                </h5>
                <p class="mb-0" style="font-size: 13px; color: #3d4a42;">
                    <asp:Label ID="lblEmail" runat="server" />
                </p>
            </div>
        </div>

        <hr class="divider" />

        <p class="seccion-titulo">Información personal</p>

        <div class="row g-3 mb-3">
            <div class="col-md-6">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Juan" />
            </div>
            <div class="col-md-6">
                <label class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej. Pérez" />
            </div>
        </div>

        <div class="mb-3">
            <label class="form-label">Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej. 3001234567" />
        </div>

        <hr class="divider" />

        <p class="seccion-titulo">Seguridad</p>

        <div class="mb-4">
            <label class="form-label">Nueva Contraseña</label>
            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" placeholder="••••••••" />
            <small style="color: #3d4a42; font-size: 12px;">Deja en blanco si no deseas cambiarla</small>
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3 fw-semibold" />

        <asp:Button ID="btnGuardar" runat="server"
            Text="Guardar Cambios"
            CssClass="btn-guardar"
            OnClick="btnGuardar_Click" />

    </div>

</asp:Content>
