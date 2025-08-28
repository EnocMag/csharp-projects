using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Xml;
using ConectaDbEf;
using Microsoft.EntityFrameworkCore;
using TechStore_db_ef;

Console.WriteLine("*******Bienvenido a la tienda online TechStore*******");

int opcion = 0;
int subOpcion = 0;
int menuPedidoProducto = 0;
var context = new TechStoreDbContext();

// TODO: Implementar la funcionalidad para agregar un nuevo producto
// Asignar el producto a una categoria existente o crear una nueva categoria

do
{
    Console.WriteLine("=================Menú principal==================");
    Console.WriteLine("1. Gestión de Clientes");
    Console.WriteLine("2. Gestión de Productos");
    Console.WriteLine("3. Gestión de Categorías");
    Console.WriteLine("4. Gestión de Pedidos");
    Console.WriteLine("0. Salir");
    Console.WriteLine("=================================================");
    
    opcion = int.Parse(Console.ReadLine());
    if (opcion == 1)
    {
        do
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("1. Buscar cliente por id");
            Console.WriteLine("2. Buscar cliente por nombre");
            Console.WriteLine("3. Agregar un nuevo cliente");
            Console.WriteLine("0. Volver menú principal");
            Console.WriteLine("x. Para salir");
            Console.WriteLine("=================================================");

            string entrada = Console.ReadLine();
            subOpcion = ComprobarEntrada.Comprobar(entrada);

            if (subOpcion == 1)
            {
                Console.WriteLine("Favor de introducir el id del cliente a consultar");
                int clienteId = int.Parse(Console.ReadLine());

                var cte = context.Clientes
                    .Include(c => c.Pedidos) // Incluye los pedidos del cliente si es necesario
                    .FirstOrDefault(x => x.ClienteId == clienteId);
                if (cte != null)
                {
                    Console.WriteLine(cte);
                    if (cte.Pedidos != null && cte.Pedidos.Any())
                    {
                        Console.WriteLine($"El cliente tiene {cte.Pedidos.Count} pedidos asociados.");
                        foreach (var pedido in cte.Pedidos)
                        {
                            Console.WriteLine(pedido);
                        }
                    }
                    else
                    {
                        Console.WriteLine("El cliente no a realizado pedidos aún.");
                    }
                }
                else
                {
                    Console.WriteLine("Cliente no encontrado.");
                }
            }
            else if (subOpcion == 2)
            {
                Console.WriteLine("Favor de introducir el nombre del cliente a consultar");
                string nombre = Console.ReadLine();
                var clientes = context.Clientes
                    .Where(x => x.Nombre.Contains(nombre))
                    .ToList();

                if (clientes.Any())
                {
                    foreach (var cte in clientes)
                    {
                        Console.WriteLine($"Cliente encontrado: {cte.Nombre} {cte.Apellido}, Email: {cte.Email}, Id: {cte.ClienteId}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron clientes con ese nombre.");
                }
            }
            else if (subOpcion == 3)
            {
                Console.WriteLine("Favor de introducir el nombre del cliente");
                var nombre = Console.ReadLine();
                Console.WriteLine("Favor de introducir el apellido del cliente");
                var apellido = Console.ReadLine();
                Console.WriteLine("Favor de introducir el email del cliente");
                var email = Console.ReadLine();
                Console.WriteLine("Favor de introducir el telefono del cliente (opcional)");
                var telefono = Console.ReadLine();
                
                var nuevoCliente = new Cliente
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    Telefono = telefono,
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };
                context.Clientes.Add(nuevoCliente);
                context.SaveChanges();
                Console.WriteLine($"Cliente {nuevoCliente.Nombre} {nuevoCliente.Apellido} agregado exitosamente con ID: {nuevoCliente.ClienteId}");
            }
        } while (subOpcion < 0);
    }
    else if (opcion == 2)
    {
        Console.WriteLine("=================================================");
        Console.WriteLine("1. Ver el catalogo de productos");
        Console.WriteLine("2. Buscar producto por categoria");
        Console.WriteLine("3. Agregar un nuevo producto");
        Console.WriteLine("0. Volver menú principal");
        Console.WriteLine("x. Para salir");
        Console.WriteLine("=================================================");

        string entrada = Console.ReadLine();
        subOpcion = ComprobarEntrada.Comprobar(entrada);

        if (subOpcion == 1)
        {
            Console.WriteLine("======================Catalogo de productos======================");
            var productos = context.Productos.ToList();
            if (productos.Any())
            {
                foreach (var producto in productos)
                {
                    Console.WriteLine(producto);
                }
            }
            else
            {
                Console.WriteLine("No hay productos disponibles en el catalogo.");
            }
            int productosCount = context.Productos.Count();
            Console.WriteLine($"Productos totales: {productosCount}");
        }
        else if (subOpcion == 2)
        {
            Console.WriteLine("Favor de elegir la categoria del producto a consultar");

            var categorias = context.Categorias.Where(c => c.CategoriaPadre == null).ToList();
            if (categorias.Any())
            {
                foreach (var categoria in categorias)
                {
                    Console.WriteLine($"Id: {categoria.CategoriaId}: {categoria.Nombre}");
                }
            }
            else
            {
                Console.WriteLine("No hay categorias disponibles.");
            }
            int categoriaId = int.Parse(Console.ReadLine());

            var categoriaSeleccionada = context.Categorias
                .Include(c => c.Subcategorias)
                    .ThenInclude(c => c.Productos)
                .FirstOrDefault(c => c.CategoriaId == categoriaId);

            if (categoriaSeleccionada.Subcategorias != null && categoriaSeleccionada.Subcategorias.Any())
            {
                var productosCategoria = categoriaSeleccionada.Subcategorias.SelectMany(x => x.Productos);
                foreach (var producto in productosCategoria)
                {
                    Console.WriteLine(producto);
                }
            }
            else
            {
                Console.WriteLine("No hay productos disponibles en esta categoria.");
            }
        }
        else if (subOpcion == 3)
        {
            Console.WriteLine("Favor de introducir el nombre del producto");
            var nombre = Console.ReadLine();
            Console.WriteLine("Favor de introducir la descripcion del producto");
            var descripcion = Console.ReadLine();
            Console.WriteLine("Favor de introducir el precio del producto");
            var precio = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Favor de introducir el SKU del producto");
            var sku = Console.ReadLine();
            Console.WriteLine("Favor de introducir el stock disponible del producto");
            var stockDisponible = int.Parse(Console.ReadLine());
            Console.WriteLine("Favor de introducir el stock minimo del producto");
            var stockMinimo = int.Parse(Console.ReadLine());
            Console.WriteLine("1. Agregar a una categoria existente");
            Console.WriteLine("2. Crear una nueva categoria");
            int categoriaOpcion = int.Parse(Console.ReadLine());

            if (categoriaOpcion == 1)
            {
                Console.WriteLine("Favor de elegir el id de la categoria");

                var cats = context.Categorias.Where(c => c.CategoriaPadre == null).ToList();
                if (cats.Any())
                {
                    foreach (var categoria in cats)
                    {
                        Console.WriteLine($"Id: {categoria.CategoriaId}: {categoria.Nombre}");
                    }
                }
                else
                {
                    Console.WriteLine("No hay categorias disponibles.");
                }
                int categoriaId = int.Parse(Console.ReadLine());

                var nuevoProducto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    CategoriaId = categoriaId,
                    Sku = sku,
                    StockDisponible = stockDisponible,
                    StockMinimo = stockMinimo,
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };
                context.Productos.Add(nuevoProducto);
                context.SaveChanges();
                Console.WriteLine($"Producto {nuevoProducto.Nombre} agregado exitosamente con ID: {nuevoProducto.ProductoId}");

            }
            else if (categoriaOpcion == 2)
            {
                Console.WriteLine("Favor de introducir el nombre de la nueva categoria");
                var nombreCategoria = Console.ReadLine();
                Console.WriteLine("Favor de introducir la descripcion de la nueva categoria");
                var descripcionCategoria = Console.ReadLine();
                Console.WriteLine("Favor de introducir el id de la categoria padre (opcional, presione Enter si no hay)");
                var categoriaPadreIdInput = Console.ReadLine();
                int? categoriaPadreId = null;
                if (!string.IsNullOrEmpty(categoriaPadreIdInput) && int.TryParse(categoriaPadreIdInput, out int padreId))
                {
                    categoriaPadreId = padreId;
                }

                //var nuevaCategoria = CrearCategoria.Crear(context);
                // Crear el nuevo producto y asociarlo a la nueva categoria

                var nuevoProducto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    //Categoria = nuevaCategoria, // Asociar la nueva categoria
                    Sku = sku,
                    StockDisponible = stockDisponible,
                    StockMinimo = stockMinimo,
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };
                var nuevaCategoria = new Categoria
                {
                    Nombre = nombreCategoria,
                    Descripcion = descripcionCategoria,
                    CategoriaPadreId = categoriaPadreId,
                    Activa = true,
                    Productos = new List<Producto> { nuevoProducto } // Asociar el producto a la nueva categoria
                };
                context.Categorias.Add(nuevaCategoria);
                //context.Productos.Add(nuevoProducto);
                context.SaveChanges();
            }
        }
    }
    else if (opcion == 3)
    {
        do
        { 
            Console.WriteLine("=================================================");
            Console.WriteLine("1. Ver el catalogo de categorias");
            Console.WriteLine("2. Buscar categoria por nombre");
            Console.WriteLine("3. Agregar una nueva categoria");
            Console.WriteLine("0. Volver menú principal");
            Console.WriteLine("x. Para salir");
            Console.WriteLine("=================================================");

            string entrada = Console.ReadLine();
            subOpcion = ComprobarEntrada.Comprobar(entrada);

            if (subOpcion == 1) // ver el catalogo de categorias
            {
                Console.WriteLine("======================Catalogo de categorias======================");
                var categorias = context.Categorias.ToList();
                if (categorias.Any())
                {
                    foreach (var categoria in categorias)
                    {
                        Console.WriteLine(categoria);
                    }
                }
                else
                {
                    Console.WriteLine("No hay categorias disponibles.");
                }
            }
            else if (subOpcion == 2) // Buscar categoria por nombre
            {
                Console.WriteLine("Favor de introducir el nombre de la categoria a consultar");
                string nombreCategoria = Console.ReadLine();
                var categorias = context.Categorias
                    .Where(x => x.Nombre.Contains(nombreCategoria))
                    .ToList();
                if (categorias.Any())
                {
                    foreach (var categoria in categorias)
                    {
                        Console.WriteLine(categoria);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron categorias con ese nombre.");
                }
            }
            else if (subOpcion == 3) // Agregar una nueva categoria
            {
                Console.WriteLine("Favor de introducir el nombre de la nueva categoria");
                var nombreCategoria = Console.ReadLine();
                // Comprobar si la categoria ya existe
                var categoriasExistente = context.Categorias
                    .Where(x => x.Nombre.Contains(nombreCategoria))
                        .ToList();
                if (categoriasExistente.Any())
                {
                    foreach (var categoria in categoriasExistente)
                    {
                        Console.WriteLine($"La categoria '{nombreCategoria}' ya existe con el ID: {categoria.CategoriaId}");
                        Environment.Exit(0);
                    }
                }

                Console.WriteLine("Favor de introducir la descripcion de la nueva categoria");
                var descripcionCategoria = Console.ReadLine();
                Console.WriteLine("Favor de introducir el id de la categoria padre (opcional, presione Enter si no hay)");
                var categoriaPadreIdInput = Console.ReadLine();
                int? categoriaPadreId = null;
                if (!string.IsNullOrEmpty(categoriaPadreIdInput) && int.TryParse(categoriaPadreIdInput, out int padreId))
                {
                    categoriaPadreId = padreId;
                }
                var nuevaCategoria = new Categoria
                {
                    Nombre = nombreCategoria,
                    Descripcion = descripcionCategoria,
                    CategoriaPadreId = categoriaPadreId,
                    Activa = true
                };
                nuevaCategoria = CrearCategoria.Crear(context);
                context.Categorias.Add(nuevaCategoria);
                context.SaveChanges();
            }
        } while (subOpcion < 0);
    }
    else if (opcion == 4)
    {
        do
        {
            // TODO: Implementar la funcionalidades gestión de pedidos
            Console.WriteLine("=================================================");
            Console.WriteLine("1. Ver el catalogo de pedidos");
            Console.WriteLine("2. Agregar un nuevo pedido");
            Console.WriteLine("3. Obtener los productos de un pedido por id"); // Buscar como hacer, pues el id es compuesto
            Console.WriteLine("4. Obtener los pedidos de un prodructo por id");
            Console.WriteLine("5. Total de ventas de un producto");
            Console.WriteLine("0. Volver menú principal");
            Console.WriteLine("x. Para salir");
            Console.WriteLine("=================================================");

            string entrada = Console.ReadLine();
            subOpcion = ComprobarEntrada.Comprobar(entrada);

            if (subOpcion == 1)
            {
                Console.WriteLine("======================Catalogo de pedidos======================");
                var pedidos = context.Pedidos.ToList();
                if (pedidos.Any())
                {
                    foreach (var pedido in pedidos)
                    {
                        Console.WriteLine(pedido);
                    }
                }
                else
                {
                    Console.WriteLine("No hay pedidos disponibles.");
                }
            }
            else if (subOpcion == 2)
            {
                // agregar un nuevo pedido
                Console.WriteLine("Ingresa tu id de cliente");
                int clienteId = int.Parse(Console.ReadLine());
                var cliente = context.Clientes.Find(clienteId);
                if (cliente == null)
                {
                    Console.WriteLine("Cliente no encontrado. Por favor, verifica el ID.");
                    continue;
                }

                // Llenar la tabla detalles_pedido
                // Mostrar los productos disponibles
                do
                {
                    Console.WriteLine("Productos disponibles:");
                    var productos = context.Productos.ToList();
                    if (productos.Any())
                    {
                        foreach (var producto in productos)
                        {
                            Console.WriteLine(producto);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay productos disponibles.");
                        continue;
                    }
                    Console.WriteLine("Ingresa el id del producto que deseas agregar al pedido");
                    Console.WriteLine("0. Para continuar");
                    int productoId = int.Parse(Console.ReadLine());
                    if (productoId == 0)
                    {
                        break; // Salir del bucle si el usuario ingresa 0
                    }
                    var productoSeleccionado = context.Productos.Find(productoId);
                    if (productoSeleccionado == null)
                    {
                        Console.WriteLine("Producto no encontrado. Por favor, verifica el ID.");
                        continue;
                    }
                    Console.WriteLine("Ingresa la cantidad del producto que deseas agregar al pedido");
                    int cantidad = int.Parse(Console.ReadLine());
                    if (cantidad <= 0 || cantidad > productoSeleccionado.StockDisponible)
                    {
                        Console.WriteLine("Cantidad no válida. Por favor, verifica la cantidad.");
                        continue;
                    }
                    // Crear el detalle del pedido
                    var detallePedido = new DetallesPedido
                    {
                        ProductoId = productoSeleccionado.ProductoId, 
                        Cantidad = cantidad
                    };
                } while (menuPedidoProducto < 0);
                
                Console.WriteLine("Selecciona el estado");
                Console.WriteLine("1. Pendiente");
                Console.WriteLine("2. Enviado");
                Console.WriteLine("3. Entregado");
                Console.WriteLine("4. Cancelado");
                int estadoOpcion = int.Parse(Console.ReadLine());
                string estado = estadoOpcion switch
                {
                    1 => "Pendiente",
                    2 => "Enviado",
                    3 => "Entregado",
                    4 => "Cancelado",
                    _ => "Pendiente"
                };

            }
            else if (subOpcion == 3) //Obtener los productos de un pedido por id
            {
                Console.WriteLine("Favor de introducir el id del pedido a consultar");
                int pedidoId = int.Parse(Console.ReadLine());
                var pedido = context.Pedidos
                    .Include(p => p.Productos)
                    .FirstOrDefault(x => x.PedidoId == pedidoId);

                foreach (var producto in pedido.Productos)
                {
                    Console.WriteLine(producto);
                }
                
            }
            else if (subOpcion == 4) // Obtener los pedidos de un prodructo por id
            {
                Console.WriteLine("Favor de itroducir el id del producto");
                int productoId = int.Parse(Console.ReadLine());
                var producto = context.Productos
                    .Include(p => p.Pedidos)
                    .FirstOrDefault(x => x.ProductoId == productoId);
                foreach (var pedido in producto.Pedidos)
                {
                    Console.WriteLine(pedido);
                }
            }
            else if (subOpcion == 5) // Total de ventas de un producto
            {
                var nVentas = 0;
                Console.WriteLine("Favor de introducir el id del producto");
                int productoId = int.Parse(Console.ReadLine());
                var producto = context.Productos
                    .Include(p => p.Pedidos)
                    .FirstOrDefault(x => x.ProductoId == productoId);

                foreach (var pedido in producto.Pedidos)
                {
                    nVentas++;
                }
                Console.WriteLine($"El producto con Id:{productoId} se a vendido {nVentas} veces");
            }
        } while (subOpcion < 0);
    }

} while (opcion > 0);
    
Console.WriteLine("Gracias por visitar nuestra aplicación. Vuelva pronto!!");