// ==================== FUNCIONES DE BÚSQUEDA Y FILTRADO ====================

function buscarEnTabla(inputId, tablaId) {
    const input = document.getElementById(inputId);
    const filter = input.value.toUpperCase();
    const tabla = document.getElementById(tablaId);
    const tr = tabla.getElementsByTagName('tr');

    for (let i = 0; i < tr.length; i++) {
        let mostrar = false;
        const td = tr[i].getElementsByTagName('td');

        for (let j = 0; j < td.length; j++) {
            if (td[j]) {
                const txtValue = td[j].textContent || td[j].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    mostrar = true;
                    break;
                }
            }
        }

        if (i > 0) { // Saltar el header
            tr[i].style.display = mostrar ? '' : 'none';
        }
    }
}

function limpiarFiltros() {
    document.getElementById('searchInput').value = '';
    document.getElementById('filtroEstado').value = '';
    document.getElementById('filtroFecha').value = '';
    buscarEnTabla('searchInput', 'tablaCotizaciones');
}

function filtrarPorEstado(tablaId) {
    const filtroEstado = document.getElementById('filtroEstado').value;
    const tabla = document.getElementById(tablaId);
    const tr = tabla.getElementsByTagName('tr');

    for (let i = 1; i < tr.length; i++) { // Saltar header
        if (filtroEstado === '') {
            tr[i].style.display = '';
        } else {
            const estadoCell = tr[i].getElementsByClassName('badge')[0];
            if (estadoCell) {
                const estado = estadoCell.textContent.trim();
                tr[i].style.display = estado === filtroEstado ? '' : 'none';
            }
        }
    }
}

function filtrarPorFecha(tablaId, columnaFecha) {
    const filtroFecha = document.getElementById('filtroFecha').value;
    if (!filtroFecha) return;

    const tabla = document.getElementById(tablaId);
    const tr = tabla.getElementsByTagName('tr');

    for (let i = 1; i < tr.length; i++) {
        const fechaCell = tr[i].getElementsByTagName('td')[columnaFecha];
        if (fechaCell) {
            const fechaTexto = fechaCell.textContent.trim();
            const fechaComparar = convertirFecha(fechaTexto);
            tr[i].style.display = fechaComparar === filtroFecha ? '' : 'none';
        }
    }
}

function convertirFecha(fechaTexto) {
    // Convertir de dd/MM/yyyy a yyyy-MM-dd
    const partes = fechaTexto.split('/');
    if (partes.length === 3) {
        return `${partes[2]}-${partes[1]}-${partes[0]}`;
    }
    return fechaTexto;
}

// ==================== FUNCIONES DE PAGINACIÓN ====================

let paginaActual = 1;
const registrosPorPagina = 10;

function cambiarPagina(direccion) {
    if (direccion === 'prev' && paginaActual > 1) {
        paginaActual--;
    } else if (direccion === 'next') {
        paginaActual++;
    }

    actualizarPaginacion();
}

function irAPagina(numeroPagina) {
    paginaActual = numeroPagina;
    actualizarPaginacion();
}

function actualizarPaginacion() {
    // Actualizar el indicador de página
    const pageInfo = document.querySelector('.page-info');
    if (pageInfo) {
        pageInfo.textContent = `Página ${paginaActual}`;
    }

    // Actualizar botones activos
    const pageBtns = document.querySelectorAll('.page-btn');
    pageBtns.forEach((btn, index) => {
        btn.classList.toggle('active', (index + 1) === paginaActual);
    });
}

// ==================== FUNCIONES DE CRUD ====================

