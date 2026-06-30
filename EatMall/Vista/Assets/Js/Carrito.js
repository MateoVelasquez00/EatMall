var CARRITO_KEY = "carrito";


function ObtenerCarrito() {
    var data = localStorage.getItem(CARRITO_KEY);
    return data ? JSON.parse(data) : [];
}


function ActualizarBadge() {
    var carrito = ObtenerCarrito();

    var totalProductos = carrito.reduce(function (suma, item) { return suma + item.Cantidad; }, 0);

    var badge = document.getElementById('ContentPlaceHolder1_lblCantidadCarrito') || document.getElementById('lblCantidadCarrito');
    if (badge) {
        badge.textContent = totalProductos;
    }
}


function RenderizarInterfazCarrito() {
    var contenedor = document.getElementById("contenedorCarrito");
    if (!contenedor) return;

    var carrito = ObtenerCarrito();
    var html = "";
    var subtotal = 0;

    var panelVacio = document.getElementById("pnlVacio") || document.getElementById("panelVacio");
    var lblSubtotal = document.getElementById("lblSubtotalText") || document.getElementById("subtotal");
    var lblTotal = document.getElementById("lblTotalText") || document.getElementById("total");
    var btnConfirmar = document.querySelector('[id$="btnConfirmar"]') || document.querySelector('[id$="BtnConfirmar"]');

    if (carrito.length === 0) {
        contenedor.innerHTML = '';
        if (panelVacio) panelVacio.style.display = 'block';
        if (lblSubtotal) lblSubtotal.textContent = "0.00";
        if (lblTotal) lblTotal.textContent = "0.00";
        if (btnConfirmar) btnConfirmar.style.display = "none";
        return;
    }

    if (panelVacio) panelVacio.style.display = 'none';


    carrito.forEach(function (item, posicion) {

        var itemSubtotal = item.Precio * item.Cantidad;
        subtotal += itemSubtotal;


        html += '<div class="producto-row p-3 mb-3">' +
            '<div class="d-flex justify-content-between align-items-center">' +
            '<div>' +
            '<h6 class="fw-bold mb-1">' + item.Nombre + '</h6>' +
            '<p class="text-muted mb-0 small">$' + item.Precio.toFixed(2) + ' x ' + item.Cantidad + '</p>' +
            '</div>' +
            '<div class="d-flex align-items-center gap-3">' +
            '<span class="fw-bold text-primary">$' + itemSubtotal.toFixed(2) + '</span>' +
            '<button type="button" class="btn btn-outline-danger btn-sm border-0" onclick="EliminarProductoCompletamente(' + posicion + ')">' +
            '<i class="bi bi-trash"></i>' +
            '</button>' +
            '</div>' +
            '</div>' +
            '</div>';
    });

    contenedor.innerHTML = html;

    if (lblSubtotal) lblSubtotal.textContent = subtotal.toFixed(2);
    if (lblTotal) lblTotal.textContent = subtotal.toFixed(2);
    if (btnConfirmar) btnConfirmar.style.display = "block";

    MtPrepararEnvio();
}


function EliminarProductoCompletamente(posicion) {
    var carrito = ObtenerCarrito();
    carrito.splice(posicion, 1);
    localStorage.setItem(CARRITO_KEY, JSON.stringify(carrito));

    ActualizarBadge();
    RenderizarInterfazCarrito();
}

function MtPrepararEnvio() {
    var carrito = localStorage.getItem(CARRITO_KEY) || "[]";

    var hiddenField = document.querySelector('[id$="CarritoData"]') || document.querySelector('[id$="hfCarritoJson"]');
    if (hiddenField) {
        hiddenField.value = carrito;
    }
}


document.addEventListener("DOMContentLoaded", function () {
    ActualizarBadge();
    RenderizarInterfazCarrito();
});