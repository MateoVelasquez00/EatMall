<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearCentroComercial.aspx.cs"
    Inherits="EatMall.Vista.Usuario.GestionAdmin.CrearCentroComercial"
    MasterPageFile="~/Vista/Admin.Master" %>

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

        textarea.form-control {
            min-height: 100px;
            resize: vertical;
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
        <span class="material-symbols-outlined">store</span>
        Crear Centro Comercial
    </h4>

    <div class="card-form">

        <p class="seccion-titulo" style="margin-top: 0;">Información general</p>


        <div class="row g-3">
            <div class="col-md-6">
                <label class="form-label">Ciudad</label>
                <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                    placeholder="Ej. Centro Comercial El Tesoro" />
            </div>
            <div class="col-md-6">
                <label class="form-label">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"
                    placeholder="Ej. Calle 123 # 45-67" />
            </div>
            <div class="col-12">
                <label class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"
                    TextMode="MultiLine" placeholder="Descripción del centro comercial..." />
            </div>
        </div>

        <p class="seccion-titulo">Ubicación</p>

        <div class="row g-3">
            <div class="col-md-12">
                <label class="form-label">URL de Ubicación (Google Maps)</label>
                <asp:TextBox ID="txtUbicacionUrl" runat="server" CssClass="form-control"
                    placeholder="https://maps.google.com/..." />
            </div>
            <div class="col-md-6">
                <label class="form-label">Latitud</label>
                <asp:TextBox ID="txtLatitud" runat="server" CssClass="form-control"
                    placeholder="Ej. 6.2442" />
            </div>
            <div class="col-md-6">
                <label class="form-label">Longitud</label>
                <asp:TextBox ID="txtLongitud" runat="server" CssClass="form-control"
                    placeholder="Ej. -75.5812" />
            </div>
        </div>

        <p class="seccion-titulo">Imagen</p>

        <div class="row g-3">
            <div class="col-md-8">
                <label class="form-label">URL de la imagen</label>
                <asp:TextBox ID="txtImagen" runat="server" CssClass="form-control"
                    placeholder="https://..." onchange="previewImagen(this.value)" />
                <img id="imgPreview" class="preview-imagen" src="" alt="Preview" />
            </div>
        </div>

        <hr class="divider" />

        <div class="acciones-formulario">
            <asp:Button ID="btnGuardar" runat="server"
                Text="Crear Centro Comercial"
                CssClass="btn-guardar"
                OnClick="btnGuardar_Click" />

            <a href="ListarCentroComercial.aspx" class="btn-cancelar">Cancelar</a>
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
