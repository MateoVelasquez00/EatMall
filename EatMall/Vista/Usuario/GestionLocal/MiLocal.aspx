<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="MiLocal.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionLocal.MiLocal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <style>
        .page-titulo {
            color: var(--color-rol);
            font-weight: 700;
            margin-bottom: 24px;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .card-info {
            background: white;
            border: 1px solid #bccac0;
            border-radius: 16px;
            padding: 32px;
            box-shadow: 0 2px 12px rgba(0,0,0,.04);
        }

        .local-imagen {
            width: 120px;
            height: 120px;
            object-fit: cover;
            border-radius: 12px;
            border: 1px solid #e5e7eb;
        }

        .local-nombre {
            font-size: 1.4rem;
            font-weight: 700;
            color: #0b1c30;
        }

        .local-email {
            font-size: 0.875rem;
            color: #3d4a42;
        }

        .seccion-titulo {
            font-size: .78rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            color: var(--color-rol);
            margin: 28px 0 12px;
        }

        .info-item {
            display: flex;
            flex-direction: column;
            gap: 4px;
            margin-bottom: 16px;
        }

        .info-label {
            font-size: .78rem;
            font-weight: 700;
            color: #6b7280;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .info-valor {
            font-size: .95rem;
            color: #0b1c30;
            font-weight: 500;
        }

        .badge-estado {
            display: inline-block;
            padding: 6px 14px;
            border-radius: 999px;
            font-size: .78rem;
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

        .divider {
            border: none;
            border-top: 1px solid #e5e7eb;
            margin: 24px 0;
        }

        .btn-editar {
            background: var(--color-rol);
            color: white;
            border: none;
            border-radius: 10px;
            padding: 11px 26px;
            font-weight: 600;
            text-decoration: none;
            display: inline-flex;
            align-items: center;
            gap: 8px;
            transition: all .2s ease;
        }

            .btn-editar:hover {
                opacity: .9;
                color: white;
                transform: translateY(-1px);
            }
    </style>

    <h4 class="page-titulo">
        <span class="material-symbols-outlined">storefront</span>
        Mi Local
    </h4>

    <div class="card-info">

        <div class="d-flex align-items-center gap-4 mb-2">
            <asp:Image ID="imgLocal" runat="server" CssClass="local-imagen"
                AlternateText="Imagen del local" />
            <div>
                <div class="local-nombre">
                    <asp:Label ID="lblNombre" runat="server" />
                </div>
                <div class="local-email">
                    <asp:Label ID="lblEmail" runat="server" />
                </div>
                <div class="mt-2">
                    <asp:Label ID="lblEstado" runat="server" />
                </div>
            </div>
        </div>

        <hr class="divider" />

        <p class="seccion-titulo">Información general</p>

        <div class="row">
            <div class="col-md-4">
                <div class="info-item">
                    <span class="info-label">Teléfono</span>
                    <span class="info-valor">
                        <asp:Label ID="lblTelefono" runat="server" />
                    </span>
                </div>
            </div>
            <div class="col-md-4">
                <div class="info-item">
                    <span class="info-label">Número de local</span>
                    <span class="info-valor">
                        <asp:Label ID="lblNumeroLocal" runat="server" />
                    </span>
                </div>
            </div>
            <div class="col-md-4">
                <div class="info-item">
                    <span class="info-label">Calificación</span>
                    <span class="info-valor">⭐
                        <asp:Label ID="lblCalificacion" runat="server" />
                    </span>
                </div>
            </div>
        </div>

        <div class="info-item">
            <span class="info-label">Descripción</span>
            <span class="info-valor">
                <asp:Label ID="lblDescripcion" runat="server" />
            </span>
        </div>

        <p class="seccion-titulo">Horario</p>

        <div class="info-valor">
            <asp:Label ID="lblHorario" runat="server" />
        </div>

        <hr class="divider" />

        <a href="EditarLocal.aspx" class="btn-editar">
            <span class="material-symbols-outlined" style="font-size: 18px;">edit</span>
            Editar información
        </a>

    </div>

</asp:Content>
