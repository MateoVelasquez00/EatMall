var CARRITO_KEY = "carrito";

function ObtenerCarrito() {
    var data = localStorage.getItem(CARRITO_KEY);
    return data ? JSON.parse(data) : [];
}

function GuardarCarrito(carrito) {
    localStorage.setItem(CARRITO_KEY, JSON.stringify(carrito));
}

function AgregarProducto(id, nombre, precio, cantidad, idLocal) {
    var carrito = ObtenerCarrito();
    var item = carrito.find(function (c) { return c.Id === id; });
    if (item) {
        item.Cantidad += cantidad;
    } else {
        carrito.push({ Id: id, Nombre: nombre, Precio: precio, Cantidad: cantidad, IdLocal: idLocal, Subtotal: precio * cantidad });
    }
    carrito.forEach(function (c) { c.Subtotal = c.Precio * c.Cantidad; });
    GuardarCarrito(carrito);
    ActualizarBadge();
}

function EliminarProducto(id) {
    GuardarCarrito(ObtenerCarrito().filter(function (c) { return c.Id !== id; }));
    location.reload();
    ActualizarBadge();
}

function VaciarCarrito() {
    localStorage.removeItem(CARRITO_KEY);
}
function ActualizarBadge() {
    var total = ObtenerCarrito().reduce(function (s, c) { return s + c.Cantidad; }, 0);
    var badge = document.getElementById('ContentPlaceHolder1_lblCantidadCarrito');
    if (badge) badge.textContent = total;
}