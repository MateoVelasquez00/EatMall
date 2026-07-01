<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="CrearLocales.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionAdminCC.CrearLocales" %>

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

    .preview-imagen {
        width: 100px;
        height: 100px;
        object-fit: cover;
        border-radius: 12px;
        border: 1px solid #e5e7eb;
        display: none;
        margin-top: 10px;
    }
</style>

<h4 class="page-titulo"><i class="bi bi-shop me-2"></i>Crear Local</h4>

<p class="seccion-titulo">Información del local</p>

<div class="card-form">
    <div class="row g-3">
        <div class="col-md-4">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. El Corral" />
        </div>
        <div class="col-md-4">
            <label class="form-label">Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej. 6011000001" />
        </div>
        <div class="col-md-4">
            <label class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="local@ejemplo.com" />
        </div>
        <div class="col-md-12">
            <label class="form-label">Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"
                TextMode="MultiLine" Rows="3" placeholder="Describe el local..." />
        </div>
    </div>

    <p class="seccion-titulo">Ubicación y configuración</p>

    <div class="row g-3">
        <div class="col-md-4">
            <label class="form-label">Plazoleta</label>
            <asp:DropDownList ID="ddlPlazoleta" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-4">
            <label class="form-label">Dueño del local</label>
            <asp:DropDownList ID="ddlDueño" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-4">
            <label class="form-label">Número de local</label>
            <asp:TextBox ID="txtNumeroLocal" runat="server" CssClass="form-control" placeholder="Ej. 101" />
        </div>
    </div>
    
    <p class="seccion-titulo">Imagen</p>

    <div class="row g-3">
        <div class="col-md-6">
            <label class="form-label">URL de imagen</label>
            <asp:TextBox ID="txtImagen" runat="server" CssClass="form-control"
                placeholder="https://..." onkeyup="previewImagen(this.value)" />
            <img id="imgPreview" class="preview-imagen" src="" alt="Preview" />
        </div>
        <div class="col-md-3">
            <label class="form-label">Estado inicial</label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                <asp:ListItem Text="Abierto" Value="Abierto" />
                <asp:ListItem Text="Cerrado" Value="Cerrado" />
            </asp:DropDownList>
        </div>
    </div>

    <hr class="divider" />

    <div class="acciones-formulario">
        <asp:Button ID="btnCrear" runat="server"
            Text="Crear Local"
            CssClass="btn-crear"
            OnClick="btnCrear_Click" />

        <asp:HyperLink ID="btnVolver" runat="server"
            Text="Cancelar"
            CssClass="btn-cancelar"
            NavigateUrl="~/Vista/Usuario/GestionAdminCC/Locales.aspx" />
    </div>
</div>

<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script>
function previewImagen(url) {
    const img = document.getElementById('imgPreview');
    if (url.trim() !== '') {
        img.src = url;
        img.style.display = 'block';
    } else {
        img.style.display = 'none';
    }
}
</script>

</asp:Content>