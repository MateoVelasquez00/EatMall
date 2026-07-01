<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="MiCentroComercial.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionAdminCC.MiCentroComercial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
        }

        .card-perfil {
            background: white;
            border: 1px solid #bccac0;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 2px 12px rgba(0,0,0,.04);
        }

        .banner-cc {
            width: 100%;
            height: 280px;
            object-fit: cover;
        }

        .banner-placeholder {
            width: 100%;
            height: 280px;
            background: var(--color-rol-light);
            display: flex;
            align-items: center;
            justify-content: center;
            color: var(--color-rol);
            font-size: 4rem;
        }

        .btn-editar {
            background: white;
            border: 1px solid var(--color-rol);
            color: var(--color-rol);
            border-radius: 10px;
            padding: 10px 18px;
            font-weight: 600;
            cursor: pointer;
            transition: .2s;
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }

            .btn-editar:hover {
                background: var(--color-rol-light);
            }

        .btn-cancelar-editar {
            background: white;
            border: 1px solid #d1d5db;
            color: #374151;
            border-radius: 10px;
            padding: 10px 18px;
            font-weight: 600;
            cursor: pointer;
            transition: .2s;
        }

            .btn-cancelar-editar:hover {
                border-color: var(--color-rol);
                color: var(--color-rol);
            }

        .descripcion-texto {
            color: #374151;
            font-size: 1.0rem;
            line-height: 1;
        }

        .info-cc {
            padding: 32px;
        }

        .nombre-cc {
            font-size: 1.8rem;
            font-weight: 700;
            color: #0b1c30;
            margin-bottom: 4px;
        }

        .direccion-cc {
            color: #6b7280;
            font-size: .95rem;
            margin-bottom: 24px;
        }

        .badge-estado-cc {
            padding: 6px 16px;
            border-radius: 999px;
            font-size: .8rem;
            font-weight: 600;
            display: inline-block;
            margin-bottom: 24px;
        }

        .badge-activo {
            background: #dcfce7;
            color: #166534;
        }

        .badge-inactivo {
            background: #fee2e2;
            color: #991b1b;
        }

        .seccion-titulo {
            font-size: .78rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            color: var(--color-rol);
            margin: 24px 0 12px;
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

            .form-control[readonly] {
                background: #f9fafb;
                color: #6b7280;
                cursor: not-allowed;
            }

        .btn-guardar {
            background: var(--color-rol);
            color: white;
            border: none;
            border-radius: 10px;
            padding: 12px 26px;
            font-weight: 600;
            transition: all .2s ease;
        }

            .btn-guardar:hover {
                opacity: .92;
                transform: translateY(-1px);
            }

        .divider {
            border: none;
            border-top: 1px solid #e5e7eb;
            margin: 24px 0;
        }

        .preview-imagen {
            width: 100%;
            height: 160px;
            object-fit: cover;
            border-radius: 12px;
            border: 1px solid #e5e7eb;
            margin-top: 10px;
        }

        #panelVista .divider {
            margin: 20px 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <h4 class="page-titulo">
        <i class="bi bi-building me-2"></i>
        Mi Centro Comercial
    </h4>

    <div class="card-perfil">

        <asp:Image ID="imgBanner" runat="server" CssClass="banner-cc" AlternateText="Imagen CC" />

        <div class="info-cc">

            <div class="d-flex justify-content-between align-items-start">
                <div>
                    <div class="nombre-cc">
                        <asp:Label ID="lblNombre" runat="server" />
                    </div>
                    <div class="direccion-cc">
                        <i class="bi bi-geo-alt me-1"></i>
                        <asp:Label ID="lblDireccion" runat="server" />
                    </div>
                    <asp:Label ID="lblEstadoBadge" runat="server" />
                </div>

                <%-- BOTÓN EDITAR --%>
                <button type="button" class="btn-editar" onclick="toggleEditar(true)">
                    <i class="bi bi-pencil-square me-1"></i>Editar
                </button>
            </div>

            <%-- DESCRIPCIÓN SOLO LECTURA --%>
            <div id="panelVista">
                <hr class="divider" />
                <p class="seccion-titulo">Descripción</p>
                <asp:Label ID="lblDescripcion" runat="server" CssClass="descripcion-texto" />
            </div>

            <%-- PANEL EDITAR (oculto por defecto) --%>
            <div id="panelEditar" style="display: none;">
                <hr class="divider" />
                <p class="seccion-titulo">Editar información</p>

                <div class="row g-3">
                    <div class="col-md-12">
                        <label class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine" Rows="4"
                            placeholder="Describe tu centro comercial..." />
                    </div>
                    <div class="col-md-8">
                        <label class="form-label">URL de imagen</label>
                        <asp:TextBox ID="txtImagen" runat="server"
                            CssClass="form-control"
                            placeholder="https://..."
                            onkeyup="previewImagen(this.value)" />
                        <img id="imgPreview" src="" alt="Preview"
                            class="preview-imagen" style="display: none;" />
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Activo" Value="True" />
                            <asp:ListItem Text="Inactivo" Value="False" />
                        </asp:DropDownList>
                    </div>
                </div>

                <hr class="divider" />

                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server"
                        Text="Guardar cambios"
                        CssClass="btn-guardar"
                        OnClick="btnGuardar_Click" />

                    <button type="button" class="btn-cancelar-editar"
                        onclick="toggleEditar(false)">
                        Cancelar
                    </button>
                </div>
            </div>

        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
function toggleEditar(mostrar) {
    document.getElementById('panelEditar').style.display = mostrar ? 'block' : 'none';
    document.getElementById('panelVista').style.display = mostrar ? 'none' : 'block';
}

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