function toggleEstado(id, activo) {
    if (confirm('¿Está seguro de cambiar el estado de este registro?')) {
        fetch(`/Ventas/ToggleEstado/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    location.reload();
                } else {
                    alert('Error al cambiar el estado');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

function verDetalle(id) {
    // Redirigir a la vista de detalle
    window.location.href = `/Ventas/Detalle/${id}`;
}

function eliminar(id) {
    if (confirm('¿Está seguro de eliminar este registro? Esta acción no se puede deshacer.')) {
        fetch(`/Ventas/Eliminar/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => {
                if (response.ok) {
                    alert('Registro eliminado exitosamente');
                    location.reload();
                } else {
                    alert('Error al eliminar el registro');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

// ==================== FUNCIONES ESPECÍFICAS DE COTIZACIONES ====================

function buscarCotizacion() {
    buscarEnTabla('searchInput', 'tablaCotizaciones');
}

function convertirCotizacionAPedido(idCotizacion) {
    if (confirm('¿Desea convertir esta cotización en un pedido?')) {
        fetch(`/Cotizacion/ConvertirAPedido/${idCotizacion}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Cotización convertida a pedido exitosamente');
                    location.reload();
                } else {
                    alert('Error al convertir la cotización');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

// ==================== FUNCIONES ESPECÍFICAS DE VENTAS ====================

function buscarVenta() {
    buscarEnTabla('searchInput', 'tablaVentas');
}

function imprimirFactura(idVenta) {
    window.open(`/Venta/ImprimirFactura/${idVenta}`, '_blank');
}

// ==================== FUNCIONES ESPECÍFICAS DE PEDIDOS ====================

function buscarPedido() {
    buscarEnTabla('searchInput', 'tablaPedidos');
}

function cambiarEstadoPedido(idPedido, nuevoEstado) {
    if (confirm(`¿Desea cambiar el estado del pedido a "${nuevoEstado}"?`)) {
        fetch(`/Pedido/CambiarEstado`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                id: idPedido,
                estado: nuevoEstado
            })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Estado actualizado exitosamente');
                    location.reload();
                } else {
                    alert('Error al actualizar el estado');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

// ==================== FUNCIONES ESPECÍFICAS DE DEVOLUCIONES ====================

function aprobarDevolucion(idDevolucion) {
    if (confirm('¿Está seguro de aprobar esta devolución?')) {
        fetch(`/Devolucion/AprobarDevolucion/${idDevolucion}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Devolución aprobada exitosamente');
                    location.reload();
                } else {
                    alert('Error al aprobar la devolución');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

function rechazarDevolucion(idDevolucion) {
    const motivo = prompt('Ingrese el motivo del rechazo:');
    if (motivo) {
        fetch(`/Devolucion/RechazarDevolucion`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                id: idDevolucion,
                motivo: motivo
            })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Devolución rechazada');
                    location.reload();
                } else {
                    alert('Error al rechazar la devolución');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

// ==================== FUNCIONES ESPECÍFICAS DE CARRITO ====================

function agregarProducto() {
    // Aquí deberías abrir un modal o formulario para seleccionar el producto
    const modal = document.getElementById('modalAgregarProducto');
    if (modal) {
        modal.style.display = 'block';
    }
}

function actualizarCantidad(idCarrito, nuevaCantidad) {
    if (nuevaCantidad < 1) {
        alert('La cantidad debe ser al menos 1');
        return;
    }

    fetch(`/Carrito/ActualizarCantidad`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            id: idCarrito,
            cantidad: parseInt(nuevaCantidad)
        })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert('Error al actualizar la cantidad');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Error al procesar la solicitud');
        });
}

function eliminarItem(idCarrito) {
    if (confirm('¿Está seguro de eliminar este producto del carrito?')) {
        fetch(`/Carrito/EliminarItem/${idCarrito}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    location.reload();
                } else {
                    alert('Error al eliminar el producto');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

function finalizarCompra() {
    if (confirm('¿Desea finalizar la compra?')) {
        fetch(`/Carrito/FinalizarCompra`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert(`Compra finalizada exitosamente. N° Venta: ${data.idVenta}`);
                    window.location.href = `/Venta/Detalle/${data.idVenta}`;
                } else {
                    alert(data.message || 'Error al finalizar la compra');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al procesar la solicitud');
            });
    }
}

// ==================== FUNCIONES DE VALIDACIÓN ====================

function validarFormulario(formularioId) {
    const form = document.getElementById(formularioId);
    if (!form) return false;

    const inputs = form.querySelectorAll('input[required], select[required], textarea[required]');
    let valido = true;

    inputs.forEach(input => {
        if (!input.value.trim()) {
            input.classList.add('error');
            valido = false;
        } else {
            input.classList.remove('error');
        }
    });

    if (!valido) {
        alert('Por favor complete todos los campos obligatorios');
    }

    return valido;
}

function validarEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}

function validarTelefono(telefono) {
    const regex = /^\d{10,15}$/;
    return regex.test(telefono.replace(/\s/g, ''));
}

function validarCedula(cedula) {
    const regex = /^\d{6,12}$/;
    return regex.test(cedula.replace(/\s/g, ''));
}

// ==================== FUNCIONES DE FORMATO ====================

function formatearMoneda(valor) {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP'
    }).format(valor);
}

function formatearFecha(fecha) {
    const date = new Date(fecha);
    const dia = String(date.getDate()).padStart(2, '0');
    const mes = String(date.getMonth() + 1).padStart(2, '0');
    const anio = date.getFullYear();
    return `${dia}/${mes}/${anio}`;
}

// ==================== INICIALIZACIÓN ====================

document.addEventListener('DOMContentLoaded', function () {
    // Agregar listeners a los filtros
    const filtroEstado = document.getElementById('filtroEstado');
    if (filtroEstado) {
        filtroEstado.addEventListener('change', function () {
            filtrarPorEstado(document.querySelector('tbody').id);
        });
    }

    const filtroFecha = document.getElementById('filtroFecha');
    if (filtroFecha) {
        filtroFecha.addEventListener('change', function () {
            filtrarPorFecha(document.querySelector('tbody').id, 2);
        });
    }

    // Agregar listener al campo de búsqueda
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('keyup', function () {
            buscarEnTabla('searchInput', document.querySelector('tbody').id);
        });
    }
});