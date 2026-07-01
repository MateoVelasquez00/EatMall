<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="Locales.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionAdminCC.Locales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />
    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
        }

        #gvLocales_wrapper {
            background: white;
            border: 1px solid #bccac0;
            border-radius: 16px;
            padding: 24px;
            box-shadow: 0 2px 12px rgba(0,0,0,.04);
        }

        .dataTables_wrapper .dataTables_length,
        .dataTables_wrapper .dataTables_filter {
            margin-bottom: 20px;
        }

            .dataTables_wrapper .dataTables_length label,
            .dataTables_wrapper .dataTables_filter label {
                color: #3d4a42;
                font-weight: 500;
            }

            .dataTables_wrapper .dataTables_length select {
                border: 1px solid #bccac0;
                border-radius: 10px;
                padding: 6px 10px;
                background: white;
                margin: 0 6px;
            }

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

        #gvLocales {
            border: none !important;
            width: 100% !important;
        }

            #gvLocales thead th {
                background: var(--color-rol) !important;
                color: white !important;
                border: none !important;
                font-size: .85rem;
                font-weight: 600;
                letter-spacing: .5px;
                padding: 14px !important;
            }

            #gvLocales tbody td {
                padding: 14px !important;
                vertical-align: middle;
            }

            #gvLocales tbody tr {
                transition: all .15s ease;
            }

                #gvLocales tbody tr:hover {
                    background: #f5faf7 !important;
                }

        table.dataTable.no-footer {
            border-bottom: 1px solid #e5e7eb;
        }

        .dataTables_wrapper .dataTables_info {
            color: #6b7280;
            font-size: .85rem;
            margin-top: 12px;
        }

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

        .badge-estado {
            padding: 6px 12px;
            border-radius: 999px;
            font-size: .75rem;
            font-weight: 600;
        }

        .badge-abierto {
            background: #dcfce7;
            color: #166534;
        }

        .badge-cerrado {
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

        .cabecera-locales {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 24px;
        }

        .btn-nuevo-local {
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

            .btn-nuevo-local:hover {
                transform: translateY(-2px);
                color: white !important;
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="cabecera-locales">
        <h4 class="page-titulo mb-0">
            Gestión de Locales
        </h4>
        <a href="CrearLocales.aspx" class="btn-nuevo-local">
            <i class="bi bi-plus-lg"></i>
            Nuevo Local
        </a>
    </div>

    <asp:GridView
        ID="gvLocales"
        runat="server"
        ClientIDMode="Static"
        AllowPaging="false"
        AllowSorting="false"
        DataKeyNames="Id"
        OnRowDataBound="gvLocales_RowDataBound"
        CssClass="table table-bordered table-striped"
        AutoGenerateColumns="false">
        <columns>
            <asp:TemplateField HeaderText="Imagen">
                <itemtemplate>
                    <img src='<%# Eval("Imagen") %>'
                        alt="Local"
                        class="img-local"
                        onerror="this.src='https://via.placeholder.com/48x48?text=Local'" />
                </itemtemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="NumeroLocal" HeaderText="N° Local" />
            <asp:TemplateField HeaderText="Estado">
                <itemtemplate>
                    <span class='<%# Eval("Estado").ToString() == "Abierto"
                        ? "badge-estado badge-abierto"
                        : "badge-estado badge-cerrado" %>'>
                        <%# Eval("Estado") %>
                    </span>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acciones">
                <itemtemplate>
                    <div class="dropdown">
                        <button type="button"
                            class="btn-acciones"
                            data-bs-toggle="dropdown">
                            <i class="bi bi-three-dots-vertical"></i>
                        </button>
                        <ul class="dropdown-menu dropdown-menu-end">
                            <li>
                                <a class="dropdown-item"
                                    href='CrearLocales.aspx?id=<%# Eval("Id") %>'>
                                    <i class="bi bi-pencil-square me-2"></i>
                                    Editar
                                </a>
                            </li>
                            <li>
                                <button type="button"
                                    class='dropdown-item <%# Eval("Estado").ToString() == "Abierto" ? "text-danger" : "text-success" %>'
                                    onclick='cambiarEstadoLocal(<%# Eval("Id") %>, "<%# Eval("Estado") %>")'>
                                    <i class='bi <%# Eval("Estado").ToString() == "Abierto" ? "bi-door-closed" : "bi-door-open" %> me-2'></i>
                                    <%# Eval("Estado").ToString() == "Abierto" ? "Cerrar local" : "Abrir local" %>
                                </button>
                            </li>
                        </ul>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
        </columns>
    </asp:GridView>

    <script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
function cambiarEstadoLocal(idLocal, estadoActual) {
    let abriendo = estadoActual === "Cerrado";
    let accion = abriendo ? "abrir" : "cerrar";
    Swal.fire({
        title: '¿Deseas ' + accion + ' este local?',
        text: abriendo
            ? 'El local aparecerá como disponible.'
            : 'El local aparecerá como cerrado.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location =
                'Locales.aspx?estado=' + idLocal +
                '&valor=' + (abriendo ? 'Abierto' : 'Cerrado');
        }
    });
}

$(document).ready(function () {
    $('#gvLocales tr:first').wrap('<thead></thead>');
    if ($.fn.dataTable.isDataTable('#gvLocales')) {
        $('#gvLocales').DataTable().destroy();
    }
    $('#gvLocales').DataTable({
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
