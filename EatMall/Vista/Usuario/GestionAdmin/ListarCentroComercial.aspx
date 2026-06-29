<%@ Page Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="ListarCentroComercial.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionAdmin.ListarCentroComercial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />

    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
        }

        /* CONTENEDOR GENERAL */
        #gvCentroComercial_wrapper {
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
        #gvCentroComercial {
            border: none !important;
            width: 100% !important;
        }

            #gvCentroComercial thead th {
                background: var(--color-rol) !important;
                color: white !important;
                border: none !important;
                font-size: .85rem;
                font-weight: 600;
                letter-spacing: .5px;
                padding: 14px !important;
            }

            #gvCentroComercial tbody td {
                padding: 14px !important;
                vertical-align: middle;
            }

            #gvCentroComercial tbody tr {
                transition: all .15s ease;
            }

                #gvCentroComercial tbody tr:hover {
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

        /* BOTÓN NUEVO USUARIO */
        .btn-nuevo-centroComercial {
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

            .btn-nuevo-centroComercial:hover {
                transform: translateY(-2px);
                color: white !important;
            }

        /* CABECERA */
        .cabecera-centrosComerciales {
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

        .img-cc {
            width: 80px;
            height: 60px;
            object-fit: cover;
            border-radius: 8px;
            border: 1px solid #e5e7eb;
        }

        .badge-activo {
            background: #dcfce7;
            color: #166534;
        }

        .badge-inactivo {
            background: #fee2e2;
            color: #991b1b;
        }

        .img-local {
            width: 48px;
            height: 48px;
            object-fit: cover;
            border-radius: 10px;
            border: 1px solid #e5e7eb;
        }
    </style>

    <div class="cabecera-centrosComerciales">
        <h4 class="page-titulo mb-0">
            <span class="material-symbols-outlined">store</span>
            Gestión de Centros Comerciales
        </h4>

        <a href="CrearCentroComercial.aspx" class="btn-nuevo-centroComercial">
            <i class="bi bi-plus-lg"></i>
            Nuevo Centro Comercial
        </a>
    </div>

    <asp:GridView
        ID="gvCentroComercial"
        runat="server"
        ClientIDMode="Static"
        AllowPaging="false"
        AllowSorting="false"
        DataKeyNames="Id"
        CssClass="table table-bordered table-striped"
        AutoGenerateColumns="false">

        <Columns>
            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <img src='<%# Eval("Imagen") %>'
                        class="img-cc"
                        onerror="this.src='https://via.placeholder.com/80x60?text=CC'" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Ciudad.NombreCiudad" HeaderText="Ciudad" />
            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Administrador" HeaderText="Administrador" />
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
                                    href='CrearCentroComercial.aspx?id=<%# Eval("Id") %>'>
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
        function cambiarEstado(idCentroComercial, estadoActual) {

            let accion = estadoActual ? "desactivar" : "activar";

            Swal.fire({
                title: '¿Deseas ' + accion + ' este centro comercial?',
                text: estadoActual
                    ? 'El centro comercial no podrá ser visitado.'
                    : 'El centro comercial podrá ser visitado nuevamente.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí',
                cancelButtonText: 'Cancelar'
            }).then((result) => {

                if (result.isConfirmed) {

                    window.location =
                        'ListarCentroComercial.aspx?estado=' +
                        idCentroComercial +
                        '&valor=' +
                        (!estadoActual);
                }
            });
        }

        $(document).ready(function () {

            $('#gvCentroComercial tr:first').wrap('<thead></thead>');

            if ($.fn.dataTable.isDataTable('#gvCentroComercial')) {
                $('#gvCentroComercial').DataTable().destroy();
            }

            $('#gvCentroComercial').DataTable({
                language: {
                    search: "",
                    searchPlaceholder: "Buscar centro comercial...",
                    lengthMenu: "Mostrar _MENU_ registros",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ centros comerciales",
                    infoEmpty: "Mostrando 0 a 0 de 0 centros comerciales",
                    infoFiltered: "(filtrado de _MAX_ centros comerciales en total)",
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

