<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="ProductosLocal.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionLocal.ProductosLocal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />

    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
        }

        /* CONTENEDOR GENERAL */
        #gvProductos_wrapper {
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
        #gvProductos {
            width: 100% !important;
            border: none !important;
        }

            #gvProductos thead th {
                background: var(--color-rol) !important;
                color: white !important;
                border: none !important;
                font-size: .85rem;
                font-weight: 600;
                letter-spacing: .5px;
                padding: 14px !important;
            }

            #gvProductos tbody td {
                padding: 14px !important;
                vertical-align: middle;
            }

            #gvProductos tbody tr {
                transition: all .15s ease;
            }

                #gvProductos tbody tr:hover {
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
            margin-top: 15px;
        }

        /* PAGINACIÓN */
        .dataTables_wrapper .dataTables_paginate {
            margin-top: 15px;
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

        /* BOTÓN NUEVO PRODUCTO */
        .btn-nuevo-Producto {
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

            .btn-nuevo-Producto:hover {
                transform: translateY(-2px);
                color: white !important;
            }

        /* CABECERA */
        .cabecera-Producto {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 24px;
        }

        /* BOTÓN ACCIONES */
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

        /* MENÚ */
        .dropdown-menu {
            border: none;
            border-radius: 12px;
            box-shadow: 0 8px 24px rgba(0,0,0,.12);
        }

        .dropdown-item {
            font-size: .9rem;
            padding: 10px 14px;
        }

        /* IMAGEN DEL PRODUCTO */
        .img-producto {
            width: 70px;
            height: 70px;
            object-fit: cover;
            border-radius: 10px;
            border: 1px solid #e5e7eb;
            display: block;
            margin: 0 auto;
        }

        /* BADGES */
        .badge-estado {
            display: inline-block;
            padding: 6px 12px;
            border-radius: 999px;
            font-size: .75rem;
            font-weight: 600;
            transition: .2s;
        }

        .badge-activo {
            background: #dcfce7;
            color: #166534;
            box-shadow: 0 0 8px rgba(34, 197, 94, .45);
        }

        .badge-inactivo {
            background: #fee2e2;
            color: #991b1b;
            box-shadow: 0 0 8px rgba(239, 68, 68, .45);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="cabecera-Producto">
        <h4 class="page-titulo mb-0">
            <span class="material-symbols-outlined">receipt_long</span>
            Gestión de Productos
        </h4>

        <a href="CrearProducto.aspx" class="btn-nuevo-Producto">
            <i class="bi bi-plus-lg"></i>
            Nuevo Producto
        </a>
    </div>

    <asp:GridView
        ID="gvProductos"
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
                        class="img-producto"
                        onerror="this.src='https://via.placeholder.com/80x60?text=CC'" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" />
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
                                    href='CrearProducto.aspx?id=<%# Eval("Id") %>'>
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
        function cambiarEstado(idProducto, estadoActual) {

            let accion = estadoActual ? "desactivar" : "activar";

            Swal.fire({
                title: '¿Deseas ' + accion + ' este producto?',
                text: estadoActual
                    ? 'El producto no podrá ser visitado.'
                    : 'El producto podrá ser visitado nuevamente.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí',
                cancelButtonText: 'Cancelar'
            }).then((result) => {

                if (result.isConfirmed) {

                    window.location =
                        'ProductosLocal.aspx?estado=' +
                        idProducto +
                        '&valor=' +
                        (!estadoActual);
                }
            });
        }

        $(document).ready(function () {

            $('#gvProductos tr:first').wrap('<thead></thead>');

            if ($.fn.dataTable.isDataTable('#gvProductos')) {
                $('#gvProductos').DataTable().destroy();
            }

            $('#gvProductos').DataTable({
                language: {
                    search: "",
                    searchPlaceholder: "Buscar producto...",
                    lengthMenu: "Mostrar _MENU_ registros",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ productos",
                    infoEmpty: "Mostrando 0 a 0 de 0 productos",
                    infoFiltered: "(filtrado de _MAX_ productos en total)",
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
