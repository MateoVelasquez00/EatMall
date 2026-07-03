<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs"
    Inherits="EatMall.Vista.Usuario.GestionAdmin.GestionUsuarios"
    MasterPageFile="~/Vista/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />

    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
        }

        /* CONTENEDOR GENERAL */
        #gvUsuarios_wrapper {
            background: white;
            border: 1px solid #bccac0;
            border-radius: 16px;
            padding: 24px;
            box-shadow: 0 2px 12px rgba(0,0,0,.04);
        }

        /* CABECERA DATATABLE */
        .dataTables_wrapper .dataTables_length,
        .dataTables_wrapper .dataTables_filter {
            margin-bottom: 20px;
        }

            .dataTables_wrapper .dataTables_length label,
            .dataTables_wrapper .dataTables_filter label {
                color: #3d4a42;
                font-weight: 500;
            }

            /* SELECT REGISTROS */
            .dataTables_wrapper .dataTables_length select {
                border: 1px solid #bccac0;
                border-radius: 10px;
                padding: 6px 10px;
                background: white;
                margin: 0 6px;
            }

            /* BUSCADOR */
            .dataTables_wrapper .dataTables_filter input {
                border: 1px solid #bccac0;
                border-radius: 10px;
                padding: 10px 14px;
                min-width: 280px;
                background: white;
                transition: all .2s ease;
            }

                .dataTables_wrapper .dataTables_filter input:focus {
                    outline: none;
                    border-color: var(--color-rol);
                    box-shadow: 0 0 0 4px rgba(0,105,72,.12);
                }

        /* TABLA */
        #gvUsuarios {
            border: none !important;
            width: 100% !important;
        }

            #gvUsuarios thead th {
                background: var(--color-rol) !important;
                color: white !important;
                border: none !important;
                font-size: .85rem;
                font-weight: 600;
                letter-spacing: .5px;
                padding: 14px !important;
            }

            #gvUsuarios tbody td {
                padding: 14px !important;
                vertical-align: middle;
            }

            #gvUsuarios tbody tr {
                transition: all .15s ease;
            }

                #gvUsuarios tbody tr:hover {
                    background: #f5faf7 !important;
                }

        /* BORDES */
        table.dataTable.no-footer {
            border-bottom: 1px solid #e5e7eb;
        }

        /* INFO */
        .dataTables_wrapper .dataTables_info {
            color: #6b7280;
            font-size: .85rem;
            margin-top: 12px;
        }

        /* PAGINACIÓN */
        .dataTables_wrapper .dataTables_paginate {
            margin-top: 12px;
        }

        .dataTables_wrapper .paginate_button {
            border-radius: 8px !important;
            margin: 0 3px !important;
            border: 1px solid #d1d5db !important;
            background: white !important;
            color: #374151 !important;
        }

            .dataTables_wrapper .paginate_button.current,
            .dataTables_wrapper .paginate_button.current:hover {
                background: var(--color-rol) !important;
                color: white !important;
                border-color: var(--color-rol) !important;
            }

            .dataTables_wrapper .paginate_button:hover {
                background: var(--color-rol-light) !important;
                color: var(--color-rol) !important;
                border-color: var(--color-rol) !important;
            }

        /* PANEL ROLES */
        .panel-roles {
            border: 1px solid var(--color-rol);
            border-radius: 10px;
            padding: 14px;
            margin-top: 8px;
            background: #f8fffb;
        }

        /* BOTÓN ROL */
        .btn-rol-toggle {
            border: 1px solid var(--color-rol) !important;
            background: white !important;
            color: var(--color-rol) !important;
            border-radius: 8px !important;
            font-weight: 600;
            font-size: .8rem;
        }

            .btn-rol-toggle:hover {
                background: var(--color-rol-light) !important;
            }

        /* BOTÓN GUARDAR */
        .btn-guardar-rol {
            background: var(--color-rol) !important;
            color: white !important;
            border: none !important;
            border-radius: 8px !important;
        }

            .btn-guardar-rol:hover {
                opacity: .9;
            }

        /* BOTÓN NUEVO USUARIO */
        .btn-nuevo-usuario {
            background: var(--color-rol);
            color: white !important;
            border-radius: 10px;
            padding: 10px 18px;
            text-decoration: none;
            font-weight: 600;
            display: inline-flex;
            align-items: center;
            gap: 8px;
            transition: .2s;
        }

            .btn-nuevo-usuario:hover {
                transform: translateY(-2px);
                color: white !important;
            }

        /* CABECERA */
        .cabecera-usuarios {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 24px;
        }

        .btn-acciones {
            border: none;
            background: transparent;
            color: #6b7280;
            font-size: 1.2rem;
            border-radius: 8px;
            padding: 4px 8px;
            transition: .2s;
        }

            .btn-acciones:hover {
                background: var(--color-rol-light);
                color: var(--color-rol);
            }

        .dropdown-menu {
            border: none;
            border-radius: 12px;
            box-shadow: 0 8px 24px rgba(0,0,0,.12);
        }

        .dropdown-item {
            font-size: .9rem;
            padding: 10px 14px;
        }

        .badge-estado {
            padding: 6px 12px;
            border-radius: 999px;
            font-size: .75rem;
            font-weight: 600;
        }

        .badge-activo {
            background: #dcfce7;
            color: #166534;
        }

        .badge-inactivo {
            background: #fee2e2;
            color: #991b1b;
        }
    </style>

    <div class="cabecera-usuarios">
        <h4 class="page-titulo mb-0">
            <i class="bi bi-people me-2"></i>
            Gestión de Usuarios
        </h4>

        <a href="CrearUsuario.aspx" class="btn-nuevo-usuario">
            <i class="bi bi-plus-lg"></i>
            Nuevo Usuario
        </a>
    </div>

    <asp:GridView
        ID="gvUsuarios"
        runat="server"
        ClientIDMode="Static"
        AllowPaging="false"
        AllowSorting="false"
        DataKeyNames="Id"
        OnRowDataBound="gvUsuarios_RowDataBound"
        CssClass="table table-bordered table-striped"
        AutoGenerateColumns="false">

        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Documento" HeaderText="Documento" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <span class='<%# Convert.ToBoolean(Eval("Estado"))
                        ? "badge-estado badge-activo"
                        : "badge-estado badge-inactivo" %>'>
                        <%# Convert.ToBoolean(Eval("Estado"))
                        ? "Activo"
                        : "Inactivo" %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rol">
                <ItemTemplate>
                    <button type="button" class="btn btn-sm btn-rol-toggle"
                        onclick="toggleRoles(this)">
                        <i class="bi bi-shield me-1"></i><%# Eval("Rol.Nombre") %> ▾
                    </button>

                    <div class="panel-roles" style="display: none;">
                        <p style="font-size: 0.78rem; font-weight: 700; color: #FFA94D; text-transform: uppercase; letter-spacing: 1px; margin-bottom: 8px;">
                            Asignar roles
                        </p>
                        <asp:CheckBoxList ID="chkRoles" runat="server" />

                        <div class="mt-2 d-flex gap-2">
                            <asp:Button ID="btnGuardarRol" runat="server"
                                Text="Guardar"
                                CssClass="btn btn-sm btn-guardar-rol"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnGuardarRol_Click" />

                            <button type="button" class="btn btn-sm btn-outline-secondary"
                                style="border-radius: 6px; font-size: 0.8rem;"
                                onclick="cancelarRoles(this)">
                                Cancelar
                            </button>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>

                    <div class="dropdown">
                        <button type="button"
                            class="btn-acciones"
                            data-bs-toggle="dropdown">

                            <i class="bi bi-three-dots-vertical"></i>
                        </button>

                        <ul class="dropdown-menu dropdown-menu-end">

                            <li>
                                <a class="dropdown-item"
                                    href='CrearUsuario.aspx?id=<%# Eval("Id") %>'>
                                    <i class="bi bi-pencil-square me-2"></i>
                                    Editar
                                </a>
                            </li>

                            <li>
                                <button type="button"
                                    class='dropdown-item <%# Convert.ToBoolean(Eval("Estado")) ? "text-danger" : "text-success" %>'
                                    onclick='cambiarEstado(<%# Eval("Id") %>, <%# Convert.ToBoolean(Eval("Estado")).ToString().ToLower() %>)'>

                                    <i class='bi <%# Convert.ToBoolean(Eval("Estado")) ? "bi-person-x" : "bi-person-check" %> me-2'></i>

                                    <%# Convert.ToBoolean(Eval("Estado"))
                                        ? "Desactivar"
                                        : "Activar" %>
                                </button>
                            </li>

                        </ul>

                    </div>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>

        function toggleRoles(btn) {
            var panel = $(btn).closest('td').find('.panel-roles');
            panel.toggle();
        }

        function cancelarRoles(btn) {
            $(btn).closest('.panel-roles').hide();
        }

        function cambiarEstado(idUsuario, estadoActual) {

            let accion = estadoActual ? "desactivar" : "activar";

            Swal.fire({
                title: '¿Deseas ' + accion + ' este usuario?',
                text: estadoActual
                    ? 'El usuario no podrá iniciar sesión.'
                    : 'El usuario podrá iniciar sesión nuevamente.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí',
                cancelButtonText: 'Cancelar'
            }).then((result) => {

                if (result.isConfirmed) {

                    window.location =
                        'GestionUsuarios.aspx?estado=' +
                        idUsuario +
                        '&valor=' +
                        (!estadoActual);
                }
            });
        }

        $(document).ready(function () {

            $('#gvUsuarios tr:first').wrap('<thead></thead>');

            if ($.fn.dataTable.isDataTable('#gvUsuarios')) {
                $('#gvUsuarios').DataTable().destroy();
            }

            $('#gvUsuarios').DataTable({
                language: {
                    search: "",
                    searchPlaceholder: "Buscar usuario...",
                    lengthMenu: "Mostrar _MENU_ registros",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ usuarios",
                    infoEmpty: "Mostrando 0 a 0 de 0 usuarios",
                    infoFiltered: "(filtrado de _MAX_ usuarios en total)",
                    zeroRecords: "No se encontraron resultados",
                    paginate: {
                        first: "Primero",
                        last: "Ultimo",
                        next: "Siguiente",
                        previous: "Anterior"
                    }
                }
            });

        });

    </script>

</asp:Content>
