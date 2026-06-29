<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs"
    Inherits="EatMall.Vista.Usuario.GestionAdmin.CrearUsuario"
    MasterPageFile="~/Vista/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
        }

        .card-form {
            background: white;
            border: 1px solid #bccac0;
            border-radius: 16px;
            padding: 32px;
            box-shadow: 0 2px 12px rgba(0,0,0,.04);
        }

        .form-label {
            font-weight: 600;
            color: #374151;
            font-size: .875rem;
            margin-bottom: 6px;
        }

        .form-control {
            border: 1px solid #d1d5db;
            border-radius: 10px;
            padding: 12px 14px;
            min-height: 48px;
            transition: all .2s ease;
        }

            .form-control:focus {
                border-color: var(--color-rol);
                box-shadow: 0 0 0 4px rgba(0,105,72,.12);
                outline: none;
            }

        .seccion-titulo {
            font-size: .78rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            color: var(--color-rol);
            margin: 32px 0 16px;
        }

        .btn-roles-toggle {
            width: 100%;
            text-align: left;
            background: white;
            border: 1px solid var(--color-rol);
            color: var(--color-rol);
            border-radius: 10px;
            padding: 12px 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all .2s ease;
        }

            .btn-roles-toggle:hover {
                background: var(--color-rol-light);
            }

        .panel-roles {
            border: 1px solid var(--color-rol);
            border-radius: 10px;
            padding: 16px;
            margin-top: 8px;
            background: #f8fffb;
            display: none;
        }

        .divider {
            border: none;
            border-top: 1px solid #e5e7eb;
            margin: 32px 0;
        }

        .btn-crear {
            background: var(--color-rol);
            color: white;
            border: none;
            border-radius: 10px;
            padding: 12px 26px;
            font-weight: 600;
            transition: all .2s ease;
        }

            .btn-crear:hover {
                opacity: .92;
                transform: translateY(-1px);
            }

        .btn-cancelar {
            border: 1px solid #d1d5db;
            border-radius: 10px;
            padding: 12px 26px;
            background: white;
            color: #374151;
            text-decoration: none;
            transition: all .2s ease;
        }

            .btn-cancelar:hover {
                border-color: var(--color-rol);
                color: var(--color-rol);
            }

        .acciones-formulario {
            display: flex;
            gap: 12px;
            margin-top: 8px;
        }

        .checklist-roles input[type=checkbox] {
            margin-right: 8px;
        }

        .checklist-roles label {
            color: #374151;
            font-size: .9rem;
            margin-bottom: 8px;
        }
    </style>

    <h4 class="page-titulo" id="lblTituloPagina" runat="server">
        <span class="material-symbols-outlined">person_add</span>
        Crear Usuario
    </h4>

    <p class="seccion-titulo">Información personal</p>

    <div class="card-form">
        <div class="row g-3">
            <div class="col-md-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Juan" />
            </div>
            <div class="col-md-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej. Pérez" />
            </div>
            <div class="col-md-3">
                <label class="form-label">Documento</label>
                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Número de documento" />
            </div>
            <div class="col-md-3">
                <label class="form-label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej. 3001234567" />
            </div>
        </div>

        <p class="seccion-titulo">Acceso al sistema</p>

        <div class="row g-3">
            <div class="col-md-6">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@ejemplo.com" />
            </div>
            <div class="col-md-6">
                <label class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="••••••••" />
            </div>
        </div>

        <p class="seccion-titulo">Roles</p>

        <div class="row">
            <div class="col-md-4">
                <button type="button" class="btn-roles-toggle" onclick="toggleRoles(this)">
                    <i class="bi bi-shield-check me-2"></i>
                    Seleccionar roles ▾
                </button>

                <div id="panelRoles" class="panel-roles">
                    <asp:CheckBoxList ID="chkRoles" runat="server" CssClass="checklist-roles" />
                </div>
            </div>
        </div>

        <hr class="divider" />

        <div class="acciones-formulario">
            <asp:Button ID="btnCrear" runat="server"
                Text="Crear Usuario"
                CssClass="btn-crear"
                OnClick="btnCrear_Click" />

            <asp:HyperLink ID="btnVolver" runat="server"
                Text="Cancelar"
                CssClass="btn-cancelar"
                NavigateUrl="~/Vista/Usuario/GestionAdmin/GestionUsuarios.aspx" />
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function toggleRoles(btn) {

            const panel = document.getElementById('panelRoles');

            if (panel.style.display === 'block') {

                panel.style.display = 'none';

                btn.innerHTML =
                    '<i class="bi bi-shield-check me-2"></i>Seleccionar roles ▾';

            } else {

                panel.style.display = 'block';

                btn.innerHTML =
                    '<i class="bi bi-shield-check me-2"></i>Ocultar roles ▴';
            }
        }
    </script>

</asp:Content>
