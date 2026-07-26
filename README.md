# NET-2026---com-3EK01
# Propuesta TP DSW

## Grupo
### Integrantes
* -	50715 - Felicevich, Mirko
  -	42062 - Coria, Lautaro


### Repositorios
* [frontend app]()
* [backend app]()


## Tema
### Descripción
El proyecto consiste en desarrollar un sistema de gestión de stock para una tienda gastronómica con 3 sucursales, disponible en dos interfaces: una aplicación de escritorio para la operación administrativa en cada local, y una aplicación web para consulta y carga desde cualquier dispositivo.

El acceso requiere autenticación, y las funcionalidades disponibles dependen del tipo de usuario. Los administradores registran productos e ingredientes, definen recetas, cargan ingresos de mercadería, ventas y elaboraciones internas, y administran usuarios y sucursales. Los empleados tienen acceso restringido: consultan el stock disponible actualizado y registran ventas y elaboraciones internas, sin poder modificar catálogos ni precios.

El sistema centraliza la información de las tres sucursales y ofrece reportes de gestión —entre ellos ventas por período y productos/ingredientes bajo stock mínimo, con visualización gráfica— que le permiten al dueño tener visibilidad global del negocio desde cualquier lugar.

### Modelo
[![Modelo-Dominio-Tp-DSW.png](https://i.postimg.cc/4325L0jw/DSW1-drawio-(1).png)](https://postimg.cc/rdWxzfVt)



## Alcance Funcional

### Alcance Mínimo

Regularidad:
|Req|Detalle|
|:-|:-|
|CRUD simple|1. CRUD Producto<br>2. CRUD Ingrediente<br>3. CRUD Sucursal<br>4. CRUD Usuario|
|CRUD dependiente|1. CRUD Venta/DetalleVenta *(Maestro/Detalle)* {depende de} Usuario/Sucursal/Producto<br>2. CRUD IngresoProducto {depende de} Producto/Sucursal|
|Listado<br>+<br>detalle|1. Lista de Productos filtrada por nombre, categoría y bajo stock, muestra stock y precio actuales<br>2. Historial de ingresos de stock por producto|
|CUU/Epic|1. Login/logout con autorización por tipo de usuario (administrador/empleado)<br>2. Registrar una venta (descuenta stock y genera el detalle de la venta)<br>3. Ingresar mercadería al stock (crea o agrega producto)|

Adicionales para Aprobación:
|Req|Detalle|
|:-|:-|
|CRUD|1. CRUD Receta/DetalleReceta *(Maestro/Detalle)*<br>2. CRUD Elaboracion/DetalleElaboracion *(Maestro/Detalle)*<br>3. CRUD IngresoIngrediente<br>4. CRUD PrecioProducto<br>5. CRUD PrecioIngrediente|
|Reportes|1. Ventas por período y sucursal, con gráfico<br>2. Productos e ingredientes bajo stock mínimo|
|CUU/Epic|1. Registrar elaboración interna a partir de una receta (registra la elaboración y su detalle de ingredientes, descuenta ingredientes y suma producto terminado)|

### Alcance Adicional Voluntario

*Nota*: El Alcance Adicional Voluntario es opcional, pero ayuda a que la funcionalidad del sistema esté completa y será considerado en la nota en función de su complejidad y esfuerzo.

|Req|Detalle|
|:-|:-|
|Listados|1. Reporte de precios de compra de ingredientes y de productos<br>2. Lista de elaboraciones filtrada por fecha y sucursal|
|CUU/Epic|1. Registrar y eliminar usuarios con asignación de rol y sucursal<br>2. Notificación de stock bajo<br>3. Comparativa de costo de receta vs. precio de venta del producto|
