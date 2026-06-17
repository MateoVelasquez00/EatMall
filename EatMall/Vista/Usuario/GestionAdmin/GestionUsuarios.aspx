<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs"
    Inherits="EatMall.Vista.Usuario.GestionAdmin.GestionUsuarios"
    MasterPageFile="~/Vista/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />

    <style>
        #gvUsuarios th {
            background-color: #FFA94D !important;
            color: white !important;
        }

        .panel-roles {
            border: 1px solid #ddd;
            padding: 10px;
            border-radius: 8px;
            margin-top: 6px;
            background: white;
            box-shadow: 0 2px 6px rgba(0,0,0,0.1);
        }

        .btn-rol-toggle {
            white-space: nowrap;
        }
    </style>

    <h4 class="mb-3">Usuarios</h4>

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
            <asp:BoundField DataField="Nombre"    HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido"  HeaderText="Apellido" />
            <asp:BoundField DataField="Documento" HeaderText="Documento" />
            <asp:BoundField DataField="Email"     HeaderText="Email" />

            <asp:TemplateField HeaderText="Rol">
                <ItemTemplate>
                    <button type="button" class="btn btn-sm btn-outline-secondary btn-rol-toggle"
                        onclick="toggleRoles(this)">
                        <%# Eval("Rol.Nombre") %> ▾
                    </button>

                    <div class="panel-roles" style="display:none;">
                        <asp:CheckBoxList ID="chkRoles" runat="server" />

                        <div class="mt-2 d-flex gap-2">
                            <asp:Button ID="btnGuardarRol" runat="server"
                                Text="Guardar"
                                CssClass="btn btn-sm btn-success"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnGuardarRol_Click" />

                            <button type="button" class="btn btn-sm btn-secondary"
                                onclick="cancelarRoles(this)">Cancelar</button>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>

    <script>
        function toggleRoles(btn) {
            // El panel está dos elementos abajo del botón dentro de la misma celda
            var panel = $(btn).closest('td').find('.panel-roles');
            panel.toggle();
        }

        function cancelarRoles(btn) {
            $(btn).closest('.panel-roles').hide();
        }

        $(document).ready(function () {
            $('#gvUsuarios tr:first').wrap('<thead></thead>');

            if ($.fn.dataTable.isDataTable('#gvUsuarios')) {
                $('#gvUsuarios').DataTable().destroy();
            }

            $('#gvUsuarios').DataTable({
                language: {
                    search: "Buscar:",
                    lengthMenu: "Mostrar _MENU_ registros",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ usuarios",
                    paginate: {
                        first: "Primero",
                        last: "Último",
                        next: "Siguiente",
                        previous: "Anterior"
                    },
                    zeroRecords: "No se encontraron resultados"
                }
            });
        });
    </script>

</asp:Content>