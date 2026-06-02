<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MetodosPago.aspx.cs"
    Inherits="EatMall.Vista.Pago.MetodosPago" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Método de Pago - EatMall</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <script src="https://js.stripe.com/v3/"></script>
    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', sans-serif;
        }

        .page-header {
            background: linear-gradient(135deg, #ff6b35, #f7931e);
            color: white;
            padding: 20px 0;
            margin-bottom: 30px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.15);
        }

        .metodo-card {
            border: 2px solid #e0e0e0;
            border-radius: 12px;
            padding: 16px;
            cursor: pointer;
            transition: all 0.3s ease;
            background: white;
            margin-bottom: 15px;
        }

            .metodo-card:hover {
                border-color: #ff6b35;
                transform: translateY(-2px);
                box-shadow: 0 4px 15px rgba(255,107,53,0.2);
            }

            .metodo-card.selected {
                border-color: #ff6b35;
                background-color: #fff5f0;
                box-shadow: 0 4px 15px rgba(255,107,53,0.3);
            }

            .metodo-card img {
                width: 50px;
                height: 50px;
                object-fit: contain;
            }

            .metodo-card .check-icon {
                color: #ff6b35;
                display: none;
            }

            .metodo-card.selected .check-icon {
                display: inline-block;
            }

        .form-section {
            background: white;
            border-radius: 12px;
            padding: 25px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            margin-bottom: 20px;
        }

        #stripe-card-element {
            border: 1px solid #ced4da;
            border-radius: 8px;
            padding: 12px;
            background: white;
        }

            #stripe-card-element.StripeElement--focus {
                border-color: #ff6b35;
                box-shadow: 0 0 0 0.2rem rgba(255,107,53,0.25);
            }

            #stripe-card-element.StripeElement--invalid {
                border-color: #dc3545;
            }

        #card-errors {
            color: #dc3545;
            font-size: 0.875rem;
            margin-top: 8px;
        }

        .btn-continuar {
            background: linear-gradient(135deg, #ff6b35, #f7931e);
            border: none;
            color: white;
            padding: 14px 40px;
            font-size: 1.1rem;
            border-radius: 50px;
            width: 100%;
            font-weight: 600;
            transition: all 0.3s;
        }

            .btn-continuar:hover {
                opacity: 0.9;
                transform: translateY(-1px);
                box-shadow: 0 5px 20px rgba(255,107,53,0.4);
                color: white;
            }

            .btn-continuar:disabled {
                opacity: 0.6;
                cursor: not-allowed;
            }

        .panel-datos {
            display: none;
            animation: fadeIn 0.3s ease;
        }

            .panel-datos.visible {
                display: block;
            }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(10px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .resumen-box {
            background: white;
            border-radius: 12px;
            padding: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            position: sticky;
            top: 20px;
        }

            .resumen-box .total {
                font-size: 1.4rem;
                font-weight: 700;
                color: #ff6b35;
            }

        .spinner-border-sm {
            width: 1rem;
            height: 1rem;
        }

        #stripe-card-number, #stripe-card-expiry, #stripe-card-cvc {
            border: 1px solid #ced4da;
            border-radius: 8px;
            padding: 12px;
            background: white;
        }

            #stripe-card-number.StripeElement--focus,
            #stripe-card-expiry.StripeElement--focus,
            #stripe-card-cvc.StripeElement--focus {
                border-color: #ff6b35;
                box-shadow: 0 0 0 0.2rem rgba(255,107,53,0.25);
            }

            #stripe-card-number.StripeElement--invalid,
            #stripe-card-expiry.StripeElement--invalid,
            #stripe-card-cvc.StripeElement--invalid {
                border-color: #dc3545;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Header -->
        <div class="page-header">
            <div class="container">
                <div class="d-flex align-items-center">

                    <div>
                        <h4 class="mb-0 fw-bold"><i class="fas fa-credit-card me-2"></i>Método de Pago</h4>
                        <small class="opacity-75">Selecciona cómo quieres pagar</small>
                    </div>
                </div>
            </div>
        </div>

        <div class="container pb-5">
            <div class="row">

                <!-- Columna principal -->
                <div class="col-lg-8">

                    <!-- Métodos de pago -->
                    <div class="form-section">
                        <h5 class="fw-bold mb-4"><i class="fas fa-wallet me-2 text-warning"></i>Selecciona tu método de pago</h5>

                        <asp:Repeater ID="rptMetodos" runat="server" OnItemDataBound="rptMetodos_ItemDataBound">
                            <ItemTemplate>
                                <div class="metodo-card" onclick="seleccionarMetodo(this, '<%# Eval("Id") %>', '<%# Eval("NombreMetodo") %>')">
                                    <div class="d-flex align-items-center justify-content-between">
                                        <div class="d-flex align-items-center gap-3">
                                            <img src='<%# Eval("Imagen") %>' alt='<%# Eval("NombreMetodo") %>' />
                                            <div>
                                                <strong class="d-block"><%# Eval("NombreMetodo") %></strong>
                                                <small class="text-muted">Pago seguro y encriptado</small>
                                            </div>
                                        </div>
                                        <i class="fas fa-check-circle fa-lg check-icon"></i>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblSinMetodos" runat="server" CssClass="text-muted"
                            Visible="false" Text="No hay métodos de pago disponibles." />
                    </div>

                    <!-- Panel: Tarjeta (Stripe) -->
                    <div id="panelTarjeta" class="form-section panel-datos">
                        <h5 class="fw-bold mb-4"><i class="fas fa-credit-card me-2 text-primary"></i>Datos de la tarjeta</h5>
                        <div class="row g-3">
                            <div class="col-12">
                                <label class="form-label fw-semibold">Nombre en la tarjeta</label>
                                <asp:TextBox ID="txtNombreTarjeta" runat="server" CssClass="form-control"
                                    placeholder="Como aparece en la tarjeta" />
                            </div>
                            <div class="col-12">
                                <label class="form-label fw-semibold">Número de tarjeta</label>
                                <div id="stripe-card-number"></div>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label fw-semibold">Fecha de vencimiento</label>
                                <div id="stripe-card-expiry"></div>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label fw-semibold">CVC</label>
                                <div id="stripe-card-cvc"></div>
                            </div>
                            <div id="card-errors" role="alert" class="col-12"></div>
                            <div class="col-12">
                                <div class="d-flex align-items-center gap-2 text-muted small">
                                    <i class="fas fa-lock text-success"></i>
                                    <span>Tus datos están protegidos con encriptación SSL de 256 bits</span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Panel: PSE / Transferencia -->
                    <div id="panelTransferencia" class="form-section panel-datos">
                        <h5 class="fw-bold mb-4"><i class="fas fa-university me-2 text-info"></i>Pago por PSE / Transferencia</h5>
                        <div class="alert alert-info">
                            <i class="fas fa-info-circle me-2"></i>
                            Serás redirigido al portal de tu banco para completar el pago de forma segura.
                        </div>
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label class="form-label fw-semibold">Banco</label>
                                <select class="form-select" id="selBanco">
                                    <option value="">-- Selecciona tu banco --</option>
                                    <option>Bancolombia</option>
                                    <option>Davivienda</option>
                                    <option>Banco de Bogotá</option>
                                    <option>BBVA</option>
                                    <option>Banco Popular</option>
                                    <option>Colpatria</option>
                                    <option>Otro</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label fw-semibold">Tipo de persona</label>
                                <select class="form-select" id="selTipoPersona">
                                    <option value="N">Natural</option>
                                    <option value="J">Jurídica</option>
                                </select>
                            </div>
                            <div class="col-12">
                                <label class="form-label fw-semibold">Número de documento</label>
                                <asp:TextBox ID="txtDocumentoPSE" runat="server" CssClass="form-control"
                                    placeholder="Cédula o NIT" />
                            </div>
                        </div>
                    </div>

                    <!-- Panel: Efectivo -->
                    <div id="panelEfectivo" class="form-section panel-datos">
                        <h5 class="fw-bold mb-4"><i class="fas fa-money-bill-wave me-2 text-success"></i>Pago en efectivo</h5>
                        <div class="alert alert-success">
                            <i class="fas fa-check-circle me-2"></i>
                            Pagarás al repartidor cuando llegue tu pedido. Ten el monto exacto si es posible.
                        </div>
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label class="form-label fw-semibold">¿Con cuánto pagas?</label>
                                <div class="input-group">
                                    <span class="input-group-text">$</span>
                                    <asp:TextBox ID="txtMontoPaga" runat="server" CssClass="form-control"
                                        TextMode="Number" placeholder="0.00" />
                                </div>
                                <small class="text-muted">Para calcular tu cambio</small>
                            </div>
                        </div>
                    </div>

                    <!-- Panel: Billetera digital (Nequi / Daviplata) -->
                    <div id="panelBilletera" class="form-section panel-datos">
                        <h5 class="fw-bold mb-4"><i class="fas fa-mobile-alt me-2 text-success"></i>Billetera digital</h5>
                        <div class="alert alert-success">
                            <i class="fas fa-info-circle me-2"></i>
                            Ingresa tu número de celular registrado en la billetera digital.
                        </div>
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label class="form-label fw-semibold">Número de celular</label>
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fas fa-phone"></i></span>
                                    <asp:TextBox ID="txtCelularBilletera" runat="server" CssClass="form-control"
                                        placeholder="300 000 0000" MaxLength="10" />
                                </div>
                                <small class="text-muted">Debe estar registrado en Nequi o Daviplata</small>
                            </div>
                            <div class="col-12">
                                <div class="alert alert-warning py-2 mb-0">
                                    <i class="fas fa-bell me-2"></i>
                                    <small>Recibirás una notificación en tu app para aprobar el pago.</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Campos ocultos -->
                    <asp:HiddenField ID="hfMetodoId" runat="server" />
                    <asp:HiddenField ID="hfMetodoNombre" runat="server" />
                    <asp:HiddenField ID="hfStripeToken" runat="server" />
                    <asp:HiddenField ID="hfIdLocal" runat="server" />

                    <!-- Botón oculto PostBack -->
                    <asp:Button ID="btnServerContinuar" runat="server"
                        OnClick="btnServerContinuar_Click"
                        Style="display: none;" Text="x" />

                    <!-- Botón visible -->
                    <button type="button" id="btnContinuar" class="btn btn-continuar mt-2"
                        onclick="procesarPago()" disabled="disabled">
                        <i class="fas fa-lock me-2"></i>Continuar con el pago
                    </button>
                    <a href="/Vista/Pedido/Carritos.aspx"
                        class="btn btn-outline-secondary mt-2 w-100 d-block text-center"
                        style="padding: 14px 40px; font-size: 1.1rem; border-radius: 50px; font-weight: 600;">
                        <i class="fas fa-times me-2"></i>Cancelar
                    </a>
                    <p class="text-center text-muted small mt-2">
                        <i class="fas fa-shield-alt me-1"></i>Pago 100% seguro
                    </p>
                </div>

                <!-- Resumen -->
                <div class="col-lg-4 mt-4 mt-lg-0">
                    <div class="resumen-box">
                        <h5 class="fw-bold mb-3"><i class="fas fa-receipt me-2 text-warning"></i>Resumen del pedido</h5>
                        <hr />
                        <asp:Label ID="lblCodigoPedido" runat="server" CssClass="text-muted small d-block mb-2" />
                        <asp:Repeater ID="rptResumen" runat="server">
                            <ItemTemplate>
                                <div class="d-flex justify-content-between mb-2">
                                    <span><%# Eval("Nombre") %> x<%# Eval("Cantidad") %></span>
                                    <span class="fw-semibold">$<%# Eval("Subtotal", "{0:F2}") %></span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <hr />
                        <div class="d-flex justify-content-between mb-1">
                            <span class="text-muted">Subtotal</span>
                            <span>
                                <asp:Label ID="lblSubtotal" runat="server" /></span>
                        </div>
                        <div class="d-flex justify-content-between mb-1">
                            <span class="text-muted">Envío</span>
                            <span class="text-success">
                                <asp:Label ID="lblEnvio" runat="server" /></span>
                        </div>
                        <div class="d-flex justify-content-between mb-1">
                            <span class="text-muted">Hora de entrega</span>
                            <span>
                                <asp:Label ID="lblHoraEntrega" runat="server" CssClass="fw-semibold" /></span>
                        </div>
                        <hr />
                        <div class="d-flex justify-content-between">
                            <span class="fw-bold fs-5">Total</span>
                            <span class="total">
                                <asp:Label ID="lblTotal" runat="server" /></span>
                        </div>


                        <div class="mt-4 p-3 bg-light rounded">
                            <small class="text-muted d-block">
                                <i class="fas fa-map-marker-alt me-1 text-danger"></i>
                                <strong>Dirección de entrega:</strong>
                            </small>
                            <small>
                                <asp:Label ID="lblDireccion" runat="server" CssClass="text-dark" /></small>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        var stripe = Stripe('pk_test_51TPoEtG8h92leoazk0frrtCMrvWH5VL1AaQzrVpK6yrsg3KCEq47Ll3Jc8dHkcAkuCWJxjuDgBpdxmOmHvYre84O00PSdsK06A'); 
        var elements = stripe.elements();


        var cardNumber = elements.create('cardNumber', {
            style: { base: { fontSize: '16px', color: '#495057', '::placeholder': { color: '#adb5bd' } }, invalid: { color: '#dc3545' } }
        });
        var cardExpiry = elements.create('cardExpiry', {
            style: { base: { fontSize: '16px', color: '#495057', '::placeholder': { color: '#adb5bd' } }, invalid: { color: '#dc3545' } }
        });
        var cardCvc = elements.create('cardCvc', {
            style: { base: { fontSize: '16px', color: '#495057', '::placeholder': { color: '#adb5bd' } }, invalid: { color: '#dc3545' } }
        });

        var stripeMonted = false;
        var metodoSeleccionado = null;

        [cardNumber, cardExpiry, cardCvc].forEach(function (el) {
            el.on('change', function (e) {
                document.getElementById('card-errors').textContent = e.error ? e.error.message : '';
            });
        });

        function seleccionarMetodo(card, id, nombre) {
            document.querySelectorAll('.metodo-card').forEach(c => c.classList.remove('selected'));
            card.classList.add('selected');
            metodoSeleccionado = { id: id, nombre: nombre.toLowerCase() };

            document.getElementById('<%= hfMetodoId.ClientID %>').value = id;
            document.getElementById('<%= hfMetodoNombre.ClientID %>').value = nombre;

            document.querySelectorAll('.panel-datos').forEach(p => p.classList.remove('visible'));

            var n = nombre.toLowerCase();
            if (n.includes('tarjeta') || n.includes('credito') || n.includes('debito') ||
                n.includes('cr\u00e9dito') || n.includes('d\u00e9bito')) {

                document.getElementById('panelTarjeta').classList.add('visible');

                if (!stripeMonted) {
                    cardNumber.mount('#stripe-card-number');
                    cardExpiry.mount('#stripe-card-expiry');
                    cardCvc.mount('#stripe-card-cvc');
                    stripeMonted = true;
                }

            } else if (n.includes('pse') || n.includes('transferencia')) {
                document.getElementById('panelTransferencia').classList.add('visible');
            } else if (n.includes('efectivo') || n.includes('contra entrega')) {
                document.getElementById('panelEfectivo').classList.add('visible');
            } else if (n.includes('nequi') || n.includes('daviplata')) {
                document.getElementById('panelBilletera').classList.add('visible');
            }

            document.getElementById('btnContinuar').disabled = false;
        }

        function procesarPago() {
            if (!metodoSeleccionado) {
                alert('Por favor selecciona un método de pago.');
                return;
            }

            var btn = document.getElementById('btnContinuar');
            btn.disabled = true;
            btn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Procesando...';

            var n = metodoSeleccionado.nombre;

            if (n.includes('tarjeta') || n.includes('credito') || n.includes('debito') ||
                n.includes('cr\u00e9dito') || n.includes('d\u00e9bito')) {

                var nombreTarjeta = document.getElementById('<%= txtNombreTarjeta.ClientID %>').value;
                if (!nombreTarjeta.trim()) {
                    alert('Por favor ingresa el nombre en la tarjeta.');
                    resetBtn(); return;
                }

                stripe.createToken(cardNumber, { name: nombreTarjeta })
                    .then(function (result) {
                        if (result.error) {
                            document.getElementById('card-errors').textContent = result.error.message;
                            resetBtn();
                        } else {
                            document.getElementById('<%= hfStripeToken.ClientID %>').value = result.token.id;
                            document.getElementById('<%= btnServerContinuar.ClientID %>').click();
                        }
                    })
                    .catch(function (err) {
                        console.error('Stripe error:', err);
                        alert('Error al procesar la tarjeta. Intenta de nuevo.');
                        resetBtn();
                    });

            } else {
                document.getElementById('<%= btnServerContinuar.ClientID %>').click();
            }
        }

        function resetBtn() {
            var btn = document.getElementById('btnContinuar');
            btn.disabled = false;
            btn.innerHTML = '<i class="fas fa-lock me-2"></i>Continuar con el pago';
        }
    </script>
</body>
</html>
