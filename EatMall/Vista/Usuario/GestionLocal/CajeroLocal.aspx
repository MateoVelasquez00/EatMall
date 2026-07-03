<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="CajeroLocal.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionLocal.CajeroLocal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">

    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />

    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
        }

        /* CONTENEDOR GENERAL */
        #gvPedidos_wrapper {
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
        #gvPedidos {
            border: none !important;
            width: 100% !important;
        }

            #gvPedidos thead th {
                background: var(--color-rol) !important;
                color: white !important;
                border: none !important;
                font-size: .85rem;
                font-weight: 600;
                letter-spacing: .5px;
                padding: 14px !important;
            }

            #gvPedidos tbody td {
                padding: 14px !important;
                vertical-align: middle;
            }

            #gvPedidos tbody tr {
                transition: all .15s ease;
            }

                #gvPedidos tbody tr:hover {
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

        /* CABECERA */
        .cabecera-Cajeros {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 24px;
        }


        .badge-estado {
            padding: 6px 12px;
            border-radius: 999px;
            font-size: .75rem;
            font-weight: 600;
        }

        .badge-pendiente {
            background: #fef3c7;
            color: #92400e;
        }

        .badge-preparando {
            background: #dbeafe;
            color: #1d4ed8;
        }

        .badge-entregado {
            background: #dcfce7;
            color: #166534;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="cabecera-Cajeros">
        <h4 class="page-titulo mb-0">
            <span class="material-symbols-outlined">receipt_long</span>
            Gestión de Cajeros
        </h4>

    </div>

    <asp:GridView
        ID="gvCajeros"
        runat="server"
        ClientIDMode="Static"
        AllowPaging="false"
        AllowSorting="false"
        DataKeyNames="Id"
        CssClass="table table-bordered table-striped"
        AutoGenerateColumns="false">

        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id Cajero" />
            <asp:BoundField DataField="Gmail" HeaderText="Email Cajero" />
            <asp:BoundField DataField="NombreLocal" HeaderText="Nombre Local" />
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <span class='badge-estado
            <%# Eval("Estado").ToString() == "Activo" ? "badge-Activo" :
                Eval("Estado").ToString() == "Inactivo" ? "badge-Inactivo" :
                "badge-entregado" %>'>
                        <%# Eval("Estado") %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        $(document).ready(function () {

            $('#gvCajeros tr:first').wrap('<thead></thead>');

            if ($.fn.dataTable.isDataTable('#gvCajeros')) {
                $('#gvCajeros').DataTable().destroy();
            }

            $('#gvCajeros').DataTable({
                language: {
                    search: "",
                    searchPlaceholder: "Buscar Cajero...",
                    lengthMenu: "Mostrar _MENU_ registros",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ cajeros",
                    infoEmpty: "Mostrando 0 a 0 de 0 Cajeros",
                    infoFiltered: "(filtrado de _MAX_ cajeros)",
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