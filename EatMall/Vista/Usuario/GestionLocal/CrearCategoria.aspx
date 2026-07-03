<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master"
    AutoEventWireup="true"
    CodeBehind="CrearCategoria.aspx.cs"
    Inherits="EatMall.Vista.Usuario.GestionLocal.CrearCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
            display: flex;
            align-items: center;
            gap: 10px;
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

        .form-select {
            border: 1px solid #d1d5db;
            border-radius: 10px;
            padding: 12px 14px;
            min-height: 48px;
            width: 100%;
            transition: all .2s ease;
        }

            .form-select:focus {
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
            margin: 28px 0 16px;
        }

        .divider {
            border: none;
            border-top: 1px solid #e5e7eb;
            margin: 32px 0;
        }

        .btn-guardar {
            background: var(--color-rol);
            color: white;
            border: none;
            border-radius: 10px;
            padding: 12px 26px;
            font-weight: 600;
            transition: all .2s ease;
            cursor: pointer;
        }

            .btn-guardar:hover {
                opacity: .92;
                transform: translateY(-1px);
                color: white;
            }

        .btn-cancelar {
            border: 1px solid #d1d5db;
            border-radius: 10px;
            padding: 12px 26px;
            background: white;
            color: #374151;
            text-decoration: none;
            transition: all .2s ease;
            display: inline-block;
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

        .preview-imagen {
            width: 80px;
            height: 80px;
            object-fit: cover;
            border-radius: 10px;
            border: 1px solid #e5e7eb;
            display: none;
            margin-top: 10px;
        }
    </style>

    <h4 class="page-titulo" id="lblTituloPagina" runat="server">
        <span class="material-symbols-outlined">category</span>
        Crear Categoría
    </h4>

    <div class="card-form">

        <p class="seccion-titulo" style="margin-top: 0;">
            Información general
        </p>

        <div class="row g-3">
            <div class="col-md-6">
                <label class="form-label">Nombre de la categoría</label>
                <asp:TextBox ID="txtNombre"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ej. Comida italiana" />
            </div>
        </div>

        <p class="seccion-titulo">Imagen</p>

        <div class="row g-3">
            <div class="col-md-8">
                <label class="form-label">URL de la imagen</label>
                <asp:TextBox
                    ID="txtImagen"
                    runat="server"
                    CssClass="form-control"
                    placeholder="https://..."
                    onchange="previewImagen(this.value)" />
                <img id="imgPreview"
                    class="preview-imagen"
                    src=""
                    alt="Preview" />
            </div>
        </div>

        <p class="seccion-titulo">Estado</p>

        <div class="row g-3">
            <div class="col-md-6">
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Activa" Value="true"></asp:ListItem>
                    <asp:ListItem Text="Inactiva" Value="false"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <hr class="divider" />

        <div class="acciones-formulario">
            <asp:Button
                ID="btnGuardar"
                runat="server"
                Text="Crear Categoría"
                CssClass="btn-guardar"
                OnClick="btnGuardar_Click" />
            <a href="Categoria.aspx" class="btn-cancelar">Cancelar</a>
        </div>

    </div>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function previewImagen(url) {
            var img = document.getElementById('imgPreview');
            if (url) {
                img.src = url;
                img.style.display = 'block';
            } else {
                img.style.display = 'none';
            }
        }
    </script>

</asp:Content>
